using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using backend.core.connectors;
using backend.helper;
using backend.helper.enums;
using backend.models.dto.media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.services
{
    public class MediaService
    {
        private readonly ApiErrors _apiErrors;
        private readonly RipDatabase _db;

        public MediaService(RipDatabase db, ApiErrors apiErrors)
        {
            _apiErrors = apiErrors;
            _db = db;
        }

        public List<CreatedMediaDto> UploadMedia([FromBody] TypeIdUpload typeIdUpload)
        {
            if (typeIdUpload.UserId != null)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == typeIdUpload.UserId)  ?? throw _apiErrors.UserNotFount;
                var avatar = typeIdUpload.Files.FirstOrDefault();
                var newFileName = UploadFile(avatar, ETypeUpload.UserAvatar);
                user.Avatar = newFileName;
                _db.SaveChanges();
                return new List<CreatedMediaDto> {new CreatedMediaDto {FileName = newFileName}};
            }

            if (typeIdUpload.NewsId != null)
            {
                var news = _db.News.FirstOrDefault(u => u.Id == typeIdUpload.NewsId);
                if (news == null)
                {
                    throw _apiErrors.NewsNotFound;
                }
                var newsFiles = typeIdUpload.Files.Select(file => UploadFile(file, ETypeUpload.NewsImages))
                    .Select(newFileName => new CreatedMediaDto {FileName = newFileName}).ToList();
                news.PathToImages = newsFiles.Select(item => item.FileName).ToList();
                _db.SaveChanges();
                return newsFiles;
            }
            
            return new List<CreatedMediaDto>();
        }

        public void DeleteFile(string fileName, ETypeUpload eTypeUpload)
        {
            var path = "";
            if (eTypeUpload == ETypeUpload.NewsImages)
            {
                path = Environment.GetEnvironmentVariable("PATH_IMAGES_NEWS");
                var news = _db.News.FirstOrDefault(n => n.PathToImages.Contains(fileName)) ?? throw _apiErrors.NewsNotFound;
                var newsImagePosition = news.PathToImages.IndexOf(fileName);
                if (newsImagePosition == -1)
                {
                    throw _apiErrors.FileNotFound;
                }
                news.PathToImages.RemoveAt(newsImagePosition);
            } else if (eTypeUpload == ETypeUpload.UserAvatar)
            {
                path = Environment.GetEnvironmentVariable("PATH_IMAGES_AVATARS");
                var user = _db.Users.FirstOrDefault(entity => entity.Avatar == fileName) ?? throw _apiErrors.UserNotFount;
                user.Avatar = null;
            }
            _db.SaveChanges();
            File.Delete(path + fileName);
        }
        
        private string UploadFile(IFormFile file, ETypeUpload eTypeUpload)
        {
            var path = "";
            if (eTypeUpload == ETypeUpload.NewsImages)
            {
                path = Environment.GetEnvironmentVariable("PATH_IMAGES_NEWS");
            } else if (eTypeUpload == ETypeUpload.UserAvatar)
            {
                path = Environment.GetEnvironmentVariable("PATH_IMAGES_AVATARS");
            }
            if (file == null)
            {
                throw _apiErrors.FileNotFound;
            }

            var newName = $"{Guid.NewGuid().ToString()}.{file.FileName.Split('.').Last()}";
            using (var fileStream = new FileStream(path + newName, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return newName;
        }
    }
}