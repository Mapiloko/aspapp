using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Data;
using Microsoft.AspNetCore.Identity;

namespace aspapp.AppBuilder
{
    public static class GlobalOps
    {
        public static async Task CreateApplicationAdministrator(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<RBSAuthDbContext>();

            context.Database.EnsureCreated();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleName = "Administrator";
            List<string> roles = new List<string>(new string[] { "Administrator", "Manager", "Employee" });

            var userManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var config = serviceProvider
                .GetRequiredService<IConfiguration>();

            foreach (string s in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(s);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager
                        .CreateAsync(new IdentityRole(s));
                    if (result.Succeeded && s == "Administrator")
                    {
                        var admin = await userManager
                            .FindByEmailAsync(config["AdminCredentials:Email"]);

                        if (admin == null)
                        {
                            admin = new IdentityUser()
                            {
                                UserName = config["AdminCredentials:Email"],
                                Email = config["AdminCredentials:Email"],
                                EmailConfirmed = true
                            };
                            result = await userManager
                                .CreateAsync(admin, config["AdminCredentials:Password"]);
                            if (result.Succeeded)
                            {
                                result = await userManager
                                    .AddToRoleAsync(admin, roleName);
                            }
                        }
                    }
                }
            }
        }
    }
}