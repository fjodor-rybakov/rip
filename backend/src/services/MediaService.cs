using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using backend.core.connectors;
using backend.helper;
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
            try
            {
                if (typeIdUpload.UserId != null)
                {
                    var user = _db.Users.FirstOrDefault(u => u.Id == typeIdUpload.UserId);
                    if (user == null)
                    {
                        throw _apiErrors.UserNotFount;
                    }

                    var avatar = typeIdUpload.Files.FirstOrDefault();
                    var newFileName = UploadFile(avatar);
                    user.Avatar = newFileName;
                    _db.SaveChanges();
                    return new List<CreatedMediaDto> {new CreatedMediaDto {FileName = newFileName}};
                }

                if (typeIdUpload.NewsId != null)
                {
                    return typeIdUpload.Files.Select(UploadFile)
                        .Select(newFileName => new CreatedMediaDto {FileName = newFileName}).ToList();
                }

                return new List<CreatedMediaDto>();
            }
            catch (Exception e)
            {
                throw _apiErrors.FailedUploadFile;
            }
        }

        private string UploadFile(IFormFile file)
        {
            const string path = @"../static-server/static/user-avatars/";
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