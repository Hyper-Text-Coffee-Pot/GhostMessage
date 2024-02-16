using Microsoft.AspNetCore.Mvc;

namespace GhostMessage.Api.Controllers.v1;

[Produces("application/json")]
[ApiController]
public class MessageController : BaseApiController
{
	private readonly ICryptographyService _cryptographyService;

	public MessageController(ICryptographyService cryptographyService)
	{
		this._cryptographyService = cryptographyService;
	}

	[HttpGet]
	[Route("encrypt")]
	public async Task<IActionResult> EncryptMessage(string message, string passphrase)
	{
		// Sanitize input.
		message = message.Trim();
		passphrase = passphrase.Trim();

		var encrypted = await this._cryptographyService.EncryptAsync(message, passphrase);

		return Ok($"Encrypted value: {Convert.ToBase64String(encrypted)}");
	}

	//[HttpGet]
	//[Route("decrypt")]
	//public async Task<IActionResult> DecryptMessage(string message, string passphrase)
	//{
	//	// Sanitize input.
	//	message = message.Trim();
	//	passphrase = passphrase.Trim();

	//	var decrypted = await this._cryptographyService.DecryptAsync(encrypted, passphrase);

	//	return Ok($"Decrypted value: {decrypted}");
	//}
}