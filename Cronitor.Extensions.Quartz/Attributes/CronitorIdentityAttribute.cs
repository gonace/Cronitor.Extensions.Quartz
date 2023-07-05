using System;

namespace Cronitor.Extensions.Quartz.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CronitorIdentityAttribute : Attribute
    {
        public string Key { get; }
        public string Environment { get; }
        public string Message { get; }

        public CronitorIdentityAttribute(string key)
        {
            Key = key;
        }

        public CronitorIdentityAttribute(string key, string environment)
        {
            Key = key;
            Environment = environment;
        }

        public CronitorIdentityAttribute(string key, string environment, string message)
        {
            Key = key;
            Environment = environment;
            Message = message;
        }
    }
}
