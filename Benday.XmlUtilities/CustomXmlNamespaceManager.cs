using System;
using System.Collections.Generic;
using System.Xml;

namespace Benday.XmlUtilities
{
    public class CustomXmlNamespaceManager : XmlNamespaceManager
    {
        private readonly Dictionary<string, string> _CreatedNamespaces = new Dictionary<string, string>();

        public CustomXmlNamespaceManager(XmlNameTable nameTable) : base(nameTable)
        {

        }

        public override string LookupPrefix(string uri)
        {
            try
            {
                Console.WriteLine($"Uri: {uri}");

                return base.LookupPrefix(uri);
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR -- Uri: {uri}");

                throw;
            }
        }

        public override string LookupNamespace(string prefix)
        {
            try
            {
                if (prefix == "xmlns")
                {
                    return String.Empty;
                }

                var hasNamespace = HasNamespace(prefix);

                if (hasNamespace == false && string.IsNullOrEmpty(prefix) == false)
                {
                    if (_CreatedNamespaces.ContainsKey(prefix) == false)
                    {
                        var newNamespace = $"uri://{Guid.NewGuid()}";

                        _CreatedNamespaces.Add(prefix, newNamespace);
                        AddNamespace(prefix, newNamespace);
                    }

                    return _CreatedNamespaces[prefix];
                }
                else
                {
                    var returnValue = base.LookupNamespace(prefix);

                    return returnValue;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Dictionary<string, string> GetCreatedNamespaces()
        {
            return new Dictionary<string, string>(_CreatedNamespaces);
        }
    }
}