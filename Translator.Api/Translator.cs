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
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "translation/{lang}/{resourceGroup}_{resource}")] HttpRequest req,
            string lang, string resourceGroup, string resource,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request. Requested language: {lang}, resource group: {resourceGroup}, resource: {resource}");

            try
            {
                // Load configurations
                string storageConnString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                string langFilesContainerName = Environment.GetEnvironmentVariable("LanaguageFilesContainer");

                // Get the blob reader and get the required translation file.
                TranslatorFactory translatorFactory = new TranslatorFactory();
                IBlobReader blobReader = translatorFactory.GetBlobReader();
                BlobDownloadInfo languageFile = await blobReader.ReadBlobAsync(storageConnString, langFilesContainerName, $"{lang}.xml");
                if (languageFile != null)
                {
                    // Read the requested resource from the translation file
                    ITranslationReader translationReader = translatorFactory.GetTranslationReader();
                    string result = translationReader.ReadResource(lang, resourceGroup, resource, log, languageFile.Content);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return new OkObjectResult(result);
                    }
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
