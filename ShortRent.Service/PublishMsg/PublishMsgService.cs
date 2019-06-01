using ShortRent.Core;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class PublishMsgService:BaseService,IPublishMsgService
    {
        #region Fields
        private readonly IRepository<PublishMsg> _publishMsgRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly IRepository<Company> _companyRepository; 
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string PublishMsgCacheKey = nameof(PublishMsgService) + nameof(PublishMsg);
        #endregion
        #region Constroctor
        public PublishMsgService(IRepository<PublishMsg> publishMsgRepository,
            IRepository<Person> personRepository,
            IRepository<UserType> userTypeRepository,
            IRepository<Company> companyRepository,
            ICacheManager cacheManager, 
            ILogger logger, 
            ApplicationConfig config)
        {
            _publishMsgRepository = publishMsgRepository;
            _personRepository = personRepository;
            _userTypeRepository = userTypeRepository;
            _companyRepository = companyRepository;
            _cacheManager = cacheManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        #region  Methods
        public void CreatePublishMsg(PublishMsg model)
        {
            _publishMsgRepository.Insert(model);
            _cacheManager.Remove(PublishMsgCacheKey);
        }
        public List<RecruiterByUserTypePersonModel> GetPageRecruiterBy(int pagedIndex, int pagedSize, string Name,int? Bussiness,DateTime? startTime, DateTime? endTime, out int total)
        {
            List<RecruiterByUserTypePersonModel> list = null;
            try
            {
                Expression<Func<RecruiterByUserTypePersonModel, bool>> expression = RecruiterBy => true;
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    expression = expression.And(c => c.Name.Contains(Name));
                }
                if (Bussiness!=null && Bussiness != 0)
                {
                    expression = expression.And(c => c.BusinessTypeId == Bussiness);
                }
                if (startTime != null)
                {
                    expression = expression.And(c => c.CreateTime >= startTime);
                }
                if (endTime != null)
                {
                    expression = expression.And(c => c.CreateTime <= endTime);
                }
                var models = from h in _publishMsgRepository.Entitys
                             join u in _userTypeRepository.Entitys on h.UserTypeInfoId equals u.ID
                             join p in _personRepository.Entitys on u.PerId equals p.ID
                             where p.Type == false &&u.Type==false
                             select new RecruiterByUserTypePersonModel()
                             {
                                 PersonId=p.ID,
                                 Name = p.Name,
                                 Address = h.Address,
                                 CreateTime = h.CreateTime,
                                 CreditScore = p.CreditScore,
                                 Type = u.Type,
                                 Currency = h.Currency,
                                 Email = h.Email,
                                 EndSection=h.EndSection,
                                 StartSection=h.StartSection,
                                 Phone=h.Phone,
                                 UserTypeId=u.ID,
                                 ID=h.ID,
                                 BusinessTypeId=h.BusinessTypeId
                                 };
                models = models.OrderByDescending(c => c.CreditScore).ThenByDescending(c => c.CreateTime).Where(expression.Compile());
                if(models.Any())
                {
                    list = models.Skip((pagedIndex - 1) * pagedSize).Take(pagedSize).ToList();
                    total = models.Count();
                }
                else
                {
                    list = new List<RecruiterByUserTypePersonModel>();
                    total = 0;
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取被招聘者发布消息列表出错", e);
                throw e;
            }
            return list;
        }

        public PublishMsg GetPublishMsgById(int id)
        {
            PublishMsg publishMsg = null;
            try
            {
                publishMsg = _publishMsgRepository.Entitys.Where(c => c.ID == id).FirstOrDefault();
                   
            }
            catch (Exception e)
            {
                _logger.Debug("获得发布信息出错", e);
                throw e;
            }
            return publishMsg;
        }

        public int PersonIdByPublishId(int id)
        {
            int personId = 0;
            try
            {
                var ids = from h in _publishMsgRepository.Entitys
                         join u in _userTypeRepository.Entitys on h.UserTypeInfoId equals u.ID
                         join p in _personRepository.Entitys on u.PerId equals p.ID
                         where p.Type == false && h.ID == id
                         select p.ID;
                personId = ids.FirstOrDefault();
            }
            catch(Exception e)
            {
                _logger.Debug("获取用户ID出错",e);
                throw e;
            }
            return personId;
        }
        public List<RecruiterUserTypePersonModel> GetPageRecruiter(int pagedIndex, int pagedSize, string CompanyName,string Name, int? Bussiness, DateTime? startTime, DateTime? endTime, out int total)
        {
            List<RecruiterUserTypePersonModel> list = null;
            try
            {
                Expression<Func<RecruiterUserTypePersonModel, bool>> expression = RecruiterBy => true;
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    expression = expression.And(c => c.Name.Contains(Name));
                }
                if (!string.IsNullOrWhiteSpace(CompanyName))
                {
                    expression = expression.And(c =>c.CompanyName.Contains(CompanyName));
                }
                if (Bussiness != null && Bussiness != 0)
                {
                    expression = expression.And(c => c.BusinessTypeId == Bussiness);
                }
                if (startTime != null)
                {
                    expression = expression.And(c => c.CreateTime >= startTime);
                }
                if (endTime != null)
                {
                    expression = expression.And(c => c.CreateTime <= endTime);
                }
                var models = from h in _publishMsgRepository.Entitys
                             join u in _userTypeRepository.Entitys on h.UserTypeInfoId equals u.ID
                             join ww in _companyRepository.Entitys on u.ID equals ww.UserTypeId
                             join p in _personRepository.Entitys on u.PerId equals p.ID
                             where p.Type == false && u.Type == true
                             select new RecruiterUserTypePersonModel()
                             {
                                 CompanyId=ww.ID,
                                 CompanyName=ww.Name,
                                 Name = p.Name,
                                 Address = h.Address,
                                 CreateTime = h.CreateTime,
                                 CreditScore = ww.Score,
                                 Type = u.Type,
                                 Currency = h.Currency,
                                 Email = h.Email,
                                 EndSection = h.EndSection,
                                 StartSection = h.StartSection,
                                 Phone = h.Phone,
                                 UserTypeId = u.ID,
                                 ID = h.ID,
                                 BusinessTypeId = h.BusinessTypeId
                             };
                models = models.OrderByDescending(c => c.CreditScore).ThenByDescending(c => c.CreateTime).Where(expression.Compile());
                if (models.Any())
                {
                    list = models.Skip((pagedIndex - 1) * pagedSize).Take(pagedSize).ToList();
                    total = models.Count();
                }
                else
                {
                    list = new List<RecruiterUserTypePersonModel>();
                    total = 0;
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取招聘者发布消息列表出错", e);
                throw e;
            }
            return list;
        }
        #endregion
    }
}
