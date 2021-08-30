using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleExcavate.service
{
    public class TestAsyncAwaiatService
    {
        public async Task<bool> GetAsyncAwaiat()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Progress..." + i.ToString());
                    Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }
    }
}
