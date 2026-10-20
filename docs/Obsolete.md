# Obsolete

[До списку розділів](../README.md)

Застарілі методи роботи з відписками. Ми рекомендуємо користуватися замість них гнучкішими методами [suppression](https://docs.allymailer.com.ua/ua/web-api-ref#suppression). Зверніть увагу, що знову підписати відписаного адресата через WebAPI не можна. Можна лише надіслати йому email із посиланням повторної підписки за допомогою методу email/subscribe.

`AllyMailer.Api.Obsolete.ObsoleteApi`, `client.Obsolete`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`UnsubscribedSetAsync`](#unsubscribedsetasync) | `POST /unsubscribed/set.json` | Внести email до списку відписаних. |
| [`UnsubscribedCheckAsync`](#unsubscribedcheckasync) | `POST /unsubscribed/check.json` | Перевіряє, чи відписано email. |
| [`UnsubscribedListAsync`](#unsubscribedlistasync) | `POST /unsubscribed/list.json` | Повертає список адрес, відписаних починаючи з указаної дати. |

## UnsubscribedSetAsync

`POST /unsubscribed/set.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-set)

Внести email до списку відписаних.

Після внесення email до списку відписаних усі майбутні спроби надіслати на нього повідомлення будуть відхилятися. Зауважте, що видалити email зі списку відписаних безпосередньо за допомогою API не можна. Але можна надіслати йому повідомлення з посиланням повторної підписки за допомогою методу [email/subscribe](https://docs.allymailer.com.ua/ua/web-api-ref#email-subscribe) або видалити його зі списку відписаних вручну на сторінці [Пошук email](https://cp.allymailer.com/ua/user/email) у панелі керування (але кількість таких видалень на день сильно обмежена).

### Запит: `ObsoleteUnsubscribedSetRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Address` | `string` | так | Email, який має бути відписано. |

### Відповідь: `ObsoleteUnsubscribedSetResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Address` | `string` | Відписаний email. |
| `Message` | `ObsoleteUnsubscribedSetMessage` | Подробиці відписки:<br><br>- "unsubscribed" якщо email було щойно відписано,<br>- "was inactive" якщо він уже був позначений як неактивний і немає сенсу відписувати,<br>- "was unsubscribed" якщо email уже було відписано раніше,<br>- "added and unsubscribed" якщо для цього email не зберігалося жодного більш раннього статусу, тому його було додано й одразу ж відписано. (`unsubscribed`, `was inactive`, `was unsubscribed`, `added and unsubscribed`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Obsolete.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Obsolete.UnsubscribedSetAsync(new ObsoleteUnsubscribedSetRequest
{
    Address = "email@example.com",
});
Console.WriteLine(response.Address);
```

## UnsubscribedCheckAsync

`POST /unsubscribed/check.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-check)

Перевіряє, чи відписано email.

### Запит: `ObsoleteUnsubscribedCheckRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Address` | `string` | так | Email, який необхідно перевірити. |

### Відповідь: `ObsoleteUnsubscribedCheckResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Address` | `string` | Email, який було перевірено. |
| `IsUnsubscribed` | `bool` | true якщо відписано, false якщо ні. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Obsolete.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Obsolete.UnsubscribedCheckAsync(new ObsoleteUnsubscribedCheckRequest
{
    Address = "email@example.com",
});
Console.WriteLine(response.Address);
```

## UnsubscribedListAsync

`POST /unsubscribed/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-list)

Повертає список адрес, відписаних починаючи з указаної дати.

### Запит: `ObsoleteUnsubscribedListRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `DateFrom` | `string` |  | Дата у форматі РРРР-ММ-ДД, щоб отримати всі відписані адреси з "date_from" по сьогоднішній день. Якщо параметр не вказати, період за замовчуванням буде з 00:00 сьогоднішнього дня за UTC. |

### Відповідь: `ObsoleteUnsubscribedListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Unsubscribed` | `List<ObsoleteUnsubscribedListResponseUnsubscribedItem>` | Масив об'єктів з даними відписаних. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Obsolete.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Obsolete.UnsubscribedListAsync(new ObsoleteUnsubscribedListRequest
{
    DateFrom = "2020-10-14",
});
Console.WriteLine(response.Unsubscribed);
```
