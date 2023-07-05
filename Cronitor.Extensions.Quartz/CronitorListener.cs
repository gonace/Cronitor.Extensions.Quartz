using Cronitor.Extensions.Quartz.Attributes;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cronitor.Extensions.Quartz
{
    public class CronitorListener : IJobListener
    {
        public string Name => nameof(CronitorListener);

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Cronitor.IsConfigured)
            {
                var type = context.JobInstance.GetType();
                var attribute = (CronitorIdentityAttribute)Attribute.GetCustomAttribute(type, typeof(CronitorIdentityAttribute));

                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Key))
                {
                    await Cronitor.Telemetries.RunAsync(attribute.Key, attribute.Message, attribute.Environment);
                }
            }
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Cronitor.IsConfigured)
            {
                var type = context.JobInstance.GetType();
                var attribute = (CronitorIdentityAttribute)Attribute.GetCustomAttribute(type, typeof(CronitorIdentityAttribute));

                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Key))
                {
                    await Cronitor.Telemetries.CompleteAsync(attribute.Key, attribute.Message, attribute.Environment);
                }
            }
        }
    }
}
