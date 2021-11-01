using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace TracerLib.Serializers
{
    public class XMLSerializer : ISerializer
    {
        private DataContractSerializer xmlSerializer;
        private XmlWriterSettings xmlWriterSettings;

        public XMLSerializer()
        {
            xmlSerializer = new DataContractSerializer(typeof(TraceResult));
            xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            };
        }
        
        public string Serialize(TraceResult traceResult)
        {
            // var formatter = new XmlSerializer(typeof(TraceResult));
            using var stream = new MemoryStream();
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
            {
                xmlSerializer.WriteObject(xmlWriter, traceResult);
            }
            
            
            // formatter.Serialize(stream, traceResult);
            stream.Position = 0;
            
            using var reader = new StreamReader(stream);
            
            return reader.ReadToEnd();
        }
    }
}