namespace ApprovalVisualiser.Models
{
    public class AvailableProperty
    {
        public bool UseForApproval { get; set; }
        public string PropertyName { get; set; }
        public string PopertyType { get; set; }
        //public Type TP { get; set; }
        public string Description { get; set; }
        public string FieldType { get; set; }
        public string FieldName { get; set; }
        public string ReferenceData { get; set; }
        public string ReferenceDataDisplayType { get; set; }


    }

    public enum ReferenceDataDescriptionType
    {
        Code,
        Description,
        Both
    }

    public enum FontType
    {
        Normal, 
        Red,
        Bold,
    }


}