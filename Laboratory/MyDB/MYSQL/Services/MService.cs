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
            context = new MYContext();
        }
        public void GetDBContext()
        {

            var result = context.Tests.FirstOrDefault();

            string tmp = string.Empty;
        }
    }
}
