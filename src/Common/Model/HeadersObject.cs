using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// Об'єкт, що містить додаткові заголовки листа, максимум 50 заголовків. Префікс “X-” в імені заголовка<br/>
/// обов'язковий, заголовки без нього будуть проігноровані, наприклад X-ALLYMAILER-Global-Language,<br/>
/// X-ALLYMAILER-Template-Engine. Стандартні заголовки "To", "CC", "BCC" передаються без префікса "X-",<br/>
/// але обробляються спеціальним чином, тому мають низку обмежень, докладніше про це можна прочитати<br/>
/// [тут](/cc-and-bcc). Також якщо [техпідтримка](https://cp.allymailer.com/ua/support) схвалила вам<br/>
/// вимкнення посилання відписки, то ви також можете передавати заголовки List-Unsubscribe,<br/>
/// List-Subscribe, List-Help, List-Owner, List-Archive, In-Reply-To і References.
/// <para/>
/// Any other header goes to Extra: ["X-MyHeader"] = "value", ["CC"], ["List-Unsubscribe"], ...; a key<br/>
/// of a named property is sent from the property, not from Extra. Unknown keys of a response are read<br/>
/// into Extra (as JsonElement values).
/// </summary>
public class HeadersObject
{
    /// <summary>
    /// Заголовок для вибору мови посилання та сторінки відписки. Припустимі значення "be", "de", "en",<br/>
    /// "es", "fr", "it", "pl", "pt", "ru", "ua", "kz".
    /// </summary>
    [JsonPropertyName("X-ALLYMAILER-Global-Language")]
    public string? XALLYMAILERGlobalLanguage { get; set; }

    /// <summary>
    /// Заголовок для вибору шаблонізатора — або “simple”, або “velocity”, або “liquid”, або “none”. За<br/>
    /// замовчуванням — “simple”. Має пріоритет над значенням, що передається в параметрі “template_engine”.
    /// </summary>
    [JsonPropertyName("X-ALLYMAILER-Template-Engine")]
    public string? XALLYMAILERTemplateEngine { get; set; }

    private static readonly string[] NamedKeys = { "X-ALLYMAILER-Global-Language", "X-ALLYMAILER-Template-Engine" };

    /// <summary>
    /// Keys beyond the named properties, sent merged into the object: string values ("X-MyHeader" = "value").
    /// Unknown keys of a response are read into it as JsonElement values (ToString() gives a string value).
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object>? Extra { get; set; }

    /// <summary>Extra as the serializer sees it: without the keys of the named properties.</summary>
    [JsonExtensionData]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dictionary<string, object>? ExtensionData
    {
        get => Extra == null || !Extra.Keys.Any(key => Array.IndexOf(NamedKeys, key) >= 0)
            ? Extra
            : Extra.Where(e => Array.IndexOf(NamedKeys, e.Key) < 0).ToDictionary(e => e.Key, e => e.Value);
        set => Extra = value;
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
