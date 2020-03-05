using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ReportGenerator.Entity.Table
{
    public class Students: Table<Student>
    {
        public Students(List<Student> students): base(students)
        {
            
        }

        public Students()
        {
            
        }

        public Students GetByUniversity(int universityId)
        {
            return new Students(Items.Where(x => x.UniversityId == universityId).ToList());
        }

        public Students GetByFaculty(int facultyId)
        {
            return new Students(Items.Where(x => x.FacultyId == facultyId).ToList());
        }

        public Students GetByLastname(string lastName)
        {
            return new Students(Items.Where(x => x.LastName == lastName).ToList());
        }

        public override int FillFromJson(JObject data, string arrayName)
        {
            var ignored = 0;

            foreach (var item in (JArray)data[arrayName])
            {
                var student = new Student
                {
                    Id = (int) item["id"], Name = (string) item["firstName"],
                    LastName = (string) item["lastName"], UniversityId = (int) item["universityId"],
                    FacultyId = (int) item["facultyId"]
                };

                if (GetById(student.Id) == null)
                {
                    Add(student);
                }
                else
                {
                    ignored++;
                }
            }

            return ignored;
        }
    }
}
