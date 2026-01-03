using LifeQuestAPI.Application.DTOs;

namespace LifeQuestAPI.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryResponse
{
    public List<CategoryDto> Categories { get; set; } = new();
}
