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

        public static (string name, string description, string version, int bonus) BotInfoLoad()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, Const.INFO_FILE);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Bot info file not found.");

            string json = File.ReadAllText(filePath);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("name", out JsonElement nameElement))
                throw new Exception("Name property missing in bot info file.");

            if (!root.TryGetProperty("description", out JsonElement descriptionElement))
                throw new Exception("Description property missing in bot info file.");

            if (!root.TryGetProperty("version", out JsonElement versionElement))
                throw new Exception("Version property missing in bot info file.");

            if (!root.TryGetProperty("bonus", out JsonElement bonus))
                throw new Exception("Bonus property missing in bot info file.");

            return (nameElement.GetString(), descriptionElement.GetString(), versionElement.GetString(), bonus.GetInt32());
        }
    }
}
