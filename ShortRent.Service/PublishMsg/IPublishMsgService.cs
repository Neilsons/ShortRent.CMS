using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IPublishMsgService
    {
        void CreatePublishMsg(PublishMsg model);
        PublishMsg GetPublishMsgById(int id);
        int PersonIdByPublishId(int id);
        List<RecruiterUserTypePersonModel> GetPageRecruiter(int pagedIndex, int pagedSize, string CompanyName, string Name, int? Bussiness, DateTime? startTime, DateTime? endTime, out int total);
        List<RecruiterByUserTypePersonModel> GetPageRecruiterBy(int pagedIndex, int pagedSize, string Name, int? Bussiness, DateTime? startTime, DateTime? endTime, out int total);
    }
}
