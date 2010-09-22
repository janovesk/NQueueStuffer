using System;
using System.IO;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
            _bus = CreateBus();
            AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(AssemblyResolve);
            
        }

        System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("mscorlib"))
                return null;

            var assemblyDirectory = Path.GetDirectoryName(args.RequestingAssembly.Location);
            var assemblyName = args.Name.Split(new string[] { "," }, StringSplitOptions.None)[0];
            var assemblyFilePath = Path.Combine(assemblyDirectory, assemblyName + ".dll");
            var assembly = Assembly.LoadFrom(Path.Combine(assemblyFilePath));
            return assembly;
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
