namespace EntityFramework_Playground.Model;

public class AnimalType
{
	public AnimalSpecie Specie { get; private set; }

	public AnimalSize Size { get; private set; }

	private AnimalType(AnimalSpecie specie, AnimalSize size)
	{
		Size = size;
		Specie = specie;
	}

	public static AnimalType FromSnapshot(string specie, string size)
	{
		if (string.IsNullOrEmpty(specie) || string.IsNullOrEmpty(size))
			throw new ArgumentException();

		return new AnimalType(Enum.Parse<AnimalSpecie>(specie), Enum.Parse<AnimalSize>(size));
	}

	public static AnimalType Tiger() => new AnimalType(AnimalSpecie.Tiger, AnimalSize.Average);
	public static AnimalType Monkey() => new AnimalType(AnimalSpecie.Monkey, AnimalSize.Small);
	public static AnimalType Gorilla() => new AnimalType(AnimalSpecie.Gorilla, AnimalSize.Big);
}

