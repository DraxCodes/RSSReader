using RSSReader.Abstractions;
using System.IO;
using System.Xml;

namespace RSSReader.Tests.MockXml
{
    internal class MockXmlReaderProvderNullUrl : IXmlReaderProvider
    {
        public XmlReader Create(string absoluteUri)
        {
            TextReader textReader = new StreamReader("./MockXml/Documents/MockXmlNullUrl.xml");
            var result = XmlReader.Create(textReader);
            return result;
        }
    }
}