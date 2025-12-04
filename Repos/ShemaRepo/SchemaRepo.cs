using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1.Repos.ShemaRepo
{
    public class SchemaRepo : ISchemaRepo
    {
        private readonly AppDbContext _context;

        public SchemaRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HardwareSchemaImage> CreateCoordinates(SchemaImageCreateDTO coordinates)
        {
            var newCoordinates = new HardwareSchemaImage()
            {
                Top = coordinates.Top,
                Left = coordinates.Left,
                Height = coordinates.Height,
                Width = coordinates.Width,
                HardwareSchemaId = coordinates.HardwareSchemaId,
                FileId = coordinates.FileId
            };
            _context.SchemaImages.Add(newCoordinates);
            await _context.SaveChangesAsync();
            return newCoordinates;

        }

        public async Task<HardwareSchema> CreateSchema(SchemaCreateDTO schema)
        {
            var newschema = new HardwareSchema()
            {
                Name = schema.Name,
                SchemaImage = schema.SchemaImage,
                StaticObjectInfoId = schema.StaticObjectInfoId,
                FileId = schema.FileId
            };
            _context.Schemas.Add(newschema);    
            await _context.SaveChangesAsync();
            return newschema;
        }

        public async Task<ICollection<HardwareSchemaImage>> GetCoordinatesBySchemaId(int id)
        {
            return await _context.SchemaImages
                //.Include(x => x.HardwareSchemaId)
                .Where(x => x.Id == id)
                .ToListAsync();
        }

        public async Task<ICollection<HardwareSchema>> GetSchemaById(int id)
        {
            return await _context.Schemas
                //.Include(x => x.StaticObjectInfoId)
                .Where (x => x.StaticObjectInfoId == id)
                .ToListAsync();
        }
    }
}
