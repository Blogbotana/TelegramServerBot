using ServerBot.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> GET(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                if(!response.IsSuccessStatusCode)
                    Console.WriteLine(response.ReasonPhrase + " in GET " + url + "\tStatus\t" + response.StatusCode.ToString());
                
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
                {
                    Console.WriteLine(response.ReasonPhrase + " in POST " + url + "\tStatus\t" + response.StatusCode.ToString());
                    //TODO make logger
                }
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
                    Console.WriteLine(response.ReasonPhrase + " in PUT " + url + "\tStatus\t" + response.StatusCode.ToString());

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
                    Console.WriteLine(response.ReasonPhrase + " in PUT " + url + "\tStatus\t" + response.StatusCode.ToString());

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
    }
}
