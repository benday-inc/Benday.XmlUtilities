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


        public override string NamespaceURI
        {
            get { return string.Empty; }
        }

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

    public class CustomXmlNamespaceManager : XmlNamespaceManager
    {
        /// <summary>Initializes a new instance of the <see cref="CustomXmlNamspaceManager" /> class with the specified <see cref="System.Xml.XmlNameTable" />.</summary>
        /// <param name="nameTable">The <see cref="System.Xml.XmlNameTable" /> to use.</param>
        /// <exception cref="System.NullReferenceException">
        /// <see langword="null" /> is passed to the constructor</exception>
        public CustomXmlNamespaceManager(XmlNameTable nameTable) : base(nameTable)
        {

        }

        public override string LookupPrefix(string uri)
        {
            try
            {
                return base.LookupPrefix(uri);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string LookupNamespace(string prefix)
        {
            try
            {
                

                return base.LookupNamespace(prefix);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class CustomXmlNameTable : NameTable
    {
        public override string Get(char[] key, int start, int len)
        {
            return base.Get(key, start, len);
        }

        public override string Get(string value)
        {
            return base.Get(value);
        }
    }
}