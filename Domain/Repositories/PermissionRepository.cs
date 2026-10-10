
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly MongoDBContext _context;
        private readonly string _baseUrl;


        public PermissionRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context)
        {
            _context = context;
            _baseUrl = apiSettings.Value.BaseUrl + "permission-index";
        }

        public async Task<PermissionDto> GetByIdAsync(string id)
        {
            var data = await _context.Permission.Find(p => p.ID.ToString() == id).FirstOrDefaultAsync();
            var res = MapToDto(data);
            return res; 
        }

        public async Task<PermissionDto> CreateAsync(PermissionDto permission)
        {

            var permissionEntity = MapToEntity(permission);
            await _context.Permission.InsertOneAsync(permissionEntity);
            return permission;
        }

        public async Task<PermissionDto> UpdateAsync(string id ,PermissionDto permission)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }
            var permissionEntity = MapToEntity(permission);
            permissionEntity.ID = objectId;
            await _context.Permission.ReplaceOneAsync(r => r.ID.ToString() == id, permissionEntity);
            return permission;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Permission.DeleteOneAsync(role => role.ID.ToString() == id);
            return result.DeletedCount > 0;
        }

        public async Task<ApiResponse<PermissionDto>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var skip = (page - 1) * pageSize;
            var permission = await _context.Permission.Find(p => true).Skip(skip).Limit(pageSize).ToListAsync();
            var totalRecords = await _context.Roles.CountDocumentsAsync(_ => true);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var permissionDtos = MapToDtoList(permission);

            return new ApiResponse<PermissionDto>
            {
                current_page = page,
                data = permissionDtos.ToList(),
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


        private PermissionDto MapToDto(Permission permission)
        {
            if (permission == null) return null;

            return new PermissionDto
            {
                _id = permission.ID.ToString(),
                Name = permission.name,
                createdFrom=permission.CreatedBy,
                created_at = permission.CreatedDate,
                updatedFrom=permission.UpdatedBy,
                updated_at = permission.UpdatedDate,    
                Version = permission.Version,
            };
        }

        private IEnumerable<PermissionDto> MapToDtoList(IEnumerable<Permission> permissions)
        {
            var permissionDtos = new List<PermissionDto>();
            foreach (var per in permissions)
            {
                permissionDtos.Add(MapToDto(per));
            }
            return permissionDtos;
        }
        private Permission MapToEntity(PermissionDto permissionDto)
        {
            return new Permission
            {
                name = permissionDto.Name,
                CreatedDate=permissionDto.created_at,
                UpdatedDate=permissionDto.updated_at,   
                CreatedBy=permissionDto.createdFrom,
                UpdatedBy=permissionDto.updatedFrom,    
                Version = permissionDto.Version,    
            };
        }
    }
}
