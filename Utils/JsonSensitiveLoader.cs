using System;
using System.IO;
using System.Text.Json;

namespace Vaulty.Utils
{
    public class JsonSensitiveLoader
    {
        public static string TokenLoad()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, Const.TOKEN_FILE);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Secrets file not found.");

            string json = File.ReadAllText(filePath);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Token", out JsonElement tokenElement))
                throw new Exception("Token property missing in secrets file.");

            return tokenElement.GetString();
        }

        public static string PrefixLoad()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, Const.TOKEN_FILE);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Secrets file not found.");

            string json = File.ReadAllText(filePath);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Prefix", out JsonElement tokenElement))
                throw new Exception("Prefix property missing in secrets file.");

            return tokenElement.GetString();
        }
    }
}
