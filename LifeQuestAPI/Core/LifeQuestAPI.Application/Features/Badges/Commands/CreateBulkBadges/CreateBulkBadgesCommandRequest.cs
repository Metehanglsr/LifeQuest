using LifeQuestAPI.Application.DTOs;
using MediatR;

namespace LifeQuestAPI.Application.Features.Badges.Commands.CreateBulkBadges;

public sealed record CreateBulkBadgesCommandRequest : IRequest<CreateBulkBadgesCommandResponse>
{
    public List<CreateBadgeDto> Badges { get; set; } = default!;
}
