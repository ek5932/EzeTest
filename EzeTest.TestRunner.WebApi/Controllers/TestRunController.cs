namespace EzeTest.TestRunner.WebApi.Controllers
{
    using System.Threading.Tasks;
    using EzeTest.Framework.Contracts;
    using EzeTest.Framework.Mapping;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Services;
    using EzeTest.TestRunner.WebApi.Resources;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/test")]
    public class TestRunController : ControllerBase
    {
        private readonly ITestOrchestrationService testOrchestrationService;
        private readonly IObjectMapper<TestConfigurationResource, TestRunConfiguration> objectMapper;

        public TestRunController(ITestOrchestrationService testOrchestrationService, IObjectMapper<TestConfigurationResource, TestRunConfiguration> objectMapper)
        {
            this.testOrchestrationService = testOrchestrationService.VerifyIsSet(nameof(testOrchestrationService));
            this.objectMapper = objectMapper.VerifyIsSet(nameof(objectMapper));
        }

        /// <summary>
        /// Runs a test.
        /// </summary>
        /// <param name="id">The ID of the test definition to run.</param>
        /// <param name="testConfiguration">The configuration to use when running the test.</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<ActionResult> RunTest(int id, TestConfigurationResource testConfiguration)
        {
            TestRunConfiguration domainConfiguration = objectMapper.Map(testConfiguration);
            await testOrchestrationService.RunTest(id, domainConfiguration);
            return Ok();
        }
    }
}
