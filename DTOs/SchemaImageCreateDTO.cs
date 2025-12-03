using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs
{
    public class SchemaImageCreateDTO
    {
        public string Top { get; set; }
        public string Left { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public int HardwareSchemaId { get; set; }
    }
}
