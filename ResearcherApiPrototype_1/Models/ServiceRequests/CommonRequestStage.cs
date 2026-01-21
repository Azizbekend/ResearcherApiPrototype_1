using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class CommonRequestStage
    {
        [Key]
        public int Id { get; set; }
   
        public string Discription { get; set; }
        public string CurrentStatus { get; set; } = "New";
        public string StageType { get; set; } // обычный, поставка, подрядчик
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime ClosedAt { get; set; }
        public string CancelDiscription { get; set; } = "None";
        public int ServiceId { get; set; }
        public int CreatorId { get; set; }
        public int CreatorsCompanyId { get; set; }
        public int ImplementerId { get; set; }
        public int ImplementersCompanyId { get; set; }

    }
}
