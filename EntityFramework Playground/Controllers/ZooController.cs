using EntityFramework_Playground.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_Playground.Controllers;

[ApiController]
[Route("[controller]")]
public class ZooController : ControllerBase
{
	private readonly ILogger<ZooController> _logger;
	private readonly IZooRepository zooRepository;

		public ZooController(ILogger<ZooController> logger, IZooRepository zooRepository)
	{
		_logger = logger;
		this.zooRepository = zooRepository;
	}

	[HttpGet(Name = "GetZoos")]
	public dynamic Get(string id)
	{
		if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid result))
			return BadRequest();

		var zoo = zooRepository.Get(result);
		if(zoo == null)
			return NotFound();

		return Ok(ZooDto.FromZoo(zoo));
	
	}
}
