using System;
using System.Collections.Generic;

namespace Benday.XmlUtilities
{
    public class FormatXmlResponse
    {
        public string Original { get; set; } = string.Empty;
        public string Formatted { get; set; } = string.Empty;
        public Dictionary<string, string> CreatedNamespaces { get; set; } = new Dictionary<string, string>();
    }
}