using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ee.Framework
{
    public static class Extensions
    {
        public static void AddSorted<T>(this IList<T> list, T item, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            int i = 0;
            while (i < list.Count && comparer.Compare(list[i], item) < 0)
            {
                i++;
            }

            list.Insert(i, item);
        }

        public static object Assign(this object source, object target)
        {
            if (source.GetType() != target.GetType())
            {
                throw new Exception("Should be same type");
            }
            Type targetType = target.GetType();
            //值类型  
            if (targetType.IsValueType == true)
            {
                source = target;
            }
            //引用类型   
            else
            {
                System.Reflection.MemberInfo[] memberCollection = targetType.GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(target);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(source, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(source,  fieldValue);
                        }

                    }
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo pi = (System.Reflection.PropertyInfo)member;
                        MethodInfo info = pi.GetSetMethod(false);
                        if (info != null)
                        {
                            object propertyValue = pi.GetValue(target, null);

                            if (pi.PropertyType.IsGenericType || pi.PropertyType.IsArray)
                            {
                                pi.SetValue(source, propertyValue == null ? null : Clone(propertyValue), null);
                            }
                            else
                            {
                                if (propertyValue is ICloneable)
                                {
                                    pi.SetValue(source, propertyValue == null ? null : (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                    pi.SetValue(source, propertyValue == null ? null : propertyValue, null);

                                }
                            }
                        }

                    }
                }
            }
            return source;
        }
        public static T Clone<T>(T obj)
        {
            using (var objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, obj);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }
}
