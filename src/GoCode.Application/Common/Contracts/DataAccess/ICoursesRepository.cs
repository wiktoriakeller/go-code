using GoCode.Domain.Entities;
using System.Linq.Expressions;

namespace GoCode.Application.Common.Contracts.DataAccess
{
    public interface ICoursesRepository : IRepository<Course>
    {
        IEnumerable<Course> GetCoursesWithAllIncluded();

        IEnumerable<Course> GetCoursesWithAllIncluded(Expression<Func<Course, bool>> predicate);
    }
}
