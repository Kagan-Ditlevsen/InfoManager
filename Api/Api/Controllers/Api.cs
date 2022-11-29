using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using infomanager.DAL;

namespace infomanager.Api
{
    public partial class ApiHelper
    {
        private static readonly byte[] _keyOne = Encoding.UTF8.GetBytes("Y5Q~H7Pe");
        private static readonly byte[] _keyTwo = Encoding.UTF8.GetBytes("f*yMbv4d");
        private static readonly byte[] _keyThree = Encoding.UTF8.GetBytes("%+(x8zR,");

        internal static string ApiException(string title, string message)
        {
            var rtn = new
            {
                Title = title,
                Message = message,
                DateTime= DateTime.Now,
            };

            return JsonConvert.SerializeObject(rtn, Formatting.None, ApiHelper.serializerSettings);
        }

        internal static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            MaxDepth = 1
        };

        internal static infomanager_dk_db_mainEntities Db()
        {
            var context = new infomanager_dk_db_mainEntities();
            context.Configuration.LazyLoadingEnabled = false; // Required to serialize to json

            return context;
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.tripledes.create?view=net-7.0
        internal static string EncryptString(string stringToEncrypt)
        {
            string key = "";
            try
            {
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
                {
                    // First pass
                    byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(stringToEncrypt.ToString());
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(_keyOne, _keyTwo), CryptoStreamMode.Write);

                    cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] bytesToEncryptSecond = memoryStream.ToArray();

                    // Second pass
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(_keyTwo, _keyThree), CryptoStreamMode.Write);

                    cryptoStream.Write(bytesToEncryptSecond, 0, bytesToEncryptSecond.Length);
                    cryptoStream.FlushFinalBlock();

                    key = Convert.ToBase64String(memoryStream.ToArray());
                }

                key = key.Replace("/", "¤").Replace("+", "$");
                return key;
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static string DecryptString(string stringToDecrypt)
        {
            stringToDecrypt = stringToDecrypt.Replace("¤", "/").Replace("$", "+");
            try
            {
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
                {
                    // First pass
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(_keyTwo, _keyThree), CryptoStreamMode.Write);
                    cryptoStream.Write(Convert.FromBase64String(stringToDecrypt), 0, Convert.FromBase64String(stringToDecrypt).Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] encryptedString2 = memoryStream.ToArray();

                    // Second pass
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(_keyOne, _keyTwo), CryptoStreamMode.Write);
                    cryptoStream.Write(encryptedString2, 0, encryptedString2.Length);
                    cryptoStream.FlushFinalBlock();

                    return
                        JsonConvert.SerializeObject(
                            Encoding.UTF8.GetString(memoryStream.ToArray()),
                            Formatting.None,
                            ApiHelper.serializerSettings
                        );
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}