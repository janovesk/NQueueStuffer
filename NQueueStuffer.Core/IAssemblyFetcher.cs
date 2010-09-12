using System;

namespace NQueueStuffer.Core
{
    public interface IAssemblyFetcher
    {
        Type[] GetTypesFromAssembly(string assemblyFilename);
    }
}