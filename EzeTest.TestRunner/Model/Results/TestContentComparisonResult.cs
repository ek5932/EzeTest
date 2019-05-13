using EzeTest.Framework.Contracts;
using EzeTest.TestRunner.TestContent;

namespace EzeTest.TestRunner.Model.Results
{
    public class TestContentComparisonResult : ITestCommandResult
    {
        private readonly ITestContentComparison comparisonResult;

        public TestContentComparisonResult(ITestContentComparison comparisonResult)
        {
            this.comparisonResult = comparisonResult.VerifyIsSet(nameof(comparisonResult));
        }

        public bool ExecutedSuccessfully => comparisonResult.AreEqual;
    }
}
