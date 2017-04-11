using System;
using System.Collections.Generic;

namespace CustomerManagement.Model
{
	public class Customer : IEntityBase
	{
		public Customer()
		{
			Orders = new List<Order>();
		}
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public int StateId { get; set; }

		public State State { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
