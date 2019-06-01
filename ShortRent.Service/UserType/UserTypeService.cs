using ShortRent.Core;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
   public  class UserTypeService:BaseService,IUserTypeService
   {
        #region Fields
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string userTypeCacheKey = nameof(UserTypeService) + nameof(UserType);
        private const string RecruiterByCacheKey = nameof(RecruiterByViewModel) + nameof(UserType);
        private const string RecruiterCacheKey = nameof(RecruiterViewModel) + nameof(UserType);
        #endregion
        #region Constroctor
        public UserTypeService(IRepository<UserType> userTypeRepository,
            IRepository<Person> personRepository,
            ICacheManager cacheManager,
            ILogger logger,
            ApplicationConfig config)
        {
            _userTypeRepository = userTypeRepository;
            _personRepository = personRepository;
            _cacheManager = cacheManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        #region  Methods
        public void CreateUserType(UserType userType)
        {
            _userTypeRepository.Insert(userType);
            _cacheManager.Remove(userTypeCacheKey);
            _cacheManager.Remove(RecruiterByCacheKey);
            _cacheManager.Remove(RecruiterCacheKey);
        }
        public void UpdateUserType(UserType userType)
        {
            _userTypeRepository.Update(userType);
            _cacheManager.Remove(userTypeCacheKey);
            _cacheManager.Remove(RecruiterByCacheKey);
            _cacheManager.Remove(RecruiterCacheKey);
        }
        //得到被招聘者的列表
        public UserTypeAudit GetUserAudit(int id)
        {
            UserTypeAudit userTypeAudit = null;
            try
            {
                var entitys = from p in _personRepository.Entitys
                             join u in _userTypeRepository.Entitys on p.ID equals u.PerId
                             where p.Type == false && u.Type == false && p.ID == id
                             select new UserTypeAudit()
                             {
                                 Birthday=p.Birthday,
                                 ID=p.ID,
                                 CreditScore=p.CreditScore,
                                 IdCard=p.IdCard,
                                 Sex=p.Sex,
                                 IdCardBack=u.IdCardBack,
                                 IdCardFront=u.IdCardFront,
                                 Name=p.Name,
                                 PerOrder=p.PerOrder,
                                 TypeMessage=u.TypeMessage,
                                 TypeUser=u.TypeUser,
                                 CreateTime=p.CreateTime,
                                 UserTypeId=u.ID
                             };
                var models = entitys.OrderByDescending(c => c.PerOrder).ThenByDescending(c => c.CreateTime).ToList();
                if(models.Any())
                {
                    userTypeAudit = models.FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                _logger.Error("根据ID获取被招聘者审核信息出错");
                throw e;
            }
            return userTypeAudit;
        }
        public UserType GetUserTypeById(int id)
        {
            UserType model = null;
            try
            {
                if (_cacheManager.Contains(userTypeCacheKey))
                {
                        model = _cacheManager.Get<List<UserType>>(userTypeCacheKey).Where(c => c.ID == id).FirstOrDefault();
                }
                else
                {
                    
                    model = _userTypeRepository.Entitys.Where(c => c.ID == id && c.IsDelete == false).FirstOrDefault();
                    if (model == null)
                    {
                        model = new UserType();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("根据ID返回UserTye出错", e);
                throw e;
            }
            return model;

        }
        /// <summary>
        /// 获取被招聘者列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="Name"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<RecruiterByViewModel> GetRecruiterByViewModelList(int pageSize, int pageNumber, string Name, out int total)
        {
            List<RecruiterByViewModel> list = null;
            try
            {
                Expression<Func<RecruiterByViewModel, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(Name))//条件
                {
                    expression = expression.And(c => c.Name.Contains(Name));
                }
                if (_cacheManager.Contains(RecruiterByCacheKey))
                {
                    var cache= _cacheManager.Get<List<RecruiterByViewModel>>(RecruiterByCacheKey).Where(expression.Compile());
                    list = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var models = from p in _personRepository.Entitys
                                 join u in _userTypeRepository.Entitys on p.ID equals u.PerId
                                 where p.Type == false && u.Type == false
                                 select new RecruiterByViewModel()
                                 {
                                     ID=p.ID,
                                     Name=p.Name,
                                     Birthday=p.Birthday,
                                     CreditScore=p.CreditScore,
                                     IdCard=p.IdCard,
                                     PerImage=p.PerImage,
                                     PerOrder=p.PerOrder,
                                     Sex=p.Sex,
                                     TypeUser=u.TypeUser,
                                     IdCardBack=u.IdCardBack,
                                     IdCardFront=u.IdCardFront,
                                     CreateTime=p.CreateTime
                                 };
                    models = models.OrderByDescending(c => c.PerOrder).ThenByDescending(c => c.CreateTime).ToList();
                    if(models.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        list = models.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = models.Where(expression.Compile()).Count();
                        _cacheManager.Set(RecruiterByCacheKey, models, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        list = new List<RecruiterByViewModel>();
                        total = 0;
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Error("获取被招聘者列表出错",e);
                throw e;
            }
            return list;            
        }
        /// <summary>
        /// 获取招聘者列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="Name"></param>
        /// <param name="total"></param>
        /// <returns></returns>
       public List<RecruiterViewModel> GetRecruiterViewModelList(int pageSize, int pageNumber, string Name, out int total)
        {
            List<RecruiterViewModel> list = null;
            try
            {
                Expression<Func<RecruiterViewModel, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(Name))//条件
                {
                    expression = expression.And(c => c.Name.Contains(Name));
                }
                if (_cacheManager.Contains(RecruiterCacheKey))
                {
                    var cache = _cacheManager.Get<List<RecruiterViewModel>>(RecruiterCacheKey).Where(expression.Compile());
                    list = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var entitys = from p in _personRepository.Entitys
                                 join u in _userTypeRepository.Entitys on p.ID equals u.PerId
                                 where p.Type == false && u.Type == true
                                 select new RecruiterViewModel()
                                 {
                                     ID = p.ID,
                                     Name = p.Name,
                                     Birthday = p.Birthday,
                                     CreditScore = p.CreditScore,
                                     IdCard = p.IdCard,
                                     PerImage = p.PerImage,
                                     PerOrder = p.PerOrder,
                                     Sex = p.Sex,
                                     CreateTime = p.CreateTime
                                 };
                   var  models = entitys.OrderByDescending(c => c.PerOrder).ThenByDescending(c => c.CreateTime).ToList();
                    if (models.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        list = models.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = models.Where(expression.Compile()).Count();
                        _cacheManager.Set(RecruiterCacheKey, models, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        list = new List<RecruiterViewModel>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取招聘者列表出错", e);
                throw e;
            }
            return list;
        }
        #endregion
    }
}
