namespace LifeQuestAPI.Application.Features.Roles.Commands.CreateRole;

public sealed record CreateRoleCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
}