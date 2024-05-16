using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Benday.XmlUtilities
{

    public class XmlFormatter
    {
        public string? Format(string xml)
        {
            // var doc = XDocument.Parse(xml);

            // xml = "<root>" + xml + "</root>";

            // var doc = LoadXmlIgnoringNamespaces(xmlSurrounded);

            var returnValue = BeautifyXmlUsingReader(xml, true);

            return returnValue.ToString();

            /*
            var reader = new NamespaceIgnorantXmlTextReader(new StringReader(xml));

            var outerXml = reader.ReadOuterXml();

            var doc = XDocument.Load(reader);

            var element = doc.Root;

            return Format(element);
            */
        }

        private string Format(XElement element)
        {
            return element.ToString();
        }

        private XDocument LoadXmlIgnoringNamespaces(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    NameTable = new NameTable()
                };
                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(settings.NameTable);
                XmlParserContext context = new XmlParserContext(null, namespaceManager, null, XmlSpace.None);
                XmlReader reader = XmlReader.Create(stringReader, settings, context);

                return XDocument.Load(reader);
            }
        }

        private string BeautifyXmlUsingReader(string xmlToBeautify, bool indentAttributes)
        {
            StringBuilder beautifiedXml = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "     ";
            settings.NewLineOnAttributes = indentAttributes;

            var writer = XmlTextWriter.Create(new StringWriter(beautifiedXml), settings);

            var byteArray = Encoding.UTF8.GetBytes(xmlToBeautify);

            var stream = new MemoryStream(byteArray);

            var reader = new NamespaceIgnorantXmlTextReader(stream);

            // var reader = new XmlTextReader(new StringReader(xmlToBeautify)); // { Namespaces = false });            

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Contains(':') == false)
                        {
                            writer.WriteStartElement(reader.Name);
                        }
                        else
                        {
                            writer.WriteStartElement(
                                reader.Prefix, 
                                reader.LocalName, 
                                reader.NamespaceURI);
                            //writer.WriteStartElement(reader.Prefix, 
                            //    reader.LocalName, reader.Name);
                        }
                        
                        // writer.WriteStartElement(reader.Name);
                        writer.WriteAttributes(reader, false);
                        if (reader.IsEmptyElement == true)
                        {
                            writer.WriteEndElement();
                        }
                        break;
                    case XmlNodeType.CDATA:
                        writer.WriteCData(reader.Value);
                        break;
                    case XmlNodeType.Comment:
                        writer.WriteComment(reader.Value);
                        break;
                    case XmlNodeType.SignificantWhitespace:
                        writer.WriteWhitespace(reader.Value);
                        break;
                    case XmlNodeType.Text:
                        writer.WriteString(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        writer.WriteEndElement();
                        break;
                    default:
                        break;
                }

            }

            writer.Flush();

            var returnValue = beautifiedXml.ToString();

            return returnValue;
        }
    }
}