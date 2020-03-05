using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReportGenerator
{
    public static class ConsoleService
    {
        /// <summary>
        /// Shows option and gets user selection
        /// </summary>
        /// <param name="options">Provided options</param>
        /// <returns>Selected option</returns>
        public static int DisplayMenu(Dictionary<int, string> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}. {option.Value}");
            }
            Console.Write(">>> ");
            int.TryParse(Console.ReadLine(), out var result);
            Console.WriteLine();
            return result;
        }

        /// <summary>
        /// Shows title, options and gets user selection
        /// </summary>
        /// <param name="options">Provided options</param>
        /// <param name="title">Text to show</param>
        /// <returns>Selected option</returns>
        public static int DisplayMenu(Dictionary<int, string> options, string title)
        {
            Console.WriteLine(title);
            return DisplayMenu(options);
        }

        /// <summary>
        /// Gets file path and writes text there
        /// </summary>
        public static void WriteToFile(string text)
        {
            var path = GetFilePath("save file");
            try
            {
                File.WriteAllText(path, text);
                Console.WriteLine($"Report saved to {path}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Can't save data: {exception.Message}");
            }
        }

        /// <summary>
        /// Gets file path from user
        /// </summary>
        /// <returns>File path</returns>
        public static string GetFilePath(string message)
        {
            Console.Write($"Enter the path of the {message}: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Validates JSON data with provided schema
        /// </summary>
        /// <param name="schemaPath">Path to schema file</param>
        /// <param name="data">JSON data to validate</param>
        /// <returns>Validation result</returns>
        public static bool ValidateJsonData(string schemaPath, JObject data)
        {
            var isValid = false;
            try
            {
                isValid = JsonParser.ValidateJson(data, schemaPath);
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Can't read schema file: {exception.Message}");
            }
            catch (JsonReaderException exception)
            {
                Console.WriteLine($"Can't parse scheme: {exception.Message}");
            }

            return isValid;
        }

        /// <summary>
        /// Reads JSON from file to object
        /// </summary>
        /// <param name="dataPath">Path to data file</param>
        /// <param name="data">Object to store data</param>
        /// <returns>Error</returns>
        public static bool GetJsonData(string dataPath, out JObject data)
        {
            data = null;
            try
            {
                data = JsonParser.GetJsonFromFile(dataPath);
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Can't read data file: {exception.Message}");
                return false;
            }
            catch (JsonReaderException exception)
            {
                Console.WriteLine($"Can't parse data: {exception.Message}");
                return false;
            }

            return true;
        }
    }
}
