using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueueStuffer.Core.MSMQ
{
    public interface IQueueFinder
    {
        List<string> GetLocalQueueNames();
    }
}
