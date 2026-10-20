using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// AttachmentsArrayItem
/// </summary>
public class AttachmentsArrayItem
{
    /// <summary>
    /// Тип вкладення, див. [MIME](https://en.wikipedia.org/wiki/MIME). Якщо не впевнені, використовуйте<br/>
    /// "application/octet-stream".
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Назва вкладення у форматі: "ім'я.розширення". У разі передачі кількох вкладень їхні назви мають бути<br/>
    /// унікальними. У назві вкладень заборонено символ ‘/’.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Вміст файлу у [base64](https://en.wikipedia.org/wiki/Base64). Максимальний розмір файлу 7 Мб<br/>
    /// (9786710 байт у base64).
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
