using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.NET8.Mocking
{
	public interface IEmployeeRepostory
	{
		void DeleteEmployee(int id);
	}

	// Mosh called this class EmployeeStorage
	public class EmployeeRepostory : IEmployeeRepostory
	{
		private readonly EmployeeContext _context;

		public EmployeeRepostory(EmployeeContext context)
		{
			_context = context;
		}

		public void DeleteEmployee(int id)
		{
			// no unit test for this method - would need to be part of an integration test
			var employee = _context.Employees.Find(id);
			if (employee == null)
				return;
			
			_context.Employees.Remove(employee);
			_context.SaveChanges();
		}
	}
}
