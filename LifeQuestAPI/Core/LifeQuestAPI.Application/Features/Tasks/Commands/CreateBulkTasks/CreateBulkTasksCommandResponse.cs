namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateBulkTasks;

public sealed record CreateBulkTasksCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int AddedCount { get; set; }
}