using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Template.Model;

/// <summary>
/// Тип редактора шаблону у вебінтерфейсі, "html" або "visual". За замовчуванням — "html". Створення<br/>
/// шаблонів із типом редактора "visual" можливе лише через вебінтерфейс.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EditorType>))]
public enum EditorType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("html")]
    Html,

    [EnumValue("visual")]
    Visual,
}
