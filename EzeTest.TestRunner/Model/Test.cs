namespace EzeTest.TestRunner.Model
{
    using System.Collections.Generic;

    public class Test : List<ITestCommand>
    {
        public Test(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
