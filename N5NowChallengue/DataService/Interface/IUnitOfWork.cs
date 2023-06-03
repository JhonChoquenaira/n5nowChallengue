using System;

namespace N5NowChallengue.DataService.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IEmployeePermissionRepository EmployeePermissions { get;  }
        IPermissionRepository Permissions { get;  }
        int Complete();
    }
}
