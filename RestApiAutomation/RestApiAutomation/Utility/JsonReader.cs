using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestApiAutomation.Utility
{
    public class JsonReader
    {
        public string ReadJson(string fileName, object itemName)
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var actualPath = path.Substring(0, path.LastIndexOf("bin", StringComparison.Ordinal));
            var projectPath = new Uri(actualPath).LocalPath;
            var reportPath = projectPath + "\\Data\\" + fileName;

            var file = File.OpenText(reportPath);
            var reader = new JsonTextReader(file);
            {
                var readerObject = (JObject)JToken.ReadFrom(reader);
                return readerObject[itemName].ToString();
            }
        }

        public string ReadJson(string fileName)
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var actualPath = path.Substring(0, path.LastIndexOf("bin", StringComparison.Ordinal));
            var projectPath = new Uri(actualPath).LocalPath;
            var reportPath = projectPath + "\\Data\\" + fileName;

            var file = File.OpenText(reportPath);
            var reader = new JsonTextReader(file);
            {
                var readerObject = (JObject)JToken.ReadFrom(reader);
                return readerObject.ToString();

            }
        }
    }
}
