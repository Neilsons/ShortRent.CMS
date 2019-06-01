using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Core;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace ShortRent.Service
{
    public class PersonService : BaseService,IPersonService
    {
        #region Field
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        //缓存中的key值
        private const string PersonsCacheKey = nameof(PersonService) + nameof(Person);
        private const string HistoryOperatorCacheKey = nameof(PersonService) + nameof(HistoryOperator);
        private const string PersonUserTypeCacheKey = nameof(PersonService) + nameof(UserTypeService);
        #endregion

        #region Contruction
        public PersonService(IRepository<Person> personRepository,
            IRepository<UserType> userTypeRrepository,
            ICacheManager cacheManager,ILogger logger,ApplicationConfig config)
        {
            _personRepository = personRepository;
            _userTypeRepository = userTypeRrepository;
            _cacheManager = cacheManager;
            _logger = logger;
            _config = config;
        }
        #endregion

        #region Method
        public void CreatePerson(Person person)
        {
            _personRepository.Insert(person);
            _cacheManager.Remove(PersonsCacheKey);
            _cacheManager.Remove(PersonUserTypeCacheKey);
        }

        public void DeletePerson(Person person)
        {
            _personRepository.Delete(person);
            _cacheManager.Remove(PersonsCacheKey);
            _cacheManager.Remove(PersonUserTypeCacheKey);
        }
        public List<Person> GetTypePerson(int pageSize, int pageNumber,string AdminName,int? Type, out int total)
        {
            Expression<Func<Person, bool>> expression = test => true;
            if(Type!=null)
            {
                expression=expression.And(c => c.Type);
            }
            if(!string.IsNullOrEmpty(AdminName))
            {
                expression = expression.And(c => c.Name.Contains(AdminName));
            }
            List<Person> persons = null;
            try
            {
                if (_cacheManager.Contains(PersonsCacheKey))
                {
                    IEnumerable<Person> list=null;
                    if (pageSize==0&&pageNumber==0)
                    {
                        list=_cacheManager.Get<List<Person>>(PersonsCacheKey).Where(expression.Compile());
                        persons =list.ToList();
                    }
                    else
                    {
                        list = _cacheManager.Get<List<Person>>(PersonsCacheKey).Where(expression.Compile());
                        persons = list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    }
                    total = list.Count();
                }
                else
                {
                    var list = _personRepository.Entitys.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        var models = list.Where(expression.Compile());
                        if (pageSize==0&&pageNumber==0)
                        {
                            persons =models.ToList();
                        }
                        else
                        {
                            persons = models.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                        }
                        total = models.Count();                         
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        //缓存半个小时
                        _cacheManager.Set(PersonsCacheKey, list, TimeSpan.FromMinutes((int)CacheTimeLev.lev1));
                    }
                    else
                    {
                        persons = new List<Person>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("获取特定的用户", e);
                throw e;
            }
            return persons;
        }
        /// <summary>
        /// 得到特定的人
        /// </summary>
        /// <param name="IsActivator">false未激活 true 激活 null 全部</param>
        /// <returns></returns>
        public List<Person> GetPersons(bool? IsActivator=null)
        {
            List<Person> persons = null;
            try
            {
                if (_cacheManager.Contains(PersonsCacheKey))
                {
                    if (IsActivator == null)
                        persons = _cacheManager.Get<List<Person>>(PersonsCacheKey);
                    else
                        persons = _cacheManager.Get<List<Person>>(PersonsCacheKey).Where(c => c.IsDelete == IsActivator).ToList();
                }
                else
                {
                    var list = _personRepository.Entitys.OrderByDescending(c=>c.CreateTime).ToList();
                    if (list.Any())
                    {
                        if (IsActivator == null)
                            persons = list;
                        else
                            persons = list.Where(c => c.IsDelete == IsActivator).ToList();
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        //缓存半个小时
                        _cacheManager.Set(PersonsCacheKey, list, TimeSpan.FromMinutes((int)CacheTimeLev.lev1));
                    }
                    else
                    {
                        persons = new List<Person>();
                    }
                }
            }
           catch(Exception e)
            {
                _logger.Debug("获取特定的用户",e);
                throw e;
            }
            return persons;
        }
        /// <summary>
        /// 得到某一个用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsActivator"></param>
        /// <returns></returns>
        public Person GetPerson(int id,bool? IsActivator=null)
        {
            Person person = null;
            try
            {
                if(_cacheManager.Contains(PersonsCacheKey))
                {
                    if (IsActivator == null)
                        person = _cacheManager.Get<List<Person>>(PersonsCacheKey).Where(c => c.ID == id).FirstOrDefault();
                    else
                        person = _cacheManager.Get<List<Person>>(PersonsCacheKey).Where(c => c.ID == id&&c.IsDelete==IsActivator).FirstOrDefault();
                }
                else
                {
                    if (IsActivator == null)
                        person = _personRepository.Entitys.Where(c => c.ID == id).FirstOrDefault();
                    else
                        person = _personRepository.Entitys.Where(c => c.ID == id && c.IsDelete == IsActivator).FirstOrDefault();
                    if(person==null)
                    {
                        person = new Person();
                    }                  
                }
            }
            catch(Exception e)
            {
                _logger.Debug("获得指定条件的用户",e);
                throw e;
            }
            return person;
        }
        /// <summary>
        /// 根据姓名得到相同的用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  Person GetPersonByName(string name)
        {
            Person person = null;
            try
            {
                if (_cacheManager.Contains(PersonsCacheKey))
                {
                    person = _cacheManager.Get<List<Person>>(PersonsCacheKey).Where(c => c.Name==name&&c.Type==false).FirstOrDefault();
                }
                else
                {
                    person = _personRepository.Entitys.Where(c => c.Name== name&&c.Type == false).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.Error("判断是否有相同的用户", e);
                throw e;
            }
            return person;
        }
        public List<HistoryOperator> GetPersonHistoryOperator(int personId)
        {
            List<HistoryOperator> list = null;
            try
            {
                if(_cacheManager.Contains(HistoryOperatorCacheKey))
                {
                    list = _cacheManager.Get<List<HistoryOperator>>(HistoryOperatorCacheKey);
                }
                else
                {
                    ///得到激活用户
                    var person = GetPerson(personId, true).HistoryOperators;
                    if(person.Any())
                    {
                       list=person.OrderByDescending(c => c.CreateTime).ToList();
                       int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(HistoryOperatorCacheKey, list,TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug("根据用户获取所有历史操作的信息",e);
                throw e;
            }
            return list;
        }
        public void UpdatePerson(Person person)
        {
            _personRepository.Update(person);
            _cacheManager.Remove(PersonsCacheKey);
            _cacheManager.Remove(PersonUserTypeCacheKey);
        }
        public List<PersonUserType> GetPersonUserType()
        {
            List<PersonUserType> list = null;
            try
            {
                if (_cacheManager.Contains(PersonUserTypeCacheKey))
                {
                    var cache = _cacheManager.Get<List<PersonUserType>>(PersonUserTypeCacheKey);
                    list = cache;
                }
                else
                {
                    var models = from p in _personRepository.Entitys
                                 join u in _userTypeRepository.Entitys on p.ID equals u.PerId
                                 where p.Type==false
                                 select new PersonUserType()
                                 {
                                     ID = p.ID,
                                     Name = p.Name,
                                     Birthday = p.Birthday,
                                     CreditScore = p.CreditScore,
                                     IdCard = p.IdCard,
                                     PerImage = p.PerImage,
                                     PerOrder = p.PerOrder,
                                     Sex = p.Sex,
                                     TypeUser = u.TypeUser,
                                     TypeMessage=u.TypeMessage,
                                     CreateTime = p.CreateTime,
                                     IsDelete=p.IsDelete,
                                     IsReduit=u.Type,
                                     PassWord=p.PassWord,
                                     PersonDetail=p.PersonDetail,
                                     Type=p.Type,
                                     Position=p.Position,
                                     Qq=p.Qq,
                                     WeChat=p.WeChat,
                                     UserTypeId=u.ID
                                 };
                    list = models.OrderByDescending(c => c.PerOrder).ThenByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(PersonUserTypeCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        list = new List<PersonUserType>();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取前台人员列表出错", e);
                throw e;
            }
            return list;
        }
        public List<Role> GetPersonRole(int id)
        {
            List<Role> roles = null;
            try
            {
                var persons = _personRepository.IncludeEntitys("Roles");
                roles = persons.FirstOrDefault(c=>c.ID==id).Roles.ToList();
            }
            catch (Exception e)
            {
                _logger.Debug("获得某个用户的所有角色", e);
                throw e;
            }
            return roles;
        }
        public void CreateUserRole(UserRole userRole)
        {
            try
            {
                SqlParameter[] sqls = new SqlParameter[]
                 {
                     new SqlParameter("@personId",userRole.PersonId),
                     new SqlParameter("@RoleId",userRole.RoleId)
                 };
                _personRepository.SqlCommand<UserType>("INSERT INTO UserRole Values(@personId,@RoleId)",sqls);
            }
            catch(Exception e)
            {
                _logger.Debug("创建用户角色信息出现错误！",e);
                throw e;
            }
        }
        public void DeleteUserRole(int id)
        {
            try
            {
                SqlParameter[] sqls = new SqlParameter[]
                 {
                     new SqlParameter("@personId",id)
                 };
                _personRepository.SqlCommand<UserType>("Delete From UserRole WHERE PersonId=@personId", sqls);
            }
            catch (Exception e)
            {
                _logger.Debug("创建用户角色信息出现错误！", e);
                throw e;
            }
        }
        #endregion

    }
}
