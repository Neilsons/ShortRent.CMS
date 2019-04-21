using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IIconsInfoService
    {
        List<IconsInfo> GetIconsInfosByTotal(int pageSize, int pageNumber, string contentName,out int total);
        List<IconsInfo> GetIconsInfos();
        void CreateIcon(IconsInfo model);
        IconsInfo GetIconsById(int id);
        void UpdateIcon(IconsInfo model);
        void Delete(IconsInfo model);
    }
}
