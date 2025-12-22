using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommandRequest : IRequest<CreateTaskCommandResponse>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public int BaseXP { get; set; }
    public int MinLevel { get; set; }
    public int Difficulty { get; set; }
}
