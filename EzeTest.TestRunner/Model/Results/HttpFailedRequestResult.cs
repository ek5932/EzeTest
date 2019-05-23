namespace EzeTest.TestRunner.Model.Results
{
    using System.Net;

    public class HttpFailedRequestResult : ITestCommandResult
    {
        public HttpFailedRequestResult(string url, HttpStatusCode httpStatusCode, string reaseonPhrase, string responseContent)
        {
        }

        public bool ExecutedSuccessfully => false;

        public override string ToString()
        {
            return $"TODO";
        }
    }
}
