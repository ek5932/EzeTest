namespace EzeTest.TestRunner.Model.External
{
    using EzeTest.TestRunner.Enumerations;
    using EzeTest.TestRunner.TestContent;

    public class TestCommand
    {
        public int Id { get; set; }
        public TestCommandType Type { get; set; }
        public string Url { get; set; }
        public string JsonInputContent { get; set; }
        public ITestContent ExpectedResult { get; set; }
        public ContentComparisonType ExpectedResultComparisonType { get; set; }
        public HttpAuthorizationType RequestAuthType { get; set; }
    }
}
