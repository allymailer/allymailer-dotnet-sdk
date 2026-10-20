using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// ProjectListResponse
/// </summary>
public class ProjectListResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Масив об'єктів, де кожен об'єкт відповідає окремому проєкту.
    /// </summary>
    [JsonPropertyName("projects")]
    public List<ProjectListObject>? Projects { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
