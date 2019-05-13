namespace EzeTest.TestRunner.Factory
{
    using System;
    using System.Net.Http;
    using EzeTest.Framework.Contracts;
    using EzeTest.Framework.Http;
    using EzeTest.TestRunner.Commands;
    using EzeTest.TestRunner.Enumerations;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Model.External;
    using Microsoft.Extensions.Logging;

    public class TestCommandFactory : ITestCommandFactory
    {
        private readonly ILogger<HttpCommand> loggerFactory;
        private readonly IHttpAuthProvider httpAuthService;

        public TestCommandFactory(ILogger<HttpCommand> loggerFactory, IHttpAuthProvider httpAuthService)
        {
            this.loggerFactory = loggerFactory.VerifyIsSet(nameof(loggerFactory));
            this.httpAuthService = httpAuthService.VerifyIsSet(nameof(httpAuthService));
        }

        public ITestCommand Create(TestCommand testCommand)
        {
            testCommand.VerifyIsSet(nameof(testCommand));
            testCommand.Type.VerifyIsSet(nameof(testCommand.Type)); // TODO: Test

            HttpMethod httpMethod = GetHttpMethod(testCommand.Type);
            return new HttpCommand(loggerFactory, httpAuthService, httpMethod, testCommand);
        }

        private HttpMethod GetHttpMethod(TestCommandType type)
        {
            switch (type)
            {
                case TestCommandType.HttpPut: return HttpMethod.Put;
                case TestCommandType.HttpGet: return HttpMethod.Get;
                case TestCommandType.HttpPost: return HttpMethod.Post;
                case TestCommandType.HttpPatch: return HttpMethod.Patch;
                case TestCommandType.HttpDelete: return HttpMethod.Delete;
            }

            throw new ApplicationException("Unexpected HttpMethod type");
        }
    }
}
