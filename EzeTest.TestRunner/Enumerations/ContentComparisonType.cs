namespace EzeTest.TestRunner.Enumerations
{
    public enum ContentComparisonType
    {
        Unknown,

        /// <summary>
        /// The response properties &amp; values must exactly match the expected data and will fail if additional properties are provided
        /// </summary>
        Exactly,

        /// <summary>
        /// The response data must contain the expected properties, but the test will not fail if additional properties are set
        /// </summary>
        AtLeast
    }
}
