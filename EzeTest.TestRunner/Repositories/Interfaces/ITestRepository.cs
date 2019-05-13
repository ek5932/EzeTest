namespace EzeTest.TestRunner.Repositories
{
    using System.Threading.Tasks;
    using EzeTest.TestRunner.Model;

    public interface ITestRepository
    {
        Task<Test> GetById(long testId);
    }
}
