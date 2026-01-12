using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearcherApiPrototype_1.Models
{
    public class HardwareCharacteristic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set;} = string.Empty;
        [ForeignKey("HardwareInfo")]
        public int HardwareId { get; set; }
        [JsonIgnore]
        public HardwareInfo Hardware { get; set; } = null! ;
    }
}
