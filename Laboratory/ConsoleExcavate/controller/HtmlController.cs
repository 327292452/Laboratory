using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Analysis;
using MyLibrary.Analysis.WebRequest;

namespace ConsoleExcavate.controller
{
    public class HtmlController
    {

        static ConcurrentDictionary<string, List<string>> dic = new ConcurrentDictionary<string, List<string>>();
        public static void GetHtmlStructrue(string url)
        {
            MyRequest.GetInfo(url);
        }
        public static void PostHtmlStructrue(string url, string parame)
        {
            //if (string.IsNullOrEmpty(url)) return;
            //MyRequest.PostInfo(url, parame);
            //GetUrl("http://hq.sinajs.cn/list=");
            //GetUrlAll("http://hq.sinajs.cn/list="); 
            GetUrlAll("https://hanyu.baidu.com/zici/s?wd=%E6%88%91&query=%E6%88%91&srcid=28232&from=kg0"); 
            //GetData(url);
        }
        private static string GetUrl(string url)
        {
            List<string> list = new List<string>();
            string urlComp = string.Empty;
            int num = 1;

            var param = "sh" + "60";
            var paramSZ = "sz" + "00";

            urlComp = url + "";
            for (int i = 0; i < 10000; i++)
            {
                urlComp += param + i.ToString().PadLeft(4, '0') + ",";
                if ((i - 867 * num) == 0)
                {
                    num++;
                    list.Add(urlComp);
                    urlComp = url + "";
                }
            }
            urlComp = url + "";
            num = 1;
            for (int i = 0; i < 10000; i++)
            {
                urlComp += paramSZ + i.ToString().PadLeft(4, '0') + ",";
                if ((i - 867 * num) == 0)
                {
                    num++;
                    list.Add(urlComp);
                    urlComp = url + "";
                }
            }
            WriteText(list);
            return urlComp;
        }
        private static string GetUrlAll(string url)
        {
            List<string> list = new List<string>();
            string urlComp = string.Empty;
            int num = 1;

            var param = "sh";
            var paramSZ = "sz";

            urlComp = url + "";
            for (int i = 0; i < 1000000; i++)
            {
                urlComp += param + i.ToString().PadLeft(6, '0') + ",";
                if ((i - 867 * num) == 0)
                {
                    num++;
                    list.Add(urlComp);
                    urlComp = url + "";
                }
            }
            urlComp = url + "";
            num = 1;
            for (int i = 0; i < 1000000; i++)
            {
                urlComp += paramSZ + i.ToString().PadLeft(6, '0') + ",";
                if ((i - 867 * num) == 0)
                {
                    num++;
                    list.Add(urlComp);
                    urlComp = url + "";
                }
            }
            WriteText(list);
            return urlComp;
        }

        private static void WriteText(List<string> list)
        {

            using (FileStream stream = new FileStream(@"C:\Users\sup\Documents\st.txt", FileMode.OpenOrCreate))
            {
                list.ForEach(f =>
                {
                    stream.Write(Encoding.UTF8.GetBytes(f + "\r\n"));
                });
            }
        }
        private static void GetData(string url)
        {
            try
            {
                string data = "var hq_str_sh601006=\"dq,0.000,5.930,5.930,0.000,0.000,0.000,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,2021-08-12,09:09:36,00,\";" +
                              "var hq_str_sh600313=\"nf,0.000,5.230,5.230,0.000,0.000,0.000,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,2021-08-12,09:09:36,00,\";" +
                              "var hq_str_sh600001=\"hd,0.000,0.000,0.000,0.000,0.000,0.000,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,0,0.000,2021-08-06,11:45:03,-3\";";
                var list = GetSTList(data);
                Parallel.ForEach(list, f =>
                {
                    var st = GetST(f).Result;
                });

                var dicList = dic.ToArray();

                //list.ForEach(f =>
                //{
                //    var st = GetST(f).Result;
                //});
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static List<string> GetSTList(string data)
        {
            List<string> list = new List<string>();
            var datas = data.Split(";");

            list = new List<string>(datas);
            list.Remove(" ");

            return list;
        }
        private async static Task<List<string>> GetST(string data)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(data)) return list;
            try
            {
                var state = data.Split("=");
                var code = state[0].Trim().Substring(state[0].Length - 6, 6);
                var content = state[1].Replace("\"", "").Split(",");
                list.Add(code);
                list.AddRange(content);

                dic.TryAdd(code, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
    }
}
