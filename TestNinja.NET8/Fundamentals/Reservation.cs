namespace TestNinja.NET8.Fundamentals
{
	public class Reservation
	{
		public User MadeBy { get; set; } = default!;

		public bool CanBeCancelledBy(User user)
		{
			return (user.IsAdmin || MadeBy == user);
			//if (user.IsAdmin)
			//{
			//	return true;
			//}
			//
			//if (MadeBy == user)
			//{
			//  return true;
			//}
			//
			//return false;
		}
	}

	public class User
	{
		public bool IsAdmin { get; set; }
	}
}