using EntityFramework_Playground.Infrastructure;
using System.Collections.ObjectModel;

namespace EntityFramework_Playground.Model;

public class Zoo
{
	public IReadOnlyCollection<Cage> Cages { get; private set; } = new List<Cage>();
	public IReadOnlyCollection<Animal> Animals
	{
		get
		{
			var result = new List<Animal>();
			foreach (var cage in Cages)
				result.AddRange(cage.Residents);

			return new ReadOnlyCollection<Animal>(result);
		}
	}

	public string Name { get; private set; }
	public Guid Id { get; private set; } = Guid.NewGuid();
	public int MinAmountOfAnimalsToOpen { get; private set; }
	public ZooStatus Status { get; private set; }

	public static Zoo FromSnapshot(ZooSnapshot snapshot)
	{
		
		var cages = snapshot.Cages.Select(cage => Cage.FromSnapshot(cage)).ToList();

		return new Zoo(cages, snapshot.Name, Guid.Parse(snapshot.Id));
	}

	public ZooSnapshot ToSnapshot()
	{
		return new ZooSnapshot
		{
			Cages = Cages.Select(c => c.ToSnapshot()).ToList(),
			Id = Id.ToString(),
			Name = Name,
		};
	}

	public void AddAnimals(IList<Animal> animals)
	{
		foreach (var animal in animals)
			AddAnimal(animal);
	}
	public void AddAnimal(Animal animal)
	{
		var animalSize = animal.Type.Size;
		var cageWithFreeSlot = Cages.FirstOrDefault(cage => cage.Size == animalSize && cage.HasFreeSlot);
		if (cageWithFreeSlot == null)
			throw new InvalidOperationException($"Cannot add animal with size {animalSize} to this zoo because there are none free cages");

		cageWithFreeSlot.AddAnimal(animal);
	}

	public bool IsOpen()
	{
		return Status == ZooStatus.Open;
	}

	public void Open()
	{
		if(Animals.Count >= MinAmountOfAnimalsToOpen)
			Status = ZooStatus.Open;
	}

	public Zoo(IList<Cage> cages, string name, int minAmountOfAnimalsToOpen)
	{
		Cages = new ReadOnlyCollection<Cage>(cages);
		Name = name;
		MinAmountOfAnimalsToOpen = minAmountOfAnimalsToOpen;
		Status = ZooStatus.Closed;
	}

	private Zoo(IList<Cage> cages, string name, Guid id)
	{
		Cages = new ReadOnlyCollection<Cage>(cages);
		Name = name;
		Id = id;
	}
}