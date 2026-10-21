using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Domain.Model;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain;

/// <summary>
/// Методи для налаштування доменів надсилання та перевірки їх статусів. Корисні, якщо вам недостатньо<br/>
/// додавання доменів через вебінтерфейс AllyMailer і, наприклад, хочеться автоматизувати додавання<br/>
/// доменів для ваших власних клієнтів.
/// <para/>
/// Щоб додати новий домен надсилання через API, ви маєте зробити таке:<br/>
/// - Підтвердіть домен, з якого надсилатимуться листи, внісши необхідні DNS-записи, отримавши їх за допомогою методу [domain/get-dns-records](https://docs.allymailer.com.ua/ua/web-api-ref#domain-get-dns-records).<br/>
/// - Після додавання раз на 5–10 хвилин викликайте [domain/list](https://docs.allymailer.com.ua/ua/web-api-ref#domain-list) із зазначенням доданого домену (або кількох доменів одразу, якщо перевірити треба декілька) і перевіряйте verification-record.status та dkim.status у відповіді domain/list. Можна робити запит і частіше — але точно не частіше за раз на хвилину.<br/>
/// - Якщо verification-record.status="unconfirmed", то ініціюйте перевірку правильності запису володіння доменом за допомогою виклику [domain/validate-verification-record](https://docs.allymailer.com.ua/ua/web-api-ref#domain-validate-verification-record).<br/>
/// - Якщо dkim.status="inactive", то ініціюйте перевірку правильності DKIM-запису за допомогою виклику [domain/validate-dkim](https://docs.allymailer.com.ua/ua/web-api-ref#domain-validate-dkim) (втім, DKIM-записи AllyMailer і сам перевіряє раз на пів години).<br/>
/// - І лише коли domain/list повернув verification-record.status="confirmed" та dkim.status="active" для домену, можна використовувати домен для надсилання та припинити його очікування й виклики для нього зазначених вище методів.
/// </summary>
public class DomainApi
{
    private readonly ApiClient _client;

    public DomainApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Повертає інформацію про записи, які потрібно внести в DNS для використання домену в AllyMailer.
    /// <para/>
    /// Щоб надсилати листи, вам потрібно підтвердити володіння доменом, зі зворотною адресою якого ви<br/>
    /// збираєтеся їх надсилати, а також налаштувати DKIM-підпис для цього домену. Метод дозволяє вам<br/>
    /// зареєструвати домен у AllyMailer і згенерувати запис для перевірки володіння та DKIM-ключ, або<br/>
    /// отримати їх, якщо вони були згенеровані раніше.
    /// <para/>
    /// Після отримання успішної відповіді вам потрібно додати DNS-записи на кшталт цих:
    /// <para/>
    /// `@             IN TXT "allymailer-validate-hash=483bb362ebdbeedd755cfb1d4d661"`
    /// <para/>
    /// `us._domainkey IN TXT "v=DKIM1; k=rsa; p=MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDo7"`
    /// <para/>
    /// `@             IN TXT  "v=spf1 include:spf.allymailer.com.ua ~all"`
    /// <para/>
    /// `_dmarc        IN CNAME "example.com.dmarc.allymailer.com.ua."`
    /// <para/>
    /// - Перший запис представлено вмістом поля "verification-record".<br/>
    /// - Другий запис має включати вміст поля "dkim" одразу після "v=DKIM1; k=rsa; p=". Зверніть увагу на ім'я запису! Префікс "us._domainkey." дуже важливий для того, щоб DKIM-селектор AllyMailer працював правильно.<br/>
    /// - Третій запис необов'язковий, але ми дуже рекомендуємо його додавати — це SPF-запис, який підвищує доставлюваність. Метод його не повертає. Можливо, вона у вас уже є — у цьому разі просто додайте include:spf.allymailer.com.ua після v=spf1.<br/>
    /// - Четвертий запис — CNAME хоста "_dmarc" на значення поля "dmarc": тоді DMARC-політику домену публікує AllyMailer. Якщо в домену вже є свій TXT-запис DMARC, залиште його.
    /// <para/>
    /// Вимоги до синтаксису записів у різних DNS-провайдерів можуть відрізнятися:<br/>
    /// - іноді потрібно вказувати повне ім'я домену із завершальною крапкою, на кшталт "example.com." замість символа @.<br/>
    /// - деякі провайдери вимагають указувати час життя (TTL, наприклад 3600)<br/>
    /// - для деяких провайдерів буде зайвим поміщення значень у подвійні лапки<br/>
    /// Будь ласка, перевірте вимоги свого провайдера й підкажіть це своїм користувачам.
    /// <para/>
    /// Для реалізації white-label рішень є можливість змінити стандартні DNS-записи на ті, що не містять<br/>
    /// підрядка "allymailer". Щоб дізнатися докладніше — напишіть у техпідтримку.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-get-dns-records
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainGetDnsRecordsResponse> GetDnsRecordsAsync(DomainGetDnsRecordsRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainGetDnsRecordsResponse>("/domain/get-dns-records.json", request, cancellationToken);
    }

    /// <summary>
    /// Ініціює перевірку запису про володіння доменом.
    /// <para/>
    /// Якщо результат виклику цього методу успішний (200 OK) — це означає, що перевірку запущено. Для<br/>
    /// отримання результату потрібно пізніше перевірити поле verification-record.status, що повертається<br/>
    /// методом [domain/list](https://docs.allymailer.com.ua/ua/web-api-ref#domain-list). Докладніше читайте<br/>
    /// [тут](https://docs.allymailer.com.ua/ua/web-api-ref#domain).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-validate-verification-record
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainValidateVerificationRecordResponse> ValidateVerificationRecordAsync(DomainValidateVerificationRecordRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainValidateVerificationRecordResponse>("/domain/validate-verification-record.json", request, cancellationToken);
    }

    /// <summary>
    /// Перевіряє DKIM-запис домену.
    /// <para/>
    /// Якщо результат виклику цього методу успішний (200 OK) — це означає, що перевірку запущено. Для<br/>
    /// отримання результату потрібно пізніше перевірити поле dkim.status, що повертається методом<br/>
    /// [domain/list](https://docs.allymailer.com.ua/ua/web-api-ref#domain-list). Докладніше читайте<br/>
    /// [тут](https://docs.allymailer.com.ua/ua/web-api-ref#domain).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-validate-dkim
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainValidateDkimResponse> ValidateDkimAsync(DomainValidateDkimRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainValidateDkimResponse>("/domain/validate-dkim.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає список усіх зареєстрованих доменів користувача або проєкту разом зі статусами останніх<br/>
    /// перевірок.
    /// <para/>
    /// Повертає статус останньої перевірки DNS-запису володіння доменом і DKIM-запису для кожного домену.<br/>
    /// Докладніше [тут](https://docs.allymailer.com.ua/ua/web-api-ref#domain).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainListResponse> ListAsync(DomainListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainListResponse>("/domain/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Створює sandbox-домен облікового запису або повертає наявний.
    /// <para/>
    /// Повертає sandbox-домен облікового запису, готовий до використання в полі `from_email` методу<br/>
    /// [email/send](https://docs.allymailer.com.ua/ua/web-api-ref#email-send). Якщо sandbox-домену немає,<br/>
    /// AllyMailer створює його та повертає. Повторні виклики повертають той самий домен.
    /// <para/>
    /// Sandbox-домен видаляється через 31 день після створення; наступний виклик створить новий. З нього<br/>
    /// можна надіслати до 100 листів на день (ліміт облікового запису за замовчуванням).
    /// <para/>
    /// Метод доступний лише з API-ключем облікового запису. API-ключі проєктів не можуть створювати<br/>
    /// sandbox-домени.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-get-sandbox-domain
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainGetSandboxDomainResponse> GetSandboxDomainAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainGetSandboxDomainResponse>("/domain/get-sandbox-domain.json", null, cancellationToken);
    }

    /// <summary>
    /// Видаляє домен. Увага: для видалення домену, що належить проєкту, вам потрібно використати API-ключ<br/>
    /// проєкту.
    /// <para/>
    /// Видаляє домен.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#domain-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainDeleteResponse> DeleteAsync(DomainDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainDeleteResponse>("/domain/delete.json", request, cancellationToken);
    }
}
