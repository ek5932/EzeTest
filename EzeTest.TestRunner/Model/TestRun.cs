namespace EzeTest.TestRunner.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EzeTest.TestRunner.Model.Results;

    public class TestRun
    {
        private readonly Guid testRunId;
        private readonly List<ITestCommandResult> commandResults;

        public TestRun(Test testDefinition, TestRunConfiguration configuration)
        {
            this.testRunId = Guid.NewGuid();
            this.TestDefinition = testDefinition;
            this.Configuration = configuration;
            this.commandResults = new List<ITestCommandResult>();
        }

        public string Id { get; }

        public TestRunConfiguration Configuration { get; set; }

        public Test TestDefinition { get; set; }

        public TimeSpan TotalTimeTaken { get; internal set; }

        public bool WasSuccessful => this.commandResults.All(x => x.ExecutedSuccessfully);

        public void AddCommandResult(ITestCommandResult commandResult)
        {
            this.commandResults.Add(commandResult);
        }

        internal void MarkAsFailed(string message)
        {
            throw new NotImplementedException();
        }
    }
}
