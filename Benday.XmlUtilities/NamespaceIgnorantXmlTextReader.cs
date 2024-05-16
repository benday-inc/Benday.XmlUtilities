using System;
using System.IO;
using System.Xml;

namespace Benday.XmlUtilities
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        
        public NamespaceIgnorantXmlTextReader(Stream stream) : 
            this(stream, 
                new XmlParserContext(
                    null, 
                    new CustomXmlNamespaceManager(new CustomXmlNameTable()), 
                    null, 
                    XmlSpace.None))
        {
        }

        private NamespaceIgnorantXmlTextReader(
            Stream stream, XmlParserContext context) : 
            base(stream, XmlNodeType.Document, context) { 
        

        }


        //public override string NamespaceURI
        //{
        //    get { return string.Empty; }
        //}

        public override bool Read()
        {
            try
            {
                return base.Read();
            }
            catch (Exception ex)
            {
                var nameTable = NameTable;

                Console.WriteLine(ex.ToString());
                throw;
            }            
        }       
    }
}