using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ASPNET5Demo1.Controllers
{
    public class ErrorController : ControllerBase
    {
        private readonly IHostEnvironment _webHostingEnvironment;
        public ErrorController(IHostEnvironment webHostingEnvironment)
        {
            this._webHostingEnvironment = webHostingEnvironment;
        }

        [HttpGet("/error")]
        public ActionResult Error(/*[FromServices]IHostEnvironment webHostingEnvironment*/)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;
            var isDev = _webHostingEnvironment.IsDevelopment();
            var problemDetail = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = isDev?$"{ex?.GetType().Name}:{ex?.Message}":"An error occured.",
                Detail = isDev?ex?.StackTrace:null,
            };

            return StatusCode(problemDetail.Status.Value,problemDetail);
        }
    }
}