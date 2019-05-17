using ee.Framework.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace ee.Framework
{
    public class JsonMarkBigObjectConverter : JsonConverter
    {

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return existingValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            var type = value.GetType();
            if (value is IList)
            {

                writer.WriteStartArray();
                var selfList = (IList)value;
                for (var pos = 0; pos < selfList.Count; pos++)
                {
                    WriteJson(writer, selfList[pos], serializer);
                }
                writer.WriteEndArray();

            }
            else
            {
                if (writer.WriteState == WriteState.Property || writer.WriteState == WriteState.Object)
                {
                    writer.WritePropertyName(type.Name);
                }

                writer.WriteStartObject();

                foreach (PropertyInfo pi in value.GetType().GetProperties())
                {
                    var subValue = pi.GetValue(value);

                    if (subValue is IList || subValue is Array || subValue is ArrayList || subValue == null)
                    {
                        writer.WritePropertyName(pi.Name);
                        if (subValue != null)
                        {
                            WriteJson(writer, subValue, serializer);
                        }
                        else
                        {
                            writer.WriteNull();
                        }
                    }
                    else
                    {
                        var attr = pi.GetCustomAttributes(typeof(JsonMarkBigObjectAttibute), true).FirstOrDefault();
                        if (attr != null)
                        {
                            var jsonMarkBigObjectAttibute = attr as JsonMarkBigObjectAttibute;
                            var marker = jsonMarkBigObjectAttibute.GetMarker(subValue);
                            writer.WritePropertyName(pi.Name);
                            writer.WriteValue(marker.ToString());
                        }
                        else
                        {
                            if (subValue is IList || subValue is Array || subValue is ArrayList || IsCustomType(pi.PropertyType))
                            {
                                WriteJson(writer, subValue, serializer);
                            }
                            else
                            {
                                writer.WritePropertyName(pi.Name);
                                writer.WriteValue(subValue);
                            }
                        }
                    }
                }
                writer.WriteEndObject();
            }
        }


        private static bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }
























    }
}
