using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Domain.Entities;
using GoCode.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoCode.Infrastructure.DataAccess
{
    public class UserCoursesRepository : BaseRepository<UserCourse>, IUserCoursesRepository
    {
        public UserCoursesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserCourse?> GetUserCourseByCourseIdAndUserId(int courseId, string userId) =>
            await _dbContext.Set<UserCourse>().FirstOrDefaultAsync(uc => uc.CourseId == courseId && uc.UserId == userId);
    }
}
