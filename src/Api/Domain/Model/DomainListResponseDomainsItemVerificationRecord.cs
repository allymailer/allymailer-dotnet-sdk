using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// Об'єкт, що описує запис перевірки володіння та її статус.
/// </summary>
public class DomainListResponseDomainsItemVerificationRecord
{
    /// <summary>
    /// Запис, який потрібно внести "як є" для підтвердження володіння цим доменом.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    /// <summary>
    /// Лише домени зі статусом "confirmed" запису про володіння доменом можуть використовуватися для<br/>
    /// надсилання листів.
    /// </summary>
    [JsonPropertyName("status")]
    public VerificationRecordStatus? Status { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
