using Application.IRepositories;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class AccessTokensRepository : IAccessTokens
    {
        private readonly MongoDBContext _context;
        public AccessTokensRepository(MongoDBContext context)
        {
                _context = context;
        }
        public async Task<bool> DeleteAccessTokenAsync(string tokenId)
        {
            var accessToken = await _context._oauth_access_tokens
                .Find(x => x.ID.ToString() == tokenId)
                .FirstOrDefaultAsync();
            if (accessToken == null)
            {
                return false;
            }
            var result = await _context._oauth_access_tokens.DeleteOneAsync(x => x.ID == accessToken.ID);
            return result.DeletedCount > 0;
        }
        public async Task<bool> CheckRevokeAsync(string tokenId)
        {
            var currentUtcTime = DateTime.UtcNow;

            return await _context._oauth_access_tokens
                .Find(x => x.IdValue.ToString() == tokenId && !x.Revoked && x.ExpiresAt > currentUtcTime)
                .AnyAsync();
        }

    }
}
