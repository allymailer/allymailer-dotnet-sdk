using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Статус webhook, за замовчуванням "active". Статус "disabled" означає, що webhook було вимкнено<br/>
/// користувачем, "stopped" означає, що webhook було зупинено системою після 24 годин невдалих викликів<br/>
/// (мінімум за 10 різними подіями).
/// </summary>
[JsonConverter(typeof(StringEnumConverter<WebhookStatus>))]
public enum WebhookStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("active")]
    Active,

    [EnumValue("disabled")]
    Disabled,

    [EnumValue("stopped")]
    Stopped,
}
