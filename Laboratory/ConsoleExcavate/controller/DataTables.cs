using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsoleExcavate.controller
{
    public class DataTables
    {
        public static string GetDataTable()
        {
            DataTable dt = new DataTable();
            try
            {

            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }

        public static string GetSetData()
        {
            DataSet ds = new DataSet();
            try
            {
                if (ds == null) throw new Exception("Is DataSet Is Null!");
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }
    }
}
