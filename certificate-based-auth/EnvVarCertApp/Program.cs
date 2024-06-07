using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core.Diagnostics;

var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME"); // Getting the Key Vault name from an environment variable

if (keyVaultName is not null) // Checking if the environment variable is set
{
    Console.WriteLine($"Key Vault name: {keyVaultName}");

    var keyVaultUri = "https://" + keyVaultName + ".vault.azure.net";

    // Use for verbose logging and troubleshooting
    /* using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

    DefaultAzureCredentialOptions options = new()
    {
        Diagnostics =
        {
            LoggedHeaderNames = { "x-ms-request-id" },
            LoggedQueryParameters = { "api-version" },
            IsLoggingContentEnabled = true
        }
    }; */

    var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential()); // Using the DefaultAzureCredential class to authenticate

    try
    {
        KeyVaultSecret secret = await client.GetSecretAsync("myTestSecret1"); // Replace 'myTestSecret1' with your secret name
        Console.WriteLine($"Secret value is: {secret.Value}");
    }
    catch (AuthenticationFailedException e)
    {
        Console.WriteLine($"[ERROR] Authentication Failed. {e.Message}");
    }
}
else
{
    Console.WriteLine("[ERROR] The KEY_VAULT_NAME environment variable is not set.");
}
