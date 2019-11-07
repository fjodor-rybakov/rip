using System;
using System.Collections.Generic;
using System.Linq;
using backend.core.connectors;
using backend.helper;
using backend.models.dto.comment;
using backend.models.entities;

namespace backend.services
{
    public class CommentService
    {
        private readonly ApiErrors _apiErrors;
        private readonly RipDatabase _db;
        
        public CommentService(RipDatabase db, ApiErrors apiErrors)
        {
            _apiErrors = apiErrors;
            _db = db;
        }
        
        public List<NewsCommentListDto> GetCommentList()
        {
            return (
                from commentEntity in _db.Comment
                from userEntity in _db.Users.Where(x => commentEntity.UserId == x.Id).DefaultIfEmpty()
                select new NewsCommentListDto
                {
                    Value = commentEntity.Value,
                    CreatedAt = commentEntity.CreatedAt,
                    Avatar = userEntity.Avatar,
                    Nickname = userEntity.Nickname,
                    UserId = userEntity.Id,
                    NewsId = commentEntity.NewsId,
                    Id = commentEntity.Id
                }).ToList();
        }

        public int CreateComment(CreateCommentDto createCommentDto)
        {
            var createdComment = new CommentEntity()
            {
                Value = createCommentDto.Value,
                NewsId = createCommentDto.NewsId,
                UserId = createCommentDto.UserId
            };
            _db.Comment.Add(createdComment);
            _db.SaveChanges();
            return createdComment.Id;
        }

        public int UpdateComment(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = GetCommentEntity(id);

            comment.Value = updateCommentDto.Value ?? comment.Value;
            comment.NewsId = updateCommentDto.NewsId ?? comment.NewsId;
            comment.UserId = updateCommentDto.UserId ?? comment.UserId;
            
            _db.SaveChanges();
            return comment.Id;
        }

        public void DeleteComment(int id)
        {
            var comment = GetCommentEntity(id);
            _db.Remove(comment);
            _db.SaveChanges();
        }

        private CommentEntity GetCommentEntity(int id)
        {
            var comment = _db.Comment.FirstOrDefault(n => n.Id == id);
            if (comment == null)
            {
                throw _apiErrors.CommentNotFound;
            }

            return comment;
        }
    }
}