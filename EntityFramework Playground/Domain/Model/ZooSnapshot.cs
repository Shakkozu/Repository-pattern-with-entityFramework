using EntityFramework_Playground.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Playground.Infrastructure;

[Table("zoo")]
public class ZooSnapshot
{
	[Key]
	[Required]
	public string Id { get; set; }
	public IList<CageSnapshot> Cages{ get; set; } = new List<CageSnapshot>();
	[Required]
	public string Name { get; set; }
}
