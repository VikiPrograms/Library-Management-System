using BlazorServer.Pages;
using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public static class HelperService
    {
        public static async Task<User> GetLoggedUser(this UserManager<User> userManager, Controller controller)
        {
            //string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(controller.User.Identity.Name))
            {
                throw new ArgumentNullException("User is not logged!");
            }

            string username = controller.User.Identity.Name;
            User user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                string message = "There is no user with that name!";
                userManager.Logger.Log(LogLevel.Error, message);
                throw new ArgumentException(message);
            }

            return user;
        }

    }
}