using System;
using System.Collections.Generic;
using System.Resources;
using Newtonsoft.Json.Linq;
using ReportGenerator.Entity.Table;
using ReportGenerator.Resources;

namespace ReportGenerator
{
    internal static class Program
    {
        private static Universities Universities = new Universities();
        private static Faculties Faculties = new Faculties();
        private static Students Students = new Students();

        private static Template[] _reportTemplates, _xmlTemplates, _jsonTemplates, _textTemplates;

        /// <summary>
        /// Fill Students, Faculties and Universities from provided JSON object
        /// </summary>
        /// <param name="data">Data to parse</param>
        /// <returns>Error</returns>
        private static bool FillTables(JObject data)
        {
            var ignored = 0;
            try
            {
                ignored += Universities.FillFromJson(data, "universities");
                ignored += Faculties.FillFromJson(data, "faculties");
                ignored += Students.FillFromJson(data, "students");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Can't parse data: {exception.Message}");
                Console.ReadKey();
                return false;
            }

            if (ignored > 0)
            {
                int option = ConsoleService.DisplayMenu(new Dictionary<int, string>
                {
                    {1, "Yes"},
                    {2, "No"}
                }, $"{ignored} entities ignored because of incorrect data. Continue?");
                if (option != 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets criterion from the user and returns students corresponding to it
        /// </summary>
        /// <param name="option">Filter type</param>
        /// <returns>Students by chosen criterion</returns>
        private static Students GetRequiredStudents(int option)
        {
            switch (option)
            {
                case 1:
                {
                    return Students.GetByUniversity(ConsoleService.DisplayMenu(
                        Universities.GetDictionary(), "Select university"));
                }
                case 2:
                {
                    return Students.GetByFaculty(ConsoleService.DisplayMenu(
                        Faculties.GetDictionary(), "Select faculty"));
                }
                case 3:
                {
                    Console.Write("Enter name: ");
                    return Students.GetByLastname(Console.ReadLine());
                }
                case 4:
                {
                    return Students;
                }
                default:
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Provides the user with a choice of template type
        /// </summary>
        /// <param name="templates">Template options</param>
        /// <returns>Index of selected template</returns>
        private static int SelectReportType(Template[] templates)
        {
            var options = new Dictionary<int, string>();
            for (var i = 0; i < templates.Length; i++)
            {
                options.Add(i + 1, templates[i].Name);
            }
            return ConsoleService.DisplayMenu(options, "Select report type") - 1;
        }

        /// <summary>
        /// Gets a report using the provided student list and templates
        /// </summary>
        private static string GetReport(Students students)
        {
            string report = null;

            // Selecting report format (XML, JSON, text)
            int reportType = SelectReportType(_reportTemplates);

            Template[] templates;
            switch (reportType)
            {
                case 0:
                {
                    templates = _xmlTemplates;
                    break;
                }
                case 1:
                {
                    templates = _jsonTemplates;
                    break;
                }
                case 2:
                {
                    templates = _textTemplates;
                    break;
                }
                default:
                {
                    Console.WriteLine("Wrong option");
                    return null;
                }
            }

            // Selecting report element format
            try
            {
                int formatterType = SelectReportType(templates);
                report = ReportGenerator.GenerateReport(students, Universities, Faculties,
                    templates[formatterType].Format, _reportTemplates[reportType].Format);

            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine($"Wrong option: {exception.Message}");
            }
            catch (FormatException exception)
            {
                Console.WriteLine($"Template is incorrect: {exception.Message}");
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine($"Wrong index: {exception.Message}");
            }

            return report;
        }

        /// <summary>
        /// Shows filters menu until the user chooses to exit, gets user selections and generates corresponding report 
        /// </summary>
        private static void DisplayReportMenu()
        {
            int option;
            do
            {
                option = ConsoleService.DisplayMenu(new Dictionary<int, string>
                {
                    {0, "Exit"},
                    {1, "Get students by university"},
                    {2, "Get students by faculty"},
                    {3, "Get students by lastname"},
                    {4, "Get all students" }
                });

                Students requiredStudents = GetRequiredStudents(option);
                
                if (requiredStudents == null || requiredStudents.Items.Count == 0)
                {
                    Console.WriteLine("No students found by this criterion");
                }
                else
                {
                    string report = GetReport(requiredStudents);

                    if (report == null)
                    {
                        Console.WriteLine("Can't generate report");
                    }
                    else
                    {
                        ConsoleService.WriteToFile(report);
                    }
                }

                Console.WriteLine();

            } while (option != 0);
        }

        /// <summary>
        /// Loads templates from resources
        /// </summary>
        private static void LoadTemplates()
        {
            _reportTemplates = new Template[3];
            var resourceManager = new ResourceManager(typeof(ReportTemplates));
            
            _reportTemplates[0].Name = "Xml";
            _reportTemplates[0].Format = resourceManager.GetString("Xml");
            _reportTemplates[1].Name = "Json";
            _reportTemplates[1].Format = resourceManager.GetString("Json");
            _reportTemplates[2].Name = "Text";
            _reportTemplates[2].Format = resourceManager.GetString("Text");

            _xmlTemplates = TemplateService.GetTemplates(typeof(XmlTemplates));
            _jsonTemplates = TemplateService.GetTemplates(typeof(JsonTemplates));
            _textTemplates = TemplateService.GetTemplates(typeof(TextTemplates));
        }

        private static void Main(string[] args)
        {
            string dataPath;
            string schemaPath;

            if (args.Length > 2) // Get paths from cmd arguments
            {
                dataPath = args[0];
                schemaPath = args[1];
            }
            else // Get paths from console
            {
                dataPath = ConsoleService.GetFilePath("data file");
                schemaPath = ConsoleService.GetFilePath("validation schema file");
            }

            if (!ConsoleService.GetJsonData(dataPath, out var data))
            {
                Console.ReadKey();
                return;
            }

            // Validation is optional
            if (!ConsoleService.ValidateJsonData(schemaPath, data))
            {
                var option = ConsoleService.DisplayMenu(new Dictionary<int, string>
                {
                    {1, "Yes"},
                    {2, "No"}
                }, "Validation failed, do you want to continue?");

                if (option != 1)
                {
                    return;
                }
            }

            LoadTemplates();

            if (!FillTables(data))
            {
                Console.ReadKey();
                return;
            }

            DisplayReportMenu();
        }
    }
}
