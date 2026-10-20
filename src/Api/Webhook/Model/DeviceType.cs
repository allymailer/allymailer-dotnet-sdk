using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Тип пристрою отримувача, визначений за user agent. Присутній для статусів "clicked" і "opened";<br/>
/// дорівнює "unknown", якщо тип пристрою визначити не вдалося.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DeviceType>))]
public enum DeviceType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("desktop")]
    Desktop,

    [EnumValue("mobile")]
    Mobile,

    [EnumValue("tablet")]
    Tablet,

    [EnumValue("other")]
    Other,

    [EnumValue("unknown")]
    Unknown,
}
