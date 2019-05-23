namespace EzeTest.TestRunner.Repositories
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.Enumerations;
    using EzeTest.TestRunner.Factory;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Model.External;

    public class TestRepository : ITestRepository
    {
        private readonly HttpClient httpClient;
        private readonly ITestCommandFactory testCommandFactory;

        public TestRepository(ITestCommandFactory testCommandFactory)
        {
            this.testCommandFactory = testCommandFactory.VerifyIsSet(nameof(testCommandFactory));
            this.httpClient = new HttpClient();
        }

        public Task<Test> GetById(long testId)
        {
            var testDefinition = this.GetTest();
            var test = new Test(testDefinition.Id);

            foreach (var item in testDefinition)
            {
                ITestCommand testCommand = this.testCommandFactory.Create(item);
                if (testCommand == null)
                {
                    throw new ApplicationException("The testCommand is null.");
                }

                test.Add(testCommand);
            }

            return Task.FromResult(test);
        }

        private TestDefinition GetTest()
        {
            // TODO:
            return new TestDefinition(4)
            {
                new TestCommand
                {
                     Id = 1,
                     Type = TestCommandType.HttpGet,
                     Url = "https://www.google.com"
                },
                new TestCommand
                {
                     Id = 2,
                     Type = TestCommandType.HttpGet,
                     Url = "https://play.google.com"
                }
            };
        }
    }
}
