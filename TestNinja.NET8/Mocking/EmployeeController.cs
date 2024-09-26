using Microsoft.EntityFrameworkCore;

namespace TestNinja.NET8.Mocking
{
    public class EmployeeController
    {
        private readonly EmployeeContext _db;

        public EmployeeController()
        {
            _db = new EmployeeContext();
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            // added null check to remove warning in .NET 8.
            if (employee != null)
            {
				_db.Employees.Remove(employee);
			}
            _db.SaveChanges();
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        // set default value to default! to resolve compiler warning
        public DbSet<Employee> Employees { get; set; } = default!;

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}