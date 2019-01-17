using PropertyChanged;

namespace ee.iLawyer.Ops.Contact.DTO
{
    //[AddINotifyPropertyChangedInterface]
    public class CategoryValue
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Value { get; set; }
    }
}
