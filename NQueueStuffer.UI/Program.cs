using System;
using System.Configuration;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NQueueStuffer.Core;
using NQueueStuffer.UI.Controller;
using NQueueStuffer.UI.View;

namespace NQueueStuffer.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var container = SetupContainer();
            var controller = container.Resolve<IQueueStufferController>();

            var defaultQueueName = ConfigurationManager.AppSettings["defaultQueueName"];
            Application.Run(new QueueStufferView(controller, defaultQueueName));
        }

        private static IWindsorContainer SetupContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IAssemblyFetcher>().ImplementedBy<AssemblyFetcher>(),
                               Component.For<IQueueStufferView>().ImplementedBy<QueueStufferView>(),
                               Component.For<IQueueStufferController>().ImplementedBy<QueueStufferController>()); ;
            return container;
        }
    }
}
