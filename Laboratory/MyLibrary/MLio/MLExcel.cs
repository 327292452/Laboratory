using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace MyLibrary.MLio
{
    public class MLExcel
    {
        public static void Write(string path, object data)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];
            worksheet.Name = DateTime.Now.ToString("yyyy-MM-dd");
            Excel.Range range = worksheet.Range["A1", "C3"];
            range.Value2 = new object[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
            //workbook.Saved = true;
            //range.Interior.ColorIndex = 6;
            workbook.SaveAs(path);
            //workbook.Save();
            workbook.Close();
            app.Quit();
        }
    }
}
