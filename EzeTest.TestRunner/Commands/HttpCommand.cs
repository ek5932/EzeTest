namespace EzeTest.TestRunner.Commands
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EzeTest.Framework.Contracts;
    using EzeTest.Framework.Http;
    using EzeTest.TestRunner.Consts;
    using EzeTest.TestRunner.Enumerations;
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Model.External;
    using EzeTest.TestRunner.Model.Results;
    using EzeTest.TestRunner.TestContent;
    using Microsoft.Extensions.Logging;

    public class HttpCommand : ITestCommand
    {
        private const string HTTP_AUTH_HEADER_KEY = "AuthToken";

        private readonly HttpClient httpClient;
        private readonly HttpMethod httpMethod;
        private readonly TestCommand testCommand;
        private readonly ILogger<HttpCommand> logger;
        private readonly IHttpAuthProvider httpAuthService;

        public HttpCommand(ILogger<HttpCommand> logger, IHttpAuthProvider httpAuthService, HttpMethod httpMethod, TestCommand testCommand)
        {
            this.logger = logger.VerifyIsSet(nameof(logger));
            this.httpMethod = httpMethod.VerifyIsSet(nameof(httpMethod));
            this.testCommand = testCommand.VerifyIsSet(nameof(testCommand));
            this.httpAuthService = httpAuthService.VerifyIsSet(nameof(httpAuthService));

            this.httpClient = new HttpClient();
        }

        public long Id => testCommand.Id;

        public async Task<ITestCommandResult> Execute(ITestContext testContext, CancellationToken cancellationToken)
        {
            testContext.VerifyIsSet(nameof(testContext));
            logger.LogInformation($"Executing {nameof(HttpCommand)} with id {Id}");

            try
            {
                var request = new HttpRequestMessage(httpMethod, testCommand.Url);

                if (testCommand.RequestAuthType == HttpAuthorizationType.OAuth)
                {
                    logger.LogInformation($"Using {testCommand.RequestAuthType} auth");
                    await SetAuthHeaders(testContext, request);
                }

                if (!string.IsNullOrWhiteSpace(testCommand.JsonInputContent))
                {
                    logger.LogInformation("Sending Json input content");

                    request.Content = new
                        StringContent(testCommand.JsonInputContent, Encoding.UTF8, "application/json");
                }

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    return await ProcessResponse(response);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error executing command");
                return new ExceptionResult(ex);
            }
        }

        private async Task SetAuthHeaders(ITestContext testContext, HttpRequestMessage request)
        {
            if (!testContext.HasValidAuthToken())
            {
                logger.LogInformation("HasValidAuthToken: false");

                try
                {
                    HttpSession session = await httpAuthService.Authenticate();
                    if (session == null)
                        throw new ApplicationException("Authenticate response is Null");

                    testContext.SetAuthSession(session);
                    logger.LogInformation("Successfully aquired a new token");
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error creating Http session", ex);
                }
            }

            if (string.IsNullOrEmpty(testContext.HttpAuthToken))
                throw new ApplicationException("Context HttpAuthToken IsNullOrEmpty");

            request.Headers.Add(HTTP_AUTH_HEADER_KEY, testContext.HttpAuthToken);
            logger.LogDebug($"{HTTP_AUTH_HEADER_KEY}: {testContext.HttpAuthToken}");
        }

        private async Task<ITestCommandResult> ProcessResponse(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var failedResult = new HttpFailedRequestResult(testCommand.Url, response.StatusCode, response.ReasonPhrase, responseContent);
                logger.LogWarning($"{Logging.COMMAND_FAILURE}: IsSuccessStatusCode: false; {failedResult}");
                return failedResult;
            }

            if (testCommand.ExpectedResult != null)
            {
                ITestContent jsonContent = JsonContent.LoadFromString(responseContent);
                ITestContentComparison comparisonResult = jsonContent.CompareTo(testCommand.ExpectedResult, testCommand.ExpectedResultComparisonType);
                if (comparisonResult == null)
                    throw new ApplicationException("Comparison result is NULL");

                if (comparisonResult.AreEqual)
                    logger.LogInformation($"{Logging.COMMAND_PASS}: ComparisonResult");
                else
                    logger.LogWarning($"{Logging.COMMAND_FAILURE}: ComparisonResult; {comparisonResult}");

                return new TestContentComparisonResult(comparisonResult);
            }

            logger.LogInformation($"{Logging.COMMAND_PASS}: TestNoContentResult");
            return new TestNoContentResult();
        }
    }
}
