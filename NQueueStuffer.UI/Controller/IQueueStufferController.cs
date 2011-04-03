using System;
using NQueueStuffer.UI.View;

namespace NQueueStuffer.UI.Controller
{
    public interface IQueueStufferController
    {
        void Initialize(IQueueStufferView view);
        void GetTypesFromAssembly(string assemblyFilename, string selectedtypeName = null);
        void MessageTypeSelectionChanged(Type messageType);
        void StuffMessageToQueue(string serializedMessageToStuff, string queueName);
    }
}