using N5NowChallengue.DataService.Context;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.DataService.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}