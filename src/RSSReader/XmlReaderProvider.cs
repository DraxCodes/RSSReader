using RSSReader.Abstractions;
using System.Xml;

namespace RSSReader
{
    public class XmlReaderProvider : IXmlReaderProvider
    {
        public XmlReader Create(string absoluteUri)
        {
            return XmlReader.Create(absoluteUri);
        }
    }
}
