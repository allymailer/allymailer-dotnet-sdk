using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// EmailSubscribeRequest
/// </summary>
public class EmailSubscribeRequest
{
    /// <summary>
    /// Адреса відправника на підтвердженому домені. (Також підтримується іменування поля як<br/>
    /// "email_address_from").
    /// </summary>
    [JsonPropertyName("from_email")]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Ім'я відправника. (Також підтримується іменування поля як "name_from").
    /// </summary>
    [JsonPropertyName("from_name")]
    public string? FromName { get; set; }

    /// <summary>
    /// Адреса отримувача. (Також підтримується іменування поля як "email_address_to").
    /// </summary>
    [JsonPropertyName("to_email")]
    public string? ToEmail { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
