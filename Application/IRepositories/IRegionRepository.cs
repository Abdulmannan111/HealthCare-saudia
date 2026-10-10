using Application.DTOs;
using Application.Entities;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRegionRepository
    {
        RegionDto GetByIdAsync(string id);
        Task<ApiResponse<DTOs.RegionDto>> GetAllAsync(int page, int pageSize, string submission_for);
        Task CreateAsync(RegionDto region);
        Task UpdateAsync(string id, RegionDto region);
        Task DeleteAsync(string id);
        Task<List<RegionDto>> GetRegionsByMethodAsync(string method);
        Task<List<RegionDto>> GetRegions();
        List<Regions> GetRegionsByIds(List<string> regionIds);
    }
}
