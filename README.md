# AllyMailer .NET (C#) SDK

[![.NET (C#)](https://img.shields.io/nuget/v/AllyMailer.Sdk)](https://www.nuget.org/packages/AllyMailer.Sdk) [![License: MIT](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

Офіційна бібліотека .NET (C#) для API транзакційних листів [AllyMailer](https://allymailer.com.ua).

[Довідник API](https://docs.allymailer.com.ua/ua/web-api-ref) · Документація для LLM: [llms.txt](https://docs.allymailer.com.ua/llms.txt)

> **ASP.NET Core?** Встановіть [`AllyMailer.AspNetCore`](https://github.com/allymailer/allymailer-aspnetcore): у ньому цей SDK, налаштування, поштовий транспорт і маршрут для вебхуків.

## Встановлення

```bash
dotnet add package AllyMailer.Sdk
```

Вимоги: .NET 8. Сторонніх залежностей немає.

Для .NET Standard 2.0 — лінія 1.x (гілка `1.x`), API той самий.

Отримайте API-ключ у [панелі керування](https://cp.allymailer.com) і зберігайте його у змінній оточення, а не в коді:

```bash
export ALLYMAILER_API_KEY="your-api-key"
```

## Надсилання листа

```csharp
using AllyMailer;
using AllyMailer.Api.Email.Model;
using AllyMailer.Common.Model;
using Recipient = AllyMailer.Api.Email.Model.MessageObjectRecipientsItem;

var client = new AllyMailerClient(System.Environment.GetEnvironmentVariable("ALLYMAILER_API_KEY") ?? throw new System.InvalidOperationException("ALLYMAILER_API_KEY is not set"));

var response = await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("Олексій <alex@example.com>")],
        FromEmail = "shop@example.com",
        FromName = "Магазин",
        Subject = "Ваше замовлення",
        Body = BodyObject.FromHtml("<p>Вітаємо, {{to_name}}! Ваше замовлення надіслано.</p>"),
    },
});
Console.WriteLine(response.JobId);
```

Адреса відправника має бути на домені, підтвердженому у вашому обліковому записі. Домену ще немає? Надішліть лист із [sandbox-домену](#без-свого-домену-тестовий-sandbox-домен) у три кроки.

## Зміст

- [Рецепти](#рецепти): персоналізація · вкладення · копія і прихована копія · відкладене надсилання · безпечний повтор · помилки · вебхуки
- [Без свого домену: тестовий sandbox-домен](#без-свого-домену-тестовий-sandbox-домен)
- [Запуск у продакшен](#запуск-у-продакшен)
- [Довідник](#довідник)
- [Допомога](#допомога)

## Рецепти

Кожен рецепт продовжує приклад вище: клієнт та імпорти вже є.

### Персоналізація для кожного одержувача

До 500 одержувачів в одному виклику, кожен отримує окрему копію зі своїми підстановками. Метадані одержувача повертаються в його подіях вебхука.

```csharp
using System.Collections.Generic;

await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients =
        [
            Recipient.FromAddress("Олексій <alex@example.com>", new Dictionary<string, string> { ["order"] = "1001" })
                .WithMetadata(new Dictionary<string, string> { ["user_id"] = "42" }),
            Recipient.FromAddress("Марія <maria@example.com>", new Dictionary<string, string> { ["order"] = "1002" })
                .WithMetadata(new Dictionary<string, string> { ["user_id"] = "43" }),
        ],
        FromEmail = "shop@example.com",
        Subject = "Замовлення №{{order}}",
        Body = BodyObject.FromHtml("<p>{{to_name}}, замовлення №{{order}} надіслано.</p>"),
    },
});
```

### Вкладення

```csharp
using System.IO;

var content = Convert.ToBase64String(File.ReadAllBytes("invoice.pdf"));
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Рахунок",
        Body = BodyObject.FromHtml("<p>Рахунок у вкладенні.</p>"),
        Attachments =
        [
            new AttachmentsArrayItem { Type = "application/pdf", Name = "invoice.pdf", Content = content },
        ],
    },
});
```

### Копія і прихована копія

Усі адреси передаються в `recipients` (кожна отримує копію), видимі — ще й у заголовках. `audit@example.com` немає в жодному заголовку: це прихована копія. Правила й обмеження: [копія і прихована копія](https://docs.allymailer.com.ua/ua/cc-and-bcc).

```csharp
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients =
        [
            Recipient.FromAddress("alex@example.com"),
            Recipient.FromAddress("boss@example.com"),
            Recipient.FromAddress("audit@example.com"),
        ],
        FromEmail = "shop@example.com",
        Subject = "Звіт за тиждень",
        Body = BodyObject.FromHtml("<p>Звіт у вкладенні.</p>"),
        Headers = new HeadersObject
        {
            Extra = new() { ["To"] = "alex@example.com", ["CC"] = "boss@example.com" },
        },
    },
});
```

### Відкладене надсилання

`send_at` — в UTC, не далі ніж на 24 години вперед.

```csharp
using System.Globalization;

// UTC, "YYYY-MM-DD hh:mm:ss"
var inOneHour = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Нагадування",
        Body = BodyObject.FromText("До зустрічі завтра!"),
        Options = new MessageObjectOptions { SendAt = inOneHour },
    },
});
```

### Безпечний повтор

Повтор із тим самим `idempotence_key` протягом хвилини не надішлеться вдруге: з ним повторюйте мережеві помилки, таймаути, 429 і 5xx.

```csharp
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Скидання пароля",
        Body = BodyObject.FromHtml("<p>Посилання для скидання пароля.</p>"),
        IdempotenceKey = "password-reset-8f3a",
    },
});
```

### Обробка помилок

Одержувачі, яких API відкинув (відписані, неіснуючі, у стоп-листі), — не помилка: вони у `failed_emails` відповіді, решта лист отримає.

```csharp
using AllyMailer.Client;

try
{
    await client.Email.SendAsync(request);
}
catch (ApiException e)
{
    // HTTP-статус (0: мережева помилка або таймаут), код помилки API, повідомлення
    Console.Error.WriteLine($"{e.HttpStatus} {e.ErrorCode} {e.Message}");
}
```

### Статуси доставки

Зареєструйте свою HTTPS-адресу один раз (`webhook/set`). Потім перевіряйте підпис кожного сповіщення (`auth`: MD5 сирого тіла, у якому значення `auth` замінено на API-ключ; на неправильний — 401) і обробляйте події.

```csharp
using System.Security.Cryptography;
using System.Text;
using AllyMailer.Api.Webhook;

// rawBody: тіло POST-запиту як надійшло; повертає HTTP-статус відповіді
static int HandleWebhook(string rawBody)
{
    var callback = WebhookApi.ParseCallback(rawBody);
    if (string.IsNullOrEmpty(callback.Auth))
    {
        return 401;
    }
    // auth: MD5 сирого тіла, у якому значення auth замінено на ваш API-ключ
    var signed = rawBody.Replace(callback.Auth, Environment.GetEnvironmentVariable("ALLYMAILER_API_KEY"));
    var md5 = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signed));
    if (BitConverter.ToString(md5).Replace("-", "").ToLowerInvariant() != callback.Auth)
    {
        return 401;
    }
    foreach (var user in callback.EventsByUser ?? new())
    {
        foreach (var evt in user.Events ?? new())
        {
            // evt.EventData: email, status (delivered, opened, hard_bounced, spam, ...), job_id, metadata
        }
    }
    return 200;
}
```

> **Відповідайте 200 протягом 3 секунд**, а події обробляйте у фоні (через чергу): інакше AllyMailer повторює запит кожні 10 хвилин протягом доби (зростає `retry_count`), а обробник, на якому за добу не пройшов жоден запит хоча б для 10 подій, вимикається. Обробник має витримувати **щонайменше 5 паралельних запитів до 100 подій у кожному**; повтор надсилає ті самі події ще раз — прибирайте дублікати.

## Без свого домену: тестовий sandbox-домен

Поки ваш домен не підтверджено, листи можна надсилати із **sandbox-домену** — тестового домену, який є в кожного облікового запису: він уже підтверджений, DKIM і SPF налаштовані. Листи з нього доставляються лише на підтверджені вами адреси (посилання для підтвердження приходить листом і діє 48 годин), до 100 на день. Через 31 день після створення sandbox-домен видаляється, але його можна отримати знову: наступний виклик `domain/get-sandbox-domain` створить новий (з іншою назвою, тому беріть її з відповіді, а не прописуйте в коді).

```csharp
using AllyMailer.Api.ConfirmedEmail.Model;

// 1. Ваш sandbox-домен: створюється під час першого виклику, далі повертається той самий
var sandbox = await client.Domain.GetSandboxDomainAsync();

// 2. Підтвердіть свою скриньку: AllyMailer надішле посилання, перейдіть за ним протягом 48 годин
await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest { Email = "you@example.com" });

// 3. Надішліть із будь-якої адреси sandbox-домену на підтверджену адресу
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("you@example.com")],
        FromEmail = $"noreply@{sandbox.Domain}",
        Subject = "Привіт від AllyMailer",
        Body = BodyObject.FromHtml("<p>Працює!</p>"),
    },
});
```

<details>
<summary>Обмеження sandbox</summary>

- Надсилання лише на підтверджені адреси: до 10 на обліковий запис.
- До 100 листів на день.
- Sandbox-домен спливає через 31 день; знову викличте `domain/get-sandbox-domain`, щоб отримати новий.
- Працює лише з API-ключем облікового запису, не з ключем проєкту.
- Не показується в `domain/list`; інші методи `domain/*` до нього не застосовуються.

Sandbox — для розробки та тестування. Не використовуйте sandbox-домен як відправника на проді.

</details>

<details>
<summary>Те саме через curl</summary>

```bash
BASE=https://cp.allymailer.com/ua/transactional/api/v1

curl -s -X POST $BASE/domain/get-sandbox-domain.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $ALLYMAILER_API_KEY" -d '{}'

curl -s -X POST $BASE/confirmed-email/create.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $ALLYMAILER_API_KEY" \
  -d '{"email": "you@example.com"}'

curl -s -X POST $BASE/email/send.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $ALLYMAILER_API_KEY" \
  -d '{
        "message": {
          "recipients": [
            {
              "email": "you@example.com"
            }
          ],
          "from_email": "noreply@<sandbox-domain>",
          "subject": "Привіт від AllyMailer",
          "body": {
            "html": "<p>Працює!</p>"
          }
        }
      }'
```

</details>

## Запуск у продакшен

**1. Підтвердіть домен надсилання.** `domain/get-dns-records` повертає запис підтвердження, DKIM-ключ і адресу для DMARC; додайте записи у свого DNS-провайдера, потім `domain/validate-verification-record` і `domain/validate-dkim`.

| Тип | Хост | Значення | Звідки взяти |
|---|---|---|---|
| TXT | `@` | `allymailer-validate-hash=…` | поле `verification-record` відповіді, як є |
| TXT | `<селектор>._domainkey` (селектор — на сторінці домену в панелі керування, наприклад `us`) | `v=DKIM1; k=rsa; p=<dkim>` | поле `dkim` відповіді після `v=DKIM1; k=rsa; p=` |
| TXT | `@` | `v=spf1 include:spf.allymailer.com ~all` | у відповіді немає; якщо SPF-запис уже є, додайте include до нього |
| CNAME | `_dmarc` | `<домен>.dmarc.…` | поле `dmarc` відповіді (DMARC-політику публікує AllyMailer); якщо в домену вже є свій TXT-запис DMARC, залиште його |

**2. Надійне надсилання.**

| | |
|---|---|
| Повтори | `429` і `5xx` з backoff і тим самим `idempotence_key`; інші помилки виправляйте, а не повторюйте |
| Ліміти | 500 одержувачів і 10 МБ на запит разом із вкладеннями: у base64 файл стає приблизно на третину більшим |
| Тіло | завжди `html` або `plaintext` (або із шаблону) |
| Статус доставки | вебхук, а не опитування |
| Черга | надсилайте з фонового завдання, а не всередині веб-запиту |
| Налаштування | `from_email` з конфігурації: перехід із sandbox на продакшен без змін у коді |
| Типові налаштування | не прибирайте блок відписки та трекінг; змінити їх можна через підтримку AllyMailer |

## Довідник

### Розділи

| Розділ | Методи | Опис |
|---|---|---|
| [Email](docs/Email.md) | `SendAsync`, `SubscribeAsync` | Ця група методів пов'язана з надсиланням email. |
| [EmailValidation](docs/EmailValidation.md) | `SingleAsync` | Методи перевірки email-адрес. Перевірка адреси електронної пошти надає докладну інформацію про працездатність email. Це дозволить виключити з надсилання неіснуючі email, знизити показник скарг, очистити список підписників і врешті-решт покращити вашу репутацію як відправника. Наприклад, ви можете інтегрувати процес перевірки у форму реєстрації вашого сайту і вже на етапі введення адреси повідомляти користувачам, що адреса, яку вони ввели у форму, недійсна. |
| [ConfirmedEmail](docs/ConfirmedEmail.md) | `CreateAsync`, `ListAsync`, `DeleteAsync` | Керування email-адресами, підтвердженими для надсилання із sandbox-домену. Методи доступні лише з API-ключем основного облікового запису. |
| [Template](docs/Template.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Ви можете зберегти шаблон на боці AllyMailer і потім посилатися на нього за ідентифікатором. Це не обов'язково, але вкрай рекомендовано у разі повторюваних email-повідомлень або за великих обсягів надсилання. AllyMailer підтримує два різні синтаксиси для шаблонів, докладніше читайте [тут](https://docs.allymailer.com.ua/ua/template-engines). |
| [Webhook](docs/Webhook.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | AllyMailer дозволяє вам отримувати сповіщення про зміну статусу доставки листа або спам-блокування. Ви надаєте URL, і в разі виникнення події, що вас цікавить, ми сповіщаємо вас, передаючи на цей URL JSON-дані про подію. |
| [Suppression](docs/Suppression.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Методи для керування адресами, надсилання на які заблоковано з різних причин — відписка, недоступність, скарги. |
| [Domain](docs/Domain.md) | `GetDnsRecordsAsync`, `ValidateVerificationRecordAsync`, `ValidateDkimAsync`, `ListAsync`, `GetSandboxDomainAsync`, `DeleteAsync` | Методи для налаштування доменів надсилання та перевірки їх статусів. Корисні, якщо вам недостатньо додавання доменів через вебінтерфейс AllyMailer і, наприклад, хочеться автоматизувати додавання доменів для ваших власних клієнтів. |
| [EventDump](docs/EventDump.md) | `CreateAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Методи для отримання списку подій, що сталися, за певний період. Дані зберігаються до 45 днів, залежно від вашого тарифу. Це означає, що можна запросити події, починаючи з 00:00:00 45-го дня тому і до поточного моменту часу. Для індивідуальних тарифів з великими обсягами за замовчуванням події доступні лише за останні 7 днів. |
| [Tag](docs/Tag.md) | `ListAsync`, `DeleteAsync` | Методи керування користувацькими мітками. За допомогою міток ви можете класифікувати листи за потрібними критеріями. Відокремити маркетингові листи від транзакційних або листи з підтвердженням реєстрації від сповіщень про замовлення. AllyMailer дозволяє позначати мітками кожне вихідне повідомлення, а потім сегментувати й аналізувати статистику на основі зазначених під час надсилання міток. |
| [Project](docs/Project.md) | `CreateAsync`, `UpdateAsync`, `ListAsync`, `DeleteAsync` | Проєкти дозволяють об'єднати кілька незалежних доменів із webhook, шаблонами та списками відписки. Це дуже корисно, якщо у вас є власні клієнти і ви керуєте їхніми даними з одного облікового запису AllyMailer. Кожен проєкт отримує власний ключ API, а виклики методів API з цим ключем обробляють лише дані цього проєкту. Таким чином, ви можете призначити окремий проєкт для кожного користувача і бути впевненими, що вони не конфліктуватимуть: наприклад, відписка від одного проєкту не вплине на інший. |
| [System](docs/System.md) | `PingAsync`, `InfoAsync` | Системні методи. |
| [Obsolete](docs/Obsolete.md) | `UnsubscribedSetAsync`, `UnsubscribedCheckAsync`, `UnsubscribedListAsync` | Застарілі методи роботи з відписками. Ми рекомендуємо користуватися замість них гнучкішими методами [suppression](https://docs.allymailer.com.ua/ua/web-api-ref#suppression). Зверніть увагу, що знову підписати відписаного адресата через WebAPI не можна. Можна лише надіслати йому email із посиланням повторної підписки за допомогою методу email/subscribe. |

### Власний HTTP-клієнт / заголовки

Передайте власний `HttpClient` (проксі, обробники, `IHttpClientFactory`) у конструктор; `Configuration.Timeout` діє на кожен запит. У моделей із довільними ключами (`HeadersObject`) є словник `Extra`: його ключі передаються поруч з іменованими властивостями.

```csharp
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AllyMailer;
using AllyMailer.Client;
using AllyMailer.Common.Model;

var http = new HttpClient(new HttpClientHandler { Proxy = new WebProxy("http://proxy.local:3128") });
var client = new AllyMailerClient(new Configuration("YOUR_API_KEY") { Timeout = TimeSpan.FromSeconds(10) }, http);

var headers = new HeadersObject
{
    Extra = new Dictionary<string, object>
    {
        ["X-MyHeader"] = "some data",
        ["CC"] = "copy@example.com",
        ["List-Unsubscribe"] = "<mailto:unsubscribe@example.com>",
    },
};
```

### Обробка помилок

Кожен метод кидає `ApiException`, якщо API повернув помилку; у ньому є [код помилки API](https://docs.allymailer.com.ua/ua/web-api-ref#api-errors) і HTTP-статус.

## Допомога

- Для ШІ-агентів: [llms.txt](https://docs.allymailer.com.ua/llms.txt), скіли: `npx skills add https://github.com/allymailer/allymailer-skills.git`
- [Підтримка](https://cp.allymailer.com/ua/support) · [Задачі](https://github.com/allymailer/allymailer-dotnet-sdk/issues)
- Розробникам: [AGENTS.md](AGENTS.md) — SDK генерується з OpenAPI-специфікації.
