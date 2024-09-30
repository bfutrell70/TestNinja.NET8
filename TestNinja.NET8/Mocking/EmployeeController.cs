using Microsoft.EntityFrameworkCore;

namespace TestNinja.NET8.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeRepostory _repository;
        
        public EmployeeController(IEmployeeRepostory repository)
        {
            _repository = repository;
        }

        // two tests:
        //      verify the returned value is correct
        //      verify that the correct method is called when deleting an employee
        public ActionResult DeleteEmployee(int id)
        {
            _repository.DeleteEmployee(id);

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