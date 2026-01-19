namespace ResearcherApiPrototype_1.DTOs.ObjectDTOs
{
    public class CreateObjectAccessesDTO
    {
        public bool IsNodeInfosEnabled { get; set; }
        public bool IsCommandsEnabled { get; set; }
        public bool Is3DEnabled { get; set; }
        public bool CanCreateCommonServiceRequests { get; set; }
        public bool CanCreateIncidentServiseRequests { get; set; }
    }
}
