using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace AppSetCertWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly SecretClient _secretClient;

    public Worker(ILogger<Worker> logger, SecretClient secretClient)
    {
        _logger = logger;
        _secretClient = secretClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            try
            {
                KeyVaultSecret secret = await _secretClient.GetSecretAsync("myTestSecret1"); // Replace 'myTestSecret1' with your secret name
                Console.WriteLine($"Secret value is: {secret.Value}");
            }
            catch (AuthenticationFailedException e)
            {
                Console.WriteLine($"[ERROR] Authentication Failed. {e.Message}");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
