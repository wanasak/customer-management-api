using System;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data
{
	public class CustomerManagementContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		public CustomerManagementContext(DbContextOptions options) : base(options)
		{
		}
	}
}
