using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.DTOs.BaseCreateDTOs
{
    public class MaitenanceHistoryGetManyDTO
    {
        public string Title { get; set; }
        public List<MaintenanceHistory> RecordsList { get; set; } = new List<MaintenanceHistory>();
    }
}
