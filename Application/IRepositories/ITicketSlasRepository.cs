using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Entities;
using Application.Pagination;

namespace Application.IRepositories
{
    public interface ITicketSlasRepository
    {
        Task<ApiResponse<TicketSlasDto>> GetAllAsync(int page, int per_page, string? search);
        Task<TicketSlasDto> GetByIdAsync(string id);
        Task<TicketSlas> GetByFormIdAsync(string formId);
        Task CreateAsync(TicketSlas ticketSla);
        Task UpdateAsync(string id, TicketSlas ticketSla);
        Task DeleteAsync(string id);
    }
}
