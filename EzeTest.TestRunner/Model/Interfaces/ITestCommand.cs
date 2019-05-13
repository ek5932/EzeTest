namespace EzeTest.TestRunner.Model
{
    using System.Threading;
    using System.Threading.Tasks;
    using EzeTest.TestRunner.Model.Results;

    public interface ITestCommand
    {
        long Id { get; }

        Task<ITestCommandResult> Execute(ITestContext testContext, CancellationToken cancellationToken);
    }
}
