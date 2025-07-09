public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _httpClient;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7295/api/pokemon", stoppingToken);
                var data = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Pokemon verisi alýndý: {data}", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API çaðrýsý sýrasýnda hata oluþtu.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
