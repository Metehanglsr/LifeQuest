using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities;
using MediatR;

namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateBulkCategories;

public sealed class CreateBulkCategoriesCommandHandler : IRequestHandler<CreateBulkCategoriesCommandRequest, CreateBulkCategoriesCommandResponse>
{
    private readonly ICategoryWriteRepository _categoryWriteRepository;

    public CreateBulkCategoriesCommandHandler(ICategoryWriteRepository categoryWriteRepository)
    {
        _categoryWriteRepository = categoryWriteRepository;
    }

    public async Task<CreateBulkCategoriesCommandResponse> Handle(CreateBulkCategoriesCommandRequest request, CancellationToken cancellationToken)
    {
        var categoriesToAdd = new List<Category>();

        foreach (var dto in request.Categories)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("HATA: Kategori adı boş olamaz.");

            categoriesToAdd.Add(new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                IconPath = string.IsNullOrEmpty(dto.IconPath) ? "default_category.png" : dto.IconPath,
            });
        }

        await _categoryWriteRepository.AddRangeAsync(categoriesToAdd);
        await _categoryWriteRepository.SaveAsync();

        return new CreateBulkCategoriesCommandResponse
        {
            IsSuccess = true,
            Message = $"{categoriesToAdd.Count} adet kategori başarıyla oluşturuldu.",
            AddedCount = categoriesToAdd.Count
        };
    }
}