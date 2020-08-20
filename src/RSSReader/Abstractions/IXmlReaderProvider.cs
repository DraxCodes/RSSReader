using System.Xml;

namespace RSSReader.Abstractions
{
    public interface IXmlReaderProvider
    {
        XmlReader Create(string absoluteUri);
    }
}