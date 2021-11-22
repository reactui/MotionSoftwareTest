using Microsoft.AspNetCore.Mvc;
using BO.DTO;
using BO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace API.Controllers
{
    // Main controller route, in real-life scenarios versions can also be added, e.g. /api/v1/calcu...
    [ApiController]
    [Route("api/calculator")]
    public class Calculator : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TaxConfiguration _taxConfiguration;
        private readonly IMemoryCache _memoryCache;

        public Calculator(IConfiguration configuration, IOptionsMonitor<TaxConfiguration> optionsMonitor, IMemoryCache memoryCache)
        {
            _config = configuration;
            _taxConfiguration = optionsMonitor.CurrentValue;
            _memoryCache = memoryCache;
        }

        // The Controller Action to take a TaxPayer (json format) from the client-side and return
        // Object result with validation messages, if any, and the result.        
        // If validation fails, validation error is returned with the Validation Framework (default one in ASP.NET Core)
        [HttpPost]
        [Route("calculate")]
        public IActionResult Calculate([FromBody] TaxPayer taxPayer)
        {
            // Validation through built-in ASP.NET Core validation framework and model state / data annotation attributes
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Taxes taxResult = GetFromCache(taxPayer);
            return Ok(taxResult);
        }

        private Taxes GetFromCache(TaxPayer taxPayer)
        {
            if (!_memoryCache.TryGetValue(taxPayer.GetCustomHash(), out Taxes cachedTaxes))
            {
                Taxes taxResult = new TaxCalculator(taxPayer, _taxConfiguration).CalculateTax();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(6));
                _memoryCache.Set(taxPayer.GetCustomHash(), taxResult, cacheEntryOptions);

                return taxResult;
            }

            return cachedTaxes;
        }

        
    }
}
