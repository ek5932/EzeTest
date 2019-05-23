namespace EzeTest.TestRunner.TestContent
{
    using System;
    using EzeTest.TestRunner.Enumerations;
    using EzeTest.TestRunner.Model;
    using Newtonsoft.Json.Linq;

    /// <remarks>
    /// This should be generic enough to support all data types but for now
    /// it is limited to JSON for ease of development.
    /// </remarks>
    public abstract class BaseTestContent : ITestContent
    {
        public BaseTestContent(JToken jsonObjectToken)
        {
            this.JsonObject = jsonObjectToken;
        }

        public JToken JsonObject { get; }

        public ITestContentComparison CompareTo(ITestContent compareTo, ContentComparisonType contentComparisonType)
        {
            if (contentComparisonType == ContentComparisonType.Exactly)
            {
                bool areEqual = JToken.DeepEquals(this.JsonObject, compareTo.JsonObject);
                return new TestContentComparison(areEqual);
            }

            throw new NotSupportedException($"Unknown ContentComparisonType: {contentComparisonType}");
        }
    }
}
