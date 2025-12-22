namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateBulkCategories;

public sealed record CreateBulkCategoriesCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int AddedCount { get; set; }
}