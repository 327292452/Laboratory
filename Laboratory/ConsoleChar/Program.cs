using CharService.CharServices;
using System;

namespace Test.CharConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCharService();
            Console.ReadKey();
        }

        private static void GetCharService()
        {
            CharWebService service = new CharWebService();
            service.WebSocketInitialize();
            service.Runing();

        }
    }
}
