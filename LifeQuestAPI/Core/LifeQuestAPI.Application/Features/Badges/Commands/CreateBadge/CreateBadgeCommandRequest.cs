using MediatR;

namespace LifeQuestAPI.Application.Features.Badges.Commands.CreateBadge;

public sealed record CreateBadgeCommandRequest : IRequest<CreateBadgeCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public int RequiredLevel { get; set; }
    public Guid? CategoryId { get; set; }
}
