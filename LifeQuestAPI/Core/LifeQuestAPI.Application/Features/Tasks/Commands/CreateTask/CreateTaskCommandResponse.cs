namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid TaskId { get; set; }
}