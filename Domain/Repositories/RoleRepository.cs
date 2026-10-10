using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MongoDBContext _context;
        private readonly string _baseUrl;

        public RoleRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context)
        {
            _context = context;
            _baseUrl = apiSettings.Value.BaseUrl + "role-index";
        }

        public async Task<ApiResponse<RoleDto>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var skip = (page - 1) * pageSize;
            var roles = await _context.Roles.Find(_ => true).Skip(skip).Limit(pageSize).ToListAsync();
            var totalRecords = await _context.Roles.CountDocumentsAsync(_ => true);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var roleDtos = MapToDtoList(roles);

            return new ApiResponse<RoleDto>
            {
                current_page = page,
                data = roleDtos.ToList(),
                first_page_url = $"{_baseUrl}?page=1",
                from = (page - 1) * pageSize + 1,
                last_page = totalPages,
                last_page_url = $"{_baseUrl}?page={totalPages}",
                links = new List<Link>
            {
                new Link
                {
                    url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                    label = "&laquo; Previous",
                    active = false
                },
                new Link
                {
                    url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                    label = "Next &raquo;",
                    active = false
                }
            },
                next_page_url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                path = _baseUrl,
                per_page = pageSize,
                prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                to = Math.Min(page * pageSize, (int)totalRecords),
                total = (int)totalRecords
            };
        }


        public async Task<RoleDto> CreateRoleAsync(RoleDto role)
        {
            var roleEntity = MapToEntity(role);
            await _context.Roles.InsertOneAsync(roleEntity);
            return role;
        }

        public async Task<RoleDto> UpdateRoleAsync(string id ,RoleDto role)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }
            var roleEntity = MapToEntity(role);
            roleEntity.ID = objectId;
            await _context.Roles.ReplaceOneAsync(r => r.ID.ToString() == id, roleEntity);
            return role;    
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var result = await _context.Roles.DeleteOneAsync(role => role.ID.ToString() == id);
            return result.DeletedCount > 0;
        }

        public async Task<RoleDto> GetRoleByIdAsync(string id)
        {
            var res= await _context.Roles.Find(f => f.ID.ToString() == id).FirstOrDefaultAsync();
            var data = MapToDto(res);
            return data;
        }

        public async Task AddPermissionToRoleAsync(string roleId, string permissionId)
        {
            var role = await GetRoleByIdAsync(roleId);
            if (role != null && !role.permission_ids.Contains(permissionId))
            {
                role.permission_ids.Add(permissionId);
                await UpdateRoleAsync(roleId, role);
            } 
        }

        public async Task RemovePermissionFromRoleAsync(string roleId, string permissionId)
        {
            var role = await GetRoleByIdAsync(roleId);
            if (role != null && role.permission_ids.Contains(permissionId))
            {
                role.permission_ids.Remove(permissionId);
                await UpdateRoleAsync(roleId, role);
            }
        }

        private RoleDto MapToDto(Role role)
        {
            if (role == null) return null;

            return new RoleDto
            {
                _id=role.ID.ToString(),
               name = role.Name,
               health_directorate=role.HealthDirectorate,
               permission_ids=role.PermissionIds,
               responsible_department=role.ResponsibleDepartment,
            };
        }

        private IEnumerable<RoleDto> MapToDtoList(IEnumerable<Role> roles)
        {
            var roleDtos = new List<RoleDto>();
            foreach (var region in roles)
            {
                roleDtos.Add(MapToDto(region));
            }
            return roleDtos;
        }
        private Role MapToEntity(RoleDto roleDto)
        {
            return new Role
            {
               Name = roleDto.name,
               HealthDirectorate=roleDto.health_directorate,
               PermissionIds=roleDto.permission_ids,
               ResponsibleDepartment=roleDto.responsible_department,
            };
        }
    }
}









