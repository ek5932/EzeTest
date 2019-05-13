using EzeTest.TestRunner.TestContent;

namespace EzeTest.TestRunner.Model
{
    public class TestContentComparison : ITestContentComparison
    {
        public TestContentComparison(bool areEqual)
        {
            AreEqual = areEqual;
        }

        public bool AreEqual { get; }
    }
}
