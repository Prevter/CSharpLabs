using System.Security.Cryptography;
using System.Text;

namespace Labs.Lab11
{
	public static class Lab11
	{
		public static string HorizontalString(string input)
		{
			if (string.IsNullOrEmpty(input)) return "";
			return string.Join('\n', input.ToCharArray());
		}

		public static string Encrypt(string message, string password)
		{
			byte[] messageBytes = Encoding.UTF8.GetBytes(message);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			byte[] keyBytes = SHA256.HashData(passwordBytes);

			using Aes aes = Aes.Create();
			aes.Key = keyBytes;
			aes.GenerateIV();

			using ICryptoTransform encryptor = aes.CreateEncryptor();
			using var memoryStream = new MemoryStream();
			using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			memoryStream.Write(aes.IV, 0, aes.IV.Length);
			cryptoStream.Write(messageBytes, 0, messageBytes.Length);
			cryptoStream.FlushFinalBlock();

			return Convert.ToBase64String(memoryStream.ToArray());
		}

		public static string Decrypt(string encryptedMessage, string password)
		{
			byte[] encryptedBytes = Convert.FromBase64String(encryptedMessage);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			byte[] keyBytes = SHA256.HashData(passwordBytes);

			using Aes aes = Aes.Create();
			aes.Key = keyBytes;

			byte[] iv = new byte[16];
			Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
			aes.IV = iv;

			using ICryptoTransform decryptor = aes.CreateDecryptor();
			using var memoryStream = new MemoryStream();
			using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
			cryptoStream.Write(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length);
			cryptoStream.FlushFinalBlock();

			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}
	}
}