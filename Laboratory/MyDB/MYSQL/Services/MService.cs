using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDB.MYSQL.Services
{
    public class MService
    {
        MYContext context;
        public MService()
        {
            context = new MYContext("Database=test;Data Source=localhost;Port=3306;User Id=root;Password=Password@1;Allow User Variables=true;default command timeout=120;Pooling=true;Max Pool Size=1000;SslMode=none;");
        }
        public void GetDBContext()
        {

            var result = context.Tests.FirstOrDefault();

            string tmp = string.Empty;
        }
    }
}
