using System;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using System.Linq;

namespace Serilog.Enrichers.Claim
{
    internal class ClaimEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string[] _claimNames;

        public ClaimEnricher(IHttpContextAccessor httpContextAccessor, string claimName)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimNames = new [] {claimName};
        }
        public ClaimEnricher(IHttpContextAccessor httpContextAccessor, string[] claimNames)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimNames = claimNames;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated) return;
            var claims = user.Identities.First().Claims;
            var claim = claims
                .FirstOrDefault(c => 
                    _claimNames.Any(_ => 
                        string.Equals(_, c.Type, StringComparison.CurrentCultureIgnoreCase)));

            if (claim == null) return;
            var userIdProperty = propertyFactory.CreateProperty(claim.Type, claim.Value);
            logEvent.AddPropertyIfAbsent(userIdProperty);
        }
    }
}
