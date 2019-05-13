namespace EzeTest.TestRunner.Services
{
    using System;
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Services.Interfaces;

    public class TestRunNotificationService : ITestRunNotificationService
    {
        private readonly INotificationOutlet[] notificationOutlets;

        public TestRunNotificationService()
        {
            notificationOutlets = new INotificationOutlet[0];
        }

        public void NotifyTestFailed(TestRun testRun) => Notify(x => x.NotifyTestFailed(testRun));

        public void NotifyTestPassed(TestRun testRun) => Notify(x => x.NotifyTestPassed(testRun));

        public void NotifyTestRunStarting(TestRun testRun) => Notify(x => x.NotifyTestRunStarting(testRun));

        private void Notify(Action<ITestRunNotificationService> notifyAction)
        {
            foreach (var outlet in notificationOutlets)
            {
                notifyAction(outlet);
            }
        }
    }
}
