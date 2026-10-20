using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// Об'єкт, що описує DKIM-запис та її статус.
/// </summary>
public class DomainListResponseDomainsItemDkim
{
    /// <summary>
    /// DKIM-підпис для домену. У цьому полі повертається лише ключ. Щоб DNS-запис був коректним, перед<br/>
    /// ключем потрібно додати "k=rsa, p=" (див.<br/>
    /// [domain/get-dns-records](https://docs.allymailer.com.ua/ua/web-api-ref#domain-get-dns-records)).
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Лише домени зі статусом DKIM-запису "active" можуть використовуватися для надсилання листів.
    /// </summary>
    [JsonPropertyName("status")]
    public DkimStatus? Status { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
