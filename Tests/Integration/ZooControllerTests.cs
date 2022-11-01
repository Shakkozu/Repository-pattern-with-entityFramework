using EntityFramework_Playground.Controllers;
using EntityFramework_Playground.Domain.Repositories;
using EntityFramework_Playground.Model;
using FakeItEasy;
using System.Net;

namespace Tests.Integration;
public class ZooControllerTests
{
	private HttpClient httpClient;
	private IZooRepository _zooRepository;
	private ZooController zooController;

	[SetUp]
	public void SetUp()
	{
		httpClient = new HttpClient();
		_zooRepository = A.Fake<IZooRepository>();
		zooController = new ZooController(null, _zooRepository);
	}

	[Test]
	public async Task GetZooByGuid_WhenZooDoesNotExist_ReturnNotFound()
	{
		A.CallTo(() => _zooRepository.Get(A<Guid>._)).Returns(null);

		var _response = zooController.Get(Guid.NewGuid().ToString());

		Assert.That(_response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
	}

	[Test]
	public async Task GetZooByGuid_WhenZooExists_ReturnZoo()
	{
		A.CallTo(() => _zooRepository.Get(A<Guid>._)).Returns(GetZoo());

		var _response = zooController.Get(Guid.NewGuid().ToString());

		Assert.That(_response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
	}

	private Zoo GetZoo() => ZooFactory.SmallZoo("testZoo");
}
