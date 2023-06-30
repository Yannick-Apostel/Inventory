namespace Inventory.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int count { get; set; }
		public string Description { get; set; }

		public string OwnerUsername { get; set; }
	}
}
