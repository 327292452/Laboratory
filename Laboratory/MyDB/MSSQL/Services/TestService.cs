using Microsoft.EntityFrameworkCore;
using System;

namespace MyDB.MSSQL.Services
{
    public class TestService
    {
        Microsoft.Extensions.Options.IOptionsMonitor<AppSettings> appSettings;
        TestContext context;
        public TestService()
        {
            context = new TestContext("10.168.1.68\\sql2008;database=NBJC;uid=sa;pwd=fuqing68+++;Pooling='true';Min Pool Size=3;");
        }
        public void GetDBContext()
        {

            var list = context.UUMSOwnerDeptses.ToListAsync().Result;
            if (list.Count > 0)
            {
                Console.WriteLine("查询成功！");
            }
        }
    }

    class AppSettings
    {
        public string JCContext { get; set; }
    }
}
