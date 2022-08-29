using GoCode.Infrastructure.Identity;
using GoCode.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace GoCode.Infrastructure.Persistence
{
    public class ApplicationDbSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AdminCredentials _adminCredentials;

        public ApplicationDbSeeder(ApplicationDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<AdminCredentials> adminOptions)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _adminCredentials = adminOptions.Value;
        }

        internal async Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                await AddAdminRole();
                await AddAdminAccount();
            }
        }

        private async Task AddAdminRole()
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");

            if (adminRole is null)
            {
                var role = new Role
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                await _roleManager.CreateAsync(role);
            }
        }

        private async Task AddAdminAccount()
        {
            var adminAccount = await _userManager.FindByEmailAsync(_adminCredentials.Email);

            if (adminAccount is null)
            {
                var admin = new User
                {
                    Email = _adminCredentials.Email,
                    UserName = _adminCredentials.Username,
                };

                await _userManager.CreateAsync(admin, _adminCredentials.Password);
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
