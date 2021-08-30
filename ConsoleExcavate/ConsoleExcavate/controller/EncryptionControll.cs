using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleExcavate.controller
{
    public class EncryptionControll
    {
        public static string EncipherMD5(string text)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(text);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }

        public static string EncipherRSA(string text)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = string.Empty;//密匙容器的名称，保持加密解密一致才能解密成功
            if (string.IsNullOrEmpty(param.KeyContainerName))
            {
                param.KeyContainerName = Console.ReadLine();
            }
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
                {
                    byte[] plaindata = Encoding.Default.GetBytes(text);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecipherRSA(string ciphertext)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = string.Empty;//密匙容器的名称，保持加密解密一致才能解密成功
            if (string.IsNullOrEmpty(param.KeyContainerName))
            {
                while (true)
                {
                    var readKey = Console.ReadKey();
                    Console.Clear();
                    if (readKey.KeyChar == 13) break;
                    param.KeyContainerName += readKey.KeyChar.ToString();
                }
            }
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
                {
                    byte[] encryptdata = Convert.FromBase64String(ciphertext);//将要加密的字符串转换为字节数组
                    byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                    return Encoding.Default.GetString(decryptdata);//将加密后的字节数组转换为字符串
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
