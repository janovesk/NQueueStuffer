using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NQueueStuffer.Core.NServicebus;
using NServiceBus;
using NServiceBus.MessageInterfaces;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serialization;

namespace NQueueStuffer.Core
{
    public class VisualSerializer
    {
        private readonly XMLMessageSerializer _xmlSerializer;
        private readonly MessageInstanceFactory _messageInstanceFactory;

        public VisualSerializer(Type messageType)
        {
            var mapper  = CreateMapper(new []{messageType});
            _xmlSerializer = CreateSerializer(mapper, messageType);
            _messageInstanceFactory = new MessageInstanceFactory(mapper, messageType);
        }

        private static XMLMessageSerializer CreateSerializer(IMessageMapper mapper, Type type)
        {
            var xmlSerializer = new XMLMessageSerializer();
            xmlSerializer.MessageMapper = mapper;
            xmlSerializer.MessageTypes = new List<Type> {type};
            return xmlSerializer;
        }

        private static IMessageMapper CreateMapper(IEnumerable<Type> types)
        {
            var mapper = new MessageMapper();
            mapper.Initialize(types);
            return mapper;
        }

        public string GetSerializedType(bool useReasonableDefaults)
        {
            var concreteInstance = _messageInstanceFactory.GetConcreteInstance(useReasonableDefaults);

            var rawXml = GetRawSerializedType(concreteInstance as IMessage);
            var indentedXml = GetIndentedXML(rawXml);
            return indentedXml;
        }

        private string GetRawSerializedType(IMessage mesasge)
        {
            using (var ms = new MemoryStream())
            {
                _xmlSerializer.Serialize(new[] { mesasge }, ms);
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }

        public IMessage[] GetDeserializedType(string serializedMessageToStuff)
        {
            var message = serializedMessageToStuff.Replace(Environment.NewLine, "\n");

            using(var ms = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var messages = _xmlSerializer.Deserialize(ms);
                return messages;
            }
        }

        private static string GetIndentedXML(string rawXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(rawXml);
           
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "    ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };

            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb, settings);
            doc.WriteContentTo(writer);
            writer.Close();
            return sb.ToString();
        }
    }
}
