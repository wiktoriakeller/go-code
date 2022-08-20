using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.Infrastructure.Persistence
{
    public class ApplicationDbSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDbSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal async Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    await _dbContext.Roles.AddRangeAsync(GetRoles());
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role() { Name = "Admin"}
            };

            return roles;
        }
    }
}
