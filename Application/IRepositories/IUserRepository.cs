using Application.DTOs.User;
using Application.Entities;
using Application.Pagination;

namespace Application.IRepositories
{
    public interface IUserRepository
    {
        Task<ApiResponse<UserDto>> GetAllUsersAsync(int page, int pageSize, string? status, string? search);
        Task<ApiResponse<UserDto>> ExportAsync(int page, int pageSize, string? status, string? search);
        Task UpdateAsync(string id, Users site);
        Task<Users> GetByIdAsync(string id);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> GetUserProfileAsync(string id);
        Task<UserDto[]> GetUserByEmailAsync(string email);
        Task<Users> GetUserByUsernameAsync(string email);
        Task<UserDto> AddUserAsync(UserDto user);
        Task<UserDto> UpdateUserAsync(string id , UserDto user);
        Task<UserDto> UpdateUserByFieldName(string id, UserDto user);
        Task<UserDto> UpdateUserStatusAsync(string id, UserDto user);
        Task DeleteUserAsync(string id);
        Task ScheduleUserDeletionAsync(string id);
        Task<List<Users>> GetFilteredUsersAsync(UserFilterDto filterDto);
        Task<List<Users>> GetFilteredAllUsersAsync(AllUsersDto req);
        Task AddRefreshTokenAt(string email);
        Task AddRoleToUserAsync(string userId, string roleId);
        Task RemoveRoleFromUserAsync(string userId, string roleId);
        Task UpdateRegionClusterFacilityWithUserId(string userId, List<string> regionIds, List<string> clusterIds, List<string> facilityIds, List<string> moduleIds, string subRegionIds, string subSectorIds);
        Task<ResultCount> GetCounts();
        Task<UserDto> GetUserFromHttpContextAsync(string userIdKey = "UserId");
        Task<UserDto> GetUserProfileFromHttpContextAsync(string userIdKey = "UserId");
        bool IsTopThree(string userType);
        Dictionary<string, (string UserEmail, string UserName)> GetUsersByIdsAsync(List<string> ids);
        Task<List<Users>> GetUsersByEmailsAsync(List<string> emails);
        Task<List<Users>> GetUsersByRolesAsync(List<string> roles);
    }
}
