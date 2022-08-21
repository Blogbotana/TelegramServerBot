using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class ApiService
    {
        static string Url = "http://localhost:5007/";

        public static async void getUserByTG(long tgId) {
            System.Net.WebRequest reqGet = System.Net.WebRequest.Create(Url+ "User/GetUserByTG?tguserid=" + tgId);
            reqGet.Timeout = 12000;
            reqGet.Method = "GET";
            System.Net.WebResponse resp = await reqGet.GetResponseAsync();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string s = sr.ReadToEnd();
            Console.WriteLine(s);
            stream.Dispose();
        }

        //DTO as param
        public static async void CreateUser(String requestJson)
        {
            System.Net.WebRequest reqPost = System.Net.WebRequest.Create(Url + "User/Create");
            reqPost.Timeout = 12000;
            reqPost.Method = "POST";
            reqPost.ContentType = "application/json";
            //convert param to string
            byte[] buffer = Encoding.GetEncoding(1251).GetBytes(requestJson);
            //for linux Encoding.UTF8.GetBytes(requestJson);
            reqPost.ContentLength = buffer.Length;
            Stream stream = reqPost.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);

            System.Net.WebResponse resp = await reqPost.GetResponseAsync();
            System.IO.Stream streamResp = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(streamResp);
            string s = sr.ReadToEnd();
            Console.WriteLine(s);
            stream.Dispose();
            streamResp.Dispose();

        }

    }


}
