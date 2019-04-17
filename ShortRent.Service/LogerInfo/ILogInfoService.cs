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
        List<LogInfo> GetLogInfos();
        List<LogInfo> GetLogPagedListInfo(int pagedIndex, int pagedSize,string machineName, string catalog, DateTime? startTime, DateTime? endTime, out int total);
        LogInfo GetDetail(int id);
        LogInfo DeleteById(int id);


    }
}
