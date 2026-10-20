using System;

namespace AllyMailer.Client;

/// <summary>
/// API connection settings.
/// </summary>
public class Configuration
{
    /// <summary>Base URL of the API.</summary>
    public const string DefaultBaseUrl = "https://cp.allymailer.com/ua/transactional/api/v1";

    /// <summary>API host cp.allymailer.com.</summary>
    public const string HostCp = "cp.allymailer.com";

    /// <summary>Path of the API on every host.</summary>
    public const string BasePath = "/ua/transactional/api/v1";

    /// <summary>
    /// Base URL for an API host: "eu1.unione.io" -> "https://eu1.unione.io" + BasePath; a full URL is kept as is.
    /// </summary>
    public static string BaseUrlFor(string host)
    {
        return host.Contains("://") ? host.TrimEnd('/') : "https://" + host.Trim('/') + BasePath;
    }

    public Configuration(string apiKey)
    {
        ApiKey = apiKey;
    }

    /// <summary>User or project API key, sent in the X-API-KEY header.</summary>
    public string ApiKey { get; set; }

    /// <summary>Base URL of the API.</summary>
    public string BaseUrl { get; set; } = DefaultBaseUrl;

    /// <summary>Timeout of a request, also with a custom HttpClient; zero or Timeout.InfiniteTimeSpan: none.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>User-Agent header value.</summary>
    public string UserAgent { get; set; } = "allymailer-dotnet-sdk/2.0.0";

    /// <summary>Package that uses the SDK and its version, e.g. "aspnetcore.1.0.0": goes to the "platform" query parameter
    /// before the SDK one (ApiClient.Platform).</summary>
    public string? Platform { get; set; }
}
