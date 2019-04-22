using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IPersonService
    {
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        List<Person> GetTypePerson(int pageSize, int pageNumber, string AdminName, int? Type, out int total);
        List<Person> GetPersons(bool? IsActivator=null);
        Person GetPerson(int id, bool? IsActivator=null);

    }
}
