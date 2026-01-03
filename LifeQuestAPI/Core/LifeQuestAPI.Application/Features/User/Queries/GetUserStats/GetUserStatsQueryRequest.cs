using MediatR;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserStats;

public class GetUserStatsQueryRequest : IRequest<GetUserStatsQueryResponse>
{
    public Guid UserId { get; set; }
}
