using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GhostMessage.Api.Controllers.v1
{
	[Route("v{version:apiVersion}/[controller]")]
	public class BaseApiController : ControllerBase
	{
	}
}