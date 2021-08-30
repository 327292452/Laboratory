using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleExcavate.controller
{
    public class GetRequest
    {
        public static string Get(string url)
        {
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            Encoding encoding;
            webRequest.Method = "GET";
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            webRequest.ContentType = "text/html;charset=iso-8859-1";//iso-8859-1
            webRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            webRequest.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Mobile Safari/537.36";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            encoding = Encoding.GetEncoding(response.CharacterSet);
            //Stream myResponseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);

            StreamReader myStreamReader;
            if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                myStreamReader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), encoding);
            else
                myStreamReader = new StreamReader(response.GetResponseStream(), encoding);

            //byte[] byt = new byte[1024];
            //string value = "";
            //while (true) {
            //    int len = myStreamReader.Read(byt, 0, byt.Length);
            //    if (len <= 0) break;
            //    value += Encoding.GetEncoding("GB2312").GetString(byt, 0, len);
            //}
            // new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //Stream myResponseStream = response.GetResponseStream();

            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            //myResponseStream.Close();
            return retString;
        }
        public string Post()
        {
            return "";
        }
        static readonly HttpClient client = new HttpClient();
        public static async Task AsyncGet(string url)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                //Above three lines can be replaced with new helper method below
                //string responseBody = await client.GetStringAsync(url);

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }

        public static async Task<string> AsyncPost(string url, string jsonParame)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                using (var http = new HttpClient(handler))
                {
                    var content = new StringContent(jsonParame, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return string.Empty;
        }
    }
}
