using Application.DTOs;
using Application.Entities;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IModuleRepository
    {
        Task<List<Modules>> GetAllModulesAsync(string id);
        Task<ModuleDto> GetByIdAsync(string id);
        Task<ApiResponse<DTOs.ModuleDto>> GetAllAsync(int page, int per_page, string? search);
        Task CreateAsync(ModuleDto module);
        Task UpdateAsync(string id ,ModuleDto module);
        Task DeleteAsync(string id);
        List<Modules> GetModulesByIds(List<string> moduleIds);
    }
}
