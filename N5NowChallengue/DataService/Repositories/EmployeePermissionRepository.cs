using System.Threading.Tasks;
using N5NowChallengue.DataService.Context;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.DataService.Repositories
{
    public class EmployeePermissionRepository: GenericRepository<EmployeePermission>, IEmployeePermissionRepository
    {
        public EmployeePermissionRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<EmployeePermission> Get(int employeeId, int permissionId)
        {
            return await _context.EmployeePermissions.FindAsync(employeeId, permissionId);
        }
    }
}