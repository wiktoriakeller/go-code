using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Domain.Entities;
using GoCode.Infrastructure.Persistence;

namespace GoCode.Infrastructure.DataAccess
{
    public class AnswersRepository : BaseRepository<Answear>, IAnswersRepository
    {
        public AnswersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
