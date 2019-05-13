namespace EzeTest.TestRunner.Model.External
{
    using System.Collections.Generic;

    public class TestDefinition : List<TestCommand>
    {
        public TestDefinition(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
