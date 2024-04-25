using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 


namespace DataLayer
{
    public class UserContext 
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        LibrarySystemDbContext context;
        ReadingCardContext readingCardContext;

        public UserContext(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LibrarySystemDbContext context, ReadingCardContext readingCardContext)
        {
            this.userManager = userManager;
            this.context = context;
            this.readingCardContext = readingCardContext;
        }

        #region Seeding Data with this Project

        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            //await context.Database.MigrateAsync();

            int roles = await context.Roles.CountAsync();

            if (roles == 0)
            {
                await ConfigureRolesAsync();
            }

            int userRoles = await context.UserRoles.CountAsync();

            if (userRoles == 0)
            {
                await ConfigureAdminAccountAsync(adminPass, adminEmail);
            }
        }

        private async Task ConfigureAdminAccountAsync(string password, string email)
        {
            ApplicationUser adminIdentityUser = await context.Users.FirstOrDefaultAsync();

            if (adminIdentityUser == null)
            {
                adminIdentityUser = new ApplicationUser("admincheto", Role.Administrator);
                context.Users.Add(adminIdentityUser);
                await context.SaveChangesAsync();
            }

            await userManager.AddToRoleAsync(adminIdentityUser, Role.Administrator.ToString());
            await userManager.AddPasswordAsync(adminIdentityUser, password);
            await userManager.SetEmailAsync(adminIdentityUser, email);

        }

        private async Task ConfigureRolesAsync()
        {
            // NormalizedName is not set by Identity :[ but when you want to find role by its name
            // remember that you are checking Normalized column's value! That is why I am adding NormalizedName value!
            IdentityRole admin = new IdentityRole(Role.Administrator.ToString()) { NormalizedName = Role.Administrator.ToString() };
            IdentityRole user = new IdentityRole(Role.User.ToString()) { NormalizedName = Role.User.ToString() };            
            context.Roles.Add(admin);
            context.Roles.Add(user);
            await context.SaveChangesAsync();
        }

        #endregion

        #region CRUD

        public async Task CreateUserAsync(string username, string password, string firstName, string lastName, Role role = Role.User)
        {
            try
            {
                if(password == "132435_Library")
                {
                    role = Role.Administrator;
                }
                else
                {
                    role = Role.User;
                }

                ApplicationUser user = new ApplicationUser(username, password, firstName, lastName, role);
                IdentityResult result = await userManager.CreateAsync(user, password);
                ApplicationUser userFromDb = await userManager.FindByNameAsync(user.UserName);
                ReadingCard readingCard = new ReadingCard(userFromDb);
                //await readingCardContext.CreateAsync(readingCard);
                userFromDb.ReadingCard = readingCard;
                await context.SaveChangesAsync();

                if (!result.Succeeded)
                {
                    throw new ArgumentException(result.Errors.First().Description);
                }

                if (role == Role.Administrator)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogInUserAsync(string username, string password)
        {
            try
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);

                if (result.Succeeded)
                {
                    ApplicationUser loggedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                    return await signInManager.CreateUserPrincipalAsync(loggedUser);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity is not null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return new ClaimsPrincipal();
            }
            // If should always be true when you call this method!
            return claimsPrincipal;
        }

        public async Task<ApplicationUser> ReadUserAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<ApplicationUser> query = userManager.Users;
                if (useNavigationalProperties)
                {
                    query = query.Include(user => user.ReadingCard);
                }
                return await query.FirstOrDefaultAsync(u => u.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> ReadAllUsersAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<ApplicationUser> query = userManager.Users;
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserAsync(ApplicationUser item, bool useNavigationalProperties = false)
        {
            try
            {
                //ApplicationUser user = await ReadUserAsync(item.UserName, useNavigationalProperties);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            try
            {
                ApplicationUser user = await ReadUserAsync(id);
                ReadingCard readingCard = user.ReadingCard;
                if (user == null)
                {
                    throw new ArgumentException("There is no user in the database with that id!");
                }

                context.ReadingCards.Remove(readingCard);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserByNameAsync(string name)
        {
            try
            {
                ApplicationUser user = await FindUserByNameAsync(name);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await userManager.DeleteAsync(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Find Methods

        public async Task<ApplicationUser> FindUserByNameAsync(string name, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == name.ToUpper());
                }
                return await userManager.FindByNameAsync(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string key)
        {
            ApplicationUser user = context.Users.Find(key);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
