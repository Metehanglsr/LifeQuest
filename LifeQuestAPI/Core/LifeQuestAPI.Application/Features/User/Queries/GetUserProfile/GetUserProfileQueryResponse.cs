using LifeQuestAPI.Application.DTOs;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserProfile;

public sealed record GetUserProfileQueryResponse
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int TotalXP { get; set; }
    public double GeneralLevel { get; set; }
    public List<UserBadgeDto> EarnedBadges { get; set; } = default!;
}