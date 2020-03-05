using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ReportGenerator
{
    public static class JsonParser
    {
        /// <summary>
        /// Reads JSON from file
        /// </summary>
        /// <param name="path">Path to data file</param>
        /// <returns>JSON object with data</returns>
        public static JObject GetJsonFromFile(string path)
        {
            return JObject.Parse(File.ReadAllText(path));
        }

        /// <summary>
        /// Validates JSON data with provided schema
        /// </summary>
        /// <param name="data">JSON data to validate</param>
        /// <param name="schemaPath">Path to schema file</param>
        /// <returns>Validation result</returns>
        public static bool ValidateJson(JObject data, string schemaPath)
        {
            return data.IsValid(JSchema.Parse(File.ReadAllText(schemaPath)));
        }

    }
}
