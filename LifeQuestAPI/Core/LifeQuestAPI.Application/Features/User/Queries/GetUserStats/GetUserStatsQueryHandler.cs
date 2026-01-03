using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserStats;

public sealed class GetUserStatsQueryHandler : IRequestHandler<GetUserStatsQueryRequest, GetUserStatsQueryResponse>
{
    private readonly IUserTaskReadRepository _userTaskReadRepository;

    public GetUserStatsQueryHandler(IUserTaskReadRepository userTaskReadRepository)
    {
        _userTaskReadRepository = userTaskReadRepository;
    }

    public async Task<GetUserStatsQueryResponse> Handle(GetUserStatsQueryRequest request, CancellationToken cancellationToken)
    {
        var completedDates = await _userTaskReadRepository
            .GetWhere(ut => ut.AppUserId == request.UserId && ut.CompletedAt.HasValue, tracking: false)
            .Select(ut => ut.CompletedAt!.Value)
            .ToListAsync(cancellationToken);

        var completedDateStrings = completedDates
            .Select(d => d.ToString("yyyy-MM-dd"))
            .ToHashSet();

        var totalCompleted = completedDates.Count;
        var now = DateTime.Now;

        var lastWeekCount = completedDates.Count(d => d >= now.AddDays(-7));
        var previousWeekCount = completedDates.Count(d => d >= now.AddDays(-14) && d < now.AddDays(-7));

        int growth = 0;
        if (previousWeekCount > 0)
            growth = ((lastWeekCount - previousWeekCount) * 100) / previousWeekCount;
        else if (lastWeekCount > 0)
            growth = 100;

        var streakList = new List<bool>();
        for (int i = 6; i >= 0; i--)
        {
            var checkDateStr = now.AddDays(-i).ToString("yyyy-MM-dd");
            streakList.Add(completedDateStrings.Contains(checkDateStr));
        }

        int currentStreak = 0;
        for (int i = streakList.Count - 1; i >= 0; i--)
        {
            if (streakList[i]) currentStreak++;
            else if (i == streakList.Count - 1) continue;
            else break;
        }

        return new GetUserStatsQueryResponse
        {
            TotalCompleted = totalCompleted,
            WeeklyGrowth = growth,
            Streak = streakList,
            StreakCount = currentStreak
        };
    }
}