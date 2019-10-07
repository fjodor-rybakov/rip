using System.Collections.Generic;
using backend.models.dto.comment;
using backend.services;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpGet(":newsId")]
        public ActionResult<List<NewsCommentListDto>> GetCommentList(int newsId)
        {
            return new OkObjectResult(_commentService.GetCommentList(newsId));
        }

        [HttpPost]
        public ActionResult CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            return new OkObjectResult(new { CreatedId = _commentService.CreateComment(createCommentDto) });
        }

        [HttpPut(":id")]
        public ActionResult UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            return new OkObjectResult(new { UpdatedId = _commentService.UpdateComment(id, updateCommentDto) });
        }
        
        [HttpDelete(":id")]
        public ActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return new OkObjectResult(new { Message = "Комментарий успешно удален" });
        }
    }
}