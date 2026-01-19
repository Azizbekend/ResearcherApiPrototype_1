using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class UserObjectCompanyLink
    {
        [Key]
        public int Id { get; set; }
        public int ObjectCompanyLinkId { get; set; }
        public int UserId { get; set; }
        public bool IsNodeInfosEnabled { get; set; }
        public bool IsCommandsEnabled { get; set; }
        public bool Is3DEnabled { get; set; }
        public bool CanCreateCommonServiceRequests { get; set; }
        public bool CanCreateIncidentServiseRequests { get; set; }
    }
}
