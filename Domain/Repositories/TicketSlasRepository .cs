using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using AutoMapper;
using Infrastructure.Mongo;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Domain.Repositories
{
    public class TicketSlasRepository : ITicketSlasRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;

        public TicketSlasRepository(MongoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TicketSlasDto>> GetAllAsync(int page, int per_page, string? search)
        {
            per_page = per_page <= 0 ? 10 : per_page;
            page = page <= 0 ? 1 : page;
            var skip = (page - 1) * per_page;

            // First, count total records
            var filter = Builders<TicketSlas>.Filter.Eq(x => x.deleted_at, null);
            var totalRecords = await _context._ticketSlasCollection.CountDocumentsAsync(filter);

            // Then fetch paginated data
            var entity = await _context._ticketSlasCollection
                .Find(filter)
                .SortByDescending(x => x.CreatedDate)
                .Skip(skip)
                .Limit(per_page)
                .ToListAsync();

            var dto = _mapper.Map<List<TicketSlasDto>>(entity);

            var totalPages = (int)Math.Ceiling(totalRecords / (double)per_page);

            return new ApiResponse<TicketSlasDto>
            {
                current_page = page,
                data = dto,
                from = (page - 1) * per_page + 1,
                last_page = totalPages,
                per_page = per_page,
                to = Math.Min(page * per_page, (int)totalRecords),
                total = (int)totalRecords
            };
        }


        public async Task<TicketSlasDto> GetByIdAsync(string id)
        {
            var entity = await _context._ticketSlasCollection
                .Find(x => x.ID.ToString() == id && x.deleted_at == null)
            .FirstOrDefaultAsync();

            var dto = _mapper.Map<TicketSlasDto>(entity);
            return dto;
        }

        public async Task<TicketSlas> GetByFormIdAsync(string formId)
        {
            return await _context._ticketSlasCollection
                .Find(x => x.FormId == formId && x.deleted_at == null)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TicketSlas ticketSla)
        {
            try
            {
                var existing = await GetByFormIdAsync(ticketSla.FormId);
                if (existing != null)
                    throw new Exception("Form ID must be unique.");

                await _context._ticketSlasCollection.InsertOneAsync(ticketSla);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(string id, TicketSlas ticketSla)
        {
            var existing = await GetByFormIdAsync(ticketSla.FormId);
            if (existing != null)
                throw new Exception("Form ID must be unique.");
            ticketSla.ID = ObjectId.Parse(id);
            await _context._ticketSlasCollection.ReplaceOneAsync(x => x.ID.ToString() == id, ticketSla);
        }

        public async Task DeleteAsync(string id)
        {
            var update = Builders<TicketSlas>.Update.Set(x => x.deleted_at, DateTime.Now);
            await _context._ticketSlasCollection.UpdateOneAsync(x => x.ID.ToString() == id, update);
        }
    }
}
