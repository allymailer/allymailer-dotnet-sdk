using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// Об'єкт із властивостями файлу дампа подій.
/// </summary>
public class EventDumpFilesObject
{
    /// <summary>
    /// Унікальний URL для завантаження файлу з розширенням .csv або .csv.gz, залежно від запитаного<br/>
    /// формату.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Розмір файлу.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
