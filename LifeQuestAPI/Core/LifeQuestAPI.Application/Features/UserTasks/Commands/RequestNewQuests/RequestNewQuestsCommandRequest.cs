using MediatR;

namespace LifeQuestAPI.Application.Features.UserTasks.Commands.RequestNewQuests;

public class RequestNewQuestsCommandRequest : IRequest<RequestNewQuestsCommandResponse>
{
    public Guid UserId { get; set; }
}
