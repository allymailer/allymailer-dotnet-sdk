using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// ProjectListRequest
/// </summary>
public class ProjectListRequest
{
    /// <summary>
    /// Опціональна можливість указати project_id для включення до списку лише вказаного проєкту.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Опціональна можливість указати project_api_key для включення до списку лише вказаного проєкту.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
