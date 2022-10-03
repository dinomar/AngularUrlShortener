using Microsoft.AspNetCore.Identity;

namespace AngularUrlShortener.Models.EF
{
    public class IdentitySeedData
    {
        private const string _adminEmail = "admin@website.com";
        private const string _adminUsername = "Admin";
        private const string _adminPassword = "Admin123$";

        private const string _adminRole = "Administrator";

        private const string _user1Email = "user1@website.com";
        private const string _user1Username = "User1";
        private const string _user1Password = "Userpass123$";

        public static async Task EnsurePopulated(IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            IdentityUser adminUser = await userManager.FindByEmailAsync(_adminEmail);
            if (adminUser == null)
            {
                if (await roleManager.FindByNameAsync(_adminRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(_adminRole));
                }

                adminUser = new IdentityUser(_adminUsername);
                adminUser.Email = _adminEmail;
                IdentityResult identityResult = await userManager.CreateAsync(adminUser, _adminPassword);
                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, _adminRole);
                }
                else
                {
                    throw new Exception("Failed to create admin user!");
                }
            }

            IdentityUser user1 = await userManager.FindByEmailAsync(_user1Email);
            if (user1 == null)
            {
                user1 = new IdentityUser(_user1Username);
                user1.Email = _user1Email;
                IdentityResult identityResult = await userManager.CreateAsync(user1, _user1Password);
                if (!identityResult.Succeeded)
                {
                    throw new Exception("Failed to create user1!");
                }
            }
        }
    }
}
