using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Utile.IO
{
    public class FileOption : IFileOption
    {
        public List<string> GetFile(string path, bool isExistSuffix, string fileSuffix)
        {
            ConcurrentDictionary<string, List<string>> dic = new ConcurrentDictionary<string, List<string>>();
            List<string> list = new List<string>();
            if (!Directory.Exists(path)) throw new Exception("未找到指定文件路径！");

            var listDir = Directory.GetDirectories(path);
            var listFile = Directory.GetFiles(path);
            dic.TryAdd(path, new List<string>());
            Parallel.ForEach(listFile, item =>
            {
                if (!item.Contains(".") && isExistSuffix) return;
                if (item.Substring(item.LastIndexOf(".")).Contains(fileSuffix))
                {
                    dic[path].Add(item);
                }
            });
            Parallel.ForEach(listDir, item =>
            {
                dic.TryAdd(item, new List<string>());
                dic[item].AddRange(FindFile(item, isExistSuffix, fileSuffix));
            });

            dic.Values.ToList().ForEach(f =>
            {
                list.AddRange(f);
            });

            return list;
        }
        public List<string> FindFile(string path, bool isExistSuffix, string fileSuffix)
        {
            List<string> list = new List<string>();

            var listFile = Directory.GetFiles(path);

            for (int i = 0; i < listFile.Length; i++)
            {
                if (!listFile[i].Contains(".") && isExistSuffix) continue;
                if (listFile[i].Substring(listFile[i].LastIndexOf(".")).Contains(fileSuffix))
                {
                    list.Add(listFile[i]);
                }
            }

            return list;
        }

        public string FileRead(string path)
        {
            var file = new FileInfo(path);
            var stream = file.OpenRead();
            byte[] byt = new byte[stream.Length];
            stream.Read(byt, 0, (int)stream.Length);
            string content = Encoding.UTF8.GetString(byt);

            return content;
        }

        public void FindDirectory(string path)
        {
            var listDir = Directory.GetDirectories(path);
            var listFile = Directory.GetFiles(path);
            Parallel.ForEach(listFile, f =>
            {
                if (f.Contains("ln"))
                {
                    var file = new FileInfo(f);
                    var stream = file.OpenRead();
                    byte[] byt = new byte[stream.Length];
                    stream.Read(byt, 0, (int)stream.Length);
                    string content = Encoding.Unicode.GetString(byt);
                    //var listObject 
                }
            });
        }

        public void CraeteFile(string path, byte[] content)
        {

            try
            {
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    fileStream.Write(content, 0, content.Length);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }
}
