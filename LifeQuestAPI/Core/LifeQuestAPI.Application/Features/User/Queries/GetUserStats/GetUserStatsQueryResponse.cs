namespace LifeQuestAPI.Application.Features.User.Queries.GetUserStats;

public class GetUserStatsQueryResponse
{
    public List<bool> Streak { get; set; } = new();
    public int StreakCount { get; set; }
    public int TotalCompleted { get; set; }
    public int WeeklyGrowth { get; set; }
}
