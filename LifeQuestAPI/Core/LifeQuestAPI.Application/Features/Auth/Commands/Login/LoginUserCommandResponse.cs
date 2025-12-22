namespace LifeQuestAPI.Application.Features.Auth.Commands.Login;

public sealed record LoginUserCommandResponse
{
    public string Token { get; set; } = string.Empty;
}