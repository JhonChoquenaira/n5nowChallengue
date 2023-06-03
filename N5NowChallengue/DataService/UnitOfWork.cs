using System;
using N5NowChallengue.DataService.Context;
using N5NowChallengue.DataService.Interface;

namespace N5NowChallengue.DataService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployeeRepository Employees { get; }
        public IEmployeePermissionRepository EmployeePermissions { get; }
        public IPermissionRepository Permissions { get; }

        public UnitOfWork(ApplicationDbContext challengueDbContext,
            IEmployeeRepository employeeRepository, IEmployeePermissionRepository employeePermissionRepository,
            IPermissionRepository permissionRepository)
        {
            this._context = challengueDbContext;
            this.Employees = employeeRepository;
            this.EmployeePermissions = employeePermissionRepository;
            this.Permissions = permissionRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}