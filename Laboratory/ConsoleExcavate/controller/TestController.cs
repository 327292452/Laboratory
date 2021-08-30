using ConsoleExcavate.model;
using ConsoleExcavate.service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcavate.controller
{
    public class TestController
    {
        public static void TestLinq()
        {
            List<TestModel> list = new List<TestModel>();
            List<TestModel> list2 = new List<TestModel>();
            list.Add(new TestModel { Vaule1 = "test1", Vaule2 = "aaa", Vaule3 = "123" });
            list.Add(new TestModel { Vaule1 = "test2", Vaule2 = "bbb", Vaule3 = "123" });
            list.Add(new TestModel { Vaule1 = "test3", Vaule2 = "ccc", Vaule3 = "123" });
            list2.Add(new TestModel { Vaule1 = "test1", Vaule2 = "ddd", Vaule3 = "123" });
            list2.Add(new TestModel { Vaule1 = "test4", Vaule2 = "eee", Vaule3 = "123" });
            list2.Add(new TestModel { Vaule1 = "test2", Vaule2 = "fff", Vaule3 = "123" });
            list.AddRange(list2);
            var tmp = list.GroupBy(g => g.Vaule1).Where(w => w.Count() > 1);
            foreach (IGrouping<string, TestModel> item in tmp)
            {
                list2.Remove(list2.SingleOrDefault(w => w.Vaule1 == item.Key));
            }
        }

        public static void TestStream()
        {
            using (StreamWriter stream = new StreamWriter(""))
            {

            }
        }

        public static void TestPartial()
        {
            TestPartialService service = new TestPartialService(1, 2);
            service.GetReuslt();
            service.GetReusltTen();
        }

        public static async Task TestAsyncAwaiat(int i)
        {
            TestAsyncAwaiatService service = new TestAsyncAwaiatService();
            Console.WriteLine(i.ToString() + "Process...");
            var t = service.GetAsyncAwaiat();
            var bolT = await t;
            Console.Write("Result Value：" + bolT.ToString());
        }

        public static void TestAsync()
        {
            TestAsyncAwaiatService service = new TestAsyncAwaiatService();
            service.GetAsyncAwaiat();
            string strT = "awita";
        }
    }
}
