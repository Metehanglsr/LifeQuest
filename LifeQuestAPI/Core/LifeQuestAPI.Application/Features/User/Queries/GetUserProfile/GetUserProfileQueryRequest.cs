using MediatR;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserProfile;

public sealed record GetUserProfileQueryRequest : IRequest<GetUserProfileQueryResponse>
{
    public Guid UserId { get; set; }
}
