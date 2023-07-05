# Cronitor.Extensions.Quartz
> Listeners are registered with the scheduler during run time, and are NOT stored in the JobStore along with the jobs and triggers. This is because listeners are typically an integration point with your application. Hence, each time your application runs, the listeners need to be re-registered with the scheduler.

## Usages
Adding `CronitorJobListener` that is interested in all jobs:

```c#
scheduler.ListenerManager.AddJobListener(new CronitorJobListener(), GroupMatcher<JobKey>.AnyGroup());
```

More examples of how to register implementations of IJobListener can be found here: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/trigger-and-job-listeners.html#using-your-own-listeners
