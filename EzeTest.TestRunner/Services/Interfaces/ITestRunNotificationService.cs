namespace EzeTest.TestRunner.Services
{
    using EzeTest.TestRunner.Model;

    public interface ITestRunNotificationService
    {
        void NotifyTestRunStarting(TestRun testRun);

        void NotifyTestFailed(TestRun testRun);

        void NotifyTestPassed(TestRun testRun);
    }
}
