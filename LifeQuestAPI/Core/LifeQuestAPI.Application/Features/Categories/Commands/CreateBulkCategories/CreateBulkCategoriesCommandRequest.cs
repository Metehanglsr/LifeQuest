using LifeQuestAPI.Application.DTOs;
using MediatR;

namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateBulkCategories;

public sealed record CreateBulkCategoriesCommandRequest : IRequest<CreateBulkCategoriesCommandResponse>
{
    public List<CreateCategoryDto> Categories { get; set; } = default!;
}
