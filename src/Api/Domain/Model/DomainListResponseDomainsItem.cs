using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// DomainListResponseDomainsItem
/// </summary>
public class DomainListResponseDomainsItem
{
    /// <summary>
    /// Ім'я домену.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Об'єкт, що описує запис перевірки володіння та її статус.
    /// </summary>
    [JsonPropertyName("verification-record")]
    public DomainListResponseDomainsItemVerificationRecord? VerificationRecord { get; set; }

    /// <summary>
    /// Об'єкт, що описує DKIM-запис та її статус.
    /// </summary>
    [JsonPropertyName("dkim")]
    public DomainListResponseDomainsItemDkim? Dkim { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
