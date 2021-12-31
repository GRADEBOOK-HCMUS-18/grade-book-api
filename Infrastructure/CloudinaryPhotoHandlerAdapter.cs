using System.Collections.Generic;
using System.IO;
using System.Net;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class CloudinaryPhotoHandlerAdapter : ICloudPhotoHandler
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryPhotoHandlerAdapter> _logger;

        public CloudinaryPhotoHandlerAdapter(Cloudinary cloudinary, ILogger<CloudinaryPhotoHandlerAdapter> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public string Upload(Stream readStream)
        {
            var uploadConfig = new ImageUploadParams
            {
                File = new FileDescription("pic", readStream),
                Tags = "grade-book-backend-api",
                EagerTransforms = new List<Transformation>
                {
                    new EagerTransformation().Width(250).Height(250).Crop("scale")
                }
            };
            var result = _cloudinary.Upload(uploadConfig);
            _logger.LogCritical(result.StatusCode.ToString());

            return result.StatusCode switch
            {
                HttpStatusCode.OK => result.SecureUrl.AbsoluteUri,
                _ => ""
            };
        }
    }
}