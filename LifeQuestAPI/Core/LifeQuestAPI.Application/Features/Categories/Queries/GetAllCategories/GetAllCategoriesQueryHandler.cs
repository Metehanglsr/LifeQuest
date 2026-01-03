using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.DTOs;
using LifeQuestAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LifeQuestAPI.Application.Features.Categories.Queries.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, GetAllCategoriesQueryResponse>
{
    private readonly ICategoryReadRepository _categoryReadRepository;

    public GetAllCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository)
    {
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = await _categoryReadRepository.GetAll(tracking: false)
            .Include(c => c.Tasks)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IconPath = c.IconPath,
                TaskCount = c.Tasks.Count(t => t.IsActive)
            })
            .ToListAsync(cancellationToken);

        return new GetAllCategoriesQueryResponse
        {
            Categories = categories
        };
    }
}