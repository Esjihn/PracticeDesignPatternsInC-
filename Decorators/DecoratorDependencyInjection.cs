using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Decorators
{
    // Autofac supports decorator pattern

    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine($"Here is your report.");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private readonly IReportingService decorated;

        public ReportingServiceWithLogging(IReportingService decorated)
        {
            this.decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public void Report()
        {
            Console.WriteLine($"Commencing log...");
            decorated.Report();
            Console.WriteLine($"Finishing log...");
        }
    }

    public class DecoratorDependencyInjection
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            // b.RegisterType<ReportingServiceWithLogging>().As<IReportingService>(); // will inject into infinity
            // we really want injection of reporting service with logging without reporting service also being injected
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting"); 
            b.RegisterDecorator<IReportingService>(
                // registered decorator when resolved. 
                (context, service) => new ReportingServiceWithLogging(service), "reporting");

            using (var c = b.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }
        }
    }
}
