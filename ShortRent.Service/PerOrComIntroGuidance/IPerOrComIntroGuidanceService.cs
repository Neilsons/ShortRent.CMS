using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IPerOrComIntroGuidanceService
    {
        List<PerOrComIntroGuidance> GetPerOrComIntroGuidances();
        List<PerOrComIntroGuidance> GetPagedPerOrComIntor(int PageSize, int PageNumber, bool? Type, out int total);
        void Insert(PerOrComIntroGuidance model);
        void Update(PerOrComIntroGuidance model);
        PerOrComIntroGuidance GetIntro(int id);
    }
}
