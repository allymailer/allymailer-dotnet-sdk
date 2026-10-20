using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Tag.Model;

/// <summary>
/// Об'єкт із властивостями користувацьких міток.
/// </summary>
public class TagsResponseObject
{
    /// <summary>
    /// Унікальний id мітки.
    /// </summary>
    [JsonPropertyName("tag_id")]
    public int? TagId { get; set; }

    /// <summary>
    /// Назва мітки.
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
