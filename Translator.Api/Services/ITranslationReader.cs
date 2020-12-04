using Microsoft.Extensions.Logging;
using System.IO;

namespace Translator.Api.Services
{
    public interface ITranslationReader
    {
        string ReadResource(string lang, string resourceGroup, string resource, ILogger log, Stream stream);
    }
}
