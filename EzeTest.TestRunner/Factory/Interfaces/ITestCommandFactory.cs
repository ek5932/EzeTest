namespace EzeTest.TestRunner.Factory
{
    using EzeTest.TestRunner.Model;
    using EzeTest.TestRunner.Model.External;

    public interface ITestCommandFactory
    {
        ITestCommand Create(TestCommand testCommand);
    }
}
