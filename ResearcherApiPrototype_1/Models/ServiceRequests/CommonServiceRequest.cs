using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class CommonServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsIncident { get; set; }
        public string Type { get; set; } // Общая, Аварийная, Плановая
        public string Status { get; set; } //Новая, В Работе, Завершена, Отклонена
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ClosedAt { get; set; }
        public string ClosingDiscription { get; set; }
        public int CreatorId { get; set; }
        public int ImplementerId { get; set; }
        public int HardwareId { get; set; }

    }
}
