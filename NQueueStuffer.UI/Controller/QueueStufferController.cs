using System;
using System.Collections.Generic;
using System.Linq;
using NQueueStuffer.Core;
using NQueueStuffer.UI.View;
using NQueueStuffer.Core.MSMQ;

namespace NQueueStuffer.UI.Controller
{
    public class QueueStufferController : IQueueStufferController
    {
        private readonly IAssemblyFetcher _assemblyFetcher;

        private IQueueStufferView _view;
        private IMessageStuffer _messageStuffer;
        private VisualSerializer _visualSerializer;
        private Type _messageType;
        private IQueueFinder _queueFinder;

        public QueueStufferController(IAssemblyFetcher assemblyFetcher, IQueueFinder queueFinder)
        {
            _assemblyFetcher = assemblyFetcher;
            _queueFinder = queueFinder;
        }

        public void Initialize(IQueueStufferView view)
        {
            _view = view;
            _view.SetStatus("No assembly with messagetypes has been loaded yet.");
        }

        public void GetTypesFromAssembly(string assemblyFilename, string selectedtypeName = null)
        {
            var types = _assemblyFetcher.GetTypesFromAssembly(assemblyFilename);
        	var selected = types.FirstOrDefault(t => t.Name == selectedtypeName);
            _view.SetMessagesTypes(types, selected);
        }

        public List<string> GetQueueNames()
        {
            return _queueFinder.GetLocalQueueNames();
        }

        public void MessageTypeSelectionChanged(Type messageType)
        {
            _messageType = messageType;
            _messageStuffer = new MessageStuffer(messageType);
            _visualSerializer = new VisualSerializer(messageType);
            
            var typeSerializedToString = _visualSerializer.GetSerializedType(true);
            _view.ShowSerializedMessageType(typeSerializedToString, messageType);
        }

        public void StuffMessageToQueue(string serializedMessageToStuff, string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                _view.SetStatus("Can not stuff to unknown queue!");
                return;
            }

            if (_messageType == null)
            {
                _view.SetStatus("No messagetype selected.");
                return;
            }

            var messages = _visualSerializer.GetDeserializedType(serializedMessageToStuff);
            _messageStuffer.StuffMessagesToQueue(queueName, messages);
            
            _view.SetStatus(string.Format("Message of type {0} has been stuffed into {1} at {2}", _messageType.Name, queueName, DateTime.Now));
        }
    }
}
