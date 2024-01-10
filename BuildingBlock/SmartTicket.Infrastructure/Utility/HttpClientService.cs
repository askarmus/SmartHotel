using System.Net.Http;
using Newtonsoft.Json;

namespace SmartHotel.Infrastructure.Utility;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T> SendRequestAsync<T>(string url, HttpMethod method = null, object requestBody = null, Dictionary<string, string> headers = null)
    {
        using (var request = CreateRequest(url, method, requestBody, headers))
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            // Check if the request was successful (status code 200)
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            else
            {
                throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    private HttpRequestMessage CreateRequest(string url, HttpMethod method, object requestBody, Dictionary<string, string> headers)
    {
        var request = new HttpRequestMessage(method ?? HttpMethod.Get, url);

        if (requestBody != null)
        {
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        return request;
    }
}
 