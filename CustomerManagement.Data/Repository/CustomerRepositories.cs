using System;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public class CustomerRepositories : EntityBaseRepository<Customer>, ICustomerRepositories
	{
		public CustomerRepositories(CustomerManagementContext context) : base(context)
		{
		}
	}
}
