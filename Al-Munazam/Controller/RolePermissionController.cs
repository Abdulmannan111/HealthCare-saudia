using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRoleRepository _roleUseCases;
        private readonly IPermissionRepository _permissionRepository;

        public RolePermissionController(IRoleRepository roleUseCases, IPermissionRepository permissionRepository)
        {
            _roleUseCases = roleUseCases;
            _permissionRepository = permissionRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll(int page = 1, int pageSize = 10)
        {
            var roles = await _roleUseCases.GetAllAsync(page, pageSize);
            var data = new
            {
                data = roles
            };
            return Ok(data);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto role)
        {
            if (role == null)
            {
                return BadRequest("role data is required.");
            }

            try
            {
                await _roleUseCases.CreateRoleAsync(role);
                return Ok(new { data = "Role Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(string id , [FromBody] RoleDto role)
        {
            try
            {
                var existingRole = await _roleUseCases.GetRoleByIdAsync(id);
                if (existingRole == null)
                {
                    return NotFound();
                }
                await _roleUseCases.UpdateRoleAsync(id, role);
                return Ok(new { data = "Role Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var role = await _roleUseCases.GetRoleByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                await _roleUseCases.DeleteRoleAsync(id);
                return Ok(new { data = "Role Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            try
            {
                var role = await _roleUseCases.GetRoleByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(new { data = role });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        [HttpGet("GetAllPermission")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermission(int page = 1, int pageSize = 10)
        {
            var permission = await _permissionRepository.GetAllAsync(page, pageSize);
            var data = new
            {
                data = permission
            };
            return Ok(data);
        }

        [HttpGet("GetPermissionById/{id}")]
        public async Task<IActionResult> GetPermissionById(string id)
        {
            try
            {
                var permission = await _permissionRepository.GetByIdAsync(id);
                if (permission == null)
                {
                    return NotFound();
                }
                return Ok(new { data = permission });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreatePermission")]
        public async Task<ActionResult> CreatePermission(PermissionDto permission)
        {
            if (permission == null)
            {
                return BadRequest("permission data is required.");
            }

            try
            {
                await _permissionRepository.CreateAsync(permission);
                return Ok(new { data = "permission Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePermission/{id}")]
        public async Task<IActionResult> UpdatePermission(string id, PermissionDto permission)
        {
            try
            {
                var existingPermission = await _permissionRepository.GetByIdAsync(id);
                if (existingPermission == null)
                {
                    return NotFound();
                }
                await _permissionRepository.UpdateAsync(id, permission);
                return Ok(new { data = "Permission Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpDelete("DeletePermission/{id}")]
        public async Task<IActionResult> DeletePermission(string id)
        {
            await _permissionRepository.DeleteAsync(id);
            return Ok(new { id = id });
        }

        [HttpPost("AddPermissionToRole")]
        public async Task<IActionResult> AddPermissionToRole(string roleId, string permissionId)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
            {
                return NotFound();
            }

            await _roleUseCases.AddPermissionToRoleAsync(roleId, permissionId);
            return Ok();
        }

        [HttpDelete("RemovePermissionFromRole")]
        public async Task<IActionResult> RemovePermissionFromRole(string roleId, string permissionId)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
            {
                return NotFound();
            }

            await _roleUseCases.RemovePermissionFromRoleAsync(roleId, permissionId);
            return Ok();
        }
    }
}
