using System.Security.Cryptography;
using System.Text;
using TheSharks.Contracts.Helpers;

namespace TheSharks.Application.Helpers;

public class EncryptionHelper : IEncryptionHelper
{
    public string EncryptString(string data, string password)
    {
        var plaintextBytes = Encoding.UTF8.GetBytes(data);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var encryptor = Aes.Create();
        encryptor.Key = passwordBytes[0..32];
        encryptor.IV = passwordBytes[0..16];
        encryptor.Padding = PaddingMode.PKCS7;

        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plaintextBytes, 0, plaintextBytes.Length);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }

    public string DecryptString(string encrypted, string password)
    {
        var encryptedBytes = Convert.FromBase64String(encrypted);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var encryptor = Aes.Create();
        encryptor.Key = passwordBytes[0..32];
        encryptor.IV = passwordBytes[0..16];
        encryptor.Padding = PaddingMode.PKCS7;

        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(encryptedBytes, 0, encryptedBytes.Length);
            }

            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}