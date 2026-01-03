using LifeQuestAPI.Application.DTOs;

namespace LifeQuestAPI.Application.Features.UserBadges.Queries.GetBadgeGallery;

public class GetBadgeGalleryQueryResponse
{
    public List<BadgeGalleryDto> Badges { get; set; } = new();
}
