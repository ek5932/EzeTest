using System.Collections.Generic;

namespace EzeTest.TestRunner.Model
{
    public class Test : List<ITestCommand>
    {
        public Test(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
