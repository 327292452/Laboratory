using Microsoft.Office.Interop.Excel;
using MyLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleExcavate.controller
{
    public class GetDynamicModel
    {
        public static void BtnQuery()
        {
            List<DynamicModel> modelList = new List<DynamicModel>();

            for (int i = 0; i < 10; i++)
            {
                dynamic model = new DynamicModel();
                model.col0 = i;
                model.col1 = i;
                model.col2 = i;
                model.col3 = i;
                model.col4 = i;
                model.col5 = i;
                modelList.Add(model);
            }
        }
    }
}
