namespace Benday.XmlUtilities.UnitTests;

[TestClass]
public class XmlFormatterFixture
{
    [TestMethod]
    public void FormatSimple()
    {
        // arrange
        var formatter = new XmlFormatter();

        var indent = XmlFormatter.INDENT_CHARS;

        var xml = "<root><child1 attr1=\"value1\" attr2=\"value2\" />" +
            "<child2 attr3=\"value3\" attr4=\"value4\" /></root>";

        Console.WriteLine($"xml: {xml}");

        string expected = "<root>" + Environment.NewLine +
            indent + "<child1" + Environment.NewLine +
            indent + indent + "attr1=\"value1\"" + Environment.NewLine +
            indent + indent + "attr2=\"value2\" />" + Environment.NewLine +
            indent + "<child2" + Environment.NewLine +
            indent + indent + "attr3=\"value3\"" + Environment.NewLine +
            indent + indent + "attr4=\"value4\" />" + Environment.NewLine +
            "</root>";

        // act
        var actual = formatter.Format(xml);

        // assert
        Assert.AreEqual<int>(0, actual.CreatedNamespaces.Count, $"Should have created a namespace");

        var expectedLines = expected.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        var actualLines = actual.Formatted.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        Assert.AreEqual<int>(expectedLines.Length, actualLines.Length, "Line count didn't match");

        for (int i = 0; i < expectedLines.Length; i++)
        {
            Assert.AreEqual<string>(expectedLines[i], actualLines[i], $"Line {i} didn't match");
        }

        Assert.AreEqual(expected, actual.Formatted, "formatted xml didn't match expected");
    }

    [TestMethod]
    public void FormatIgnoresNamespaces()
    {
        // arrange
        var formatter = new XmlFormatter();

        var indent = XmlFormatter.INDENT_CHARS;

        var xml = "<dgm:cxnLst>\r\n        " +
            "<dgm:cxn modelId=\"{984C432E-56F6-4FFD-AE06-1D5BC5C8986A}\" srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\" " +
            "   destId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\" srcOrd=\"0\" destOrd=\"0\"\r\n            " +
            "parTransId=\"{93DA09AE-1B35-48A0-A4C0-7E64C592EEB6}\"\r\n            sibTransId=\"{B051AA4E-3C0D-425D-9E5F-7098424FD377}\" />\r\n        " +
            "<dgm:cxn modelId=\"{13EB87B3-CF1F-4B4D-9F39-A239D315CF43}\" type=\"presOf\"\r\n            srcId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"\r\n            destId=\"{7E55710F-0F6B-4406-A2D6-704006B1480F}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        " +
            "<dgm:cxn modelId=\"{9987CEC8-35BC-49E6-ABAB-02F35EC297A8}\" type=\"presOf\"\r\n            srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"\r\n            destId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        " +
            "</dgm:cxnLst>\r\n";
        
        string expected = "<dgm:cxnLst" + Environment.NewLine +
            indent + "%%GENERATED_NAMESPACE%%>" + Environment.NewLine +
            indent + "<dgm:cxn" + Environment.NewLine +
            indent + indent + "modelId=\"{984C432E-56F6-4FFD-AE06-1D5BC5C8986A}\"" + Environment.NewLine +
            indent + indent + "srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"" + Environment.NewLine +
            indent + indent + "destId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"" + Environment.NewLine +
            indent + indent + "srcOrd=\"0\"" + Environment.NewLine +
            indent + indent + "destOrd=\"0\"" + Environment.NewLine +
            indent + indent + "parTransId=\"{93DA09AE-1B35-48A0-A4C0-7E64C592EEB6}\"" + Environment.NewLine +
            indent + indent + "sibTransId=\"{B051AA4E-3C0D-425D-9E5F-7098424FD377}\" />" + Environment.NewLine +
            indent + "<dgm:cxn" + Environment.NewLine +
            indent + indent + "modelId=\"{13EB87B3-CF1F-4B4D-9F39-A239D315CF43}\"" + Environment.NewLine +
            indent + indent + "type=\"presOf\"" + Environment.NewLine +
            indent + indent + "srcId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"" + Environment.NewLine +
            indent + indent + "destId=\"{7E55710F-0F6B-4406-A2D6-704006B1480F}\"" + Environment.NewLine +
            indent + indent + "srcOrd=\"0\"" + Environment.NewLine +
            indent + indent + "destOrd=\"0\"" + Environment.NewLine +
            indent + indent + "presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />" + Environment.NewLine +
            indent + "<dgm:cxn" + Environment.NewLine +
            indent + indent + "modelId=\"{9987CEC8-35BC-49E6-ABAB-02F35EC297A8}\"" + Environment.NewLine +
            indent + indent + "type=\"presOf\"" + Environment.NewLine +
            indent + indent + "srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"" + Environment.NewLine +
            indent + indent + "destId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\"" + Environment.NewLine +
            indent + indent + "srcOrd=\"0\"" + Environment.NewLine +
            indent + indent + "destOrd=\"0\"" + Environment.NewLine +
            indent + indent + "presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />" + Environment.NewLine +
            "</dgm:cxnLst>";

        // act
        var actual = formatter.Format(xml);

        // assert
        Assert.AreEqual<int>(1, actual.CreatedNamespaces.Count, $"Should have created a namespace");

        var nsKey = actual.CreatedNamespaces.First().Key ?? throw new InvalidOperationException();
        var nsValue = actual.CreatedNamespaces.First().Value ?? throw new InvalidOperationException();

        expected = expected.Replace("%%GENERATED_NAMESPACE%%", $"xmlns:{nsKey}=\"{nsValue}\"");

        var expectedLines = expected.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        var actualLines = actual.Formatted.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        Assert.AreEqual<int>(expectedLines.Length, actualLines.Length, "Line count didn't match");

        for (int i = 0; i < expectedLines.Length; i++)
        {
            Assert.AreEqual<string>(expectedLines[i], actualLines[i], $"Line {i} didn't match");
        }

        Assert.AreEqual(expected, actual.Formatted, "formatted xml didn't match expected");
    }
}