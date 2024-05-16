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
        public FormatXmlResponse Format(string xml)
        {
            return BeautifyXmlUsingReader(xml, true);
        }

        public const string INDENT_CHARS = "    ";

        private FormatXmlResponse BeautifyXmlUsingReader(string xmlToBeautify, bool indentAttributes)
        {
            var returnValue = new FormatXmlResponse();

            returnValue.Original = xmlToBeautify;
            
            var beautifiedXml = new StringBuilder();

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = INDENT_CHARS;
            settings.NewLineOnAttributes = indentAttributes;
            settings.OmitXmlDeclaration = true;

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

            returnValue.Formatted = beautifiedXml.ToString();

            returnValue.CreatedNamespaces = reader.GetCreatedNamespaces();

            return returnValue;
        }
    }
}