using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Якість трафіку для статусів "opened" і "clicked". "human" — розпізнано звичайний браузер або<br/>
/// поштовий клієнт без машинних ознак; "machine" — бот, сканер посилань або проксі безпеки; "protected"<br/>
/// — механізм приватності скриньки/ОС, участь людини підтвердити не можна (Apple Mail Privacy<br/>
/// Protection / iCloud Private Relay, Gmail Image Proxy; Apple MPP — це protected, не machine);<br/>
/// "unknown" — User-Agent відсутній, пошкоджений або не розпізнаний.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<TrafficType>))]
public enum TrafficType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("human")]
    Human,

    [EnumValue("machine")]
    Machine,

    [EnumValue("protected")]
    Protected,

    [EnumValue("unknown")]
    Unknown,
}
