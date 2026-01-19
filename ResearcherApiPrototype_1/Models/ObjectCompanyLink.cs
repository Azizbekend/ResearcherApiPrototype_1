using System.ComponentModel.DataAnnotations;

namespace ResearcherApiPrototype_1.Models
{
    public class ObjectCompanyLink
    {
        [Key]
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string CompanyRole { get; set; }


    }


}

    


