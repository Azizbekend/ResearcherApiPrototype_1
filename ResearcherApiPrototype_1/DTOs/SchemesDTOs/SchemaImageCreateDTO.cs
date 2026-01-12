using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs.SchemesDTOs
{
    public class SchemaImageCreateDTO
    {
        public string Top { get; set; }
        public string Left { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public int HardwareSchemaId { get; set; }
        public int FileId { get; set; }
        public int HardwareId { get; set; }
        public int RedFileId { get; set; }
        public int GreenFileId { get; set; }
    }
}
