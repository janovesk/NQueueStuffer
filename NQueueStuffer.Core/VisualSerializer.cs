using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            string serializedType = SerializeToString(_xmlSerializer, concreteInstance as IMessage);

            return serializedType.Replace("\n", Environment.NewLine);
        }
       
        private static string SerializeToString(IMessageSerializer xmlSerializer, IMessage concreteInstance)
        {
            using (var ms = new MemoryStream())
            {
                xmlSerializer.Serialize(new[] {concreteInstance}, ms);
                ms.Seek(0, SeekOrigin.Begin);
                var sw = new StreamReader(ms, Encoding.UTF8);
                var serializedType = sw.ReadToEnd();
                return serializedType;
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
    }
}
