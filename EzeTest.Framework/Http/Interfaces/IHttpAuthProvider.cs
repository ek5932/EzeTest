namespace EzeTest.Framework.Http
{
    using System.Threading.Tasks;

    public interface IHttpAuthProvider
    {
        Task<HttpSession> Authenticate();
    }
}
