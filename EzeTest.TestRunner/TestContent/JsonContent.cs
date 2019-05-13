namespace EzeTest.TestRunner.TestContent
{
    using System;
    using Newtonsoft.Json.Linq;

    public class JsonContent : BaseTestContent
    {
        private JsonContent(JToken jsonObject) : base(jsonObject)
        {
        }

        public static ITestContent LoadFromString(string stringContent)
        {
            try
            {
                var jsonObject = JObject.Parse(stringContent);
                return new JsonContent(jsonObject);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to Parse Json content", ex);
            }
        }
    }
}
