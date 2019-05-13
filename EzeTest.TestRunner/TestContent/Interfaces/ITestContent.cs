using EzeTest.TestRunner.Enumerations;
using Newtonsoft.Json.Linq;

namespace EzeTest.TestRunner.TestContent
{
    public interface ITestContent
    {
        JToken JsonObject { get; }

        ITestContentComparison CompareTo(ITestContent compareTo, ContentComparisonType contentComparisonType);
    }
}
