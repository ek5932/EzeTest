namespace EzeTest.Framework.Http
{
    using System.Threading.Tasks;

    public class HttpAuthProvider : IHttpAuthProvider
    {
        public Task<HttpSession> Authenticate()
        {
            throw new System.NotImplementedException();
        }
    }
}
