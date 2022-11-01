using EntityFramework_Playground.Model;

namespace EntityFramework_Playground.Domain.Repositories;

public interface IZooRepository
{
	public void Update(Zoo zoo);
	public void Save(Zoo zoo);

	public Zoo Get(Guid id);
}
