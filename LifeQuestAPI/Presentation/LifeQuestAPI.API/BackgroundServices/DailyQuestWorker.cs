using LifeQuestAPI.Application.Abstractions.DailyQuest;

namespace LifeQuestAPI.API.BackgroundServices;

public class DailyQuestWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DailyQuestWorker> _logger;
    private readonly TimeSpan _period = TimeSpan.FromHours(24);

    public DailyQuestWorker(IServiceProvider serviceProvider, ILogger<DailyQuestWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker Başlatıldı.");

        using PeriodicTimer timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dailyQuestService = scope.ServiceProvider.GetRequiredService<IDailyQuestService>();

                    await dailyQuestService.DistributeDailyQuestsAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker hata aldı!");
            }
        }
    }
}