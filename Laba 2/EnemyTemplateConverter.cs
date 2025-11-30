using laba_2;
using System.Text.Json;
using System.Text.Json.Serialization;

public class EnemyTemplateConverter : JsonConverter<CEnemyTemplate>
{
    public override CEnemyTemplate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (!root.TryGetProperty("$type", out var typeElem))
            throw new JsonException("Missing $type property");

        string typeName = typeElem.GetString();
        string json = doc.RootElement.GetRawText();

        return typeName switch
        {
            nameof(AverageEnemy) => JsonSerializer.Deserialize<AverageEnemy>(json, options),
            nameof(ArmoredEnemy) => JsonSerializer.Deserialize<ArmoredEnemy>(json, options),
            nameof(HealingEnemy) => JsonSerializer.Deserialize<HealingEnemy>(json, options),
            nameof(YkorachEnemy) => JsonSerializer.Deserialize<YkorachEnemy>(json, options),
            _ => throw new NotSupportedException($"Unknown enemy type: {typeName}")
        };
    }

    public override void Write(Utf8JsonWriter writer, CEnemyTemplate value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("$type", value.GetType().Name);
        foreach (var prop in value.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        {
            if (prop.CanRead) 
            {
                object propValue = prop.GetValue(value);
                writer.WritePropertyName(prop.Name);
                JsonSerializer.Serialize(writer, propValue, prop.PropertyType, options);
            }
        }
        writer.WriteEndObject();
    }
}