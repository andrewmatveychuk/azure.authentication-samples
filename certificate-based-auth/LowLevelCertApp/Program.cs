using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

string keyVaultName = "kv-4zdnwe1wgbwdp"; // The name of the Key Vault you want to access
var keyVaultUri = "https://" + keyVaultName + ".vault.azure.net"; // The Key Vault URI
string tenantId = "3f5ed419-0e1b-4f47-8f94-a5b9fa4f298e"; // Your Azure tenant ID
string clientId = "76a95e90-ec2c-4d59-b92b-9c5b8316cff4"; // Your app registration in the tenant
string certificateThumbprint = "5378d04cd9a86a6cde595478d664cc9e2f755d4b"; // That should be your unique certificate thumbprint

using (X509Store store = new(StoreLocation.LocalMachine))
{
    try
    {
        store.Open(OpenFlags.ReadOnly);

        X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false); // The validOnly parameter is set to 'false' as I'm using a self-signed certificate, which is not trusted by any Trusted Root Certification Authority, in this sample
        if (certificates.Count > 0)
        {
            X509Certificate2 clientCertificate = certificates[0];

            var client = new SecretClient(new Uri(keyVaultUri), new ClientCertificateCredential(tenantId, clientId, clientCertificate));

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
            Console.WriteLine("[ERROR] No client certificate with thumbprint '{0}' was found.", certificateThumbprint);
        };

    }
    catch (CryptographicException)
    {
        Console.WriteLine("[ERROR] No {0}, {1}", store.Name, store.Location);
    }
    finally
    {
        store.Close();
    }
}