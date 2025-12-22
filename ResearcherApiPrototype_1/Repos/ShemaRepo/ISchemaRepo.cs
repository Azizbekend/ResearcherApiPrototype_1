using ResearcherApiPrototype_1.DTOs.SchemesDTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ShemaRepo
{
    public interface ISchemaRepo
    {
        //base
        Task<HardwareSchema> CreateSchema(SchemaCreateDTO schema);
        Task<ICollection<HardwareSchema>> GetSchemaById(int id);
        //coordinates
        Task<HardwareSchemaImage> CreateCoordinates(SchemaImageCreateDTO coordinates);
        Task<ICollection<HardwareSchemaImage>> GetCoordinatesBySchemaId(int id);
        Task<HardwareSchemaImage> UpdateCoordinates(SchemeImageUpdateDTO dto);
        Task DeleteCoordinates(int id);
        //scheme cards
        Task<SchemeCard> CreateSchemeCard(SchemeCard newCard);
        Task<SchemeCard> GetSingleSchemeCard(int id);
        Task<SchemeCard> UpdateSchemeCard(SchemeCardUpdateDTO card);
        Task<SchemeCard> DeleteCard(int id);
        Task<ICollection<SchemaCardsInfoGetDTO>> GetCardsBySchemeId(int schemeId);

    }
}
