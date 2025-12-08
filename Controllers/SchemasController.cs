using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherApiPrototype_1.DTOs;
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
    }
}
