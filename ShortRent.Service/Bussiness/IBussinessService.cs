using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IBussinessService
    {
        void CreateBussiness(Business model);
        List<Business> GetPagedBussiness(int pageSize, int pageNumber, out int total);
        Business GetBussiness(int id);
        void UpdateBussiness(Business model);
        List<Business> GetBussinesss();
    }
}
