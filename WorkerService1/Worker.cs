using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _logFilePath = "C:\\Users\\milmast\\source\\repos\\WorkerService1\\WorkerService1\\bin\\Debug\\net8.0\\PokemonLog.txt";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://localhost:7295")
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var response = await _httpClient.GetAsync("/api/pokemon", stoppingToken);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync(stoppingToken);

                        await File.AppendAllTextAsync(_logFilePath,
                            $"[{DateTime.Now}] Veri çekildi:\n{json}\n\n", stoppingToken);

                        _logger.LogInformation("Veri baþarýyla dosyaya yazýldý: {time}", DateTimeOffset.Now);
                    }
                    else
                    {
                        var errorMsg = $"[{DateTime.Now}] API baþarýsýz: {response.StatusCode}\n";
                        await File.AppendAllTextAsync(_logFilePath, errorMsg, stoppingToken);
                        _logger.LogWarning("API baþarýsýz: {status}", response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    var errorLog = $"[{DateTime.Now}] HATA: {ex.Message}\n";
                    await File.AppendAllTextAsync(_logFilePath, errorLog, stoppingToken);
                    _logger.LogError(ex, "Hata oluþtu.");
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
