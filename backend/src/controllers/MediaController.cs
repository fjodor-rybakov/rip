using System.Collections.Generic;
using backend.helper.enums;
using backend.models.assets;
using backend.models.dto.media;
using backend.services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<List<CreatedMediaDto>> UploadMedia([FromForm]TypeIdUpload typeIdUpload)
        {
            return new OkObjectResult(_mediaService.UploadMedia(typeIdUpload));
        }

        [HttpDelete("{fileName}/{eTypeUpload}")]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult DeleteFile(string fileName, ETypeUpload eTypeUpload)
        {
            _mediaService.DeleteFile(fileName, eTypeUpload);
            return new OkObjectResult(new
            {
                Message = "Фотография успешно удалена"
            });
        }
    }
}