using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using backend.core.connectors;
using backend.helper;
using backend.models.dto.news;
using backend.models.entities;

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
        
        public List<NewsListDto> GetNewsList()
        {
            return (
                from newsEntity in _db.News
                from userEntity in _db.Users
                select new NewsListDto
                {
                    Title = newsEntity.Title,
                    Description = newsEntity.Description,
                    PathToImages = newsEntity.PathToImages,
                    CreatedAt = newsEntity.CreatedAt,
                    Nickname = userEntity.Nickname,
                    Avatar = userEntity.Avatar,
                }).ToList();
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
            return createdNews.Id;
        }

        public int UpdateNews(int id, UpdatedNewsDto updatedNewsDto)
        {
            var news = GetNewsEntity(id);
            
            news.Title = updatedNewsDto.Title ?? news.Title;
            news.Description = updatedNewsDto.Description ?? news.Description;
            news.UserId = updatedNewsDto.UserId ?? news.UserId;
            
            _db.News.Update(news);
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