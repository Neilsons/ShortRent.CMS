using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface ICompanyService
    {
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        List<Company>  GetPagedCompanys(int pageSize,int  pageNumber,string Name, out int total);
        Company GetCompanyById(int id);
    }
}
