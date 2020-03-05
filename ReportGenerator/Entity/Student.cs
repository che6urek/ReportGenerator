namespace ReportGenerator.Entity
{
    public class Student: Entity
    {
        public string LastName { get; set; }
        public int UniversityId { get; set; }
        public int FacultyId { get; set; }

        public Student(int id, string name, string lastName, int universityId, int facultyId): base(id, name)
        {
            LastName = lastName;
            UniversityId = universityId;
            FacultyId = facultyId;
        }

        public Student()
        {
           
        }
    }
}
