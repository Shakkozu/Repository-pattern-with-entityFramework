using EntityFramework_Playground.Model;

namespace EntityFramework_Playground.Controllers;

public class ZooDto
{
	private ZooDto(Guid id, int count)
	{
		Id = id;
		Count = count;
	}
	public Guid Id { get; }
	public int Count { get; }

	public static ZooDto FromZoo(Zoo zoo)
	{
		return new ZooDto(zoo.Id, zoo.Animals.Count);
	}
}