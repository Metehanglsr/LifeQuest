using MediatR;

namespace LifeQuestAPI.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
{
    public string Name { get; set; } = string.Empty;    
    public string Description { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
}
