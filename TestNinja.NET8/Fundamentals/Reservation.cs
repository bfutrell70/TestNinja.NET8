namespace TestNinja.NET8.Fundamentals
{
	public class Reservation
	{
		public User MadeBy { get; set; } = default!;

		public bool CanBeCancelledBy(User user)
		{
			return (user.IsAdmin || MadeBy == user);
		}
	}

	public class User
	{
		public bool IsAdmin { get; set; }
	}
}