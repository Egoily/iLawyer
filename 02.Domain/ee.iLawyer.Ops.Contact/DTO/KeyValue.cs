using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{
    public class KeyValue
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }
        public KeyValue()
        {
            Id = 0;
            Key = 0;
            Value = null;
        }
        public KeyValue(int key, string value, int id = 0)
        {
            Id = id;
            Key = key;
            Value = value;
        }
    }
}
