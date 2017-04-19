using System;
using System.Collections.Generic;
using CustomerManagement.Data;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CustomerManagement.Api
{
	[Route("api/[controller]")]
	public class CustomersController : Controller
	{
		readonly ICustomerRepositories _customerRepository;
		readonly IStateRepositories _stateRepository;

		public CustomersController(
			ICustomerRepositories customerRepository,
			 IStateRepositories stateRepository)
		{
			_customerRepository = customerRepository;
			_stateRepository = stateRepository;
		}

		[HttpGet]
		public IEnumerable<Customer> Get()
		{
			return _customerRepository
				.GetAll()
				.OrderBy(c => c.FirstName)
				.ToList();
		}

		[HttpGet("{id}")]
		public Customer GetCustomer(int id)
		{
			return _customerRepository
				.AllIncluding(c => c.Orders, c => c.State)
				.FirstOrDefault(c => c.Id == id);
		}

		[HttpGet("page/{page}/{pageSize}")]
		public IEnumerable<Customer> GetCustomersPage(int page = 1, int pageSize = 10)
		{
			IEnumerable<Customer> customers = _customerRepository
				.AllIncluding(c => c.Orders, c => c.State)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			int totalCustomers = _customerRepository.Count();

			Response.AddPagination(totalCustomers);
			return customers;
		}

		[HttpPost]
		public void CreateCustomer([FromBody]Customer customer)
		{
			Customer newCustomer = new Customer()
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Gender = customer.Gender,
				Address = customer.Address,
				City = customer.City,
				StateId = customer.StateId
			};
			_customerRepository.Add(newCustomer);
			_customerRepository.Commit();
		}

		[HttpPut("{id}")]
		public void UpdateCustomer(int id, [FromBody]Customer customer)
		{
			Customer upateCustomer = _customerRepository.GetSingle(id);
			if (customer != null)
			{
				upateCustomer.FirstName = customer.FirstName;
				upateCustomer.LastName = customer.LastName;
				upateCustomer.Gender = customer.Gender;
				upateCustomer.Address = customer.Address;
				upateCustomer.City = customer.City;
				upateCustomer.StateId = customer.StateId;
				_customerRepository.Update(upateCustomer);
				_customerRepository.Commit();
			}
		}

		[HttpDelete("{id}")]
		public void DeleteCustomer(int id)
		{
			Customer deleteCustomer = new Customer { Id = id };
			_customerRepository.Delete(deleteCustomer);
			_customerRepository.Commit();
		}

		[HttpGet("states")]
		public IEnumerable<State> GetSates()
		{
			return _stateRepository
				.GetAll()
				.OrderBy(s => s.Name)
				.ToList();
		}
	}
}
