using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserCategoryProgressWriteRepository : WriteRepository<UserCategoryProgress>, IUserCategoryProgressWriteRepository
{
    public UserCategoryProgressWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}