using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GhostMessage.Api.Controllers.v1
{
	[ApiVersion(1.0)]
	[ApiController]
	public class BaseApiController : ControllerBase
	{
	}
}