using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IUserTypeService
    {
        void CreateUserType(UserType userType);
        void UpdateUserType(UserType userType);
        UserTypeAudit GetUserAudit(int id);
        UserType GetUserTypeById(int id);
        List<RecruiterByViewModel> GetRecruiterByViewModelList(int pageSize, int pageNumber, string Name, out int total);
        List<RecruiterViewModel> GetRecruiterViewModelList(int pageSize, int pageNumber, string Name, out int total);
    }
}
