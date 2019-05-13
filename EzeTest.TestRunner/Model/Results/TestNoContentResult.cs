using System;

namespace EzeTest.TestRunner.Model.Results
{
    public class TestNoContentResult : ITestCommandResult
    {
        public bool ExecutedSuccessfully => true;
    }
}
