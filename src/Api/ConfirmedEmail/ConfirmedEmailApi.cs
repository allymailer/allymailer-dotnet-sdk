using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.ConfirmedEmail.Model;
using AllyMailer.Client;

namespace AllyMailer.Api.ConfirmedEmail;

/// <summary>
/// Керування email-адресами, підтвердженими для надсилання із sandbox-домену. Методи доступні лише з<br/>
/// API-ключем основного облікового запису.
/// </summary>
public class ConfirmedEmailApi
{
    private readonly ApiClient _client;

    public ConfirmedEmailApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Adds an email address and sends a confirmation link.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailCreateResponse> CreateAsync(ConfirmedEmailRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailCreateResponse>("/confirmed-email/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Lists confirmed and unconfirmed email addresses for the account.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailListResponse>("/confirmed-email/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Deletes an email address from the account's confirmed email list.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailDeleteResponse> DeleteAsync(ConfirmedEmailRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailDeleteResponse>("/confirmed-email/delete.json", request, cancellationToken);
    }
}
