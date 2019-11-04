using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using backend.core.connectors;
using backend.helper;
using backend.models.dto.news;
using backend.models.entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace backend.services
{
    public class NewsService
    {
        private readonly ApiErrors _apiErrors;
        private readonly RipDatabase _db;
        
        public NewsService(RipDatabase db, ApiErrors apiErrors)
        {
            _apiErrors = apiErrors;
            _db = db;
        }
        
        public List<NewsListDto> GetNewsList(int userId, bool? onlyMy)
        {
            var query = from newsEntity in _db.News
                join userEntity in _db.Users on newsEntity.UserId equals userEntity.Id
                select new NewsListDto
                {
                    UserId = newsEntity.UserId,
                    Title = newsEntity.Title,
                    Description = newsEntity.Description,
                    PathToImages = newsEntity.PathToImages,
                    CreatedAt = newsEntity.CreatedAt,
                    Nickname = userEntity.Nickname,
                    Avatar = userEntity.Avatar,
                };

            if (onlyMy != null && onlyMy == true)
            {
                query = query.Where(entity => entity.UserId == userId);
            }
            
            return query.ToList();
        }

        public int CreateNews(CreateNewsDto createUserDto)
        {
            var createdNews = new NewsEntity()
            {
                Title = createUserDto.Title, 
                Description = createUserDto.Description, 
                PathToImages = null,
                UserId = createUserDto.UserId
            };
            _db.News.Add(createdNews);
            _db.SaveChanges();
            return createdNews.Id;
        }

        public int UpdateNews(int id, UpdatedNewsDto updatedNewsDto)
        {
            var news = GetNewsEntity(id);
            
            news.Title = updatedNewsDto.Title ?? news.Title;
            news.Description = updatedNewsDto.Description ?? news.Description;
            news.UserId = updatedNewsDto.UserId ?? news.UserId;
            
            _db.News.Update(news);
            _db.SaveChanges();
            return news.Id;
        }

        public void DeleteNews(int id)
        {
            var news = GetNewsEntity(id);
            var path = Environment.GetEnvironmentVariable("PATH_IMAGES_NEWS");
            foreach (var item in news.PathToImages)
            {
                File.Delete(path + item);
            }
            _db.Remove(news);
        }

        private NewsEntity GetNewsEntity(int id)
        {
            var news = _db.News.FirstOrDefault(n => n.Id == id);
            if (news == null)
            {
                throw _apiErrors.NewsNotFound;
            }

            return news;
        }
    }
}