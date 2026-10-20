using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// ProjectCreateResponse
/// </summary>
public class ProjectCreateResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор проєкту, ASCII-рядок до 36 символів завдовжки.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// API-ключ проєкту. Ви можете використовувати цей ключ замість користувацького api_key у всіх методах,<br/>
    /// крім project-методів.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
