namespace EzeTest.TestRunner.Model
{
    using EzeTest.Framework.Contracts;
    using EzeTest.Framework.Http;

    public class TestContext : ITestContext
    {
        private HttpSession httpSession;

        public string HttpAuthToken => this.httpSession.Token;

        public bool HasValidAuthToken() => !(this.httpSession == null || this.httpSession.HasExpired);

        public void SetAuthSession(HttpSession httpSession)
        {
            httpSession.VerifyIsSet(nameof(httpSession));
            this.httpSession = httpSession;
        }
    }
}
