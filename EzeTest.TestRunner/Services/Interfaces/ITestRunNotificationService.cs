using EzeTest.TestRunner.Model;

namespace EzeTest.TestRunner.Services
{
    public interface ITestRunNotificationService
    {
        void NotifyTestRunStarting(TestRun testRun);
        void NotifyTestFailed(TestRun testRun);
        void NotifyTestPassed(TestRun testRun);
    }
}
