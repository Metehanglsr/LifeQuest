using MediatR;

namespace LifeQuestAPI.Application.Features.User.Queries.GetLeaderboard;

public sealed record GetLeaderboardQueryRequest : IRequest<List<GetLeaderboardQueryResponse>>;
