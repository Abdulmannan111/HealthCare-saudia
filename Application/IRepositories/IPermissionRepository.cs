using Application.DTOs;
using Application.Entities;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IPermissionRepository
    {
        Task<PermissionDto> GetByIdAsync(string id);
        Task<PermissionDto> CreateAsync(PermissionDto permission);
        Task<PermissionDto> UpdateAsync(string id ,PermissionDto permission);
        Task<bool> DeleteAsync(string id);
        Task<ApiResponse<PermissionDto>> GetAllAsync(int page, int pageSize); 
     }
}
