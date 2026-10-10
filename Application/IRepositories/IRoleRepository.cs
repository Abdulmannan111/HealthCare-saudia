using Application.DTOs;
using Application.Entities;
using Application.Pagination;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRoleRepository
    {
        Task<ApiResponse<RoleDto>> GetAllAsync(int page, int pageSize);
        Task<RoleDto> CreateRoleAsync(RoleDto role);
        Task<RoleDto> UpdateRoleAsync(string id ,RoleDto role);
        Task<bool> DeleteRoleAsync(string id);
        Task<RoleDto> GetRoleByIdAsync(string id);
        Task AddPermissionToRoleAsync(string roleId, string permissionId);
        Task RemovePermissionFromRoleAsync(string roleId, string permissionId);
    }
}
