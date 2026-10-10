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
    public interface IClusterRepository
    {
        ClusterDto GetById(string id);
        Task<ApiResponse<DTOs.ClusterDto>> GetAllAsync(int page, int pageSize, string? search, string submission_for);
        Task CreateAsync(ClusterDto region);
        Task UpdateAsync(string id, ClusterDto region);
        Task DeleteAsync(string id);
        Task<List<ClusterDto>> GetClustersByRegionIdAsync(string regionId);
    }
}
