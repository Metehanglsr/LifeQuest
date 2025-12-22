namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}