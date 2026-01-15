namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class SupplyServiceLink
    {
        public int Id { get; set; }
        public int SupplyRequestId { get; set; }
        public int ServiceRequestId { get; set; }
    }
}
