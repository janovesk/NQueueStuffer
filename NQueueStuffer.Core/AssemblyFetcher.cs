using System;
using System.Linq;
using System.Reflection;
using NServiceBus;

namespace NQueueStuffer.Core
{
    public class AssemblyFetcher : IAssemblyFetcher
    {
        public Type[] GetTypesFromAssembly(string assemblyFilename)
        {
            return GetTypesFromAssembly(Assembly.LoadFile(assemblyFilename));
        }

        public Type[] GetTypesFromAssembly(Assembly assembly)
        {
            var messageTypes = assembly.GetTypes().Where(type => typeof(IMessage).IsAssignableFrom(type));
            return messageTypes.ToArray();
        }

    }
}
