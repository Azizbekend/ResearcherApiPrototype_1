using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.Models
{
    public class NodeIndicates
    {
        public NodeIndicates(string indicates, int nodeInfoId)
        {
            Indicates = indicates;
            NodeInfoId = nodeInfoId;
        }

        public NodeIndicates(int nodeInfoId)
        {
            NodeInfoId = nodeInfoId;
        }


        [Key]
        public int Id { get; set; }
        public string Indicates { get; set; } //double
        [ForeignKey("NodeInfo")]
        public int NodeInfoId { get; set; }
        public NodeInfo NodeInfo { get; set; } = null!;
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
