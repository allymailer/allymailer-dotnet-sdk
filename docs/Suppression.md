# Suppression

[До списку розділів](../README.md)

Методи для керування адресами, надсилання на які заблоковано з різних причин — відписка, недоступність, скарги.

`AllyMailer.Api.Suppression.SuppressionApi`, `client.Suppression`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`SetAsync`](#setasync) | `POST /suppression/set.json` | Додає email-адресу до списку недоступних. Ви завжди зможете пізніше видалити цю адресу зі списку недоступних методом [suppression/delete](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-delete). |
| [`GetAsync`](#getasync) | `POST /suppression/get.json` | Повертає причину й дату блокування email-адреси. |
| [`ListAsync`](#listasync) | `POST /suppression/list.json` | Повертає список недоступних, починаючи з указаної дати. |
| [`DeleteAsync`](#deleteasync) | `POST /suppression/delete.json` | Видаляє email-адресу зі списку недоступних (лише записи з is_deletable=true). Якщо жодного запису для email не знайдено, повертається помилка API 3003. Якщо записи про недоступність знайдено, але жодну з них видалити не можна, повертається помилка API 3004. Якщо перевищено ліміт видалень на добу, повертається помилка API 906. |

## SetAsync

`POST /suppression/set.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-set)

Додає email-адресу до списку недоступних. Ви завжди зможете пізніше видалити цю адресу зі списку недоступних методом [suppression/delete](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-delete).

### Запит: `SuppressionSetRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Email` | `string` | так | Email-адреса, яку потрібно додати до списку недоступних. |
| `Cause` | `string` | так | Причина, з якої надсилання на цей email блокується. Одна з таких:<br><br>- __unsubscribed__ - зазначену адресу відписано;<br>- __temporary_unavailable__ - адреса тимчасово недоступна. Це означає, що протягом наступних трьох діб надсилання на цю адресу повертатиме помилку. Email може бути тимчасово недоступним з різних причин, наприклад:<br><br>- попереднє надсилання було відхилено сервером отримувача як спам;<br>- поштова скринька отримувача переповнена або не використовується;<br>- домен не приймає пошту через неправильне налаштування на боці отримувача або з інших причин;<br>- сервер відправника було відхилено через блеклистинг;<br><br>- __permanent_unavailable__ - адреса перманентно недоступна через багаторазові недоставки;<br>- __complained__ - в одному з попередніх листів адресат натиснув “Це спам”. |
| `Created` | `string` |  | Коли було встановлено недоступність, у часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс". |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Suppression.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Suppression.SetAsync(new SuppressionSetRequest
{
    Email = "email@example.com",
    Cause = "unsubscribed",
    Created = "2021-12-19 10:15:49",
});
Console.WriteLine(response);
```

## GetAsync

`POST /suppression/get.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-get)

Повертає причину й дату блокування email-адреси.

### Запит: `SuppressionGetRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Email` | `string` | так | Email-адреса, для якої потрібно отримати дані про недоступність. |
| `AllProjects` | `bool` |  | Якщо у користувача увімкнено проєкти, all_projects=true дозволить шукати за даними всіх проєктів. |

### Відповідь: `SuppressionGetResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Email` | `string` | Email-адреса, для якої було запитано дані про недоступність. |
| `Suppressions` | `List<SuppressionGetObject>` | Масив об'єктів з інформацією про недоступність. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Suppression.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Suppression.GetAsync(new SuppressionGetRequest
{
    Email = "email@example.com",
});
Console.WriteLine(response.Email);
```

## ListAsync

`POST /suppression/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-list)

Повертає список недоступних, починаючи з указаної дати.

### Запит: `SuppressionListRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Cause` | `string` |  | Причина, з якої надсилання на цей email блокується. Одна з таких:<br><br>- __unsubscribed__ - зазначену адресу відписано;<br>- __temporary_unavailable__ - адреса тимчасово недоступна. Це означає, що протягом наступних трьох діб надсилання на цю адресу повертатиме помилку. Email може бути тимчасово недоступним з різних причин, наприклад:<br><br>- попереднє надсилання було відхилено сервером отримувача як спам;<br>- поштова скринька отримувача переповнена або не використовується;<br>- домен не приймає пошту через неправильне налаштування на боці отримувача або з інших причин;<br>- сервер відправника було відхилено через блеклистинг;<br><br>- __permanent_unavailable__ - адреса перманентно недоступна через багаторазові недоставки;<br>- __complained__ - в одному з попередніх листів адресат натиснув “Це спам”.<br>- __blocked__ - надсилання на цю адресу заборонено адміністрацією AllyMailer.<br>У майбутньому можуть з'явитися й інші причини. |
| `Source` | `string` |  | Джерело потрапляння до списку недоступних. Одне з таких:<br><br>- __user__ - дії користувача за допомогою методу [suppression/set](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-set);<br>- __system__ - надсилання на цю адресу заборонено системою, наприклад через багаторазові недоставки;<br>- __subscriber__ - у зв'язку з діями отримувача. В одному з попередніх листів адресат натиснув “Це спам” або відписався. |
| `StartTime` | `string` |  | Дата у форматі РРРР-ММ-ДД гг:хх:сс, щоб отримати список блокування з "start_time" по сьогоднішній день. Ігнорується, якщо параметр “cursor” не порожній. |
| `Cursor` | `string` |  | Параметр указує, з якої позиції починати вибірку. Має бути порожнім або відсутнім для отримання першої порції даних; для отримання другої та наступних порцій його необхідно задавати, підставляючи значення поля "cursor" з попередньої порції. |
| `Limit` | `int` |  | Максимум записів, що видаються за один раз, за замовчуванням 50. |

### Відповідь: `SuppressionListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Suppressions` | `List<SuppressionListObject>` | Масив об'єктів з інформацією про недоступність. |
| `Cursor` | `string` | Параметр указує, з якої позиції починати вибірку. Має бути порожнім або відсутнім для отримання першої порції даних; для отримання другої та наступних порцій його необхідно задавати, підставляючи значення поля "cursor" з попередньої порції. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Suppression.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Suppression.ListAsync(new SuppressionListRequest
{
    Cause = "unsubscribed",
    Source = "user",
    StartTime = "2020-10-14 17:00:00",
    Cursor = "Ajfb6Hvdkn3hdhhvG57xbdufhG5",
    Limit = 50,
});
Console.WriteLine(response.Suppressions);
```

## DeleteAsync

`POST /suppression/delete.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-delete)

Видаляє email-адресу зі списку недоступних (лише записи з is_deletable=true). Якщо жодного запису для email не знайдено, повертається помилка API 3003. Якщо записи про недоступність знайдено, але жодну з них видалити не можна, повертається помилка API 3004. Якщо перевищено ліміт видалень на добу, повертається помилка API 906.

### Запит: `SuppressionDeleteRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Email` | `string` | так | Email-адреса, яку потрібно видалити зі списку недоступних. |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Suppression.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Suppression.DeleteAsync(new SuppressionDeleteRequest
{
    Email = "email@example.com",
});
Console.WriteLine(response);
```
