namespace EzeTest.TestRunner.Services
{
    using EzeTest.TestRunner.Model;
    using System.Threading.Tasks;

    public interface ITestRunnerService
    {
        Task RunTest(TestRun testRun);
    }
}
