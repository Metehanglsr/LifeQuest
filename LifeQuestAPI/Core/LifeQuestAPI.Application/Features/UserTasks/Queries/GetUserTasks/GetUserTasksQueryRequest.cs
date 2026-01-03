using MediatR;

namespace LifeQuestAPI.Application.Features.UserTasks.Queries.GetUserTasks;

public class GetUserTasksQueryRequest : IRequest<GetUserTasksQueryResponse>
{
    public Guid UserId { get; set; }
    public Guid? CategoryId { get; set; }
}
