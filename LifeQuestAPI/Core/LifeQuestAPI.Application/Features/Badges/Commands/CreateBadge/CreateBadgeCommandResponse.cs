namespace LifeQuestAPI.Application.Features.Badges.Commands.CreateBadge;

public sealed record CreateBadgeCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid BadgeId { get; set; }
}