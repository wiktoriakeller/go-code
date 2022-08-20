using GoCode.Domain.Entities;
using System.Linq.Expressions;

namespace GoCode.Application.Common.Contracts.DataAccess
{
    public interface ICoursesRepository : IRepository<Course>
    {
        IEnumerable<Course> GetCoursesWithAll();

        IEnumerable<Course> GetCoursesWithAll(Expression<Func<Course, bool>> predicate);
    }
}
