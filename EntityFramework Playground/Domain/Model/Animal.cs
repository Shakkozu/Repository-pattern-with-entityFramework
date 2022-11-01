using EntityFramework_Playground.Infrastructure;

namespace EntityFramework_Playground.Model;

public class Animal
{
	public Animal(string name, AnimalType type, DateTime birthDateTime)
	{
		Name = name;
		Type = type;
		BirthDateTime = birthDateTime;
	}

	public Guid Id { get; private set; } = Guid.NewGuid();
	public DateTime BirthDateTime { get; private set; }

	public string Name { get; private set; }
	public AnimalType Type { get; private set; }

	public static Animal FromSnapshot(AnimalSnapshot animal)
	{
		return new Animal(animal.Name, AnimalType.FromSnapshot(animal.Specie, animal.Size), animal.BirthDateTime);
	}

	public AnimalSnapshot ToSnapshot()
	{
		return new AnimalSnapshot
		{
			Id = Id.ToString(),
			Name = Name,
			BirthDateTime = BirthDateTime,
			Specie = Type.Specie.ToString(),
			Size = Type.Size.ToString(),
		};
	}
}

