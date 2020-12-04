using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Translator.Api.Tests
{
    public class TranslatorTests
    {
        public TranslatorTests()
        {
            TestFactory.LoadEnvironmentVariables();
        }

        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Translator_should_return_known_string()
        {
            var request = TestFactory.CreateHttpRequest();
            var response = (OkObjectResult)await Translator.Run(request, "en", "Common", "OKButtonText", logger);
            Assert.Equal("OK", response.Value);
        }

        [Theory]
        [MemberData(nameof(TestFactory.KnownData), MemberType = typeof(TestFactory))]
        public async void Translator_should_return_known_string_from_data(string lang, string rg, string resource, string result)
        {
            var request = TestFactory.CreateHttpRequest();
            var response = (OkObjectResult)await Translator.Run(request, lang, rg, resource, logger);
            Assert.Equal(result, response.Value);
        }

        [Theory]
        [MemberData(nameof(TestFactory.UndefinedTranslations), MemberType = typeof(TestFactory))]
        public async void Translator_should_return_known_bad_response_for_undefined(string lang, string rg, string resource)
        {
            var request = TestFactory.CreateHttpRequest();
            var response = await Translator.Run(request, lang, rg, resource, logger);
            Assert.NotEqual(typeof(OkObjectResult), response.GetType());
        }
    }
}
