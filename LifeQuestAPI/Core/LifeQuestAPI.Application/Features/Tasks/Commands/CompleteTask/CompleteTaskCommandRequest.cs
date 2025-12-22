using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CompleteTask;

public sealed record CompleteTaskCommandRequest : IRequest<CompleteTaskCommandResponse>
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
}
