using EntityFramework_Playground.Infrastructure;
using EntityFramework_Playground.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration;

public class ZooRepositoryTests
{
	private SqliteConnection _connection;
	private DbContextOptions<ZooContext> _contextOptions;
	private EntityFrameworkZooRepository zooRepository;
	private ZooContext context;

	[SetUp]
	public void SetUp()
	{
		context = new ZooContext(_contextOptions);
		context.Database.EnsureCreated();

		zooRepository = new EntityFrameworkZooRepository(context);
	}

	[OneTimeSetUp]
	public void SetupFixture()
	{
		_connection = new SqliteConnection("DataSource=:memory:");
		_connection.Open();

		_contextOptions = new DbContextOptionsBuilder<ZooContext>()
			.UseSqlite(_connection)
			.Options;
	}

	[TearDown]
	public void TearDown()
	{
		context.Dispose();
	}

	[Test]
	public void ShouldSaveAndReturnZoo()
	{
		var zoo = GetZoo();
		zooRepository.Save(zoo);

		var zooFromDb = zooRepository.Get(zoo.Id);

		Assert.IsNotNull(zooFromDb);
	}

	[Test]
	public void GetZoo_WhenZooNotCreated_ShouldReturnNull ()
	{
		var zoo = GetZoo();
		var zooFromDb = zooRepository.Get(zoo.Id);

		Assert.Null(zooFromDb);
	}

	[Test]
	public void GetZoo_WhenZooHasAnimals_ReturnZooWithAnimals()
	{
		var zoo = GetZoo();
		zoo.AddAnimal(new Animal("bobby", AnimalType.Tiger(), DateTime.Now.AddYears(-2)));
		zooRepository.Save(zoo);

		var zooFromDb = zooRepository.Get(zoo.Id);

		Assert.NotNull(zooFromDb);
		Assert.NotNull(zooFromDb.Animals.First(animal => animal.Name == "bobby"));
	}

	private Zoo GetZoo() => ZooFactory.SmallZoo("testZoo");
}

public abstract class IntegrationControllersTestFixture
{
	protected SqliteConnection _connection;
	protected DbContextOptions<ZooContext> _contextOptions;
	protected ZooContext CreateContext() => new ZooContext(_contextOptions);


	protected void RunIntegrationTests(Action<ZooContext> action)
	{
		_connection = new SqliteConnection("DataSource=:memory:");
		_connection.Open();

		_contextOptions = new DbContextOptionsBuilder<ZooContext>()
			.UseSqlite(_connection)
			.Options;

		using var context = new ZooContext(_contextOptions);
		{
			context.Database.EnsureCreated();
			try
			{
				action(context);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}