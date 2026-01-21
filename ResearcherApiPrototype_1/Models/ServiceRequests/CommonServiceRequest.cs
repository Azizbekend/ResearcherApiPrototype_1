using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class CommonServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // Общая, Аварийная, Плановая
        public string Status { get; set; } //Новая, В Работе, Завершена, Отклонена
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime ClosedAt { get; set; }
        public string CancelDiscription { get; set; } = "None";
        public int CreatorId { get; set; }
        public int CreatorsCompanyId { get; set; }
        public int ImplementerId { get; set; }
        public int ImplementersCompaneId { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }

    }
}
