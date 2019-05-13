namespace EzeTest.TestRunner.Services
{
    using System;
    using System.Threading.Tasks;
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Repositories;
    using EzeTest.TestRunner.Services.Interfaces;

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
            Test test = await GetTestDefinition(testId);
            await PerformRunTest(configuration, test);
        }

        private async Task<TestRun> PerformRunTest(TestRunConfiguration configuration, Test test)
        {
            var testRun = new TestRun(test, configuration);
            testRunNotificationService.NotifyTestRunStarting(testRun);

            try
            {
                await testRunnerService.RunTest(testRun);

                if (testRun.WasSuccessful)
                    testRunNotificationService.NotifyTestPassed(testRun);
                else
                    testRunNotificationService.NotifyTestFailed(testRun);
            }
            catch
            {
                testRunNotificationService.NotifyTestFailed(testRun);
                throw;
            }

            return testRun;
        }

        private async Task<Test> GetTestDefinition(long testId)
        {
            Test test = await testRepository.GetById(testId);
            if (test == null)
                throw new ApplicationException($"No test definition exists with id {testId}");
            return test;
        }
    }
}
