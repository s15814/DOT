﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using BoardGameApp.Models;
using Owin;

[assembly: OwinStartup(typeof(BoardGameApp.App_Start.Startup))]

namespace BoardGameApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Aby uzyskać więcej informacji o sposobie konfigurowania aplikacji, odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists(Notifications.ROLE_ADMIN))
            {
                var role = new IdentityRole
                {
                    Name = Notifications.ROLE_ADMIN
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(Notifications.ROLE_USER))
            {
                var role = new IdentityRole
                {
                    Name = Notifications.ROLE_USER
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(Notifications.ROLE_EMPLOYEE))
            {
                var role = new IdentityRole
                {
                    Name = Notifications.ROLE_EMPLOYEE
                };
                roleManager.Create(role);
            }
        }
    }
}