namespace LifeQuestAPI.Application.Features.Roles.Commands.AssignRoleToUser;

public sealed record AssignRoleToUserCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}