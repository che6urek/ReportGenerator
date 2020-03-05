using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportGenerator.Entity.Table;

namespace ReportGenerator
{
    public static class ReportGenerator
    {
        private static readonly Dictionary<string, string> StudentReplacements
            = new Dictionary<string, string>
        {
            { "firstName", "" },
            { "lastName", "" },
            { "university", "" },
            { "faculty", "" }
        };

        private static readonly Dictionary<string, string> ReportReplacements
            = new Dictionary<string, string>
        {
            { "date",  "" },
            { "students", "" }
        };

        /// <summary>
        /// Fill template with provided values
        /// </summary>
        /// <param name="template">Format to fill</param>
        /// <param name="replacements">Values to insert</param>
        /// <returns>Filled template</returns>
        public static string FormatFromDictionary(string template, Dictionary<string, string> replacements)
        {
            var i = 0;
            var formatString = new StringBuilder(template);
            var keyToInt = new Dictionary<string, int>();
            foreach (var tuple in replacements)
            {
                formatString = formatString.Replace("{" + tuple.Key + "}", "{" + i + "}");
                keyToInt.Add(tuple.Key, i);
                i++;
            }
            return string.Format(formatString.ToString(), replacements.OrderBy(x => keyToInt[x.Key])
                                                                   .Select(x => x.Value)
                                                                   .ToArray());
        }

        /// <summary>
        /// Generates students records using the provided template
        /// </summary>
        /// <returns>Records with students information</returns>
        public static string GenerateStudentsRecords(Students students, Universities universities,
            Faculties faculties, string studentTemplate)
        {
            var content = new StringBuilder();
            foreach (var student in students)
            {
                StudentReplacements["firstName"] = student.Name;
                StudentReplacements["lastName"] = student.LastName;
                StudentReplacements["university"] = universities.GetById(student.UniversityId).Name;
                StudentReplacements["faculty"] = faculties.GetById(student.FacultyId).Name;
                content.Append(FormatFromDictionary(studentTemplate, StudentReplacements));
            }
            return content.ToString();
        }

        /// <summary>
        /// Generates report using provided templates
        /// </summary>
        /// <returns>Report</returns>
        public static string GenerateReport(Students students, Universities universities,
            Faculties faculties, string studentTemplate, string reportTemplate)
        {
            ReportReplacements["date"] = DateTime.Now.ToString("o");
            ReportReplacements["students"] = GenerateStudentsRecords(students, universities, faculties, studentTemplate);
            return FormatFromDictionary(reportTemplate, ReportReplacements);
        }
    }
}
