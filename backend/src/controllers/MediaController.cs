using System.Collections.Generic;
using backend.models.dto.media;
using backend.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _mediaService;

        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPut("upload")]
        public List<CreatedMediaDto> UploadMedia([FromForm]TypeIdUpload typeIdUpload)
        {
            return _mediaService.UploadMedia(typeIdUpload);
        }
    }
}