using System.Collections.Generic;
using backend.helper;
using backend.models.assets;
using backend.models.dto.comment;
using backend.services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<IdDto> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            return new OkObjectResult(new { Id = _commentService.CreateComment(createCommentDto) });
        }

        [HttpPut(":id")]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<IdDto> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            return new OkObjectResult(new { Id = _commentService.UpdateComment(id, updateCommentDto) });
        }
        
        [HttpDelete(":id")]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return new OkObjectResult(new { Message = "Комментарий успешно удален" });
        }
    }
}