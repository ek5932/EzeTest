namespace EzeTest.TestRunner.TestContent
{
    using EzeTest.TestRunner.Enumerations;
    using Newtonsoft.Json.Linq;

    public interface ITestContent
    {
        JToken JsonObject { get; }

        ITestContentComparison CompareTo(ITestContent compareTo, ContentComparisonType contentComparisonType);
    }
}
