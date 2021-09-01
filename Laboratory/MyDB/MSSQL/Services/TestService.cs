using Microsoft.EntityFrameworkCore;
using System;

namespace MyDB.MSSQL.Services
{
    public class TestDBService
    {
        Microsoft.Extensions.Options.IOptionsMonitor<AppSettings> appSettings;
        TestContext context;
        public TestDBService(string connection)
        {
            context = new TestContext(connection);
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
