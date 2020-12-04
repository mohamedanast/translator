using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.Api.Services
{
    public class TranslatorFactory
    {
        public IBlobReader GetBlobReader()
        {
            return new BlobReader();
        }

        public ITranslationReader GetTranslationReader()
        {
            return new XmlTranslationReader();
        }
    }
}
