using System;
using NServiceBus;

namespace NQueueStuffer.Core
{
    public class MessageStuffer : IMessageStuffer
    {
        private readonly Type _messageType;
        private IBus _bus;

        public MessageStuffer(Type messageType)
        {
            _messageType = messageType;
            _bus = CreateBus();
        }

        private IBus CreateBus()
        {
            return NServiceBus.Configure.With(new []{_messageType})
               .CastleWindsorBuilder()
               .XmlSerializer()
               .MsmqTransport()
                   .IsTransactional(true)
                   .PurgeOnStartup(false)
               .UnicastBus()
                   .ImpersonateSender(false)
                   .CreateBus()
               .Start();
        }

        public void StuffMessagesToQueue(string queueName, IMessage[] messages)
        {
            _bus.Send(queueName, messages);
        }
    }
}
