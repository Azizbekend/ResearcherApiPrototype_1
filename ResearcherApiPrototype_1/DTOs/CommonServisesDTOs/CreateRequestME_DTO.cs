namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs
{
    public class CreateRequestME_DTO
    {
        public string Title { get; set; }
        public string Type { get; set; } // Общая, Аварийная, Плановая
        public int CreatorId { get; set; }
        public int ImplementerId { get; set; }
        public int HardwareId { get; set; }
        public int ObjectId { get; set; }
    }
}
