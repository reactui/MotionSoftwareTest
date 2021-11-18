using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BO.DTO;
using BO;
using System.Net;

namespace API.Controllers
{
    // Main controller route, in real-life scenarios versions can also be added, e.g. /api/v1/calcu...
    [ApiController]
    [Route("api/calculator")]
    public class Calculator : ControllerBase
    {
        public Calculator()
        {
        }

        // The Controller Action to take a TaxPayer (json format) from the client-side and return
        // Object result with validation messages, if any, and the result.
        // If validation fails, result is null
        [HttpPost]
        [Route("calculate")]
        public IActionResult Calculate([FromBody] TaxPayer taxPayer)
        {
            TaxCalculator taxCalculator = new TaxCalculator(taxPayer);

            string validationError = taxCalculator.ValidateTaxes();  // validate          
            Taxes taxes = taxCalculator.CalculateTax();                        

            var result = new
            {
                ValidationMessage = validationError,
                Data = string.IsNullOrEmpty(validationError) ? taxes : null
            };

            // Per RFC directives, failed validation is typically marked as BadRequest
            // https://www.ietf.org/rfc/rfc2616.txt
            HttpStatusCode statusCode = string.IsNullOrEmpty(validationError) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new ObjectResult(result)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
