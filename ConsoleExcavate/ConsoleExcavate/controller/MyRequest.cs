using MyLibrary.Analysis;
using MyLibrary.Analysis.WebRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcavate.controller
{
    public class MyRequest
    {
        public static void GetInfo(string url)
        {
            IStructure structure = new HttpAnalysis();
            //structure.GetHttpSturcture(GetRequest.Get(url));
            Task resultAsyncGet = GetRequest.AsyncGet(url);
        }

        public static void PostInfo(string url, string parame)
        {
            IStructure structure = new HttpAnalysis();
            //structure.GetHttpSturcture(GetRequest.Get(url));
            Task resultAsyncPost = GetRequest.AsyncPost(url, parame);

        }
    }
}
