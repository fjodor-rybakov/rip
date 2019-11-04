using System.Collections.Generic;
using backend.helper;
using backend.models.assets;
using backend.models.dto.news;
using backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<List<NewsListDto>> GetNewsList()
        {
            return new OkObjectResult(_newsService.GetNewsList());
        }

        [HttpPost]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<IdDto> CreateNews([FromBody] CreateNewsDto createUserDto)
        {
            return new OkObjectResult(new { Id = _newsService.CreateNews(createUserDto) });
        }

        [HttpPut(":id")]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<IdDto> UpdateNews(int id, [FromBody] UpdatedNewsDto updatedNewsDto)
        {
            return new OkObjectResult(new { Id = _newsService.UpdateNews(id, updatedNewsDto) });
        }
        
        [HttpDelete(":id")]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult DeleteNews(int id)
        {
            _newsService.DeleteNews(id);
            return new OkObjectResult(new { Message = "Новость успешно удалена" });
        }
    }
}