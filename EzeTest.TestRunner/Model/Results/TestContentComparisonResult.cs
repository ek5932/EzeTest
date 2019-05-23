namespace EzeTest.TestRunner.Model.Results
{
    using EzeTest.Framework.Contracts;
    using EzeTest.TestRunner.TestContent;

    public class TestContentComparisonResult : ITestCommandResult
    {
        private readonly ITestContentComparison comparisonResult;

        public TestContentComparisonResult(ITestContentComparison comparisonResult)
        {
            this.comparisonResult = comparisonResult.VerifyIsSet(nameof(comparisonResult));
        }

        public bool ExecutedSuccessfully => this.comparisonResult.AreEqual;
    }
}
