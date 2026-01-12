namespace ResearcherApiPrototype_1.DTOs.NodesDTOs
{
    public class NodeUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mesurement { get; set; } = string.Empty; //кв.ч, м3 и так далее
        public bool IsValue { get; set; }
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string PlcNodeId { get; set; }
    }
}
