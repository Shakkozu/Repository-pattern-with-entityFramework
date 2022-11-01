using EntityFramework_Playground.Domain.Repositories;
using EntityFramework_Playground.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Playground.Infrastructure;

public class EntityFrameworkZooRepository : IZooRepository
{
	private readonly ZooContext context;

	public EntityFrameworkZooRepository(ZooContext context)
	{
		this.context = context;
	}

	public Zoo Get(Guid id)
	{
		var snapShot = context.Zoos.FirstOrDefault(zoo => zoo.Id == id.ToString());
		if (snapShot == null)
			return null;

		return Zoo.FromSnapshot(snapShot);
	}

	public void Save(Zoo zoo)
	{
		context.Zoos.Add(zoo.ToSnapshot());
		context.SaveChanges();
	}

	public void Update(Zoo zoo)
	{
		throw new NotImplementedException();
	}
}

public class ZooContext : DbContext
{
	public ZooContext(DbContextOptions options) : base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<ZooSnapshot> Zoos { get; set; }
}
