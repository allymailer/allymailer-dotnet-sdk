using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// DomainGetDnsRecordsResponse
/// </summary>
public class DomainGetDnsRecordsResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Домен, для якого потрібно отримати інформацію про DNS-записи.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Запис, який потрібно внести "як є" для підтвердження володіння цим доменом.
    /// </summary>
    [JsonPropertyName("verification-record")]
    public string? VerificationRecord { get; set; }

    /// <summary>
    /// DKIM-підпис для домену. У цьому полі повертається лише ключ. Щоб TXT-запис був коректним, перед<br/>
    /// ключем потрібно додати "v=DKIM1; k=rsa; p=".
    /// </summary>
    [JsonPropertyName("dkim")]
    public string? Dkim { get; set; }

    /// <summary>
    /// Значення CNAME-запису для хоста "_dmarc" домену.
    /// </summary>
    [JsonPropertyName("dmarc")]
    public string? Dmarc { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
