using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class SchemeCard
    {
        [Key]
        public int Id { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public int NodeInfoId { get; set; }
        public int SchemeId { get; set; }
    }
}
