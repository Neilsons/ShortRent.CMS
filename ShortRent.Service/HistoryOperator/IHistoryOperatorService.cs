using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
   public interface IHistoryOperatorService
    {
        void CreateHistoryOperator(HistoryOperator model);
        List<HistoryOperator> GetHistoryOperators();
        List<HistoryOperator> GetHistoryOperators(int pageSize, int pageNumber, string pName, string entityName, out int total);
        HistoryOperator GetHistoryOperator(int id);
        void DeleteHistory(HistoryOperator model);
    }
}
