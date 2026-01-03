using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Abstractions.Gamification;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.UserTasks.Commands.CompleteUserTask;

public sealed class CompleteUserTaskCommandHandler : IRequestHandler<CompleteUserTaskCommandRequest, CompleteUserTaskCommandResponse>
{
    private readonly IUserTaskReadRepository _userTaskReadRepository;
    private readonly IUserTaskWriteRepository _userTaskWriteRepository;
    private readonly IGamificationService _gamificationService;

    public CompleteUserTaskCommandHandler(
        IUserTaskReadRepository userTaskReadRepository,
        IUserTaskWriteRepository userTaskWriteRepository,
        IGamificationService gamificationService)
    {
        _userTaskReadRepository = userTaskReadRepository;
        _userTaskWriteRepository = userTaskWriteRepository;
        _gamificationService = gamificationService;
    }

    public async Task<CompleteUserTaskCommandResponse> Handle(CompleteUserTaskCommandRequest request, CancellationToken cancellationToken)
    {
        var userTask = await _userTaskReadRepository.Table
            .Include(ut => ut.AppTask)
            .FirstOrDefaultAsync(ut => ut.Id == request.UserTaskId, cancellationToken);

        if (userTask == null)
            return new CompleteUserTaskCommandResponse { Success = false, Message = "Görev bulunamadı." };

        if (userTask.AppUserId != request.UserId)
            return new CompleteUserTaskCommandResponse { Success = false, Message = "Bu görev size ait değil." };

        if (userTask.CompletedAt.HasValue)
            return new CompleteUserTaskCommandResponse { Success = false, Message = "Bu görev zaten tamamlanmış." };

        int xpToEarn = userTask.AppTask?.BaseXP ?? 0;

        userTask.CompletedAt = DateTime.UtcNow;
        userTask.Status = AppTaskStatus.Completed;
        userTask.EarnedXp = xpToEarn;

        await _userTaskWriteRepository.SaveAsync();

        await _gamificationService.AddExperienceAsync(request.UserId, xpToEarn, userTask.AppTask?.CategoryId);

        return new CompleteUserTaskCommandResponse
        {
            Success = true,
            Message = "Görev başarıyla tamamlandı.",
            EarnedXp = xpToEarn
        };
    }
}