using System;
namespace CustomerManagement.Model
{
	public class User : IEntityBase
	{
		public User()
		{
		}
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
