using Microsoft.AspNetCore.Mvc;

namespace Hahn.Customers.Api.Infrustructure.ActionReuslts
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
