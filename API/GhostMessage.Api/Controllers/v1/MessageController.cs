using Microsoft.AspNetCore.Mvc;

namespace GhostMessage.Api.Controllers.v1;

[Route("v{version:apiVersion}/message")]
public class MessageController : BaseApiController
{
	private readonly ICryptographyService _cryptographyService;

	public MessageController(ICryptographyService cryptographyService)
	{
		this._cryptographyService = cryptographyService;
	}

	[HttpGet]
	public async Task<IActionResult> EncryptMessage(string message, string passphrase)
	{
		var iv = this._cryptographyService.GenerateInitializationVector();
		var encrypted = await this._cryptographyService.EncryptAsync(message, iv, passphrase);
		var decrypted = await this._cryptographyService.DecryptAsync(encrypted, iv, passphrase);

		return Ok($"Encrypted value: {BitConverter.ToString(encrypted)} -- Decrypted value: {decrypted}");
	}
}