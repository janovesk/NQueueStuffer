using System;

namespace NQueueStuffer.UI.View
{
    public interface IQueueStufferView
    {
        void SetStatus(string message);
        void SetMessagesTypes(Type[] types);
        void ShowSerializedMessageType(string serializedMessageType, Type messageType);
    }
}