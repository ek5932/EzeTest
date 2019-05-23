namespace EzeTest.TestRunner.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EzeTest.Framework;
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Model.Results;
    using Microsoft.Extensions.Logging;

    public class TestRunnerService : ITestRunnerService
    {
        private readonly ILogger<TestRunnerService> logger;

        public TestRunnerService(ILogger<TestRunnerService> logger)
        {
            this.logger = logger.VerifyIsSet(nameof(logger));
        }

        public async Task RunTest(TestRun testRun)
        {
            testRun.VerifyIsSet(nameof(testRun));
            testRun.Configuration.VerifyIsSet(nameof(testRun.Configuration));
            testRun.TestDefinition.VerifyIsSet(nameof(testRun.TestDefinition));
            testRun.TotalTimeTaken = await Time.Async(() => this.DoRunTest(testRun));
        }

        private async Task DoRunTest(TestRun testRun)
        {
            this.logger.LogInformation($"Starting execution of test '{testRun.TestDefinition.Id}'.");

            try
            {
                var context = new TestContext();
                foreach (var testCommand in testRun.TestDefinition)
                {
                    ITestCommandResult commandResult = await this.RunCommand(testRun, testRun.TestDefinition, testCommand, context);
                    testRun.AddCommandResult(commandResult);

                    bool shouldTerminate = this.ProcessTestResult(testCommand, commandResult, testRun.Configuration);
                    if (shouldTerminate)
                    {
                        this.logger.LogWarning("Prematurely terminating test.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Unexpected error running test {testRun.Id}.");
                testRun.MarkAsFailed(ex.Message);
            }
        }

        private bool ProcessTestResult(ITestCommand command, ITestCommandResult commandResult, TestRunConfiguration configuration)
        {
            if (!commandResult.ExecutedSuccessfully)
            {
                this.logger.LogWarning($"Executed command {command.Id} with failure result.");

                if (configuration.ExitOnFailure)
                {
                    this.logger.LogWarning($"Test is configured with ExitOnFailure; quitting test.");
                    return true;
                }
            }
            else
            {
                this.logger.LogInformation($"Executed command {command.Id} with success result.");
            }

            return false;
        }

        private async Task<ITestCommandResult> RunCommand(TestRun testRun, Test test, ITestCommand command, TestContext context)
        {
            this.logger.LogInformation($"Starting execution of command {command.Id} for test {test.Id}.");

            try
            {
                return await command.Execute(context, CancellationToken.None);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Unexpected error executing command {command.Id}.");
                throw;
            }
        }
    }
}
