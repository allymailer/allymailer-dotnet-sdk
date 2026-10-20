using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.System.Model;
using AllyMailer.Client;

namespace AllyMailer.Api.System;

/// <summary>
/// Системні методи.
/// </summary>
public class SystemApi
{
    private readonly ApiClient _client;

    public SystemApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Перевіряє валідність API-ключа та повертає ідентифікатор користувача.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#system-ping
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SystemPingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SystemPingResponse>("/system/ping.json", null, cancellationToken);
    }

    /// <summary>
    /// Повертає інформацію про користувача або проєкт за API-ключем.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#system-info
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SystemInfoResponse> InfoAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SystemInfoResponse>("/system/info.json", null, cancellationToken);
    }
}
