# AllyMailer .NET (C#) SDK 1.0.0

Web API 1.90

Офіційна бібліотека .NET (C#) для API транзакційних листів [AllyMailer](https://allymailer.com.ua).

Покроковий посібник: [Надсилання листів на .NET (C#)](https://docs.allymailer.com.ua/ua/send-with-dotnet) · Документація для LLM: [llms.txt](https://docs.allymailer.com.ua/llms.txt) · [Довідник API](https://docs.allymailer.com.ua/ua/web-api-ref)

## Встановлення

```bash
dotnet add package AllyMailer.Sdk
```

Вимоги: .NET Standard 2.0 (.NET Framework 4.6.1+, .NET Core 2.0+). Сторонніх залежностей немає.

Це лінія 1.x (гілка `1.x`) для .NET Standard 2.0. Для нових версій — лінія 2.x (гілка `main`), API той самий.

## Отримайте API-ключ

1. Зареєструйтеся на [cp.allymailer.com](https://cp.allymailer.com).
2. Скопіюйте API-ключ у [панелі керування](https://cp.allymailer.com).
3. Покладіть його у змінну оточення:

```bash
export ALLYMAILER_API_KEY="your-api-key"
```

> **Лише на сервері.** Використовуйте цей SDK у серверному коді: API-маршрути, серверні функції, воркери.
> Ніколи не передавайте API-ключ у браузер чи мобільний застосунок і не комітьте його.

## Перший лист — без налаштування DNS

Кожен обліковий запис може одразу надсилати тестові листи із **sandbox-домену**: він уже підтверджений, DKIM і SPF налаштовані.
Листи доставляються лише на підтверджені вами адреси.

<!-- checked examples -->

**1. Отримайте sandbox-домен** (`domain/get-sandbox-domain`). Перший виклик створює домен, наступні повертають той самий.

```csharp
using AllyMailer;

var client = new AllyMailerClient(System.Environment.GetEnvironmentVariable("ALLYMAILER_API_KEY") ?? throw new System.InvalidOperationException("ALLYMAILER_API_KEY is not set"));

var response = await client.Domain.GetSandboxDomainAsync();
Console.WriteLine(response.Domain);
```

**2. Підтвердіть свою скриньку** (`confirmed-email/create`). AllyMailer надішле лист із підтвердженням; перейдіть за посиланням із нього протягом 48 годин.

```csharp
using AllyMailer;
using AllyMailer.Api.ConfirmedEmail.Model;

var client = new AllyMailerClient(System.Environment.GetEnvironmentVariable("ALLYMAILER_API_KEY") ?? throw new System.InvalidOperationException("ALLYMAILER_API_KEY is not set"));

var response = await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest
{
    Email = "you@example.com",
});
Console.WriteLine(response.ConfirmedEmail);
```

Статус перевірте через `confirmed-email/list` — у вашої адреси має бути `"status": "confirmed"`.

**3. Надішліть лист** (`email/send`) з будь-якої адреси sandbox-домену на підтверджену адресу.

```csharp
using System.Collections.Generic;
using AllyMailer;
using AllyMailer.Api.Email.Model;
using AllyMailer.Common.Model;

var client = new AllyMailerClient(System.Environment.GetEnvironmentVariable("ALLYMAILER_API_KEY") ?? throw new System.InvalidOperationException("ALLYMAILER_API_KEY is not set"));

var response = await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = new List<MessageObjectRecipientsItem>
        {
            new MessageObjectRecipientsItem
            {
                Email = "you@example.com",
            },
        },
        Body = new BodyObject
        {
            Html = "<p>Працює!</p>",
        },
        Subject = "Привіт від AllyMailer",
        FromEmail = "noreply@<sandbox-domain>",
    },
});
Console.WriteLine(response.JobId);
```

<!-- /checked examples -->

### Що повертається

```json
{
  "status": "success",
  "job_id": "1ZymBc-00041N-9X",
  "emails": [
    "you@example.com"
  ]
}
```

У разі помилки API повертає `status: "error"`, `message` і числовий `code`:

```json
{
  "status": "error",
  "code": 102,
  "message": "Error ID:83274F98-C7D5-11F1-B221-6214FF8707B1. Can not decode key wrongkey"
}
```

### Те саме через curl

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

## Обмеження sandbox

- Надсилання лише на підтверджені адреси: до 10 на обліковий запис.
- До 100 листів на день.
- Sandbox-домен спливає через 31 день; знову викличте `domain/get-sandbox-domain`, щоб отримати новий.
- Працює лише з API-ключем облікового запису, не з ключем проєкту.
- Не показується в `domain/list`; інші методи `domain/*` до нього не застосовуються.

Sandbox — для розробки та тестування. Не використовуйте sandbox-домен як відправника на проді.

## Перед продом: підтвердіть свій домен

1. `domain/get-dns-records` — повертає значення для DNS-записів.
2. Додайте ці записи у свого DNS-провайдера (їх також показує сторінка домену в панелі керування):

   | Тип | Хост | Значення |
   |---|---|---|
   | TXT | `@` | значення `verification-record` (`allymailer-validate-hash=…`) |
   | TXT | `<селектор>._domainkey` (селектор — на сторінці домену в панелі керування, наприклад `us`) | `v=DKIM1; k=rsa; p=<dkim>` |
   | TXT | `@` | `v=spf1 include:spf.allymailer.com ~all` |
   | CNAME | `_dmarc` | значення `dmarc` |

3. `domain/validate-verification-record`, потім `domain/validate-dkim`.
4. `domain/list` — дочекайтеся, поки запис підтвердження буде підтверджено, а DKIM — активний.
5. Беріть `from_email` з конфігурації, щоб перехід із sandbox не потребував змін у коді.

## Надійне надсилання

- **Повтори:** ставте `message.idempotence_key` у надсиланнях, які можуть повторюватися, щоб повторний запит не надіслав лист двічі.
- **Ліміти:** до 500 отримувачів і 10 МБ на запит; кожне вкладення до 7 МБ.
- **Тіло:** завжди передавайте `body.html` або `body.plaintext`.
- **Статус доставки:** зареєструйте вебхук (`webhook/set`) зі швидким ідемпотентним HTTPS-обробником замість опитування.
- **Типові налаштування:** не прибирайте стандартний блок відписки та трекінг; змінити їх можна через підтримку AllyMailer.
- **Помилки:** повторюйте `429` і `5xx` з backoff; інші помилки без змін не повторюйте.

## Посилання

- [Надсилання листів на .NET (C#)](https://docs.allymailer.com.ua/ua/send-with-dotnet) та інші [посібники з інтеграції](https://docs.allymailer.com.ua/ua/integrations/)
- [Довідник Web API](https://docs.allymailer.com.ua/ua/web-api-ref)
- [llms.txt](https://docs.allymailer.com.ua/llms.txt) для AI-асистентів
- [Підтримка](https://cp.allymailer.com/ua/support)

## Для розробників SDK

Див. [AGENTS.md](AGENTS.md): як цей SDK генерується з OpenAPI-специфікації, збирається, перевіряється та випускається.

---

# Довідка з SDK

## Власний HTTP-клієнт / заголовки

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

## Обробка помилок

Кожен метод кидає `ApiException`, якщо API повернув помилку; у ньому є [код помилки API](https://docs.allymailer.com.ua/ua/web-api-ref#api-errors) і HTTP-статус.

## Розділи

| Розділ | Методи | Опис |
|---|---|---|
| [Email](docs/Email.md) | `SendAsync`, `SubscribeAsync` | Ця група методів пов'язана з надсиланням email. |
| [EmailValidation](docs/EmailValidation.md) | `SingleAsync` | Методи перевірки email-адрес. Перевірка адреси електронної пошти надає докладну інформацію про працездатність email. Це дозволить виключити з надсилання неіснуючі email, знизити показник скарг, очистити список підписників і врешті-решт покращити вашу репутацію як відправника. Наприклад, ви можете інтегрувати процес перевірки у форму реєстрації вашого сайту і вже на етапі введення адреси повідомляти користувачам, що адреса, яку вони ввели у форму, недійсна. |
| [ConfirmedEmail](docs/ConfirmedEmail.md) | `CreateAsync`, `ListAsync`, `DeleteAsync` | Керування email-адресами, підтвердженими для надсилання із sandbox-домену. Методи доступні лише з API-ключем основного облікового запису. |
| [Template](docs/Template.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Ви можете зберегти шаблон на боці AllyMailer і потім посилатися на нього за ідентифікатором. Це не обов'язково, але вкрай рекомендовано у разі повторюваних email-повідомлень або за великих обсягів надсилання. AllyMailer підтримує два різні синтаксиси для шаблонів, докладніше читайте [тут](https://docs.allymailer.com.ua/en/template-engines). |
| [Webhook](docs/Webhook.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | AllyMailer дозволяє вам отримувати сповіщення про зміну статусу доставки листа або спам-блокування. Ви надаєте URL, і в разі виникнення події, що вас цікавить, ми сповіщаємо вас, передаючи на цей URL JSON-дані про подію. |
| [Suppression](docs/Suppression.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Методи для керування адресами, надсилання на які заблоковано з різних причин — відписка, недоступність, скарги. |
| [Domain](docs/Domain.md) | `GetDnsRecordsAsync`, `ValidateVerificationRecordAsync`, `ValidateDkimAsync`, `ListAsync`, `GetSandboxDomainAsync`, `DeleteAsync` | Методи для налаштування доменів надсилання та перевірки їх статусів. Корисні, якщо вам недостатньо додавання доменів через вебінтерфейс AllyMailer і, наприклад, хочеться автоматизувати додавання доменів для ваших власних клієнтів. |
| [EventDump](docs/EventDump.md) | `CreateAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Методи для отримання списку подій, що сталися, за певний період. Дані зберігаються до 45 днів, залежно від вашого тарифу. Це означає, що можна запросити події, починаючи з 00:00:00 45-го дня тому і до поточного моменту часу. Для індивідуальних тарифів з великими обсягами за замовчуванням події доступні лише за останні 7 днів. |
| [Tag](docs/Tag.md) | `ListAsync`, `DeleteAsync` | Методи керування користувацькими мітками. За допомогою міток ви можете класифікувати листи за потрібними критеріями. Відокремити маркетингові листи від транзакційних або листи з підтвердженням реєстрації від сповіщень про замовлення. AllyMailer дозволяє позначати мітками кожне вихідне повідомлення, а потім сегментувати й аналізувати статистику на основі зазначених під час надсилання міток. |
| [Project](docs/Project.md) | `CreateAsync`, `UpdateAsync`, `ListAsync`, `DeleteAsync` | Проєкти дозволяють об'єднати кілька незалежних доменів із webhook, шаблонами та списками відписки. Це дуже корисно, якщо у вас є власні клієнти і ви керуєте їхніми даними з одного облікового запису AllyMailer. Кожен проєкт отримує власний ключ API, а виклики методів API з цим ключем обробляють лише дані цього проєкту. Таким чином, ви можете призначити окремий проєкт для кожного користувача і бути впевненими, що вони не конфліктуватимуть: наприклад, відписка від одного проєкту не вплине на інший. |
| [System](docs/System.md) | `PingAsync`, `InfoAsync` | Системні методи. |
| [Obsolete](docs/Obsolete.md) | `UnsubscribedSetAsync`, `UnsubscribedCheckAsync`, `UnsubscribedListAsync` | Застарілі методи роботи з відписками. Ми рекомендуємо користуватися замість них гнучкішими методами [suppression](https://docs.allymailer.com.ua/ua/web-api-ref#suppression). Зверніть увагу, що знову підписати відписаного адресата через WebAPI не можна. Можна лише надіслати йому email із посиланням повторної підписки за допомогою методу email/subscribe. |
