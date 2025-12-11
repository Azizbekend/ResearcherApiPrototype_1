namespace ResearcherApiPrototype_1.DTOs
{
    public class MassNodeCreateDTO
    {
        public int HardwareId { get; set; }
        public List<NodeCreateDTO> Nodes { get; set; } = null!;
    }
}
