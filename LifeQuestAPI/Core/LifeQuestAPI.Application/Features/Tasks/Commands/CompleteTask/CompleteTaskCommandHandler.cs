using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Abstractions.Gamification;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Enums;
using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CompleteTask;

public sealed class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommandRequest, CompleteTaskCommandResponse>
{
    private readonly IUserTaskWriteRepository _userTaskWriteRepository;
    private readonly IUserTaskReadRepository _userTaskReadRepository;
    private readonly IAppTaskReadRepository _appTaskReadRepository;
    private readonly IGamificationService _gamificationService;

    public CompleteTaskCommandHandler(
        IGamificationService gamificationService, IUserTaskWriteRepository userTaskWriteRepository, IUserTaskReadRepository userTaskReadRepository, IAppTaskReadRepository appTaskReadRepository)
    {
        _gamificationService = gamificationService;
        _userTaskWriteRepository = userTaskWriteRepository;
        _userTaskReadRepository = userTaskReadRepository;
        _appTaskReadRepository = appTaskReadRepository;
    }

    public async Task<CompleteTaskCommandResponse> Handle(CompleteTaskCommandRequest request, CancellationToken cancellationToken)
    {
        var task = await _appTaskReadRepository.GetByIdAsync(request.TaskId.ToString());
        if (task == null) throw new Exception("Görev bulunamadı!");

        var existingUserTask = await _userTaskReadRepository.GetSingleAsync(
            ut => ut.AppUserId == request.UserId && ut.AppTaskId == request.TaskId,
            tracking: false);

        if (existingUserTask != null)
            return new CompleteTaskCommandResponse
            {
                Success = false,
                Message = "Bu görevi zaten tamamladınız."
            };

        var userTask = new UserTask
        {
            AppUserId = request.UserId,
            AppTaskId = request.TaskId,
            Status = AppTaskStatus.Completed,
            EarnedXp = task.BaseXP,
            CompletedAt = DateTime.UtcNow
        };

        await _userTaskWriteRepository.AddAsync(userTask);
        await _userTaskWriteRepository.SaveAsync();

        var updatedUser = await _gamificationService.AddExperienceAsync(request.UserId, task.BaseXP);

        return new CompleteTaskCommandResponse
        {
            Success = true,
            Message = $"Tebrikler! {task.BaseXP} XP kazandınız.",
            NewTotalXP = updatedUser.TotalXP,
            NewLevel = updatedUser.GeneralLevel
        };
    }
}