using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.DTOs;
using LifeQuestAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserProfile;

public sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQueryRequest, GetUserProfileQueryResponse>
{
    private readonly IAppUserReadRepository _userReadRepository;
    private readonly IUserBadgeReadRepository _userBadgeReadRepository;

    public GetUserProfileQueryHandler(IAppUserReadRepository userReadRepository, IUserBadgeReadRepository userBadgeReadRepository)
    {
        _userReadRepository = userReadRepository;
        _userBadgeReadRepository = userBadgeReadRepository;
    }

    public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(request.UserId.ToString(), tracking: false);
        if (user == null) throw new Exception("Kullanıcı bulunamadı.");

        var userBadges = await _userBadgeReadRepository
            .GetWhere(ub => ub.AppUserId == request.UserId, tracking: false)
            .Include(ub => ub.Badge)
            .ToListAsync(cancellationToken);

        return new GetUserProfileQueryResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            UserName = user.UserName,
            TotalXP = user.TotalXP,
            GeneralLevel = user.GeneralLevel,
            EarnedBadges = userBadges.Select(ub => new UserBadgeDto
            {
                BadgeName = ub.Badge.Name,
                IconPath = ub.Badge.IconPath,
                Description = ub.Badge.Description,
                EarnedAt = ub.EarnedAt
            }).ToList()
        };
    }
}