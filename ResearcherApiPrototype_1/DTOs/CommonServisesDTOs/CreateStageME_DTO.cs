namespace ResearcherApiPrototype_1.DTOs.CommonServisesDTOs
{
    public class CreateStageME_DTO
    {
        public string Discription { get; set; }
        public string StageType { get; set; }
        public int ServiceId { get; set; }//Общая глобальная заявка
        public int CreatorId { get; set; }
        public int ImplementerId { get; set; }
    }
}
