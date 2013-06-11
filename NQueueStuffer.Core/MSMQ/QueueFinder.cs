using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace NQueueStuffer.Core.MSMQ
{
    public class QueueFinder : IQueueFinder
    {
        public List<string> GetLocalQueueNames()
        {
            return MessageQueue.GetPrivateQueuesByMachine("localhost").Select(m=>m.QueueName).Select(q=>
                        {
                            if(q.StartsWith("private$\\", StringComparison.OrdinalIgnoreCase))
                            {
                                return q.Substring(9);
                            }
                            return q;
                        }).OrderBy(q=>q).ToList();
        }
    }
}
