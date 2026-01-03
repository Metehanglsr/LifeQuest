using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuestAPI.Application.Abstractions.DailyQuest;

public interface IDailyQuestService
{
    Task DistributeDailyQuestsAsync();
    Task<List<Guid>> AssignQuestsToUserAsync(Guid userId);
}