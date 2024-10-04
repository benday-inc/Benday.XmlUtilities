using System;
using System.IO;
using System.Text;

public class Utf8StringWriter : StringWriter
{
    public Utf8StringWriter(StringBuilder builder) : base(builder)
    {

    }

    // Override the Encoding property to return UTF-8
    public override Encoding Encoding => Encoding.UTF8;
}