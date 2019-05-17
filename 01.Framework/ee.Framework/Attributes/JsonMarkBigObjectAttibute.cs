using System;

namespace ee.Framework.Attributes
{
    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class JsonMarkBigObjectAttibute : Attribute
    {
        public int MaxLength { get; set; } = -1;
        public object Marker { get; set; }


        public object GetMarker(object obj)
        {
            if (obj.ToString().Length > MaxLength)
            {
                return Marker;
            }
            else
            {
                return obj;
            }
        }

    }
}
