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
    public interface ICustomNotificationRepository
    {
        Task<ApiResponse<CustomNotificationDto>> PaginateAsync(int page = 0, int perPage = 20, string search = null);
        Task<ApiResponse> CreateAsync(CreateNotificationRequest request);
    }
}
