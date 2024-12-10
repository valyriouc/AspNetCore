using System.Text.Json;
using System.Text.Json.Serialization;

namespace LearningPlatform;

public class AppConfig
{
    public static string ConnectionString { get; private set; }

    public static async Task LoadAsync(string filePath, CancellationToken cancellationToken)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
        
        ConfigWrapper? wrapper = JsonSerializer.Deserialize<ConfigWrapper>(await File.ReadAllTextAsync(filePath, cancellationToken));
        if (wrapper is null)
        {
            throw new JsonException("Could not deserialize config file");
        }
        ConnectionString = wrapper.ConnectionString;
    }
}

[method: JsonConstructor]
file class ConfigWrapper(string connectionString)
{
    [JsonPropertyName("connection_string")]
    public string ConnectionString { get; } = connectionString;
}