namespace EzeTest.TestRunner.Services
{
    using System.Threading.Tasks;
    using EzeTest.TestRunner.Model;

    public interface ITestOrchestrationService
    {
        Task RunTest(long testId, TestRunConfiguration configuration);
    }
}
