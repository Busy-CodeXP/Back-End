using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Buzy.DataAccess;

namespace Buzy
{
    public class Program
    {
         public static void Main(string[] args)
        {
            bool runSeed = false;

            if (args.Contains("seed"))
            {
                runSeed = true;
                args = args.Where(a => a != "seed").ToArray();
            }

            var host = BuildWebHost(args);

            if (runSeed) RunSeed(host);
            else host.Run();
        }

        private static void RunSeed(IWebHost host)
        {
            Console.WriteLine("Running seed...");

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<BusHelperContext>();
                    DbInitializer.DbInitialize(context);
                }
                catch (Exception e)
                {
                    var logger = service.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Ocorreu um erro enquanto os dados foram enviados");
                }
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
