using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserTaskWriteRepository : WriteRepository<UserTask>, IUserTaskWriteRepository
{
    public UserTaskWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}