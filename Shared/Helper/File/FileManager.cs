using Microsoft.AspNetCore.Http;


namespace Shared.Helper.File
{
    public class FileManager
    {
        private readonly string _uploadsDirectory;

        public FileManager(string uploadsDirectory)
        {
            _uploadsDirectory = uploadsDirectory;
        }

        public async Task<string> SaveFileToPhysicalDrive(IFormFile file, string organizationName)
        {
            var orgDirectory = Path.Combine(_uploadsDirectory, organizationName);

            // Check if the organization directory exists, create it if not
            if (!Directory.Exists(orgDirectory))
            {
                Directory.CreateDirectory(orgDirectory);
            }

            // Combine organization directory with file name to get full file path
            var filePath = Path.Combine(orgDirectory, file.FileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
