using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Domain.Entities;
using GoCode.Infrastructure.Persistence;

namespace GoCode.Infrastructure.DataAccess
{
    public class QuestionsRepository : BaseRepository<Question>, IQuestionsRepository
    {
        public QuestionsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
