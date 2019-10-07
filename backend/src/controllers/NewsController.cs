using System.Collections.Generic;
using backend.models.dto.news;
using backend.services;
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
        public ActionResult CreateNews([FromBody] CreateNewsDto createUserDto)
        {
            return new OkObjectResult(new { CreatedId = _newsService.CreateNews(createUserDto) });
        }

        [HttpPut(":id")]
        public ActionResult UpdateNews(int id, [FromBody] UpdatedNewsDto updatedNewsDto)
        {
            return new OkObjectResult(new { UpdatedId = _newsService.UpdateNews(id, updatedNewsDto) });
        }
        
        [HttpDelete(":id")]
        public ActionResult DeleteNews(int id)
        {
            _newsService.DeleteNews(id);
            return new OkObjectResult(new { Message = "Новость успешно удалена" });
        }
    }
}