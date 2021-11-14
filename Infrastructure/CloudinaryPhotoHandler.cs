using System;
using System.IO;
using System.Net;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class CloudinaryPhotoHandler: ICloudPhotoHandler
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryPhotoHandler> _logger;

        public CloudinaryPhotoHandler(Cloudinary cloudinary, ILogger<CloudinaryPhotoHandler> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }
        public string Upload(Stream readStream)
        {
            var uploadConfig = new ImageUploadParams()
            {
                File = new FileDescription("pic", readStream),
                Tags = "grade-book-backend-api"
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