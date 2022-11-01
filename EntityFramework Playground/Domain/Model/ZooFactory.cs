namespace EntityFramework_Playground.Model;

public static class ZooFactory 
{ 
	public static Zoo SmallZoo(string name, int minAnimalsToOpen = 5)
	{
		var cages = new List<Cage>
		{
			Cage.SmallAnimalCage(8),
			Cage.SmallAnimalCage(12),
			Cage.SmallAnimalCage(14),
			Cage.AverageAnimalCage(5),
			Cage.AverageAnimalCage(6),
			Cage.BigAnimalCage(1),
		};

		return new Zoo(cages, name, minAnimalsToOpen);
	}
}

