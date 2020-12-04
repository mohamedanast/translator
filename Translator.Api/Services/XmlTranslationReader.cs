using Microsoft.Extensions.Logging;
using System.IO;
using System.Xml.Linq;

namespace Translator.Api.Services
{
    public class XmlTranslationReader : ITranslationReader
    {
        public string ReadResource(string lang, string resourceGroup, string resource, ILogger log, Stream stream)
        {
            XElement translations = XElement.Load(stream);
            var resGroup = translations.Element(resourceGroup);
            if (resGroup != null)
            {
                var res = resGroup.Element(resource);
                if (res != null)
                {
                    // Translation exist for resource.
                    log.LogInformation("Success! Translation exists");
                    return res.Value;
                }
                else
                {
                    log.LogInformation($"The requested resource does not exist for {lang}");
                }
            }
            else
            {
                log.LogInformation($"The requested resource group does not exist for {lang}");
            }
            return null;
        }
    }
}
