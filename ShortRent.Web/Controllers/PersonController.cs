using AutoMapper;
using ShortRent.Core;
using ShortRent.Core.Domain;
using ShortRent.Core.Language;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web.Controllers
{
    public class PersonController : BaseController
    {
        #region Field 
        private readonly IPersonService _personService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IAutnenticationProvider _authenticationProvider;
        //autoMapper
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Construction
        public PersonController(IPersonService personService
            ,IMapper mapper,
            MapperConfiguration mapperConfiguration,ILogger logger
            ,IHistoryOperatorService historyOperator
            ,IAutnenticationProvider autnentication)
        {
            this._personService = personService;
            this._historyOperatorService = historyOperator;
            this._authenticationProvider = autnentication;
            this._mapper = mapper;
            this._mapperConfig = mapperConfiguration;
            this._logger = logger;
        }
        #endregion
        #region Method
        public ActionResult List()
        {
            ViewBag.Title = "后台用户管理";
            ViewBag.Content = "列表";
            return View();
        }
        public ActionResult Index(int? pageSize,int? pageNumber,string AdminName)
        {
            List<PersonAdminIndexModel> list = null;
            PagedListViewModel<PersonAdminIndexModel> paged = new PagedListViewModel<PersonAdminIndexModel>();
            try
            {
                int total;
                var persons = _personService.GetTypePerson(pageSize ?? 0, pageNumber ?? 0,AdminName, 1, out total);
                if(persons.Any())
                {
                    list = _mapper.Map<List<PersonAdminIndexModel>>(persons);
                }
                else
                {
                    list = new List<PersonAdminIndexModel>();
                }
                paged.Rows = list;
                paged.Total = total;
            }
            catch(Exception e)
            {
                list = new List<PersonAdminIndexModel>();
                _logger.Debug("获得列表时出现错误",e);
                throw e;
            }
            return Json(paged,JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Create()
        {
            ViewBag.Title = "后台用户管理";
            ViewBag.Content = "后台用户创建";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonAdminEditModel personAdminEditModel, HttpPostedFileBase headPhoto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Person person = new Person();
                    Dictionary<string, string> str = new Dictionary<string, string>();//上传成功后返回文件的信息
                    if (headPhoto != null)
                    {
                        //上传文件到服务器
                        str = UploadImg(headPhoto, "/Content/Images/AdminImg");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传文件格式不正确，请重新上传，所修改数据未被保留！", Url = Url.Action(nameof(PersonController.PersonAdminDetail)) });
                        }
                    }
                    person = _mapper.Map<Person>(personAdminEditModel);
                    person.CreateTime = DateTime.Now;
                    if(str["ImagePath"]!=null)
                    {
                        person.PerImage = str["ImagePath"];
                    }
                    person.IdCard = "000000";
                    person.Type = true;
                    person.Birthday = DateTime.Now.AddYears(-18);
                    person.CreditScore = 0;
                    _personService.CreatePerson(person);                   
                    PersonAdminHumanEditModel human = _mapper.Map<PersonAdminHumanEditModel>(personAdminEditModel);
                    //将修改的信息加入记录中去
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<PersonAdminHumanEditModel>("创建了一个后台用户，详情", human),
                        EntityModule = "用户管理",
                        Operates = "后台用户创建",
                        PersonId = GetCurrentPerson().ID
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                }
                else
                {
                    return View(personAdminEditModel);
                }
            }
            catch(Exception e)
            {
                _logger.Debug("创建用户失败",e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "创建后台用户成功！", Url = Url.Action(nameof(PersonController.List)) });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "后台用户管理";
            ViewBag.Content = "后台用户编辑";
            PersonAdminEditModel person = new PersonAdminEditModel();
            try
            {
                person = _mapper.Map<PersonAdminEditModel>(_personService.GetPerson(id));
            }
            catch(Exception e)
            {
                _logger.Debug("显示编辑出错！",e);
                throw e;
            }
            return View("Create",person);
        }
        public ActionResult ReSetPassWord(int id)
        {
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "重置密码成功！", Url = Url.Action(nameof(PersonController.List)) });
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PersonLoginModel model,string returnUrl)
        {
            try
            {
                //获得所有的用户
                var person = _personService.GetPersons().Where(c=>c.Type==true).SingleOrDefault(c => c.Name == model.Name && c.PassWord == model.PassWord);
                if(person!=null)
                {
                    var singnPer = person;
                    //身份登陆成功
                    _authenticationProvider.SignIn(singnPer, model.ReadMe);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = model.Name + "登陆了系统",
                        EntityModule = "用户登陆",
                        Operates = "登陆",
                        PersonId = person.ID
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "登陆成功！",Url=Url.Action(nameof(PersonController.Home))});
                }
            }
            catch(Exception e)
            {
                _logger.Debug("登陆出现错误！",e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.NotFound, Message = "用户名或密码错误", Url =Url.Action(nameof(PersonController.Login)) });
        }
        public ActionResult Home()
        {
            ViewBag.Title = "后台首页";
            return View();
        }
        public ActionResult SignOut()
        {
            _authenticationProvider.SignOut();
            throw new HttpException((int)HttpStatusCode.Unauthorized,"");
        }
        public ActionResult PersonalData()
        {
            ViewBag.Title = "个人资料";
            ViewBag.Content = "详情";
            return View();
        }
        public ActionResult PersonAdminDetail()
        {
            ViewBag.Title = "个人资料编辑";
            ViewBag.Content = "编辑";
            PersonAdminEditModel admin = null;
            
            try
            {
                WorkContext workContext = new WorkContext();
                    //得到当前人的信息
                    admin = _mapper.Map<PersonAdminEditModel>(workContext.CurrentPerson);
            }
            catch(Exception e)
            {
                _logger.Debug("后台人员修改个人资料出错");
                throw e;
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonAdminDetail(PersonAdminEditModel model, HttpPostedFileBase headPhoto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Dictionary<string, string> str = new Dictionary<string, string>();//上传成功后返回文件的信息
                    if (headPhoto != null)
                    {
                        //上传文件到服务器
                        str = UploadImg(headPhoto, "/Content/Images/AdminImg");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传文件格式不正确，请重新上传，所修改数据未被保留！", Url = Url.Action(nameof(PersonController.PersonAdminDetail)) });
                        }
                        Person person = _mapper.Map<Person>(model);
                        person.PerImage = str["ImagePath"];
                        person.IdCard = "000000";
                        person.Birthday = DateTime.Now.AddYears(-18);
                        //更新用户
                        _personService.UpdatePerson(person);
                    }
                    else
                    {
                        Person person = _mapper.Map<Person>(model);
                        person.IdCard = "000000";
                        person.Birthday = DateTime.Now.AddYears(-18);
                        _personService.UpdatePerson(person);
                    }
                }
                else
                {
                    return View(model);
                }
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "个人资料修改成功！", Url = Url.Action(nameof(PersonController.PersonalData)) });
            }
            catch(Exception e)
            {
                _logger.Debug("修改详情出错",e);
                throw e;
            }
        }
        public ActionResult EditPassWord()
        {
            ViewBag.Title = "个人资料";
            ViewBag.Content = "密码修改";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassWord(PassWordEditModel model)
        {
            try
            {
                //先将该用户从数据库中查出来
                Person person = _personService.GetPersons().SingleOrDefault(c => c.ID == model.ID && c.PassWord == model.OldPassWord);
                if(person!=null)
                {
                    if(model.PassWord==person.PassWord)
                    {
                        return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "新密码不能和旧密码相同，请重新输入", Url = Url.Action(nameof(PersonController.EditPassWord)) });
                    }
                    //将用户放到adminPerson作为缓冲
                    PersonAdminUpdate adminPerson = _mapper.Map<PersonAdminUpdate>(person);
                    //查到之后修改该人的密码
                    adminPerson.PassWord = model.PassWord;
                    Person updatePerson = _mapper.Map<Person>(adminPerson);
                    //保存起来
                    _personService.UpdatePerson(updatePerson);
                }
                else
                {
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "旧密码输入错误，请输入登录时的密码！", Url = Url.Action(nameof(PersonController.EditPassWord)) });
                }
            }
            catch(Exception e)
            {
                _logger.Debug("修改密码出错！");
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult=(int)HttpStatusCode.OK,Message="修改密码成功！",Url=Url.Action(nameof(PersonController.PersonalData)) });
        }
        private Dictionary<string,string> UploadImg(HttpPostedFileBase file,string dir)
        {
            string Image_path = null;//保存文件的路径
            Dictionary<string, string> DicInfo = new Dictionary<string, string>();//返回的文件信息
            string fileName = Path.GetFileName(file.FileName); //获取文件名
            string fileExt = Path.GetExtension(fileName);      //获取扩展名
            if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".png")
            {
                string NewDay = DateTime.Now.ToString("yyyy-MM-dd");
                dir = dir + "/" + NewDay + "/";
                if (!Directory.Exists(Request.MapPath(dir)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                }
                //创建文件夹
                //需要对上传的文件进行重命名
                string newfileName = Guid.NewGuid().ToString();
                //构建文件完整路径
                string fullDir = dir + newfileName + fileExt;
                file.SaveAs(Request.MapPath(fullDir));  //保存文件  
                //将上传成功的图片的路径存到数据库中
                Image_path = NewDay + "/"+newfileName + fileExt;
                DicInfo.Add("ImagePath", Image_path);
                DicInfo.Add("Result", "1");
            }
            else
            {
                DicInfo.Add("ImagePath", "");
                DicInfo.Add("Result", "0");
            }
            return DicInfo;
        }
        #endregion
    }
}