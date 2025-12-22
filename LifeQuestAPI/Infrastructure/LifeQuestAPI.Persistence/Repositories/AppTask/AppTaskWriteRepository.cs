using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class AppTaskWriteRepository : WriteRepository<AppTask>, IAppTaskWriteRepository
{
    public AppTaskWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}