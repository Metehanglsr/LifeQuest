using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class BadgeWriteRepository : WriteRepository<Badge>, IBadgeWriteRepository
{
    public BadgeWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}