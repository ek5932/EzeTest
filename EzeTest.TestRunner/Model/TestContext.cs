namespace EzeTest.TestRunner.Model
{
    using EzeTest.Framework.Contracts;
    using EzeTest.Framework.Http;

    public class TestContext : ITestContext
    {
        private HttpSession httpSession;

        public string HttpAuthToken => httpSession.Token;

        public bool HasValidAuthToken() => !(httpSession == null || httpSession.HasExpired);

        public void SetAuthSession(HttpSession httpSession)
        {
            httpSession.VerifyIsSet(nameof(httpSession));
            this.httpSession = httpSession;
        }
    }
}
