using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace Service.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<string>> GetFoldersAsync();
        public Task<bool> CheckIfFolderExists(string folderName);
        public Task<IEnumerable<string>> UploadFilesToFolderAsync(string folderName, List<IFormFile> imageFiles);
        public Task<IEnumerable<BackgroundImageDTO>> GetBackgroundImagesAsync();
    }
}
