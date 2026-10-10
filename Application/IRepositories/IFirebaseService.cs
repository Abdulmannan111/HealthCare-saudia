using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IFirebaseService
    {
        Task<string> SendNotificationAsync(string token, string title, string body);
        Task UpdateTokenAsync(string userId, string fcmToken);
    }
}
