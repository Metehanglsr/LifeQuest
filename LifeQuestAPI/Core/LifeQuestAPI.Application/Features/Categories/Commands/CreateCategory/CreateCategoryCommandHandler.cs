using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using MediatR;

namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
{
    private readonly ICategoryWriteRepository _categoryWriteRepository;

    public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository)
    {
        _categoryWriteRepository = categoryWriteRepository;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var newCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IconPath = string.IsNullOrEmpty(request.IconPath) ? "default_category.png" : request.IconPath,
        };

        await _categoryWriteRepository.AddAsync(newCategory);
        await _categoryWriteRepository.SaveAsync();

        return new CreateCategoryCommandResponse
        {
            IsSuccess = true,
            Message = $"'{newCategory.Name}' kategorisi başarıyla oluşturuldu.",
            CategoryId = newCategory.Id
        };
    }
}