using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class AppUserReadRepository : ReadRepository<AppUser>, IAppUserReadRepository
{
    public AppUserReadRepository(LifeQuestDbContext context) : base(context)
    {
    }
}