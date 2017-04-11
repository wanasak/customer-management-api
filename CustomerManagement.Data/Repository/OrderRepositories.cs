using System;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public class OrderRepositories : EntityBaseRepository<Order>, IOrderRepositories
	{
		public OrderRepositories(CustomerManagementContext context) : base(context)
		{
		}
	}
}
