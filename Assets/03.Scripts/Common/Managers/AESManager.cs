using System.Security.Cryptography;
using System.Text;

public static class AESManager
{
    private static byte[] key = Encoding.UTF8.GetBytes(StringManager.AESKey);
    private static byte[] iv = Encoding.UTF8.GetBytes(StringManager.AESKey);

    public static string EncryptString(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

            return System.Convert.ToBase64String(encrypted);
        }
    }

    public static string DecryptString(string cipherText)
    {
        byte[] buffer = System.Convert.FromBase64String(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] decrypted = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}