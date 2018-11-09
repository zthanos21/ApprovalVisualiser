namespace ApprovalVisualiser.Models
{
    public class AvailableProperty
    {
        public bool UseForBRMS { get; set; }
        public bool UseForSummary { get; set; }
        public bool UseForDetails { get; set; }
        public string PropertyName { get; set; }
        public string PopertyType { get; set; }
        //public Type TP { get; set; }
        public string Description { get; set; }
        public FieldType FieldType { get; set; }
        public string FieldName { get; set; }
        public string ReferenceData { get; set; }
        public string ReferenceDataDisplayType { get; set; }


    }

    public enum FieldType
    {
        String,
        Date,
        ReferenceData,
        Number,
        Amount,
        Custom
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