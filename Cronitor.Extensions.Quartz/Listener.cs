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


            var monitorKey = "";
            var message = "";
            var environment = "";


            await Cronitor.Telemetry.RunAsync(monitorKey, message, environment);
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = new CancellationToken())
        {
            var monitorKey = "";
            var message = "";
            var environment = "";

            await Cronitor.Telemetry.CompleteAsync(monitorKey, message, environment);
        }
    }
}
