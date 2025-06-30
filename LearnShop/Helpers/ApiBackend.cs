using System.Text.Json;

namespace LearnShop.Helpers;

public static class ApiBackend
{
    private static string BaseUrl { get; set; } = null;

    private static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    static ApiBackend()
    {
        BaseUrl = "http://localhost:5057/";
    }

    public static async Task<T?> GetAsync<T>(string urlComplement, string? token = null)
    {
        var fullUrl = BaseUrl + urlComplement;
        var httpClient = new HttpClient();

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var result = await httpClient.GetStringAsync(fullUrl);
        httpClient.Dispose();

        return JsonSerializer.Deserialize<T>(result, Options);
    }

    public static async Task<T?> PostAsync<T, K>(string urlComplement, K obj, string? token = null)
    {
        var fullUrl = BaseUrl + urlComplement;
        var httpClient = new HttpClient();

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(obj, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(fullUrl, content);

        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadAsStringAsync();
            throw new Exception($"Ocorreu um erro ao enviar a requisição: {result.StatusCode} {error}");
        }

        var resultContent = await result.Content.ReadAsStringAsync();
        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task<T?> PostAsync<T>(string urlComplement, T obj, string? token = null)
    {
        var fullUrl = BaseUrl + urlComplement;
        var httpClient = new HttpClient();

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(obj, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(fullUrl, content);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception($"Ocorreu um erro ao enviar a requisição: {result.StatusCode}");
        }

        var resultContent = await result.Content.ReadAsStringAsync();
        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task PutAsync<T>(string urlComplement, T obj, string? token = null)
    {
        var fullUrl = BaseUrl + urlComplement;
        var httpClient = new HttpClient();

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(obj, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PutAsync(fullUrl, content);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception($"Ocorreu um erro ao enviar a requisição: {result.StatusCode}");
        }

        httpClient.Dispose();
    }

    public static async Task DeleteAsync(string urlComplement, string? token = null)
    {
        var fullUrl = BaseUrl + urlComplement;
        var httpClient = new HttpClient();

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        
        var result = await httpClient.DeleteAsync(fullUrl);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception($"Ocorreu um erro ao enviar a requisição: {result.StatusCode}");
        }
        
        httpClient.Dispose();
    }
}