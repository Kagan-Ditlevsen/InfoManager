using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InfoAndroidApp
{
    internal class Api
    {
        internal static readonly byte[] _keyOne = Encoding.UTF8.GetBytes("Y5Q~H7Pe");
        internal static readonly byte[] _keyTwo = Encoding.UTF8.GetBytes("f*yMbv4d");
        internal static readonly byte[] _keyThree = Encoding.UTF8.GetBytes("%+(x8zR,");
        internal static string ApiAuthenticationKey(int userId = 10001)
        {
            // #TODO userId should come from logon
            string key = "";
            try
            {
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new())
                {
                    // First pass
                    byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(userId.ToString());
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

                return key;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> Get(string callFunction, string callQuery = "")
        {
            string rtn = "_not_set_";
            using (var client = new HttpClient())
            {
                try
                {
                    Task<HttpResponseMessage> response = client.GetAsync($"https://infomanager.dk/Api/{Api.ApiAuthenticationKey()}/{callFunction}?{callQuery}");
                    if (response.Result != null)
                    {
                        rtn = "Response is loading...";
                        Task task = response.Result.Content.ReadAsStreamAsync().ContinueWith(t =>
                        {
                            byte[] buffer = new byte[(int)t.Result.Length];
                            t.Result.Read(buffer, 0, (int)t.Result.Length);
                            rtn = Encoding.UTF8.GetString(buffer);
                        });
                        task.Wait();

                        return rtn;
                    }
                    else
                    {
                        return "Response is empty...";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }
    }
}