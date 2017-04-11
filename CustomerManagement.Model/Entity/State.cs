using System;
namespace CustomerManagement.Model
{
	public class State : IEntityBase
	{
		public State()
		{
		}
		public int Id { get; set; }
		public string Abbreviation { get; set; }
		public string Name { get; set; }
	}
}
