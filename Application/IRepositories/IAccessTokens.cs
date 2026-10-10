using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IAccessTokens
    {
        Task<bool> DeleteAccessTokenAsync(string tokenId);
        Task<bool> CheckRevokeAsync(string tokenId);
    }
}
