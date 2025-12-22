using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Queries.GetAll;

public sealed record GetAllTasksQueryRequest : IRequest<List<GetAllTasksQueryResponse>>;
