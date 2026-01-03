using MediatR;

namespace LifeQuestAPI.Application.Features.UserBadges.Queries.GetBadgeGallery;

public class GetBadgeGalleryQueryRequest : IRequest<GetBadgeGalleryQueryResponse>
{
    public Guid UserId { get; set; }
}
