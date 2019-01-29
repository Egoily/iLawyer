namespace ee.iLawyer.Ops.Contact.DTO
{
    //[AddINotifyPropertyChangedInterface]
    public class CategoryValue
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Value { get; set; }

        public CategoryValue()
        {

        }
        public CategoryValue(int categoryId, string categoryName, string value, int id=0)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Value = value;
            Id = id;
        }
    }
}
