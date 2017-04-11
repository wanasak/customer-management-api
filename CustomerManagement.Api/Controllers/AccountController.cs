using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Data;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CustomerManagement.Api
{
	[Route("api/auth")]
	public class AccountController : Controller
	{
		readonly IUserRepositories _userRepository;

		public AccountController(IUserRepositories userRepository)
		{
			_userRepository = userRepository;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<bool> Login([FromBody]User user)
		{
			var authUser = _userRepository
				.GetSingle(x => x.Email == user.Email && x.Password == user.Password);

			if (authUser == null) return false;

			List<Claim> _claims = new List<Claim>();
			Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Email);
			_claims.Add(_claim);

			await HttpContext.Authentication.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(new ClaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme)));
			return true;
		}

		[AllowAnonymous]
		[HttpPost("logout")]
		public async Task<bool> Logout()
		{
			await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return true;
		}

		[HttpPost]
		public void CreateCustomer([FromBody]User customer)
		{
			_userRepository.Add(customer);
			_userRepository.Commit();
		}
	}
}
