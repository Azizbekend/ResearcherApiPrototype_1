using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.Models
{
    public class NodeIndicates
    {
        [Key]
        public int Id { get; set; }
        public string Indicates { get; set; } //double
        public string PlcNodeId { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
