using MediatR;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserActivities;

public class GetUserActivitiesQueryRequest : IRequest<GetUserActivitiesQueryResponse>
{
    public Guid UserId { get; set; }
}
