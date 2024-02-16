using GhostMessage.Shared.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace GhostMessage.Shared.Services;

public class CryptographyService : ICryptographyService
{
	public async Task<string> DecryptAsync(byte[] encrypted, byte[] initializationVector, string passphrase)
	{
		using Aes aes = Aes.Create();
		aes.Key = DeriveKeyFromPassword(passphrase);
		aes.IV = initializationVector;

		using MemoryStream input = new(encrypted);
		using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

		using MemoryStream output = new();
		await cryptoStream.CopyToAsync(output);

		return Encoding.Unicode.GetString(output.ToArray());
	}

	public byte[] DeriveKeyFromPassword(string password)
	{
		var salt = this.GenerateSalt();
		var iterations = 1000;
		var desiredKeyLength = 16; // 16 bytes equal 128 bits.
		var hashMethod = HashAlgorithmName.SHA384;
		return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
										 salt,
										 iterations,
										 hashMethod,
										 desiredKeyLength);
	}

	public async Task<byte[]> EncryptAsync(string clearText, string passphrase)
	{
		using (Aes aes = Aes.Create())
		{
			aes.Key = DeriveKeyFromPassword(passphrase);

			using MemoryStream output = new();
			using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

			await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(clearText));
			await cryptoStream.FlushFinalBlockAsync();

			return output.ToArray();
		}
	}

	public byte[] GenerateSalt()
	{
		byte[] salt = new byte[16];

		using (var rng = new RNGCryptoServiceProvider())
		{
			rng.GetBytes(salt);
		}

		return salt;
	}
}