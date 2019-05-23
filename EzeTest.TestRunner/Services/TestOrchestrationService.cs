namespace EzeTest.TestRunner.Services
{
    using System;
    using System.Threading.Tasks;
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Repositories;

    public class TestOrchestrationService : ITestOrchestrationService
    {
        private readonly ITestRepository testRepository;
        private readonly ITestRunnerService testRunnerService;
        private readonly ITestRunNotificationService testRunNotificationService;

        public TestOrchestrationService(ITestRepository testRepository, ITestRunnerService testRunnerService, ITestRunNotificationService testRunNotificationService)
        {
            this.testRepository = testRepository.VerifyIsSet(nameof(testRepository));
            this.testRunnerService = testRunnerService.VerifyIsSet(nameof(testRunnerService));
            this.testRunNotificationService = testRunNotificationService.VerifyIsSet(nameof(testRunNotificationService));
        }

        public async Task RunTest(long testId, TestRunConfiguration configuration)
        {
            Test test = await this.GetTestDefinition(testId);
            await this.PerformRunTest(configuration, test);
        }

        private async Task<TestRun> PerformRunTest(TestRunConfiguration configuration, Test test)
        {
            var testRun = new TestRun(test, configuration);
            this.testRunNotificationService.NotifyTestRunStarting(testRun);

            try
            {
                await this.testRunnerService.RunTest(testRun);

                if (testRun.WasSuccessful)
                {
                    this.testRunNotificationService.NotifyTestPassed(testRun);
                }
                else
                {
                    this.testRunNotificationService.NotifyTestFailed(testRun);
                }
            }
            catch
            {
                this.testRunNotificationService.NotifyTestFailed(testRun);
                throw;
            }

            return testRun;
        }

        private async Task<Test> GetTestDefinition(long testId)
        {
            Test test = await this.testRepository.GetById(testId);
            if (test == null)
            {
                throw new ApplicationException($"No test definition exists with ID {testId}.");
            }

            return test;
        }
    }
}
