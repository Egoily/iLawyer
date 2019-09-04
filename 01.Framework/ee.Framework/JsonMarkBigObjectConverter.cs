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
            if (value == null)
            {
                writer.WriteNull();
                return;
            }


            var type = value.GetType();
            if (type.IsAnsiClass)
            {
                if (value is Array)
                {
                    SerializeMultidimensionalArray(writer, serializer, (Array)value, 0, ArrayEmpty<int>());
                }
                else if (value is ArrayList)
                {
                    SerializeMultidimensionalArray(writer, serializer, ((ArrayList)value).ToArray(), 0, ArrayEmpty<int>());
                }
                else if (value is IList)
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
                    writer.WriteStartObject();

                    foreach (PropertyInfo pi in value.GetType().GetProperties())
                    {
                        var subValue = pi.GetValue(value);
                        if (subValue == null)
                        {
                            writer.WritePropertyName(pi.Name);
                            writer.WriteNull();
                        }
                        else
                        {
                            var attr = pi.GetCustomAttributes(typeof(JsonMarkBigObjectAttibute), true).FirstOrDefault();
                            if (attr != null)
                            {
                                writer.WritePropertyName(pi.Name);
                                var jsonMarkBigObjectAttibute = attr as JsonMarkBigObjectAttibute;
                                var marker = jsonMarkBigObjectAttibute.GetMarker(subValue);
                                writer.WriteValue(marker.ToString());

                            }
                            else
                            {
                                if (subValue is IList || subValue is Array || subValue is ArrayList || IsCustomType(pi.PropertyType))
                                {
                                    writer.WritePropertyName(pi.Name);
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
            else
            {
                writer.WriteValue(value);
            }
        }


        private static bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }
        public static T[] ArrayEmpty<T>()
        {
            T[] array = Enumerable.Empty<T>() as T[];
            return array ?? new T[0];

        }

        private void SerializeMultidimensionalArray(JsonWriter writer, JsonSerializer serializer, Array values, int initialDepth, int[] indices)
        {
            int dimension = indices.Length;
            int[] newIndices = new int[dimension + 1];
            for (int i = 0; i < dimension; i++)
            {
                newIndices[i] = indices[i];
            }

            writer.WriteStartArray();

            for (int i = values.GetLowerBound(dimension); i <= values.GetUpperBound(dimension); i++)
            {
                newIndices[dimension] = i;
                bool isTopLevel = (newIndices.Length == values.Rank);

                if (isTopLevel)
                {
                    object value = values.GetValue(newIndices);

                    try
                    {
                        WriteJson(writer, value, serializer);
                    }
                    catch (Exception ex)
                    {
                        throw;

                    }
                }
                else
                {
                    SerializeMultidimensionalArray(writer, serializer, values, initialDepth + 1, newIndices);
                }
            }

            writer.WriteEndArray();
        }


























    }
}
