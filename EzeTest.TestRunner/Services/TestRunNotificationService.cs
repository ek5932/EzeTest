namespace EzeTest.TestRunner.Services
{
    using System;
    using System.Collections.Generic;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Services.Interfaces;

    public class TestRunNotificationService : ITestRunNotificationService
    {
        private readonly List<INotificationOutlet> notificationOutlets;

        public TestRunNotificationService()
        {
            this.notificationOutlets = new List<INotificationOutlet>();
        }

        public void NotifyTestFailed(TestRun testRun) => this.Notify(x => x.NotifyTestFailed(testRun));

        public void NotifyTestPassed(TestRun testRun) => this.Notify(x => x.NotifyTestPassed(testRun));

        public void NotifyTestRunStarting(TestRun testRun) => this.Notify(x => x.NotifyTestRunStarting(testRun));

        private void Notify(Action<ITestRunNotificationService> notifyAction)
        {
            foreach (var outlet in this.notificationOutlets)
            {
                notifyAction(outlet);
            }
        }
    }
}
