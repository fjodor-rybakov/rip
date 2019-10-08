using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace backend.models.dto.media
{
    public class TypeIdUpload
    {
        public int? UserId { get; set; }

        public int? NewsId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}