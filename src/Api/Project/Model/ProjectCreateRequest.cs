using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// ProjectCreateRequest
/// </summary>
public class ProjectCreateRequest
{
    /// <summary>
    /// Об'єкт із властивостями проєкту.
    /// </summary>
    [JsonPropertyName("project")]
    public ProjectObject? Project { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
