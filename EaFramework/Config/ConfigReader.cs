using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EaFramework.Config;

public static class ConfigReader
{
    public static TestSettings ReadConfig()
    {
        var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                                          + "/appsetting.json");
        var jsonSeriaLizerSettings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        jsonSeriaLizerSettings.Converters.Add(new JsonStringEnumConverter());
        
        return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSeriaLizerSettings);
    }

}