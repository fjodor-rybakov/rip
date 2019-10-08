﻿using System;
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
                var user = _db.Users.FirstOrDefault(u => u.Id == typeIdUpload.UserId);
                if (user == null)
                {
                    throw _apiErrors.UserNotFount;
                }

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
                return typeIdUpload.Files.Select(file => UploadFile(file, ETypeUpload.NewsImages))
                    .Select(newFileName => new CreatedMediaDto {FileName = newFileName}).ToList();
            }
            
            return new List<CreatedMediaDto>();
        }

        private string UploadFile(IFormFile file, ETypeUpload eTypeUpload)
        {
            var path = "";
            if (eTypeUpload == ETypeUpload.NewsImages)
            {
                path = @"../static-server/static/photo-news/";
            } else if (eTypeUpload == ETypeUpload.UserAvatar)
            {
                path = @"../static-server/static/user-avatars/";
            }
            if (file == null)
            {
                throw _apiErrors.FileNotFound;
            }

            var newName = $"{Guid.NewGuid().ToString()}.{file.FileName.Split('.').Last()}";
            file.CopyTo(new FileStream(path + newName, FileMode.Create));
            return newName;
        }
    }
}