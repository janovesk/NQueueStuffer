using NServiceBus;

namespace NQueueStuffer.Core
{
    public interface IMessageStuffer
    {
        void StuffMessagesToQueue(string queueName, IMessage[] messages);
    }
}