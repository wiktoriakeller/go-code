using GoCode.Domain.Entities;

namespace GoCode.Application.Common.Contracts.DataAccess
{
    public interface IUserCoursesRepository : IRepository<UserCourse>
    {
        Task<UserCourse?> GetUserCourseByCourseIdAndUserId(int courseId, string userId);
    }
}
