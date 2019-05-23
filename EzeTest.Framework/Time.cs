namespace EzeTest.Framework
{
    using System;
    using System.Threading.Tasks;

    public static class Time
    {
        public static async Task<TimeSpan> Async(Func<Task> action)
        {
            // TODO
            await action();
            return new TimeSpan();
        }
    }
}
