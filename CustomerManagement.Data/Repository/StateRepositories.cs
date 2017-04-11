using System;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public class StateRepositories : EntityBaseRepository<State>, IStateRepositories
	{
		public StateRepositories(CustomerManagementContext context) : base(context)
		{
		}
	}
}
