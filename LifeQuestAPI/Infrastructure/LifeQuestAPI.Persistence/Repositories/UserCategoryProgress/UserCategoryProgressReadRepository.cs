using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserCategoryProgressReadRepository : ReadRepository<UserCategoryProgress>, IUserCategoryProgressReadRepository
{
    public UserCategoryProgressReadRepository(LifeQuestDbContext context) : base(context)
    {
    }
}