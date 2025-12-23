using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.DTOs.SchemesDTOs;
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
                FileId = coordinates.FileId,
                HardwareId = coordinates.HardwareId,
                RedFileId = coordinates.RedFileId,
                GreenFileId = coordinates.GreenFileId

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

        public async Task<SchemeCard> CreateSchemeCard(SchemeCard newCard)
        {
            _context.SchemeCards.Add(newCard);
            await _context.SaveChangesAsync();
            return newCard;
        }

        public async Task<SchemeCard> DeleteCard(int id)
        {
            var card = await _context.SchemeCards.FirstOrDefaultAsync(x => x.Id == id);
            _context.SchemeCards.Remove(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task DeleteCoordinates(int id)
        {
            var toDel = await _context.SchemaImages.FirstOrDefaultAsync(x=>x.Id == id);
            if (toDel != null)
            {
                _context.SchemaImages.Remove(toDel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<SchemaCardsInfoGetDTO>> GetCardsBySchemeId(int schemeId)
        {
            List<SchemaCardsInfoGetDTO> infos = new List<SchemaCardsInfoGetDTO>();
           var list = await _context.SchemeCards.Where(x=> x.SchemeId == schemeId).ToListAsync();
            foreach (var item in list)
            {
                var nodeInfo = await _context.Nodes.FirstOrDefaultAsync(x => x.Id == item.NodeInfoId);
                var schemaCard = new SchemaCardsInfoGetDTO
                {
                    Id = item.Id,
                    Left = item.Left,
                    Top = item.Top,
                    NodeInfoId = nodeInfo.Id,
                    NodeName = nodeInfo.Name,
                    MeasurementName = nodeInfo.Mesurement
                };
                infos.Add(schemaCard);
            }
            return infos;
        }

        public async Task<ICollection<HardwareSchemaImage>> GetCoordinatesBySchemaId(int id)
        {
            return await _context.SchemaImages
                //.Include(x => x.HardwareSchemaId)
                .Where(x => x.HardwareSchemaId == id)
                .ToListAsync();
        }

        public async Task<ICollection<HardwareSchema>> GetSchemaById(int id)
        {
            return await _context.Schemas
                //.Include(x => x.StaticObjectInfoId)
                .Where (x => x.StaticObjectInfoId == id)
                .ToListAsync();
        }

        public Task<SchemeCard> GetSingleSchemeCard(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HardwareSchemaImage> UpdateCoordinates(SchemeImageUpdateDTO dto)
        {
            var si = await _context.SchemaImages.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (si != null)
            {
                si.Top = dto.Top;
                si.Left = dto.Left;
                si.Height = dto.Height;
                si.Width = dto.Width;
                si.FileId = dto.FileId;
                si.GreenFileId = dto.GreenFileId;
                si.RedFileId = dto.RedFileId;
                _context.SchemaImages.Attach(si);
                await _context.SaveChangesAsync();
                return si;
            }
            else
                throw new InvalidOperationException("Not found!");
        }

        public async Task<SchemeCard> UpdateSchemeCard(SchemeCardUpdateDTO card)
        {
            var schemecard = await _context.SchemeCards.FirstOrDefaultAsync(x => x.Id == card.Id);
            schemecard.Left = card.Left;
            schemecard.Top = card.Top;
            _context.SchemeCards.Attach(schemecard);
            await _context.SaveChangesAsync();
            return schemecard;
        }
    }
}
