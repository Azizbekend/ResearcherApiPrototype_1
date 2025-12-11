using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.DTOs
{
    public class CharCreateDTO
    {
        public int HardwareId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
