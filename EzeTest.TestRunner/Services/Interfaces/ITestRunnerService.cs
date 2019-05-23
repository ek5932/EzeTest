namespace EzeTest.TestRunner.Services
{
    using System.Threading.Tasks;
    using EzeTest.TestRunner.Model;

    public interface ITestRunnerService
    {
        Task RunTest(TestRun testRun);
    }
}
