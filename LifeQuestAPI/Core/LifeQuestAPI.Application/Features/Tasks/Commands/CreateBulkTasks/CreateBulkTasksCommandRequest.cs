using LifeQuestAPI.Application.DTOs;
using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateBulkTasks;

public sealed record CreateBulkTasksCommandRequest : IRequest<CreateBulkTasksCommandResponse>
{
    public List<CreateTaskDto> Tasks { get; set; } = default!;
}