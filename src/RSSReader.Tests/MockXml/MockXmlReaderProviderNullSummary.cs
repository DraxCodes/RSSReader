using RSSReader.Abstractions;
using System.IO;
using System.Xml;

namespace RSSReader.Tests.MockXml
{
    internal class MockXmlReaderProviderNullSummary : IXmlReaderProvider
    {
        public XmlReader Create(string absoluteUri)
        {
            TextReader textReader = new StreamReader("./MockXml/Documents/MockXmlNullSummary.xml");
            var result = XmlReader.Create(textReader);
            return result;
        }
    }
}