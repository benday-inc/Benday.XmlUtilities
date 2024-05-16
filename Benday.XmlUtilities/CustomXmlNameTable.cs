using System;
using System.Xml;

namespace Benday.XmlUtilities
{
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