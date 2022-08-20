using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Domain.Entities;
using GoCode.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoCode.Infrastructure.DataAccess
{
    public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
    {
        public CoursesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Course> GetCoursesWithAll() =>
            _dbContext.Set<Course>()
            .Include(c => c.Questions)
            .ThenInclude(q => q.Answers)
            .Include(c => c.UserCourses);

        public IEnumerable<Course> GetCoursesWithAll(Expression<Func<Course, bool>> predicate) =>
            _dbContext.Set<Course>()
            .Where(predicate)
            .Include(c => c.Questions)
            .ThenInclude(q => q.Answers)
            .Include(c => c.UserCourses);
    }
}
