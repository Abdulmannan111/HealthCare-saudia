using Application.DTOs;
using Application.Entities;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IFacilitySiteRepository
    {
        Task<ObligationSitesDto> GetFacilitySiteByIdAsync(string id);
        Task<List<ParentSiteDto>> GetParentSiteAsync(
    string regionid,
    string clusterid,
    string? search,
    int skip,
    int take);
        Task<ApiResponse<ObligationSitesDto>> GetAllFacilitySitesAsync(int page, int pageSize, string? search, string? region_id, string? facility, int? version, string? status, string submission_for);
        Task CreateFacilitySiteAsync(ObligationSitesDto site);
        Task UpdateFacilitySiteAsync(string id, ObligationSitesDto site);
        Task DeleteAsync(string id);
        Task<ObligationSites> GetByIdAsync(string id);
        Task UpdateAsync(string id, ObligationSites site);
        Task<ResultCount> GetCounts();
    }
}
