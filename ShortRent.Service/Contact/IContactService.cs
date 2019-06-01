using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IContactService
    {
        void CreateContact(Contact model);
        List<Contact> GetContacts(int pageSize, int pageNumber, DateTime? startTime, DateTime? endTime, out int total);
    }
}
