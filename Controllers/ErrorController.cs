using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ASPNET5Demo1.Controllers
{
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public ActionResult Error([FromServices]IHostingEnvironment webHostingEnvironment)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;
            var isdev = webHostingEnvironment.IsDevelopment();
            var problemDetail = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = isdev?$"{ex.GetType().Name}:{ex.Message}":"An error occured.",
                Detail = isdev?ex.StackTrace:null,
            };

            return StatusCode(problemDetail.Status.Value,problemDetail);
        }
    }
}