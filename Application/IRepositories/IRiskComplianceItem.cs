
using Application.DTOs;
using Application.DTOs.Category;
using Application.DTOs.RiskCompliance;
using Application.Entities;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRiskComplianceItem
    {
        Task<RiskComplianceItemDto> GetByIdAsync(string id);
        Task<ApiResponse<RiskComplianceItemDto>> GetAllAsync(int page, int pageSize, string? search, string deleted_at);
        Task CreateAsync(RiskComplianceItemDto region);
        Task UpdateAsync(string id, RiskComplianceRequestDto region);
        Task DeleteAsync(string id);
        Task<List<CategoriesDomainsDto>> categories();
        Task<ResultCount> GetCounts();
        Task<MemoryStream> ExportToExcelAsync(int page = 1, int pageSize = 20000, string? search = null, string? regionId = null);
        Task<List<activity_logDto>> activity();
    }
}