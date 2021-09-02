using System;
using System.Collections.Generic;
using System.Text;

namespace MyDB.MYSQL.Services
{
    public class WorkBankService
    {
        MYContext context;
        public WorkBankService()
        {
            context = new MYContext();
        }
        public bool AddWork(List<string> works)
        {
            var seq = 0;
            try
            {
                works.ForEach(f =>
                {
                    context.WorkBanks.Add(new WorkBank { pinyin = "", work = f, tone = 0, seq = seq++ });
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
