using System.IO;

namespace ApplicationCore.Interfaces
{
    public interface ICloudPhotoHandler
    {
        string Upload(Stream readStream);
    }
}