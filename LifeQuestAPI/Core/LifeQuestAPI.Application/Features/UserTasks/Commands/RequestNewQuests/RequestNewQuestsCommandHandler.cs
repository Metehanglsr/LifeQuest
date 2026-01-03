using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Abstractions.DailyQuest;
using MediatR;

namespace LifeQuestAPI.Application.Features.UserTasks.Commands.RequestNewQuests;
public class RequestNewQuestsCommandHandler : IRequestHandler<RequestNewQuestsCommandRequest, RequestNewQuestsCommandResponse>
{
    private readonly IDailyQuestService _dailyQuestService;

    public RequestNewQuestsCommandHandler(IDailyQuestService dailyQuestService)
    {
        _dailyQuestService = dailyQuestService;
    }

    public async Task<RequestNewQuestsCommandResponse> Handle(RequestNewQuestsCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var taskIds = await _dailyQuestService.AssignQuestsToUserAsync(request.UserId);
            return new RequestNewQuestsCommandResponse
            {
                Success = true,
                Message = "Yeni görevler atandı!",
                AssignedCount = taskIds.Count
            };
        }
        catch (Exception ex)
        {
            return new RequestNewQuestsCommandResponse
            {
                Success = false,
                Message = ex.Message,
                AssignedCount = 0
            };
        }
    }
}