using System.IO;
using NServiceBus;

namespace NQueueStuffer.Core.NServicebus
{
    public interface IMessageSerializer
    {
        void Serialize(IMessage[] messages, Stream stream);
        IMessage[] Deserialize(Stream stream);
    }
}
