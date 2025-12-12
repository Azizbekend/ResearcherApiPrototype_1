namespace ResearcherApiPrototype_1.DTOs.NodesDTOs
{
    public class MassNodeCreateDTO
    {
        public int HardwareId { get; set; }
        public List<NodeCreateDTO> Nodes { get; set; } = null!;
    }
}
