using System;

namespace NQueueStuffer.UI.View
{
    public interface IQueueStufferView
    {
        void SetStatus(string message);
        void SetMessagesTypes(Type[] types, Type selectedType = null);
        void ShowSerializedMessageType(string serializedMessageType, Type messageType);
    	NqsSettingItem SelectedSettings { get; }
    }
}