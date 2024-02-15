using System.Security.Cryptography;
using System.Text;

namespace GhostMessage.Shared.Services;

public class CryptographyService
{
	public byte[] DeriveKeyFromPassword(string password)
	{
		var emptySalt = Array.Empty<byte>();
		var iterations = 1000;
		var desiredKeyLength = 16; // 16 bytes equal 128 bits.
		var hashMethod = HashAlgorithmName.SHA384;
		return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
										 emptySalt,
										 iterations,
										 hashMethod,
										 desiredKeyLength);
	}
}