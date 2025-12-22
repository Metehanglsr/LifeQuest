using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Enums;
using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommandRequest, CreateTaskCommandResponse>
{
    private readonly IAppTaskWriteRepository _taskWriteRepository;

    public CreateTaskCommandHandler(IAppTaskWriteRepository taskWriteRepository)
    {
        _taskWriteRepository = taskWriteRepository;
    }

    public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommandRequest request, CancellationToken cancellationToken)
    {
        var newTask = new AppTask
        {
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            BaseXP = request.BaseXP,
            MinLevel = request.MinLevel,
            Difficulty = (DifficultyLevel)request.Difficulty,
            IsActive = true
        };

        await _taskWriteRepository.AddAsync(newTask);
        await _taskWriteRepository.SaveAsync();

        return new CreateTaskCommandResponse
        {
            IsSuccess = true,
            Message = "Görev başarıyla eklendi.",
            TaskId = newTask.Id
        };
    }
}
