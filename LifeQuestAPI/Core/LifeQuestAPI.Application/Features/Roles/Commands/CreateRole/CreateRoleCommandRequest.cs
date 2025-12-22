using MediatR;

namespace LifeQuestAPI.Application.Features.Roles.Commands.CreateRole;

public sealed record CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
{
    public string RoleName { get; set; } = string.Empty;
}
