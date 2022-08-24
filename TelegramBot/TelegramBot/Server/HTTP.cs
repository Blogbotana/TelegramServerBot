using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public string GET(string url)//в доработке
        {
            string answer = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.AllowAutoRedirect = false;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.Timeout = 5000;

                answer = GetAnswer(request);
            }
            catch (WebException e1)
            {
                throw e1;
            }
            return answer;
        }

        private string GetAnswer(HttpWebRequest request, byte[] bytes = null)
        {
            string answer = "";
            HttpWebResponse response = null;
            try
            {
                if (bytes != null)
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes);
                    }

                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        answer = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException webEx)
            {
                throw webEx;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return answer;
        }

        public async void POST(string url, string data)
        {
            try
            {
                var content = new System.Net.Http.ByteArrayContent(Encoding.UTF8.GetBytes(data));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await httpClient.PostAsync(url, content);
            }
            catch (WebException ex)
            {
                string error = "";
                WebExceptionStatus status = ex.Status;

                if ((ex.Response != null) && (ex.Response.GetResponseStream() != null))
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        error += reader.ReadToEnd() + "\n";
                    }
                }
                else
                {
                    throw new Exception("Не удалось отправить POST запрос: \n" + ex.ToString());
                }

                if (status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                    error += $"Статусный код ошибки: {(int)httpResponse.StatusCode} - {httpResponse.StatusCode} \n";
                }

                throw new Exception("Не удалось отправить POST запрос: \n" + error);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось отправить POST запрос. Не WebException: \n" + e.ToString());
            }
        }
    }
}
