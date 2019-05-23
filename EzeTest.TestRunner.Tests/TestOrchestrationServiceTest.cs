////namespace EzeTest.TestRunner.Tests
////{
////    using System.Threading.Tasks;
////    using EzeTest.Framework.Http;
////    using EzeTest.TestRunner.Commands;
////    using EzeTest.TestRunner.Factory;
////    using EzeTest.TestRunner.Model.Http;
////    using EzeTest.TestRunner.Repositories;
////    using EzeTest.TestRunner.Services;
////    using Microsoft.Extensions.Logging;
////    using Moq;
////    using NUnit.Framework;

////    [TestFixture]
////    public class TestOrchestrationServiceTest
////    {
////        private Mock<ILogger<HttpCommand>> mockHttpCommandLogger;
////        private Mock<IHttpAuthService> mockIHttpAuthService;
////          private Mock<ILogger<TestRunnerService>> mockTestRunnerServiceLogger;
////        private Mock<ITestRunNotificationService> mockITestRunNotificationService;

////        [SetUp]
////        public void TestSetUp()
////        {
////            mockIHttpAuthService = new Mock<IHttpAuthService>();
////            mockIHttpAuthService.Setup(x => x.Authenticate())
////                                .Returns(Task.FromResult(new HttpSession()));

////            mockHttpCommandLogger = new Mock<ILogger<HttpCommand>>();
////            mockTestRunnerServiceLogger = new Mock<ILogger<TestRunnerService>>();
////            mockITestRunNotificationService = new Mock<ITestRunNotificationService>();
////        }

////        [Test]
////        public async Task Test()
////        {
////            var factory = new TestCommandFactory(mockHttpCommandLogger.Object, mockIHttpAuthService.Object);
////            var repo = new TestRepository(factory);
////            var testRunner = new TestRunnerService(mockTestRunnerServiceLogger.Object);
////            var instance = new TestOrchestrationService(repo, testRunner, mockITestRunNotificationService.Object);

////            await instance.RunTest(4, new Model.TestRunConfiguration());
////        }
////    }
////}
