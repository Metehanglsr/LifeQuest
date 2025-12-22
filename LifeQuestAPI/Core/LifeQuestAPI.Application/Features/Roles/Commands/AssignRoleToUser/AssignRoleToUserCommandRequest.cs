using MediatR;

namespace LifeQuestAPI.Application.Features.Roles.Commands.AssignRoleToUser;

public sealed record AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
