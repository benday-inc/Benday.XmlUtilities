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
            var omitXmlDeclaration = true;

            if (xmlToBeautify.TrimStart().StartsWith("<?xml ") == true)
            {
                omitXmlDeclaration = false;
            }

            var returnValue = new FormatXmlResponse();

            returnValue.Original = xmlToBeautify;
            
            var beautifiedXml = new StringBuilder();            

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = INDENT_CHARS,
                NewLineOnAttributes = indentAttributes,
                OmitXmlDeclaration = omitXmlDeclaration,
                Encoding = Encoding.UTF8
            };

            Utf8StringWriter stringWriterInstance = new Utf8StringWriter(beautifiedXml);

            var writer = XmlTextWriter.Create(
                stringWriterInstance, 
                settings);

            var byteArray = Encoding.UTF8.GetBytes(xmlToBeautify);

            var stream = new MemoryStream(byteArray);

            var reader = new NamespaceIgnorantXmlTextReader(stream);

            // var reader = new XmlTextReader(new StringReader(xmlToBeautify)); // { Namespaces = false });            

            bool hasXmlDeclaration = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        // write xml declaration
                        writer.WriteStartDocument();
                        hasXmlDeclaration = true;

                        break;
                    case XmlNodeType.Element:
                        if (reader.Name.Contains(':') == false && 
                            string.IsNullOrWhiteSpace(reader.NamespaceURI) == true)
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

            if (hasXmlDeclaration == true)
            {
                writer.WriteEndDocument();
            }

            writer.Flush();

            returnValue.Formatted = beautifiedXml.ToString();

            returnValue.CreatedNamespaces = reader.GetCreatedNamespaces();

            return returnValue;
        }
    }
}
