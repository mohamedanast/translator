using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Api.Services
{
    interface IBlobReader
    {
        Task<BlobDownloadInfo> ReadBlobAsync(string storageConnString, string containerName, string blobName);
    }
}
