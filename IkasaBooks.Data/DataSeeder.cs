using IkasaBooks.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Data
{
    public class DataSeeder
    {
        public static async Task SeedUsers(IkasaDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.AppUsers.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                List<AppUser> users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = "Ikasa Team",
                        Email = "ikasa@gmail.com",
                        PhoneNumber = "09043546576",
                        Password = "Password@123",
                        UserName = "ikasa"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = "Sunday Ola",
                        Email = "ola@gmail.com",
                        PhoneNumber = "08066554433",
                        Password = "Password@123",
                        UserName = "sundayola"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = "Adeola Oguns",
                        Email = "adexoguns@gmail.com",
                        PhoneNumber = "08122334455",
                        Password = "Password@123",
                        UserName = "adexoguns"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = "Agnes Ugo",
                        Email = "agnesugo@gmail.com",
                        PhoneNumber = "09023456789",
                        Password = "Password@123",
                        UserName = "agnes"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = "Ik Njoku",
                        Email = "njokuik@gmail.com",
                        PhoneNumber = "08123443322",
                        Password = "Password@123",
                        UserName = "njoku"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Password@123");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                        await userManager.AddToRoleAsync(user, "Regular");
                }
            }
        }
    }
}
