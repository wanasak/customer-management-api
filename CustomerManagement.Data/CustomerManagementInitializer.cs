using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Model;

namespace CustomerManagement.Data
{
	public class CustomerManagementInitializer
	{
		private static CustomerManagementContext _context;
		public static void Initialize(IServiceProvider serviceProvider)
		{
			_context = (CustomerManagementContext)serviceProvider.GetService(typeof(CustomerManagementContext));

			InitializeData();
		}
		private static void InitializeData()
		{
			InitStates();
			InitializeUser();
			InitializeCustomers();
			InitializeOrders();
		}
		private static void InitializeUser()
		{
			User user = new User
			{
				Email = "admin@admin.com",
				Password = "admin123"
			};
			_context.Users.Add(user);
			_context.SaveChanges();
		}
		private static void InitializeCustomers()
		{
			List<Customer> customers = new List<Customer>
			{
				new Customer
				{
					FirstName = "Alex",
					LastName = "Sailorm",
					Gender = "M",
					Address = "345 Cedar Point Ave.",
					City = "Encinitas",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Ron",
					LastName = "Samir",
					Gender = "M",
					Address = "576 Crescent Blvd.",
					City = "Dallas",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Page",
					LastName = "Collin",
					Gender = "M",
					Address = "9874 Center St.",
					City = "Orlando",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Lynton",
					LastName = "Emmerson",
					Gender = "M",
					Address = "4651 Tuvo St.",
					City = "Anaheim",
					StateId = 1
				},
				new Customer
				{
					FirstName = "David",
					LastName = "Bridger",
					Gender = "M",
					Address = "9874 Lake Blvd.",
					City = "Zurich",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Tony",
					LastName = "Jonathan",
					Gender = "M",
					Address = "2543 Cassiano",
					City = "Rio de Janeiro",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Somchai",
					LastName = "Willis",
					Gender = "F",
					Address = "98756 Center St.",
					City = "Barcelona",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Fitz",
					LastName = "Murray",
					Gender = "M",
					Address = "12 Ocean View St.",
					City = "New York",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Fletcher",
					LastName = "Alva",
					Gender = "F",
					Address = "23423 Adams St.",
					City = "Los Angeles",
					StateId = 1
				},
				new Customer
				{
					FirstName = "Zach",
					LastName = "Alvin",
					Gender = "M",
					Address = "Los Angeles",
					City = "Atlanta",
					StateId = 1
				},
				new Customer
				{
					FirstName = "John",
					LastName = "Papa",
					Gender = "F",
					Address = "66 Ray St",
					City = "Orlando",
					StateId = 1
				}
			};
			_context.Customers.AddRange(customers);
			_context.SaveChanges();
		}
		private static void InitializeOrders()
		{
			List<Order> orders = new List<Order>
			{
				new Order
				{
					ProductName = "Basketball",
					ItemCost = 25,
					CustomerId = 1
				},
				new Order
				{
					ProductName = "Shoes",
					ItemCost = 5.25M,
					CustomerId = 1
				},
				new Order
				{
					ProductName = "Frisbee",
					ItemCost = 178.50M,
					CustomerId = 2
				},
				new Order
				{
					ProductName = "Hat",
					ItemCost = 2.99M,
					CustomerId = 3
				},
				new Order
				{
					ProductName = "Boomerang",
					ItemCost = 29.98M,
					CustomerId = 3
				}
			};
			_context.Orders.AddRange(orders);
			_context.SaveChanges();
		}
		private static void InitStates()
		{
			List<State> states = new List<State>
			{
				new State
				{
					Name = "Alabama",
					Abbreviation = "AL"
				},
				new State
				{
					Name = "California",
					Abbreviation = "CA"
				},
				new State
				{
					Name = "Illinois",
					Abbreviation = "IL"
				},
				new State
				{
					Name = "Mississippi",
					Abbreviation = "MS"
				},
				new State
				{
					Name = "New Mexico",
					Abbreviation = "NM"
				}

			};
			_context.States.AddRange(states);
			_context.SaveChanges();
		}
	}
}
