using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace PluralsightDemo
{
    public class PluralsightUserStore : IUserStore<PluralsightUser>, IUserPasswordStore<PluralsightUser>
    {
        public void Dispose()
        {

        }

        public Task<string> GetUserIdAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(PluralsightUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(PluralsightUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public static DbConnection GetopenConnection()
        {
            var connection = new SqlConnection("Data Source=BS-173;Initial Catalog=PluralsightDemo;trusted_connection=yes");
            connection.Open();
            return connection;
        }

        public async Task<IdentityResult> CreateAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetopenConnection())
            {
                await connection.ExecuteAsync(
                    @"INSERT INTO [dbo].[PluralsightUsers]
                            ([Id]
                            ,[UserName]
                            ,[NormalizedUserName]
                            ,[PasswordHash])
                            VALUES(@id,@userName,@normalizedUserName,@passwordHash)", new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetopenConnection())
            {
                await connection.ExecuteAsync(
                                @"UPDATE [dbo].[PluralsightUsers]
                                SET [Id] = @Id
                                ,[UserName] = @UserName
                                ,[NormalizedUserName] = @NormalizedUserName
                                ,[PasswordHash] = @PasswordHash
                                WHERE [Id] = @Id",
                                new
                                {
                                    user.Id,
                                    user.UserName,
                                    user.NormalizedUserName,
                                    user.PasswordHash
                                });
            }
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PluralsightUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetopenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PluralsightUser>(@"Select * from [PluralsightUsers]
                                WHERE [Id] = @Id", new { Id = userId });
            }
        }

        public async Task<PluralsightUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetopenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PluralsightUser>(@"Select * from [PluralsightUsers]
                                WHERE [normalizedUserName] = @normalizedUserName", new { normalizedUserName = normalizedUserName });
            }
        }

        public Task SetPasswordHashAsync(PluralsightUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
