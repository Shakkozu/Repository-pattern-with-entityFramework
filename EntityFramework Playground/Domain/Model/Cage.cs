using EntityFramework_Playground.Infrastructure;

namespace EntityFramework_Playground.Model;

public class Cage
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public int Slots { get; private set; }
	public int OccupiedSlots { get; private set; }
	public AnimalSize Size { get; private set; }
	public bool HasFreeSlot => Slots - OccupiedSlots > 0;
	public bool HasResident => OccupiedSlots > 0;

	public IList<Animal> Residents { get; private set; } = new List<Animal>();

	public static Cage SmallAnimalCage(int noSlots) => new Cage(noSlots, AnimalSize.Small , new List<Animal>());
	public static Cage AverageAnimalCage(int noSlots) => new Cage(noSlots, AnimalSize.Average, new List<Animal>());
	public static Cage BigAnimalCage(int noSlots) => new Cage(noSlots, AnimalSize.Big, new List<Animal>());

	private Cage(int noSlots, AnimalSize size, IList<Animal> residents, int occupiedSlots = 0)
	{
		Slots = noSlots;
		Size = size;
		OccupiedSlots = occupiedSlots;
		Residents = residents;
	}

	public static Cage FromSnapshot(CageSnapshot snapshot)
	{
		var residents = snapshot.Residents.Select(resident => Animal.FromSnapshot(resident))
			.ToList();

		return new Cage(snapshot.Slots,
			Enum.Parse<AnimalSize>(snapshot.AnimalSize),
			residents,
			snapshot.OccupiedSlots);
	}
	public CageSnapshot ToSnapshot()
	{
		return new CageSnapshot
		{
			Residents = Residents.Select(r => r.ToSnapshot()).ToList(),
			Id = Id.ToString(),
			Slots = Slots,
			OccupiedSlots = OccupiedSlots,
			AnimalSize = Size.ToString(),
		};
	}

	public void AddAnimal(Animal animal)
	{
		if (Residents.FirstOrDefault(resident => resident.Id == animal.Id) != null)
			throw new ArgumentException($"Animal {animal.Id} is already in this cage");

		if(HasFreeSlot && animal.Type.Size == Size)
		{
			Residents.Add(animal);
			OccupiedSlots++;
		}
	}
	public void RemoveAnimal(Animal animal)
	{
		if(HasResident && Residents.Contains(animal))
			OccupiedSlots--;
	}
}

