using AppSetCertWorker;
using Microsoft.Extensions.Azure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAzureClients(clientBuilder => clientBuilder.AddSecretClient(builder.Configuration.GetSection("KeyVault")));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
