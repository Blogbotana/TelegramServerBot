using System.Net.Http.Json;

namespace TelegramBot.Server
{
    /// <summary>
    /// Реализует работу по протоколу HTTP 
    /// </summary>
    public class HTTP
    {
        private static HTTP instance;
        private static object SyncObject = new object();
        private readonly static HttpClient httpClient = new HttpClient();
        private HTTP()
        {
            httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        ~HTTP()
        {
            httpClient.Dispose();
        }

        public static HTTP GetInstance
        {
            get
            {
                if (instance == null)
                    lock (SyncObject)
                    {
                        if (instance == null)
                            instance = new HTTP();
                    }
                return instance;
            }
        }

        public static void SetJWTToken(string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
        }
        public async Task<string> GET<T>(string url, T data) where T : class
        {
            try
            {
                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Content = JsonContent.Create(data)
                };
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    if (await IsTokenExpired(response))
                        return await GET<T>(url, data);

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Couldn't send GET request:\n" + ex.ToString(), ex.InnerException, ex.StatusCode);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't send GET request. Не HttpRequestException\n" + e.ToString());
            }
        }

        public async Task<string> GET(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    if (await IsTokenExpired(response))
                        return await GET(url);

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Couldn't send GET request:\n" + ex.ToString(), ex.InnerException, ex.StatusCode);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't send GET request. Не HttpRequestException\n" + e.ToString());
            }
        }

        public async Task<HttpResponseMessage> POST<T>(string url, T data) where T : class
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, data);

                if (!response.IsSuccessStatusCode)
                    if (await IsTokenExpired(response))
                        return await POST(url, data);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Couldn't send POST request:\n" + ex.ToString(), ex.InnerException, ex.StatusCode);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't send POST request. Не HttpRequestException\n" + e.ToString());
            }
        }

        public async Task<HttpResponseMessage> PUT<T>(string url, T data) where T : class
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(url, data);

                if (!response.IsSuccessStatusCode)
                    if (await IsTokenExpired(response))
                        return await PUT(url, data);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Couldn't send PUT request:\n" + ex.ToString(), ex.InnerException, ex.StatusCode);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't send PUT request. Не HttpRequestException\n" + e.ToString());
            }
        }

        public async Task<HttpResponseMessage> PUT(string url)
        {
            try
            {
                StringContent stringContent = new StringContent(url);
                var response = await httpClient.PutAsync(url, stringContent);//TODO test for work, i'm not sure it's working PUT stringcontent

                if (!response.IsSuccessStatusCode)
                    if (await IsTokenExpired(response))
                        return await PUT(url);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Couldn't send PUT request:\n" + ex.ToString(), ex.InnerException, ex.StatusCode);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't send PUT request. Не HttpRequestException\n" + e.ToString());
            }
        }

        private async Task<bool> IsTokenExpired(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await ServerAPI.GetInstance.AuthorizeBot();
                return true; 
            }
            else
                return false;
        }
    }
}
