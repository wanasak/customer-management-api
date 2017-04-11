using System;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public class UserRepositories : EntityBaseRepository<User>, IUserRepositories
	{
		public UserRepositories(CustomerManagementContext context) : base(context)
		{
		}
	}
}
