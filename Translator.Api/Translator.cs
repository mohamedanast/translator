using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using System.Xml.Linq;
using Azure.Storage.Blobs.Models;
using Translator.Api.Services;
using System.Collections.Generic;
using System.Linq;

namespace Translator.Api
{
    public static class Translator
    {
        [FunctionName("Translator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{lang}/{resourceGroup}_{resource}")] HttpRequest req,
            string lang, string resourceGroup, string resource,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request. Requested language: {lang}, resource group: {resourceGroup}, resource: {resource}");

            try
            {
                string storageConnString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                string langFilesContainerName = Environment.GetEnvironmentVariable("LanaguageFilesContainer");

                IBlobReader blobReader = new BlobReader();
                BlobDownloadInfo languageFile = await blobReader.ReadBlobAsync(storageConnString, langFilesContainerName, $"{lang}.xml");
                if (languageFile != null)
                {
                    XElement translations = XElement.Load(languageFile.Content);
                    var resGroup = translations.Element(resourceGroup);
                    if (resGroup != null)
                    {
                        var res = resGroup.Element(resource);
                        if (res != null)
                        {
                            // Translation exist for resource.
                            log.LogInformation("Success! Translation exists");
                            return new OkObjectResult(res.Value);
                        }
                    }
                    log.LogInformation($"The requested resource group does not exist for {lang}");
                    return new BadRequestObjectResult("Requested resource does not exist");
                }

                log.LogInformation("Translations don't exist for the language");
                return new BadRequestObjectResult("Translations don't exist for the language");
            }
            catch (Exception e)
            {
                log.LogError(e, "Unexpected error");
                return new BadRequestResult();
            }

        }

    }
}
