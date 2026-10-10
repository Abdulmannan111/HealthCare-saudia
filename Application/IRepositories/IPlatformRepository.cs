using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.IRepositories
{
    public interface IPlatformRepository
    {
        Task<List<PlatformDto>> GetAllAsync();
        Task<PlatformDto?> GetByIdAsync(string id);
        Task CreateAsync(PlatformDto dto);
        Task UpdateAsync(string id, PlatformDto dto);
        Task DeleteAsync(string id);
    }
}
