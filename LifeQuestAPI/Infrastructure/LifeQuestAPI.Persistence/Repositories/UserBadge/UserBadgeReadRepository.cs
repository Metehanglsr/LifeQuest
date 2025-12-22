using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserBadgeReadRepository : ReadRepository<UserBadge>, IUserBadgeReadRepository
{
    public UserBadgeReadRepository(LifeQuestDbContext context) : base(context)
    {
    }
}