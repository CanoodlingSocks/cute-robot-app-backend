using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BlobStorage;

namespace project_nebula_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobStorageController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet("Folders")]
        public async Task<IActionResult> GetFolders()
        {
            try
            {
                IEnumerable<string> folderNames = await _blobStorageService.GetFoldersAsync();
                return Ok(folderNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed getting Folders" + ex.Message);
            }
        }

        [HttpPost("CheckIfFolderExists")]
        public async Task<IActionResult> CheckIfFolderExistsAsync([FromForm] string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                return BadRequest("Folder name is required.");
            }

            try
            {
                bool folderExists = await _blobStorageService.CheckIfFolderExists(folderName);

                if (folderExists)
                {
                    return Ok($"Folder '{folderName}' already exists.");
                }
                else
                {
                    return Ok("Folder is available");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed Checking Folder Availability" + ex.Message);
            }
        }


        [HttpPost("UploadImages")]
        public async Task<IActionResult> UploadFilesToFolderAsync([FromForm] string folderName, [FromForm] List<IFormFile> imageFiles)
        {
            if (string.IsNullOrWhiteSpace(folderName) || imageFiles == null || imageFiles.Count == 0)
            {
                return BadRequest("Invalid input parameters");
            }

            try
            {
                var imageUrls = await _blobStorageService.UploadFilesToFolderAsync(folderName, imageFiles);

                if (imageUrls != null)
                {
                    return Ok(imageUrls);
                }
                else
                {
                    return BadRequest("Failed to upload one or more files");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed To Upload Images" + ex.Message);
            }
        }

        [HttpGet("background-images")]
        public async Task<IActionResult> GetBackgroundImages()
        {
            try
            {
                var backgroundImages = await _blobStorageService.GetBackgroundImagesAsync();

                if (backgroundImages != null)
                    return Ok(backgroundImages);
                else
                    return StatusCode(500, "Failed to retrieve background images.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve background images: {ex.Message}");
            }
        }
    }
}