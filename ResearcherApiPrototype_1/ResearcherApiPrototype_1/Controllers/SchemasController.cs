using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs.SchemesDTOs;
using ResearcherApiPrototype_1.Models;
using ResearcherApiPrototype_1.Repos.ShemaRepo;

namespace ResearcherApiPrototype_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemasController : ControllerBase
    {
        private readonly ISchemaRepo _schemaRepo;

        public SchemasController(ISchemaRepo schemaRepo)
        {
            _schemaRepo = schemaRepo;
        }
        [HttpPost("schema/create")]
        public async  Task<ActionResult<HardwareSchema>> CreateSchema(SchemaCreateDTO dto)
        {
            var schema = await _schemaRepo.CreateSchema(dto);
            return Ok(schema);
        }
        [HttpPost("schema/coordinates/create")]
        public async Task<ActionResult<HardwareSchemaImage>> CreateCoordinates(SchemaImageCreateDTO dto)
        {
            var coordinates = await _schemaRepo.CreateCoordinates(dto);
            return Ok(coordinates);
        }

        [HttpGet("schemas")]
        public async Task<ActionResult<ICollection<HardwareSchema>>> GetSchemas(int id)
        {
            var schema = await _schemaRepo.GetSchemaById(id); 
            return Ok(schema);
        }

        [HttpGet("schemas/coordinates")]
        public async Task<ActionResult<ICollection<HardwareSchema>>> GetCoordinates(int id)
        {
            var schema = await _schemaRepo.GetCoordinatesBySchemaId(id);
            return Ok(schema);
        }

        [HttpPut("schemas/coordinates/update")]
        public async Task<ActionResult<HardwareSchemaImage>> UpdateImageCoordinates(SchemeImageUpdateDTO dto)
        {
            var newCoordinates = await _schemaRepo.UpdateCoordinates(dto);
            return Ok(newCoordinates);
        }
        [HttpDelete("schema/coordinates")]
        public async Task<IActionResult> DeleteCoordinates(int id)
        {
            await _schemaRepo.DeleteCoordinates(id);
            return Ok();
        }

        [HttpPost("card/create")]
        public async Task<ActionResult<SchemeCard>> SchemeCardCreate(SchemeCard card)
        {
            var schemeCard = await _schemaRepo.CreateSchemeCard(card);
            return Ok(schemeCard);
        }

        [HttpGet("scheme/cards")]
        public async Task<ActionResult<ICollection<SchemaCardsInfoGetDTO>>> GetAllCardsById(int id)
        {
            var cards = await _schemaRepo.GetCardsBySchemeId(id); 
            return Ok(cards);
        }

        [HttpPut("card/update")]
        public async Task<ActionResult<SchemeCard>> Update(SchemeCardUpdateDTO schemeCard)
        {
            var updatedCard = await _schemaRepo.UpdateSchemeCard(schemeCard);
            return Ok(updatedCard);
        }

        [HttpDelete("card/delete")]
        public async Task<ActionResult<SchemeCard>> Delete(int id)
        {
            var deleted = await _schemaRepo.DeleteCard(id);
            return Ok(deleted);
        }
    }
}
