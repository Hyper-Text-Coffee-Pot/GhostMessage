namespace GhostMessage.Shared.Abstractions;

public interface ICryptographyService
{
	Task<string> DecryptAsync(byte[] encrypted, byte[] initializationVector, string passphrase);

	byte[] DeriveKeyFromPassword(string password);

	Task<byte[]> EncryptAsync(string clearText, byte[] initializationVector, string passphrase);

	byte[] GenerateInitializationVector();
}