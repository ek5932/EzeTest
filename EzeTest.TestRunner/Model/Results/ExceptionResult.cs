namespace EzeTest.TestRunner.Model.Results
{
    using System;

    public class ExceptionResult : ITestCommandResult
    {
        public ExceptionResult(Exception exception)
        {

        }

        public bool ExecutedSuccessfully => false;
    }
}
