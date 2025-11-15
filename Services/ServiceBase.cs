using System.Net;

namespace BlazorApp2.Services
{
    public class ServiceBase
    {
        private static readonly HttpClient Client;

        static ServiceBase()
        {
            Client = new HttpClient();
        }

        private HttpClient GetHttpClient()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            return Client;
        }

        private HttpRequestMessage GetRequestMessage(string uri, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, uri) { Content = content };
            return request;
        }

        public string CreateApiUrl(string serviceName)
        {
            return $"http://localhost:5224/api/{serviceName}";
        }

        protected T? GetJson<T>(string uri)
        {
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Get)).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode && ValidateJsonContent(response.Content))
            {
                return response.Content.ReadFromJsonAsync<T>().Result;
            }

            return default;
        }

        protected T? PostJson<T>(string uri, T value)
        {
            return PostJson<T, T>(uri, value);
        }

        protected TResult? PostJson<TValue, TResult>(string uri, TValue value)
        {
            HttpRequestMessage request;
            if (value is MultipartFormDataContent data)
            {
                request = GetRequestMessage(uri, HttpMethod.Post, data);
            }
            else
            {
                request = GetRequestMessage(uri, HttpMethod.Post, JsonContent.Create(value));
            }



            var response = GetHttpClient().SendAsync(request)
                .GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode && ValidateJsonContent(response.Content))
            {
                var result = response.Content.ReadFromJsonAsync<TResult>().Result;
                return result;
            }



            return default;
        }

        protected bool DeleteJson<TValue>(string uri, TValue? value)
        {
            HttpRequestMessage request;
            if (value != null)
            {
                if (value is MultipartFormDataContent data)
                {
                    request = GetRequestMessage(uri, HttpMethod.Delete, data);
                }
                else
                {
                    request = GetRequestMessage(uri, HttpMethod.Delete, JsonContent.Create(value));
                }

            }
            else
            {
                request = GetRequestMessage(uri, HttpMethod.Delete);
            }



            var response = GetHttpClient().SendAsync(request)
                .GetAwaiter().GetResult();
            return response.IsSuccessStatusCode;
        }

        protected bool PutJson<TValue>(string uri, TValue? value)
        {
            HttpRequestMessage request;
            if (value != null)
            {
                if (value is MultipartFormDataContent data)
                {
                    request = GetRequestMessage(uri, HttpMethod.Put, data);
                }
                else
                {
                    request = GetRequestMessage(uri, HttpMethod.Put, JsonContent.Create(value));
                }

            }
            else
            {
                request = GetRequestMessage(uri, HttpMethod.Put);
            }



            var response = GetHttpClient().SendAsync(request)
                .GetAwaiter().GetResult();
            return response.IsSuccessStatusCode;
        }

        private static bool ValidateJsonContent(HttpContent content)
        {
            var mediaType = content?.Headers.ContentType?.MediaType;
            return mediaType != null && mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

    }
}
