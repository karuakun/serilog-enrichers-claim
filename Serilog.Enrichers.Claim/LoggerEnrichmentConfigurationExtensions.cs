using Microsoft.AspNetCore.Http;
using Serilog.Configuration;
using System;

namespace Serilog.Enrichers.Claim
{
    public static class LoggerEnrichmentConfigurationExtensions
    {
        public static LoggerConfiguration WithClaim(
            this LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
            IHttpContextAccessor httpContextAccessor, 
            params string[] claimName)
        {
            if (loggerEnrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(loggerEnrichmentConfiguration));
            if (httpContextAccessor == null)
                throw new ArgumentNullException(nameof(httpContextAccessor));
            if (claimName == null)
                throw new ArgumentNullException(nameof(claimName));

            return loggerEnrichmentConfiguration.With(new ClaimEnricher(httpContextAccessor, claimName));
        }
    }
}
