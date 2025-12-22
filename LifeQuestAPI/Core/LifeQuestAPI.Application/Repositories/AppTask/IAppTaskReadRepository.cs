using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Entities.Identity;

namespace LifeQuestAPI.Application.Repositories;

public interface IAppTaskReadRepository : IReadRepository<AppTask>
{
}