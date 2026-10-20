# Email

[До списку розділів](../README.md)

Ця група методів пов'язана з надсиланням email.

`AllyMailer.Api.Email.EmailApi`, `client.Email`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`SendAsync`](#sendasync) | `POST /email/send.json` | Метод для надсилання листів вашим підписникам. |
| [`SubscribeAsync`](#subscribeasync) | `POST /email/subscribe.json` | Надіслати посилання на (пере)підписку. |

## SendAsync

`POST /email/send.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#email-send)

Метод для надсилання листів вашим підписникам.

Під час надсилання ви можете використовувати підстановки змінних, шаблони, увімкнути відстеження прочитань або переходів за посиланнями та багато іншого.

Обмеження:
* максимальний розмір запиту — 10 MB
* максимальна кількість отримувачів (recipients) — 500 email
* у назві вкладень заборонено символ '/'

Якщо під час надсилання листа у списку отримувачів будуть некоректно введені, неіснуючі, повторювані або відписані адреси, вони будуть повернуті у відповіді за допомогою параметра failed_emails, лист же буде надіслано на всі інші прийнятні адреси. Якщо всі адреси потраплять до failed_emails, надсилання здійснено не буде, буде повернуто помилку з API-кодом 204.

### Запит: `EmailSendRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Message` | `MessageObject` | так | Об'єкт, що містить усі властивості повідомлення, яке надсилається. |

### Відповідь: `EmailSendResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `JobId` | `string` | Ідентифікатор завдання з надсилання, може виявитися корисним під час з'ясування причин збоїв. |
| `Emails` | `List<string>` | Масив email-адрес, успішно прийнятих до надсилання. |
| `FailedEmails` | `Dictionary<string, string>` | Об'єкт із email-адресами, не прийнятими до надсилання. Email представлено іменем властивості, а його статус — значенням властивості, наприклад: {"email1@gmail.com": "temporary_unavailable"}. Можливі статуси адрес, на які не вдалося здійснити надсилання:<br><br>- __unsubscribed__ — зазначену адресу відписано;<br>- __invalid__ — адреса не існує або введена некоректно;<br>- __duplicate__ — адреса повторюється в запиті, повторне надсилання на той самий email запобігнуто;<br>- __temporary_unavailable__ — адреса тимчасово недоступна. Це означає, що протягом наступних трьох діб надсилання на цю адресу повертатиме помилку. Email може бути тимчасово недоступним з різних причин, наприклад:<br><br>- попереднє надсилання було відхилено сервером отримувача як спам;<br>- поштова скринька отримувача переповнена або не використовується;<br>- домен не приймає пошту через неправильне налаштування на боці отримувача або з інших причин;<br>- сервер відправника було відхилено через блеклистинг;<br><br>- __permanent_unavailable__ — адреса перманентно недоступна через багаторазові недоставки;<br>- __complained__ — в одному з попередніх листів адресат натиснув “Це спам”;<br>- __blocked__ — надсилання на цю адресу заборонено адміністрацією AllyMailer.<br>У майбутньому можуть з'явитися й інші статуси. |

### Приклад

```csharp
using System.Collections.Generic;
using AllyMailer;
using AllyMailer.Api.Email.Model;
using AllyMailer.Common.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = new List<MessageObjectRecipientsItem>
        {
            new MessageObjectRecipientsItem
            {
                Email = "user@example.com",
                Substitutions = new Dictionary<string, string>
                {
                    ["CustomerId"] = "12452",
                    ["to_name"] = "John Smith",
                },
                Metadata = new Dictionary<string, string>
                {
                    ["campaign_id"] = "c77f4f4e-3561-49f7-9f07-c35be01b4f43",
                    ["customer_hash"] = "b253ac7",
                },
            },
        },
        TemplateId = "00000000-0000-0000-0000-000000000000",
        GlobalLanguage = "en",
        TemplateEngine = TemplateEngine.Velocity,
        Body = new BodyObject
        {
            Html = "<b>Hello, {{to_name}}</b>",
            Plaintext = "Hello, {{to_name}}",
            Amp = "<!doctype html><html amp4email><head> <meta charset=\"utf-8\"><script async src=\"https://cdn.ampproject.org/v0.js\"></script> <style amp4email-boilerplate>body{visibility:hidden}</style></head><body> Hello, AMP4EMAIL world.</body></html>",
        },
        Subject = "AllyMailer test email ",
        FromEmail = "email@example.com",
        FromName = "John Smith",
        ReplyTo = "email@example.com",
        ReplyToName = "John Smith",
        IdempotenceKey = "SG1VsbG68sIH2dvc5mx890kIQ",
    },
});
Console.WriteLine(response.JobId);
```

## SubscribeAsync

`POST /email/subscribe.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#email-subscribe)

Надіслати посилання на (пере)підписку.

Метод дозволяє повторно підписати колишнього підписника, який відписався раніше, був заблокований через скарги або чия адреса недоступна з якоїсь іншої причини.

Під час виклику надсилається лист у нейтральному дизайні з посиланням підписки. При переході за посиланням з email підписник перестає бути недоступним для користувача та/або перестає бути глобально відписаним.

За замовчуванням дозволено лише одне надсилання листа повторної підписки на добу. Якщо вам потрібно більше — напишіть у техпідтримку.

### Запит: `EmailSubscribeRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `FromEmail` | `string` | так | Адреса відправника на підтвердженому домені. (Також підтримується іменування поля як "email_address_from"). |
| `FromName` | `string` | так | Ім'я відправника. (Також підтримується іменування поля як "name_from"). |
| `ToEmail` | `string` | так | Адреса отримувача. (Також підтримується іменування поля як "email_address_to"). |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Email.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Email.SubscribeAsync(new EmailSubscribeRequest
{
    FromEmail = "email@example.com",
    FromName = "Example",
    ToEmail = "email@example.com",
});
Console.WriteLine(response);
```
