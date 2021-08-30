using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Utile.IO
{
    public interface IFileOption
    {
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="isExistSuffix"></param>
        /// <param name="fileSuffix"></param>
        /// <returns></returns>
        List<string> GetFile(string path, bool isExistSuffix, string fileSuffix);
        /// <summary>
        /// 查找文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="isExistSuffix"></param>
        /// <param name="fileSuffix"></param>
        /// <returns></returns>
        List<string> FindFile(string path, bool isExistSuffix, string fileSuffix);
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        string FileRead(string path);
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content"></param>
        void CraeteFile(string path, byte[] content);
    }
}
