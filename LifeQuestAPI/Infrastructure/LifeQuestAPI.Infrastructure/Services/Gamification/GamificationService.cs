using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Abstractions.Gamification;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Infrastructure.Services.Gamification;

public sealed class GamificationService : IGamificationService
{
    private readonly IAppUserReadRepository _userReadRepository;
    private readonly IAppUserWriteRepository _userWriteRepository;

    private readonly IBadgeReadRepository _badgeReadRepository;
    private readonly IUserBadgeReadRepository _userBadgeReadRepository;
    private readonly IUserBadgeWriteRepository _userBadgeWriteRepository;

    public GamificationService(
        IAppUserReadRepository userReadRepository,
        IAppUserWriteRepository userWriteRepository,
        IBadgeReadRepository badgeReadRepository,
        IUserBadgeReadRepository userBadgeReadRepository,
        IUserBadgeWriteRepository userBadgeWriteRepository)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _badgeReadRepository = badgeReadRepository;
        _userBadgeReadRepository = userBadgeReadRepository;
        _userBadgeWriteRepository = userBadgeWriteRepository;
    }

    public async Task<AppUser> AddExperienceAsync(Guid userId, int xpAmount)
    {
        var user = await _userReadRepository.GetByIdAsync(userId.ToString(), tracking: true);

        if (user == null) throw new Exception("Kullanıcı bulunamadı!");

        user.TotalXP += xpAmount;
        double newLevel = 1 + (user.TotalXP / 100.0);
        user.GeneralLevel = newLevel;

        _userWriteRepository.Update(user);
        await _userWriteRepository.SaveAsync();

        await CheckAndAwardBadges(user.Id, (int)user.GeneralLevel);

        return user;
    }

    private async Task CheckAndAwardBadges(Guid userId, int currentLevel)
    {
        var eligibleBadges = await _badgeReadRepository
            .GetWhere(b => b.RequiredLevel <= currentLevel, tracking: false)
            .ToListAsync();

        var ownedBadges = await _userBadgeReadRepository
            .GetWhere(ub => ub.AppUserId == userId, tracking: false)
            .ToListAsync();

        var ownedBadgeIds = ownedBadges.Select(ub => ub.BadgeId).ToList();

        foreach (var badge in eligibleBadges)
        {
            if (!ownedBadgeIds.Contains(badge.Id))
            {
                var newUserBadge = new UserBadge
                {
                    AppUserId = userId,
                    BadgeId = badge.Id,
                    EarnedAt = DateTime.UtcNow
                };

                await _userBadgeWriteRepository.AddAsync(newUserBadge);
            }
        }

        await _userBadgeWriteRepository.SaveAsync();
    }
}