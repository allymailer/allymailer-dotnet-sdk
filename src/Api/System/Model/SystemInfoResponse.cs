using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.System.Model;

/// <summary>
/// SystemInfoResponse
/// </summary>
public class SystemInfoResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор користувача.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Email користувача.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор проєкту, ASCII-рядок до 36 символів завдовжки. Присутній лише якщо<br/>
    /// API-ключ, використаний для запиту, був API-ключем проєкту.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Назва проєкту, унікальна в межах облікового запису користувача. Присутня лише якщо API-ключ,<br/>
    /// використаний для запиту, був API-ключем проєкту.
    /// </summary>
    [JsonPropertyName("project_name")]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Об'єкт із властивостями лічильника листів. Присутній лише якщо API-ключ, використаний для запиту,<br/>
    /// був API-ключем проєкту.
    /// </summary>
    [JsonPropertyName("project_accounting")]
    public ProjectAccountingObject? ProjectAccounting { get; set; }

    /// <summary>
    /// Об'єкт з властивостями періоду обліку. Присутній лише якщо API-ключ, використаний для запиту, був<br/>
    /// API-ключем користувача.
    /// </summary>
    [JsonPropertyName("accounting")]
    public SystemInfoResponseAccounting? Accounting { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
