# Serilog.Enrichers.Claim
Enriches Serilog events with information from the HttpContext.User.Identites.Claim.

# Included enrichers
- WithClaim()

# Usage

``` cs:Startup.cs
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // ...
        }
    }
```

``` cs:Program.cs
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(
                    (context, services, configuration) =>
                    configuration.
                        MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .Enrich.WithClaim(services.GetService<IHttpContextAccessor>(), "sub")
                        .WriteTo.Console(formatter: new JsonFormatter())

                    )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
```
