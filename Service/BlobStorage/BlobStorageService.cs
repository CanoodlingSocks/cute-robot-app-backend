using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly ILogger<BlobStorageService> _logger;

        public BlobStorageService(string connectionString, ILogger<BlobStorageService> logger)
        {
            _logger = logger;
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient("ChubbyBlobs");
        }
        
        public async Task<IEnumerable<string>> GetFoldersAsync()
        {
            List<string> folderNames = new List<string>();

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("ChubbyBlobs");

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                string blobName = blobItem.Name;
                int delimiterIndex = blobName.IndexOf('/');

                if (delimiterIndex >= 0)
                {
                    string folderName = blobName.Substring(0, delimiterIndex);
                    if (!folderNames.Contains(folderName))
                    {
                        folderNames.Add(folderName);
                    }
                }
            }

            return folderNames;
        }

        public async Task<bool> CheckIfFolderExists(string folderName)
        {
            try
            {
                var existingFolders = await GetFoldersAsync();
                if (existingFolders != null)
                {
                    if (existingFolders.Contains(folderName))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking folder existence: {ex}");
                return false;
            }
        }

        public async Task<IEnumerable<string>> UploadFilesToFolderAsync(string folderName, List<IFormFile> imageFiles)
        {
            var imageUrls = new List<string>();

            try
            {
                foreach (var imageFile in imageFiles)
                {
                    var blobName = $"{folderName}/{imageFile.FileName}";
                    var blockBlobClient = _containerClient.GetBlobClient(blobName);

                    using (Stream stream = imageFile.OpenReadStream())
                    {
                        var blobUploadOptions = new BlobUploadOptions
                        {
                            HttpHeaders = new BlobHttpHeaders
                            {
                                ContentType = "image/png",
                            }
                        };

                        await blockBlobClient.UploadAsync(stream, blobUploadOptions);
                    }

                    imageUrls.Add(blockBlobClient.Uri.ToString());
                }

                return imageUrls;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Uploading Files: {ex}");
                return null;
            }
        }

        public async Task<IEnumerable<BackgroundImageDTO>> GetBackgroundImagesAsync()
        {
            try
            {
                var backgroundImages = await Task.Run(() =>
                {
                    string blobFolder = "Background Images";
                    var allBlobs = _containerClient.GetBlobs(prefix: $"{blobFolder}");

                    var result = new List<BackgroundImageDTO>();

                    foreach (var blob in allBlobs)
                    {
                        string blobName = blob.Name.ToString();
                        var blockBlobClient = _containerClient.GetBlobClient(blobName);

                        result.Add(new BackgroundImageDTO
                        {
                            ImageName = blobName,
                            ImageUrl = blockBlobClient.Uri.ToString(),
                        });
                    }

                    return result;
                });

                return backgroundImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting background images: {ex}");
                return null;
            }
        }

    }
}

