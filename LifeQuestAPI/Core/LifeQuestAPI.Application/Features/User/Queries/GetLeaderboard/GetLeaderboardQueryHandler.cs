using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.User.Queries.GetLeaderboard;

public sealed class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQueryRequest, List<GetLeaderboardQueryResponse>>
{
    private readonly IAppUserReadRepository _userReadRepository;

    public GetLeaderboardQueryHandler(IAppUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<List<GetLeaderboardQueryResponse>> Handle(GetLeaderboardQueryRequest request, CancellationToken cancellationToken)
    {
        var topUsers = await _userReadRepository.GetAll(tracking: false)
                                                .OrderByDescending(u => u.TotalXP)
                                                .Take(10)
                                                .ToListAsync(cancellationToken);

        return topUsers.Select((user, index) => new GetLeaderboardQueryResponse
        {
            Rank = index + 1,
            UserName = user.UserName,
            ProfileImage = "default.png",
            TotalXP = user.TotalXP,
            GeneralLevel = user.GeneralLevel
        }).ToList();
    }
}
