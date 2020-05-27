using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnlineBooksStore.Integration.Client
{
    public abstract class RestService
    {
        private readonly HttpClient _httpClient;

        protected RestService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("default");
        }

        /// <summary>
        /// Sends a GET request to the specified URI, and parses the JSON response body
        /// to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        protected async Task<T> GetJsonAsync<T>(string requestUri)
        {
            var stringContent = await _httpClient.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(stringContent);
        }

        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        protected Task<T> PostJsonAsync<T>(string requestUri, object content)
            => SendJsonAsync<T>(HttpMethod.Post, requestUri, content);

        /// <summary>
        /// Sends a PUT request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        protected Task<T> PutJsonAsync<T>(string requestUri, object content)
            => SendJsonAsync<T>(HttpMethod.Put, requestUri, content);

        /// <summary>
        /// Sends an HTTP request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="method">The HTTP method.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        private async Task<T> SendJsonAsync<T>(HttpMethod method, string requestUri, object content)
        {
            var requestJson = JsonConvert.SerializeObject(content);
            var response = await _httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });

            // Make sure the call was successful before we
            // attempt to process the response content
            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringContent);
            }

            await ProcessResponseAsync(response);
            return default;
        }

        private async Task ProcessResponseAsync(HttpResponseMessage response)
        {

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new HttpRequestException(
                        $"{response.StatusCode}: Передаваемый в теле запроса параметр модели не соответствует ожидаемому. {response.ReasonPhrase}");
                case HttpStatusCode.Unauthorized:
                    throw new HttpRequestException(
                        $"{response.StatusCode}: Пользователь не авторизирован для этой операции. {response.ReasonPhrase}");
                case HttpStatusCode.Forbidden:
                    throw new HttpRequestException(
                        $"{response.StatusCode}: Доступ к данной информации запрещен. {response.ReasonPhrase}");
                case HttpStatusCode.NotFound:
                    throw new HttpRequestException(
                        $"{response.StatusCode}: Запрашиваемая информация отсутствует на сервере. {response.ReasonPhrase}");
                case HttpStatusCode.InternalServerError:
                    var exception = await GetResponseExceptionAsync(response);
                    throw exception ?? new HttpRequestException($"{response.StatusCode}: Внутренняя ошибка сервера. {response.ReasonPhrase}");
                default:
                    throw new HttpRequestException($"{response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        protected virtual async Task<Exception> GetResponseExceptionAsync(HttpResponseMessage message)
        {
            try
            {
                return new Exception(await message.Content.ReadAsStringAsync());
            }
            catch
            {
                return default;
            }
        }
    }
}
