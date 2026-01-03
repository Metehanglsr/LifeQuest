using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LifeQuestAPI.Application.DTOs;
using LifeQuestAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserActivities;

public sealed class GetUserActivitiesQueryHandler : IRequestHandler<GetUserActivitiesQueryRequest, GetUserActivitiesQueryResponse>
{
    private readonly IUserTaskReadRepository _userTaskReadRepository;
    private readonly IUserBadgeReadRepository _userBadgeReadRepository;

    public GetUserActivitiesQueryHandler(IUserTaskReadRepository userTaskReadRepository, IUserBadgeReadRepository userBadgeReadRepository)
    {
        _userTaskReadRepository = userTaskReadRepository;
        _userBadgeReadRepository = userBadgeReadRepository;
    }

    public async Task<GetUserActivitiesQueryResponse> Handle(GetUserActivitiesQueryRequest request, CancellationToken cancellationToken)
    {
        var tasksData = await _userTaskReadRepository
            .GetWhere(ut => ut.AppUserId == request.UserId && ut.CompletedAt.HasValue, tracking: false)
            .OrderByDescending(ut => ut.CompletedAt)
            .Take(5)
            .Select(ut => new
            {
                ut.Id,
                Title = ut.AppTask.Title,
                ut.EarnedXp,
                OriginalDate = ut.CompletedAt!.Value
            })
            .ToListAsync(cancellationToken);

        var badgesData = await _userBadgeReadRepository
            .GetWhere(ub => ub.AppUserId == request.UserId, tracking: false)
            .OrderByDescending(ub => ub.EarnedAt)
            .Take(5)
            .Select(ub => new
            {
                ub.Id,
                Name = ub.Badge.Name,
                OriginalDate = ub.EarnedAt
            })
            .ToListAsync(cancellationToken);

        var taskDtos = tasksData.Select(t => {
            var trTime = t.OriginalDate.AddHours(3);
            var cleanTrTime = DateTime.SpecifyKind(trTime, DateTimeKind.Unspecified);

            return new ActivityDto
            {
                Id = t.Id,
                Type = "TASK",
                Text = $"\"{t.Title}\" görevini tamamladın",
                Xp = $"+{t.EarnedXp} XP",
                Time = cleanTrTime.ToString("dd MMM HH:mm"),
                RawDate = cleanTrTime
            };
        });

        var badgeDtos = badgesData.Select(b => {
            var trTime = b.OriginalDate.AddHours(3);
            var cleanTrTime = DateTime.SpecifyKind(trTime, DateTimeKind.Unspecified);

            return new ActivityDto
            {
                Id = b.Id,
                Type = "BADGE",
                Text = $"\"{b.Name}\" rozetini kazandın!",
                Xp = "",
                Time = cleanTrTime.ToString("dd MMM HH:mm"),
                RawDate = cleanTrTime
            };
        });

        var allActivities = taskDtos
            .Concat(badgeDtos)
            .OrderByDescending(x => x.RawDate)
            .Take(5)
            .ToList();

        return new GetUserActivitiesQueryResponse
        {
            Activities = allActivities
        };
    }
}