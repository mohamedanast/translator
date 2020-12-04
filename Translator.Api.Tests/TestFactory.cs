using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Translator.Api.Tests
{
    public class TestFactory
    {
        public static IEnumerable<object[]> KnownData()
        {
            return new List<object[]>
            {
                new object[] { "en", "Common", "OKButtonText", "OK" },
                new object[] { "en", "Common", "CancelButtonText", "Cancel" },
                new object[] { "fi", "Common", "OKButtonText", "FI_OK" },
                new object[] { "fi", "Common", "CancelButtonText", "FI_Cancel" }
            };
        }

        public static IEnumerable<object[]> UndefinedTranslations()
        {
            return new List<object[]>
            {
                new object[] { "en", "Uncommon", "OKButtonText" },
                new object[] { "en", "Uncommon", "OKButtonXXXX" },
                new object[] { "nolang", "Common", "CancelButtonText" }
            };
        }

        public static HttpRequest CreateHttpRequest()
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection();
            return request;
        }

        public static ILogger CreateLogger()
        {
            return new ListLogger();
        }

        public static void LoadEnvironmentVariables()
        {
            using (var file = File.OpenText(@"local.settings.json"))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);

                var variables = jObject
                    .GetValue("Values").Select(p => p)
                    .ToList();

                foreach (JProperty variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
            }
        }
    }
}
