using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// Параметр для вибору [шаблонізатора](https://docs.allymailer.com.ua/en/template-engines) — або<br/>
/// "simple", або "velocity", або "liquid", або "none". За замовчуванням — "simple".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<TemplateEngine>))]
public enum TemplateEngine
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("simple")]
    Simple,

    [EnumValue("velocity")]
    Velocity,

    [EnumValue("liquid")]
    Liquid,

    [EnumValue("none")]
    None,
}
