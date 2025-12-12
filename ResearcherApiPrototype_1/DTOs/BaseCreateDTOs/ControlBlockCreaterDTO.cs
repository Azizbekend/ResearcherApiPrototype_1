using ResearcherApiPrototype_1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearcherApiPrototype_1.DTOs.BaseCreateDTOs
{
    public class ControlBlockCreaterDTO
    {
        public string Name { get; set; } = string.Empty;
        public string PlcIpAdress { get; set; } = string.Empty;
        public int StaticObjectInfoId { get; set; }

    }
}
