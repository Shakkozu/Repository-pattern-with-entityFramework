using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Playground.Infrastructure;

[Table("animal")]
public class AnimalSnapshot
{
	[Required]
	[Key]
	public string Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public DateTime BirthDateTime { get; set; }

	[Required]
	public string Specie { get; set; }

	[Required]
	public string Size { get; set; }
}