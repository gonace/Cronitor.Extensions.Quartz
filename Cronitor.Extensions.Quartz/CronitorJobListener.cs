using Cronitor.Extensions.Quartz.Attributes;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cronitor.Extensions.Quartz
{
    public class CronitorJobListener : IJobListener
    {
        public string Name => nameof(CronitorJobListener);

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            if (Cronitor.IsConfigured)
            {
                var attribute = GetAttribute(context.JobInstance);
                if (attribute != null && !string.IsNullOrEmpty(attribute.Key))
                {
                    await Cronitor.Telemetries.CompleteAsync(attribute.Key, attribute.Message, attribute.Environment);
                }
            }
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            if (Cronitor.IsConfigured)
            {
                var attribute = GetAttribute(context.JobInstance);
                if (attribute != null && !string.IsNullOrEmpty(attribute.Key))
                {
                    await Cronitor.Telemetries.CompleteAsync(attribute.Key, attribute.Message, attribute.Environment);
                }
            }
        }

        private CronitorIdentityAttribute GetAttribute(IJob job)
        {
            var type = job.GetType();
            var attribute = (CronitorIdentityAttribute)Attribute.GetCustomAttribute(type, typeof(CronitorIdentityAttribute));

            return attribute;
        }
    }
}
