namespace ResearcherApiPrototype_1.DTOs
{
    public class NodeUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mesurement { get; set; } = string.Empty; //кв.ч, м3 и так далее
        public bool IsValue { get; set; }
        public string PlcNodeId { get; set; }
    }
}
