using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace azure_keyvault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((ctx, builder) => {
                        IConfiguration config = builder.Build();

                        // builder.AddAzureKeyVault(
                        //     config["Azure:KeyVault:Endpoint"],
                        //     config["Azure:KeyVault:ClientID"],
                        //     config["Azure:KeyVault:ClientSecret"]
                        // );

                        builder.AddAzureKeyVault(
                            config["Azure:KeyVault:Endpoint"], 
                            new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback)), 
                            new DefaultKeyVaultSecretManager()
                        );
                   })
                   .UseStartup<Startup>();
    }
}
