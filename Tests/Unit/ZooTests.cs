using EntityFramework_Playground.Model;

namespace Tests.Unit;
public class ZooTests
{
	[Test]
	public void Open_WhenMinimumAmountOfAnimalsIsNotMeet_DoNotOpen()
	{
		var zoo = ZooFactory.SmallZoo("testZoo");

		zoo.Open();

		Assert.That(zoo.IsOpen(), Is.EqualTo(false));
	}
	
	[Test]
	public void Open_WhenMinimumAmountOfAnimalsIsMeet_OpenZoo()
	{
		var zoo = ZooFactory.SmallZoo("testZoo", 3);
		zoo.AddAnimal(new Animal("bobby", AnimalType.Tiger(), DateTime.Now.AddYears(-2)));
		zoo.AddAnimal(new Animal("harambe", AnimalType.Gorilla(), DateTime.Now.AddYears(-5)));
		zoo.AddAnimal(new Animal("tabasco", AnimalType.Monkey(), DateTime.Now.AddYears(-1)));

		zoo.Open();

		Assert.That(zoo.IsOpen(), Is.EqualTo(true));
	}
}
