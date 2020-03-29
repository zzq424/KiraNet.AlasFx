﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace KiraNet.AlasFx.Security
{
    /// <summary>
    /// AES加密解密操作类
    /// </summary>
    public static class AesManager
    {
        /// <summary>
        /// 128位处理key 
        /// </summary>
        /// <param name="keyArray">原字节</param>
        /// <param name="key">处理key</param>
        /// <returns></returns>
        private static byte[] GetAesKey(byte[] keyArray, string key)
        {
            byte[] newArray = new byte[16];
            if (keyArray.Length < 16)
            {
                for (int i = 0; i < newArray.Length; i++)
                {
                    if (i >= keyArray.Length)
                    {
                        newArray[i] = 0;
                    }
                    else
                    {
                        newArray[i] = keyArray[i];
                    }
                }
            }
            return newArray;
        }
        /// <summary>
        /// 使用AES加密字符串,按128位处理key, 默认编码为UTF8
        /// </summary>
        /// <param name="value">加密内容</param>
        /// <param name="key">秘钥，需要128位、256位.....</param>
        /// <param name="encoding">编码</param>
        /// <param name="autoHandle">是否自动处理key值</param>
        /// <returns>Base64字符串结果</returns>
        public static string Encrypt(string value, string key, Encoding encoding = null, bool autoHandle = true)
        {
            Check.NotNullOrEmpty(value, nameof(value));
            Check.NotNullOrEmpty(key, nameof(key));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] keyArray = encoding.GetBytes(key);
            if (autoHandle)
            {
                keyArray = GetAesKey(keyArray, key);
            }
            byte[] toEncryptArray = encoding.GetBytes(value);

            using (SymmetricAlgorithm des = Aes.Create())
            {
                des.Key = keyArray;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = des.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray);
            }
        }
        /// <summary>
        /// 使用AES解密字符串,按128位处理key, 默认编码为UTF8
        /// </summary>
        /// <param name="value">解密内容</param>
        /// <param name="key">秘钥，需要128位、256位.....</param>
        /// <param name="encoding">编码</param>
        /// <param name="autoHandle">是否自动处理key值</param>
        /// <returns>解密结果</returns>
        public static string Decrypt(string value, string key, Encoding encoding = null, bool autoHandle = true)
        {
            Check.NotNullOrEmpty(value, nameof(value));
            Check.NotNullOrEmpty(key, nameof(key));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] keyArray = encoding.GetBytes(key);
            if (autoHandle)
            {
                keyArray = GetAesKey(keyArray, key);
            }
            byte[] toEncryptArray = encoding.GetBytes(value);

            using (SymmetricAlgorithm des = Aes.Create())
            {
                des.Key = keyArray;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = des.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
        }
    }
}