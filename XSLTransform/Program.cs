using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace XSLTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplyTransform(args[0], args[1]);
        }

        private static void ApplyTransform(string filePath, string xsltFilePath)
        {
            StringWriter stringWriter = new StringWriter();

            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                xmlTextWriter.Formatting = Formatting.Indented;
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(xsltFilePath, new XsltSettings(true, true), new XmlUrlResolver());
                File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                xslTransform.Transform(filePath, xmlTextWriter);
                xmlTextWriter.Close();
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            xmlDocument.Save(filePath);
        }
    }
}
