namespace LifeQuestAPI.Application.Features.Tasks.Commands.CompleteTask;

public sealed record CompleteTaskCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int NewTotalXP { get; set; }
    public double NewLevel { get; set; }
}