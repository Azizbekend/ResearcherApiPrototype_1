using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models.ServiceRequests
{
    public class SuppliRequestStageLink
    {
        [Key]
        public  int  Id { get; set; }
        public  int SuppluRequestId { get; set; }
        public int SuppluStageId { get; set; }
    }
}
