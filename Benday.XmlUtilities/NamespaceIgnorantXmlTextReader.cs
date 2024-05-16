using System;
using System.Collections.Generic;
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

        private readonly XmlParserContext _Context;

        private NamespaceIgnorantXmlTextReader(
            Stream stream, XmlParserContext context) : 
            base(stream, XmlNodeType.Document, context) 
        {
            _Context = context;
        }

        public Dictionary<string, string> GetCreatedNamespaces()
        {
            var namespaceMgr = _Context.NamespaceManager as CustomXmlNamespaceManager;

            if (namespaceMgr == null)
            {
                return new Dictionary<string, string>();
            }
            else
            {
                return namespaceMgr.GetCreatedNamespaces();
            }
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
}