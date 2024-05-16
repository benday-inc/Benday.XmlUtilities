namespace Benday.XmlUtilities.UnitTests;

[TestClass]
public class XmlFormatterFixture
{
    [TestMethod]
    public void FormatIgnoresNamespaces()
    {
        // arrange
        var formatter = new XmlFormatter();
        var xml = "<dgm:cxnLst>\r\n        <dgm:cxn modelId=\"{984C432E-56F6-4FFD-AE06-1D5BC5C8986A}\"\r\n            srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"\r\n            destId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\" srcOrd=\"0\" destOrd=\"0\"\r\n            parTransId=\"{93DA09AE-1B35-48A0-A4C0-7E64C592EEB6}\"\r\n            sibTransId=\"{B051AA4E-3C0D-425D-9E5F-7098424FD377}\" />\r\n        <dgm:cxn modelId=\"{13EB87B3-CF1F-4B4D-9F39-A239D315CF43}\" type=\"presOf\"\r\n            srcId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"\r\n            destId=\"{7E55710F-0F6B-4406-A2D6-704006B1480F}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{9987CEC8-35BC-49E6-ABAB-02F35EC297A8}\" type=\"presOf\"\r\n            srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"\r\n            destId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{BB4CBA06-B709-45C0-93F1-48DFA80A8F39}\" type=\"presParOf\"\r\n            srcId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\"\r\n            destId=\"{1408703E-7561-4C2C-80F0-F5C14057BAB1}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{C4F9B8C0-8B61-48C8-84CD-4E41EE31232D}\" type=\"presParOf\"\r\n            srcId=\"{1408703E-7561-4C2C-80F0-F5C14057BAB1}\"\r\n            destId=\"{9EA9C567-01E6-48EC-ADDB-B5E02BA15FCA}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{392B9839-6041-47CA-871C-6EC89971A187}\" type=\"presParOf\"\r\n            srcId=\"{9EA9C567-01E6-48EC-ADDB-B5E02BA15FCA}\"\r\n            destId=\"{FE8E84F7-C32D-4478-B33C-5A912DC36BA0}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{063F8CA3-BDDF-4CD8-B26F-53D9E0F99B91}\" type=\"presParOf\"\r\n            srcId=\"{FE8E84F7-C32D-4478-B33C-5A912DC36BA0}\"\r\n            destId=\"{7E55710F-0F6B-4406-A2D6-704006B1480F}\" srcOrd=\"0\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{A19F5E82-C605-4B6D-871E-EF11321C6C3F}\" type=\"presParOf\"\r\n            srcId=\"{FE8E84F7-C32D-4478-B33C-5A912DC36BA0}\"\r\n            destId=\"{528975AC-58D1-4373-81F6-89E39744A865}\" srcOrd=\"1\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n        <dgm:cxn modelId=\"{16C376C1-A576-497B-B8F7-493EAEA2E14E}\" type=\"presParOf\"\r\n            srcId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\"\r\n            destId=\"{AE189223-0634-4024-88B0-0160DA113CA1}\" srcOrd=\"1\" destOrd=\"0\"\r\n            presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />\r\n    </dgm:cxnLst>\r\n";
        var expected = "<dgm:cxnLst>" +
            "   <dgm:cxn " +
            "       modelId=\"{984C432E-56F6-4FFD-AE06-1D5BC5C8986A}\"" +
            "       srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"" +
            "       destId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"" +
            "       srcOrd=\"0\"" +
            "       destOrd=\"0\"" +
            "       parTransId=\"{93DA09AE-1B35-48A0-A4C0-7E64C592EEB6}\"" +
            "       sibTransId=\"{B051AA4E-3C0D-425D-9E5F-7098424FD377}\" />" +
            "   <dgm:cxn " +
            "       modelId=\"{13EB87B3-CF1F-4B4D-9F39-A239D315CF43}\"" +
            "       type=\"presOf\"" +
            "       srcId=\"{483A9269-D346-44D3-BC91-7BE1710F0BD8}\"" +
            "       destId=\"{7E55710F-0F6B-4406-A2D6-704006B1480F}\"" +
            "       srcOrd=\"0\"" +
            "       destOrd=\"0\"" +
            "       presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />" +
            "   <dgm:cxn " +
            "       modelId=\"{9987CEC8-35BC-49E6-ABAB-02F35EC297A8}\"" +
            "       type=\"presOf\"" +
            "       srcId=\"{E6D4CB22-F4EF-4249-9CF0-9F99ACCA3C76}\"" +
            "       destId=\"{1DDEF675-989C-45A0-97EC-C202056D8D1B}\"" +
            "       srcOrd=\"0\"" +
            "       destOrd=\"0\"" +
            "       presId=\"urn:microsoft.com/office/officeart/2005/8/layout/hierarchy5\" />" +
            "</dgm:cxnLst>";

        // act
        var actual = formatter.Format(xml);

        // assert
        Assert.AreEqual(expected, actual);
    }
}