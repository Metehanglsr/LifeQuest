namespace LifeQuestAPI.Application.Features.Badges.Commands.CreateBulkBadges;

public sealed record CreateBulkBadgesCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int AddedCount { get; set; }
}