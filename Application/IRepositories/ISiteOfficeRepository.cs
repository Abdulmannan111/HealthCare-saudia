using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Entities.Facility;
using Application.Pagination;

namespace Application.IRepositories
{
    public interface ISiteOfficeRepository
    {
        Task<ApiResponse<SiteOfficeDto>> GetAllAsync(int page, int pageSize, string? search, string? region_id, string? facility, int? version, string? status);
        Task CreateSiteOfficeAsync(List<SiteOfficeDto> site);
        Task UpdateSiteOfficeAsync(string id, SiteOfficeDto site);
        Task DeleteAsync(string id);
        Task<SiteOfficeDto> GetByIdAsync(string id);
    }
}
