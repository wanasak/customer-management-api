using System;
namespace CustomerManagement.Model
{
	public class Order : IEntityBase
	{
		public Order()
		{
		}
		public int Id { get; set; }
		public string ProductName { get; set; }
		public decimal ItemCost { get; set; }
		public int CustomerId { get; set; }
	}
}
