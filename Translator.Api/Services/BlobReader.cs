using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Threading.Tasks;

namespace Translator.Api.Services
{
    public class BlobReader: IBlobReader
    {
        public async Task<BlobDownloadInfo> ReadBlobAsync(string storageConnString, string containerName, string blobName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnString);
            BlobContainerClient filesContainer = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = filesContainer.GetBlobClient(blobName);
            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo file = await blobClient.DownloadAsync();
                return file;
            }

            return null;
        }
    }
}
