using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface ICompanyPerTagsService
    {
        List<CompanyPerTag> GetPagedCompanyPerTags(int pageSize, int pageNumber, string tagName, out int total);
        void Create(CompanyPerTag model);
        CompanyPerTag GetCompanyPerTags(int id);
        void Update(CompanyPerTag model);
        void Delete(CompanyPerTag model);
    }
}
