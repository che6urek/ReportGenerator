using System.Collections.Generic;

namespace ReportGenerator.Entity.Table
{
    public class Universities: Table<University>
    {
        public Universities(List<University> universities): base(universities)
        {

        }

        public Universities()
        {

        }
    }
}
