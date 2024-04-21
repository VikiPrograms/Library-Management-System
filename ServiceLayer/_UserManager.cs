    using BusinessLayer;
    using DataLayer;
    using Microsoft.AspNetCore.Components.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.Xml;
    using System.Text;
    using System.Threading.Tasks;

    namespace ServiceLayer
    {
        public class _UserManager
        {
            private  readonly UserContext context;

            public _UserManager(UserContext userContext)
            {
                context = userContext;
            }

            public async Task SeedDataAsync(string adminPass, string adminEmail)
            {
                 await context.SeedDataAsync(adminPass, adminEmail);
            }

            #region CRUD
            public async Task CreateUserAsync(ApplicationUser user)
            {
                await context.CreateUserAsync(user.UserName, user.Password, user.FirstName, user.LastName, user.Role); //System.NullReferenceException: 'Object reference not set to an instance of an object.'
            }

            public async Task<ClaimsPrincipal> LogInUserAsync(string username, string password)
            {
                return await context.LogInUserAsync(username, password);
            }

            public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
            {
                return await context.LogOutUserAsync(claimsPrincipal);
            }

            public async Task<ApplicationUser> ReadUserAsync(string key, bool useNavigationalProperties = false)
            {
                return await context.ReadUserAsync(key, useNavigationalProperties);
            }

            public async Task<IEnumerable<ApplicationUser>> ReadAllUsersAsync(bool useNavigationalProperties = false)
            {
                 return await context.ReadAllUsersAsync(useNavigationalProperties);
            }

            public async Task UpdateUserAsync(ApplicationUser item, bool useNavigationalProperties = false)
            {
                 await context.UpdateUserAsync(item, useNavigationalProperties);
            }

            public async Task DeleteUserAsync(string id)
            {
                await context.DeleteUserAsync(id);
            }

            public async Task DeleteUserByNameAsync(string username)
            {
                await context.DeleteUserByNameAsync(username);
            }
            #endregion

            #region Find Methods

            public async Task<ApplicationUser> FindUserByNameAsync(string name, bool useNavigationalProperties = false)
            {
                ApplicationUser user = await context.FindUserByNameAsync(name, useNavigationalProperties);
                return user;
            }

            public bool Exists(string key)
            {
                return context.Exists(key);
            }

            #endregion

        }
    }
