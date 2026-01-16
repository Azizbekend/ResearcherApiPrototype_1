namespace ResearcherApiPrototype_1.DTOs.NodesDTOs
{
    public class CommandNodeGetDTO
    {
        public string Name { get; set; }
        public string Mesurement { get; set; } = string.Empty; //кв.ч, м3 и так далее
        public string PlcNodeId { get; set; }
        public bool IsCommand { get; set; } = false;
        public bool IsValue { get; set; } = true;
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string? LastValue { get; set; }
    }
}
