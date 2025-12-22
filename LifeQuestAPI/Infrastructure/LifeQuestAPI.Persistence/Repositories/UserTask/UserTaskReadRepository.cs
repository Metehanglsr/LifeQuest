using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Entities.Identity;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class UserTaskReadRepository : ReadRepository<UserTask>, IUserTaskReadRepository
{
    public UserTaskReadRepository(LifeQuestDbContext context) : base(context)
    {
    }
}