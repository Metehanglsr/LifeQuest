using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Enums;
using MediatR;

namespace LifeQuestAPI.Application.Features.Tasks.Commands.CreateBulkTasks;

public sealed class CreateBulkTasksCommandHandler : IRequestHandler<CreateBulkTasksCommandRequest, CreateBulkTasksCommandResponse>
{
    private readonly IAppTaskWriteRepository _taskWriteRepository;

    public CreateBulkTasksCommandHandler(IAppTaskWriteRepository taskWriteRepository)
    {
        _taskWriteRepository = taskWriteRepository;
    }

    public async Task<CreateBulkTasksCommandResponse> Handle(CreateBulkTasksCommandRequest request, CancellationToken cancellationToken)
    {
        var tasks = request.Tasks.Select(t => new AppTask
        {
            Id = Guid.NewGuid(),

            Title = t.Title,
            Description = t.Description,
            CategoryId = t.CategoryId,
            BaseXP = t.BaseXP,
            MinLevel = t.MinLevel,
            Difficulty = (DifficultyLevel)t.Difficulty,
            IsActive = true,
        }).ToList();

        try
        {
            await _taskWriteRepository.AddRangeAsync(tasks);
            await _taskWriteRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            throw new Exception($"Veritabanı Kayıt Hatası: {errorMessage}");
        }

        return new CreateBulkTasksCommandResponse
        {
            IsSuccess = true,
            Message = $"{tasks.Count} adet görev başarıyla eklendi.",
            AddedCount = tasks.Count
        };
    }
}