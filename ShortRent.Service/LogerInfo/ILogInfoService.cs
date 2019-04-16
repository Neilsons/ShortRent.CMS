using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface ILogInfoService
    {
        void Delete(LogInfo info);
        List<LogInfo> GetLogInfos();
        List<LogInfo> GetLogPagedListInfo(int pagedIndex, int pagedSize, out int total);
        LogInfo GetDetail(int id);


    }
}
