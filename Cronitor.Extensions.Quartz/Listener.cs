using Quartz;
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
                var monitorKey = "";
                var message = "";
                var environment = "";

                await Cronitor.Telemetries.RunAsync(monitorKey, message, environment);
            }
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Cronitor.IsConfigured)
            {
                var monitorKey = "";
                var message = "";
                var environment = "";

                await Cronitor.Telemetries.CompleteAsync(monitorKey, message, environment);
            }
        }
    }
}
