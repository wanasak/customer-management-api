using System;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public interface IUserRepositories : IEntityBaseRepository<User> { }
	public interface ICustomerRepositories : IEntityBaseRepository<Customer> { }
	public interface IOrderRepositories : IEntityBaseRepository<Order> { }
	public interface IStateRepositories : IEntityBaseRepository<State> { }
}
