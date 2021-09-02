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
        public bool AddWork(List<WorkBank> works)
        {
            var seq = 0;
            try
            {
                works.ForEach(f =>
                {
                    context.WorkBanks.Add(f);
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
