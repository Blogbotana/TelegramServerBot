using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

        public string GET(string url)
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

        private string GetAnswer(HttpWebRequest request)
        {
            string answer = "";
            HttpWebResponse response = null;
            try
            {
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

        public void POST(string url, Dictionary<string, string> parameters)
        {
            POST(url, DataPrepare(parameters));
        }

        private string DataPrepare(Dictionary<string, string> postParameters)
        {
            try
            {
                List<string> postdata = new List<string>();
                foreach (var postParameter in postParameters)
                {
                    postdata.Add(postParameter.Key + '=' + WebUtility.UrlEncode(postParameter.Value));
                }
                return string.Join("&", postdata);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось преобразовать post data: " + e.ToString());
            }
        }

        private void POST(string url, string data)
        {
            try
            {
                url += "?" + data;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.Timeout = 5000;

                string answ = GetAnswer(request);
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
