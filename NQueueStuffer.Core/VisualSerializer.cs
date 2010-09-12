using System;
using System.IO;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NQueueStuffer.Core.NServicebus;
using NServiceBus;
using NServiceBus.MessageInterfaces;

namespace NQueueStuffer.Core
{
    public class VisualSerializer
    {
        private readonly Type _messageType;
        private IWindsorContainer _container;
        private IBus _bus;
        private IMessageMapper _messageMapper;
        private IMessageSerializer _xmlSerializer;

        public VisualSerializer(Type messageType)
        {
            _messageType = messageType;
            _container = CreateContainerWithBus(messageType);
            _bus = _container.Resolve<IBus>();
            _messageMapper = _container.Resolve<IMessageMapper>();
            _xmlSerializer = _container.Resolve<IMessageSerializer>();
        }

        public string GetSerializedType()
        {
            var concreteType = _messageMapper.GetMappedTypeFor(_messageType);
            var concreteInstance = _bus.CreateInstance(concreteType);

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

        private static IWindsorContainer CreateContainerWithBus(Type messageType)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IMessageSerializer>().ImplementedBy<XMLMessageSerializer>());
            NServiceBus.Configure.With(new Type[] { messageType })
               .CastleWindsorBuilder(container)
               .XmlSerializer()
               .MsmqTransport()
                   .IsTransactional(true)
                   .PurgeOnStartup(false)
               .UnicastBus()
                   .ImpersonateSender(false)
                   .CreateBus()
               .Start();

            var serializer = container.Resolve<IMessageSerializer>();
            var nserviceBusSerializer = container.Resolve<NServiceBus.Serialization.IMessageSerializer>();
            
            (serializer as XMLMessageSerializer).MessageTypes =
                (nserviceBusSerializer as NServiceBus.Serializers.XML.MessageSerializer).MessageTypes;

            return container;
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
