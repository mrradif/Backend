using Microsoft.AspNetCore.Http;

namespace Shared.Helper.File
{
    public static class FileHelper
    {
        private static readonly string[] _allowedImageFormats = { "image/jpeg", "image/png", "image/gif" };

        public static byte[] ConvertToByteArray(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static bool IsValidFile(IFormFile file)
        {
            if (file == null)
            {
                return true; 
            }

            return _allowedImageFormats.Contains(file.ContentType);
        }
    }
}
