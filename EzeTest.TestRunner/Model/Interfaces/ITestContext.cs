namespace EzeTest.TestRunner.Model
{
    using EzeTest.Framework.Http;

    public interface ITestContext
    {
        string HttpAuthToken { get; }

        bool HasValidAuthToken();

        void SetAuthSession(HttpSession session);
    }
}
