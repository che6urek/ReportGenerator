using System.Collections.Generic;

namespace ReportGenerator.Entity.Table
{
    public class Faculties : Table<Faculty>
    {
        public Faculties(List<Faculty> faculties) : base(faculties)
        {

        }

        public Faculties()
        {

        }
    }
}