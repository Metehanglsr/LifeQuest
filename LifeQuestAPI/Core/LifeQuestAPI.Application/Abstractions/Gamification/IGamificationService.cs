using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Domain.Entities.Identity;

namespace LifeQuestAPI.Application.Abstractions.Gamification;

public interface IGamificationService
{
    Task<AppUser> AddExperienceAsync(Guid userId, int xpAmount, Guid? categoryId = null);
}