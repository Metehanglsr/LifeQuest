using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserBadgeWriteRepository : WriteRepository<UserBadge>, IUserBadgeWriteRepository
{
    public UserBadgeWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}