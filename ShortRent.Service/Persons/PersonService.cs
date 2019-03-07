using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Cache;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;

namespace ShortRent.Service
{
    public class PersonService : IPersonService
    {
        #region Field
        private readonly IRepository<Person> _personRepository;
        //缓存
        private readonly ICacheManager _cacheManager;
        //缓存中的key值
        private const string PersonsCacheKey = nameof(PersonService) + nameof(Person);
        #endregion

        #region Contruction
        public PersonService(IRepository<Person> personRepository,
            ICacheManager cacheManager)
        {
            _personRepository = personRepository;
            _cacheManager = cacheManager;
        }
        #endregion

        #region Method
        public void CreatePerson(Person person)
        {
            _personRepository.Insert(person);
        }

        public void DeletePerson(Person person)
        {
            _personRepository.Delete(person);
        }

        public List<Person> GetPersons()
        {
            List<Person> persons = null;
            if(_cacheManager.Contains(PersonsCacheKey))
            {
                persons = _cacheManager.Get<List<Person>>(PersonsCacheKey);
            }
            else
            {
                var list = _personRepository.Persons;
                if(list!=null)
                {
                    persons = list.ToList();
                    //缓存半个小时
                    _cacheManager.Set(PersonsCacheKey, persons, TimeSpan.FromMinutes(1));
                }
                else
                {
                    persons = new List<Person>();
                }
            }
            return persons;
        }

        public void UpdatePerson(Person person)
        {
            _personRepository.Update(person);
        }
        #endregion

    }
}
