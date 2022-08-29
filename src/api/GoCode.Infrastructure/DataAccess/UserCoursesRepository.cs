using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Domain.Entities;
using GoCode.Infrastructure.Persistence;

namespace GoCode.Infrastructure.DataAccess
{
    public class UserCoursesRepository : BaseRepository<UserCourse>, IUserCoursesRepository
    {
        public UserCoursesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
