using System;
using backend.core.connectors;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult GetNews()
        {
            return new OkObjectResult(new { Message = "Список новостей" });
        }
    }
}