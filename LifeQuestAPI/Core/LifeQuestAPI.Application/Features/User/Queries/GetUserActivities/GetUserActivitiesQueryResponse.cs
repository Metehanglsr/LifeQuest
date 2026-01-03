using LifeQuestAPI.Application.DTOs;

namespace LifeQuestAPI.Application.Features.User.Queries.GetUserActivities;

public class GetUserActivitiesQueryResponse
{
    public List<ActivityDto> Activities { get; set; } = new();
}
