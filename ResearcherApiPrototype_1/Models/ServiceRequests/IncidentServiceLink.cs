using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class IncidentServiceLink
    {
        [Key]
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int ServiceRequestId { get; set; }
    }
}
