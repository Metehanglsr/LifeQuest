namespace LifeQuestAPI.Application.Features.Auth.Commands.Register;

public sealed record RegisterUserCommandResponse
{
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = "Kayıt işlemi başarılı.";
}