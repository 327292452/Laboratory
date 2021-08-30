using ConsoleExcavate.controller;
using ConsoleExcavate.service;
using MyDB.MSSQL.Services;
using MyDB.MYSQL.Services;
using MyLibrary.Consciousness;
using MyLibrary.SUPTools.EMail;
using MyLibrary.Utile.PLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcavate
{
    class Program
    {
        private static string config_p = "C:\\Users\\ZC-024\\Desktop\\config.lab";
        private static string data_p = "C:\\Users\\ZC-024\\Desktop\\data2.txt";
        private static string data_e = "C:\\Users\\ZC-024\\Desktop\\data_result.xlsx";
        static void Main(string[] args)
        {
            try
            {
                Init();
                //UnitModel unit = new UnitModel();
                //unit.GetHashCode();
                //PasswordCipher();
                //DataTable();
                //SendEmail();
                //NerveState();
                //TestController.TestLinq();
                //RequestHTML();
                //GetDynamicModel.BtnQuery();
                //GetAsyncAwiait.MyMain();
                //RWTxt();
                //RTxt();
                //RWTxt_True();
                //GetData();
                //MLExcel.Write(data_e,new object());
                //GetAsyncAwaiat();
                //GetAsync();
                //GetQuery();
                GetPock();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey(true);
            }
        }

        private static void Init()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        private static void RequestHTML()
        {
            //MyRequest.GetInfo("https://news.163.com/");
            //MyRequest.GetInfo("http://news.163.com/special/index2015/");
            //MyRequest.GetInfo("https://news.qq.com/");
            //MyRequest.GetInfo("http://127.0.0.1:5501/jobsupervistion.html");
            //HtmlController.GetHtmlStructrue("http://127.0.0.1:5501/jobsupervistion.html");
            //HtmlController.GetHtmlStructrue("https://news.163.com/");

            //HtmlController.GetHtmlStructrue("https://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=1&rsv_idx=1&tn=baidu&wd=c%23&rsv_pq=b9f001b50027c4e1&rsv_t=d3f26rXHP8w%2Fz%2FiUFji8%2F8%2BBgwazSLLIWL7YwSZy2bVx1TrhJhw%2BSi2aKl0&rqlang=cn&rsv_enter=1&rsv_dl=tb&rsv_sug3=6&rsv_sug1=7&rsv_sug7=101&rsv_sug2=0&inputT=461012&rsv_sug4=461012&rsv_sug=2");
            //HtmlController.GetHtmlStructrue("http://www.weather.com.cn/data/sk/101110101.html"); 
            //HtmlController.GetHtmlStructrue("https://eq.10jqka.com.cn/fenshiCapitalTab/Public/data/000687/lhb_0,gzlx_1,rzlx_1,dzjy_0.txt");
            //while (true)
            //{
            //    try
            //    {
            //        HtmlController.PostHtmlStructrue("", "");
            //        if (Console.ReadLine().ToUpper() == "CLS") break;
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            HtmlController.PostHtmlStructrue("", "");
        }
        private static void RWTxt()
        {
            byte[] byt = new byte[1024];
            byte[] byt_a;
            FileStream fs = new FileStream(config_p, FileMode.OpenOrCreate);
            byt = Encoding.GetEncoding("utf-8").GetBytes("你好我的世界".ToCharArray());
            byt_a = new byte[byt.Length + 1];
            Array.Copy(byt, 0, byt_a, 1, byt.Length);
            fs.Write(byt_a, 0, byt_a.Length);
            fs.Flush();
            fs.Close();

        }

        private static void RWTxt_True()
        {
            byte[] byt_a;
            byte[] byt = new byte[1];
            FileStream fs = new FileStream(config_p, FileMode.OpenOrCreate);
            byt_a = new byte[fs.Length];
            fs.Read(byt_a, 0, byt_a.Length);

            byt[0] = Convert.ToByte(true);

            Array.Copy(byt, byt_a, 1);
            fs.Write(byt_a, 0, byt_a.Length);
            fs.Flush();
            fs.Close();

        }

        private static void RTxt()
        {
            byte[] byt = new byte[1024];
            byte[] byt_str;

            FileStream fs = new FileStream(config_p, FileMode.OpenOrCreate);
            fs.Read(byt, 0, byt.Length);
            fs.Close();
            byt_str = new byte[byt.Length];
            Array.Copy(byt, 1, byt_str, 0, byt.Length - 1);
            string str = Encoding.GetEncoding("utf-8").GetString(byt_str);
        }

        private static List<string> GetData()
        {
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(data_p, Encoding.UTF8))
            {
                string str;
                while (true)
                {
                    str = sr.ReadLine();
                    if (str != null)
                    {
                        list.Add(str);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return list;
        }

        private static string ScaleNum(int num)
        {
            int int_t = num - 26;
            if (num - 26 > 0)
            {
                ScaleNum(int_t);
            }
            else
            {
                //Encoding.ASCII.GetString( Convert.ToByte(int_t + 64));
            }
            return "";
        }

        private static void PasswordCipher()
        {
            string ciphertext = EncryptionControll.EncipherRSA("123456");
            //string ciphertext = "TBnKLD+hRCP3K3cist5iJEj8DCXJc02hKT7U5zvYDFaU2o5BkK00GJqoSJ72iqNZeCEEZ4CE2u8UBJ243JEolZgC4aRsIxLoTz+ngTqMkOeUOlJELlAoUhLz0piW2Le+D7VEJBot4MW/8vdnXoQDEAljAxcWE7l6rDgYiTCydN8=";
            ciphertext = EncryptionControll.DecipherRSA(ciphertext);
        }

        private static void DataTable()
        {
            try
            {
                DataTables.GetSetData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void SendEmail()
        {

            string ciphertext = "Ll8OtEc/+uh5wySpAkur6e7MQ/ZtdbFNY8xsUu/7fY3if11Yuy6wH0Veg5MZJL5T6b1hFDKX0w+uSzGYkU0r/w2dG+hsNKaGaVufoCis9s38RNkShrmGCrz+8Se2pBw/Wh6Q3rf53S2bhYKS915fcRHR94LeM9kLMCglLHg/iN4=";
            string style = "<span style='color:red;font-size:36px; '>{0}</span>";
            //style = "<p style=\"font-size: 10pt\">{0} 以下内容为系统自动发送，请勿直接回复，谢谢。</p><table cellspacing=\"1\" cellpadding=\"3\" border=\"0\" bgcolor=\"000000\" style=\"font-size: 10pt;line-height: 15px;\">";
            var h1 = new ConfigHost()
            {
                Server = "smtp.163.com",
                Port = 25,
                Username = "cyyc8@163.com",
                Password = EncryptionControll.DecipherRSA(ciphertext),
                EnableSsl = false
            };
            var m1 = new ConfigMail()
            {
                Subject = "Test",
                Body = string.Format(style, "你好！"),
                From = "cyyc8@163.com",
                To = new string[] { "yongy_cai@163.com" },
            };

            var agents = new List<IEMailBase>() { new EMailBase() };
            foreach (var agent in agents)
            {
                var output = "Send m1 via h1 " + agent.GetType().Name + " ";
                Console.WriteLine(output + "start");
                try
                {
                    agent.CreateHost(h1);
                    m1.Subject = output;
                    agent.CreateMail(m1);
                    agent.SendMail();
                    Console.WriteLine(output + "success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(output + "end");
                Console.WriteLine("-----------------------------------");
            }
            Console.Read();
        }

        private static void NerveState()
        {
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            int add1 = cell.GetHashCode();
            int add2 = cell2.GetHashCode();
            cell.Content = "我们需要了解这个含义";
            cell2.Content = "哈希是什么";
            cell.Loop.Add(cell2.GetHashCode());
            cell2.Loop.Add(cell.GetHashCode());
            add1 = cell.GetHashCode();
            add2 = cell2.GetHashCode();
        }

        private static void GetPartial()
        {
        }

        private static void GetAsyncAwaiat()
        {
            var t = TestController.TestAsyncAwaiat(1);
            //Parallel.For(0, 4, (f) => {
            //    var t =  TestController.TestAsyncAwaiat(f);
            //});
        }

        private static void GetAsync()
        {
            Parallel.For(0, 4, (f) =>
            {

                TestController.TestAsync();
            });
        }

        private static void GetJsonToObject()
        {

        }

        private static void GetQuery()
        {
            while (true)
            {
                try
                {
                    GetQueryInfo();
                    if (Console.ReadLine().ToUpper() == "CLOSE") break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                }
            }
        }
        private static void GetQueryInfo()
        {
            TestService service = new TestService();
            MService mService = new MService();
            //service.GetDBContext();
            mService.GetDBContext();
        }
        private static void GetPock()
        {
            ProcessLog log = new ProcessLog("pockLog\\");
            while (true)
            {
                try
                {
                    log.Logger.Debug("Staret.......");
                    List<string> list = new List<string>();
                    string context = string.Empty;
                    DZPockService service = new DZPockService();
                    var eq = service.OprationShuffle();
                    var allCount = eq.Count;
                    for (int i = 0; i < allCount; i++)
                    {
                        var p = eq.Dequeue();
                        context += p.Num + "-" + p.Col + "\t";
                        if ((i + 1) % 13 == 0)
                        {
                            Console.WriteLine(context);
                            context = string.Empty;
                        }
                    }
                    if (Console.ReadLine().ToUpper() == "CLOSE") break;
                }
                catch (Exception ex)
                {
                    log.Logger.Error("Error: " + ex.Message);
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
