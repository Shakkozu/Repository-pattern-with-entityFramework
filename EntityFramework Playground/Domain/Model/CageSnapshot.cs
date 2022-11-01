using EntityFramework_Playground.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Playground.Infrastructure;

[Table("cage")]
public class CageSnapshot
{
	[Required]
	[Key]
	public string Id { get; set; }

	[Required]
	public int Slots { get; set; }
	[Required]
	public int OccupiedSlots { get; set; }
	[Required]
	public string AnimalSize { get; set; }

	public IList<AnimalSnapshot> Residents { get; set; } = new List<AnimalSnapshot>();

}
