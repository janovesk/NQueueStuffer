using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NServiceBus;

namespace NQueueStuffer.Core
{
    public class AssemblyFetcher : IAssemblyFetcher
    {
        public Type[] GetTypesFromAssembly(string assemblyFilename)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
            var assembly = Assembly.LoadFile(assemblyFilename);
            var types = GetMessageTypesFromAssembly(assembly);
            AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(AssemblyResolve);

            return types;
        }

        System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("mscorlib"))
                return null;

            var assemblyDirectory = Path.GetDirectoryName(args.RequestingAssembly.Location);
            var assemblyName = args.Name.Split(new string[] { "," }, StringSplitOptions.None)[0];
            var assemblyFilePath = Path.Combine(assemblyDirectory, assemblyName + ".dll");
            if (!File.Exists(assemblyFilePath))
            {
                assemblyFilePath = Path.Combine(assemblyDirectory, assemblyName + ".exe");
            }
            var assembly = Assembly.LoadFrom(Path.Combine(assemblyFilePath));
            return assembly;
        }

        private Type[] GetMessageTypesFromAssembly(Assembly assembly)
        {
            var messageTypes = assembly.GetTypes().Where(type => typeof(IMessage).IsAssignableFrom(type) && (!type.IsAbstract || type.IsInterface));
            return messageTypes.ToArray();
        }
    }
}
