using System;
using CustomerManagement.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using CustomerManagement.Api;
using CustomerManagement.Model;
using System.Linq;
using System.Collections.Generic;

namespace CustomerManagement.Test
{
	public class CustomerControllerTest
	{
		private readonly DbContextOptions<CustomerManagementContext> _contextOptions;
		private ICustomerRepositories _customerRepository;
		private IStateRepositories _stateRepository;

		public CustomerControllerTest() 
		{
			// Create a service provider to be shared by all test methods
			var serviceProvider = new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider();

			// Create options telling the context to use an 
			// inmemory database and the service provider
			var builder = new DbContextOptionsBuilder<CustomerManagementContext>();
			builder.UseInMemoryDatabase()
				   .UseInternalServiceProvider(serviceProvider);
			_contextOptions = builder.Options;

			// Insert the seed data
			using (var context = new CustomerManagementContext(_contextOptions))
			{
				
				CustomerManagementInitializer.Initialize(context);
			}
		}
		[Fact]
		public void Test_Get_Customer()
		{
			using (var context = new CustomerManagementContext(_contextOptions))
			{
				_customerRepository = new CustomerRepositories(context);
				_stateRepository = new StateRepositories(context);
				var controller = new CustomersController(_customerRepository, _stateRepository);
				Customer customer = controller.GetCustomer(1);

				Assert.Equal("Alex", customer.FirstName);
				Assert.Equal(2, customer.Orders.Count);
			}
		}
		//[Fact]
		//public void Test_Get_Customers_Page()
		//{
		//	using (var context = new CustomerManagementContext(_contextOptions))
		//	{
		//		_customerRepository = new CustomerRepositories(context);
		//		_stateRepository = new StateRepositories(context);
		//		var controller = new CustomersController(_customerRepository, _stateRepository);
		//		IEnumerable<Customer> customers = controller.GetCustomersPage(2, 10);

		//		Assert.Equal(1, customers.Count());
		//	}
		//}
		[Fact]
		public void Test_Create_Customer()
		{
			using (var context = new CustomerManagementContext(_contextOptions))
			{
				_customerRepository = new CustomerRepositories(context);
				_stateRepository = new StateRepositories(context);
				var controller = new CustomersController(_customerRepository, _stateRepository);

				Customer newCustomer = new Customer()
				{
					FirstName = "Wanasak",
					LastName = "Suraintaranggoon",
					Gender = "M",
					Address = "199/46 Muang",
					City = "Chiang Mai",
					StateId = 4
				};
				controller.CreateCustomer(newCustomer);

				Assert.Equal("Wanasak", context.Customers.Last().FirstName);
				Assert.Equal(12, context.Customers.Count());
			}
		}
		[Fact]
		public void Test_Update_Customer()
		{
			using (var context = new CustomerManagementContext(_contextOptions))
			{
				_customerRepository = new CustomerRepositories(context);
				_stateRepository = new StateRepositories(context);
				var controller = new CustomersController(_customerRepository, _stateRepository);

				Customer updateCustomer = new Customer()
				{
					FirstName = "Alex",
					LastName = "Morgan",
					Gender = "M",
					Address = "345 Cedar Point Ave.",
					City = "Encinitas",
					StateId = 5
				};
				controller.UpdateCustomer(1, updateCustomer);

				Assert.Equal("Morgan", context.Customers.First().LastName);
				Assert.Equal(5, context.Customers.First().StateId);
			}
		}
		[Fact]
		public void Test_Delete_Customer()
		{
			using (var context = new CustomerManagementContext(_contextOptions))
			{
				_customerRepository = new CustomerRepositories(context);
				_stateRepository = new StateRepositories(context);
				var controller = new CustomersController(_customerRepository, _stateRepository);
				controller.DeleteCustomer(1);

				Assert.Equal(10, context.Customers.Count());
			}
		}
	}
}
