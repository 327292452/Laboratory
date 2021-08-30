using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyLibrary.Analysis.WebRequest
{
    public class HttpAnalysis : IStructure
    {
        public string GetHttpSturcture(string html)
        {
            Dictionary<int, string> dic_html_r = new Dictionary<int, string>();
            html = html.IndexOf("<!DOCTYPE html>") != -1 ? html.Substring(html.IndexOf("<!DOCTYPE html>")) : html;
            html = html.Replace("\r\n", "\n");
            html = html.Replace("\t", "");

            string[] html_r = html.Split('\n');
            List<string> html_l = new List<string>(html.Split('\n'));
            html_l.RemoveAll(w => string.IsNullOrWhiteSpace(w));
            string find = html_l.Find(f => f.Contains("<body"));
            int Startlen = html_l.FindIndex(f => f.Contains("<body"));
            int Endlen = html_l.FindIndex(f => f.Contains("</body"));
            int i = 1;
            foreach (string item in html_r)
            {
                GetStartEnd(item);
                dic_html_r.Add(i++, item);
            }
            var tmp = GetStartEnd(html);

            Regex regex = new Regex(@"<html>[.]{*}</html>");

             string str = regex.Match(html).ToString();
            return str;
        }

        private char[] GetStartEnd(string value)
        {
            return value.ToCharArray();
        }
    }
}
