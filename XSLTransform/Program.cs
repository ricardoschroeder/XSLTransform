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
            bool areFilesValid = true;

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"XML file {args[0]} does not exist.");
                areFilesValid = false;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine($"XSLT file {args[1]} does not exist.");
                areFilesValid = false;
            }

            if (!areFilesValid)
            {
                return;
            }

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
                File.SetAttributes(filePath, FileAttributes.Normal);
                xslTransform.Transform(filePath, xmlTextWriter);
                xmlTextWriter.Close();
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            xmlDocument.Save(filePath);
        }
    }
}
