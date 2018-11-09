namespace ApprovalVisualiser.Models
{
    public class PropertyDefinition
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public bool IsEntity { get; set; }
        public bool IsCollection { get; set; }
    }

}




