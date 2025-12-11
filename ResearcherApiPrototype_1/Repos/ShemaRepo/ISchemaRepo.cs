using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ShemaRepo
{
    public interface ISchemaRepo
    {
        Task<HardwareSchema> CreateSchema(SchemaCreateDTO schema);
        Task<ICollection<HardwareSchema>> GetSchemaById(int id);
        Task<HardwareSchemaImage> CreateCoordinates(SchemaImageCreateDTO coordinates);
        Task<ICollection<HardwareSchemaImage>> GetCoordinatesBySchemaId(int id);
        Task<HardwareSchemaImage> UpdateCoordinates(SchemeImageUpdateDTO dto);
        Task DeleteCoordinates(int id);

    }
}
