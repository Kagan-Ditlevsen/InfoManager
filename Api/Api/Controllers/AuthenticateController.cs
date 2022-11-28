using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace infomanager.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        internal static readonly byte[] _keyOne = Encoding.UTF8.GetBytes("Y5Q~H7Pe");
        internal static readonly byte[] _keyTwo = Encoding.UTF8.GetBytes("f*yMbv4d");
        internal static readonly byte[] _keyThree = Encoding.UTF8.GetBytes("%+(x8zR,");
        //[HttpGet("Get", Name = "AuthGet")]
        //public string AuthGet(string id)
        //{ 
        //    return EncryptAuthenticateKey(id);
        //}

        [HttpGet("{id}", Name = "Auth")]
        public string Auth(string id)
        {
            return DecryptAuthenticateKey(id);
        }

        internal string EncryptAuthenticateKey(string stringToEncrypt)
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
        internal string DecryptAuthenticateKey(string stringToDecrypt)
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
                            AuthenticatedUser.Retrieve(Encoding.UTF8.GetString(memoryStream.ToArray())),
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