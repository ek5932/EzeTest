namespace EzeTest.TestRunner.Model
{
    using EzeTest.TestRunner.TestContent;

    public class TestContentComparison : ITestContentComparison
    {
        public TestContentComparison(bool areEqual)
        {
            this.AreEqual = areEqual;
        }

        public bool AreEqual { get; }
    }
}
