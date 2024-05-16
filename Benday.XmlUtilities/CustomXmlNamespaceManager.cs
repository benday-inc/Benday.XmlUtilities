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
                Console.WriteLine($"Prefix: {prefix}");

                var hasNamespace = HasNamespace(prefix);

                Console.WriteLine($"LookupNamespace.HasNamespace('{prefix}'): {hasNamespace}");

                if (hasNamespace == false && string.IsNullOrEmpty(prefix) == false)
                {
                    if (_CreatedNamespaces.ContainsKey(prefix) == false)
                    {
                        _CreatedNamespaces.Add(prefix, $"http://{Guid.NewGuid()}");
                        AddNamespace(prefix, $"http://{Guid.NewGuid()}");
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
                Console.WriteLine($"ERROR -- Prefix: {prefix}");

                throw;
            }
        }
    }
}