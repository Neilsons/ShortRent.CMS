using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Areas.ShortWeb.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields
        private readonly IPersonService _personService;
        private readonly IUserTypeService _userTypeService;
        private readonly ICompanyService _companyService;
        private readonly IAutnenticationProvider _authenticationProvider;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Constroctor
        public HomeController(IPersonService personService,
            IMapper mapper,
            MapperConfiguration mapperconfig,
            ILogger logger,
            IUserTypeService userTypeService,
            ICompanyService companyService,
            IAutnenticationProvider autnenticationProvider)
        {
            _personService = personService;
            _mapper = mapper;
            _mapperConfig = mapperconfig;
            _logger = logger;
            _userTypeService = userTypeService;
            _companyService = companyService;
            _authenticationProvider = autnenticationProvider;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        // GET: ShortWeb/Home
        [AllowAnonymous]
        public ActionResult Login(string returnUrl,bool? isReduit)
        {
            ViewBag.ReturnUrl = returnUrl;
            if(isReduit!=null)
            {
                ViewBag.IsReduit = isReduit.ToString();
            }
            else
            {
                ViewBag.IsReduit = null;
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(WebLoginModel model, string returnUrl)
        {
            try
            {
                //获得所有的用户
                var person = _personService.GetPersonUserType().Where(c => c.IsReduit==model.Type).FirstOrDefault(c => c.Name == model.Name && c.PassWord == model.PassWord);
                if (person != null)
                {
                    var singnPer = person;
                    //身份登陆成功
                    _authenticationProvider.SignIn(singnPer, model.ReadMe);
                    if(returnUrl!=null)
                        return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "登陆成功！", Url = returnUrl });
                    else
                        return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "登陆成功！", Url = Url.Action(nameof(HomeController.List))});
                }
            }
            catch (Exception e)
            {
                _logger.Debug("登陆出现错误！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.NotFound, Message = "用户名密码或账户状态出错", Url = Url.Action(nameof(HomeController.Login)) });
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            _authenticationProvider.SignOut();
            return RedirectToAction(nameof(HomeController.Login));
        }
        public ActionResult EditPassWord()
        {
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
                if (person != null)
                {
                    if (model.PassWord == person.PassWord)
                    {
                        return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "新密码不能和旧密码相同，请重新输入", Url = Url.Action(nameof(HomeController.EditPassWord)) });
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
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "旧密码输入错误，请输入登录时的密码！", Url = Url.Action(nameof(HomeController.EditPassWord)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug("修改密码出错！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "修改密码成功！", Url = Url.Action(nameof(HomeController.List)) });
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(WebRegister model,UploadWebImage uploadImage)
        {
            try
            {
                //先添加基本信息
                Person person = _mapper.Map<Person>(model);
                //先定义默认的数据
                person.Type = false;
                person.CreditScore = 100;
                person.IsDelete = true;
                person.CreateTime = DateTime.Now;
                //个人的图片信息
                 Dictionary<string, string> str = new Dictionary<string, string>();//上传成功后返回文件的信息
                if (uploadImage.HeadPhoto != null)
                {
                    //上传文件到服务器
                    str = UploadImg(uploadImage.HeadPhoto, "/Content/Images/WebImage/Avatar");
                    if (str["Result"].ToString() == "0")
                    {
                        return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传个人头像格式不正确，请重新上传，所修改数据未被保留！" });
                    }
                    else
                    {
                        if (str.Keys.Any(c => c == "ImagePath"))
                        {
                            person.PerImage = str["ImagePath"];
                        }
                    }
                }
                //添加个人信息
                _personService.CreatePerson(person);
                //获得人的ID
                int ID = person.ID;
                //判断注册为招聘者还是被招聘者
                if(model.IsRecruit==true)
                {
                    //注册为被招聘者
                    //只用在UserType表中添加
                    UserType userType = new UserType();
                    if (uploadImage.IdCardFront != null)
                    {
                        str = new Dictionary<string, string>();
                        //上传文件到服务器
                        str = UploadImg(uploadImage.IdCardFront, "/Content/Images/WebImage/IdCard");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传身份证正面格式不正确，请重新上传，所修改数据未被保留！" });
                        }
                        else
                        {
                            if (str.Keys.Any(c => c == "ImagePath"))
                            {
                                userType.IdCardFront = str["ImagePath"];
                            }
                        }
                    }
                    if (uploadImage.IdCardBack != null)
                    {
                        str = new Dictionary<string, string>();
                        //上传文件到服务器
                        str = UploadImg(uploadImage.IdCardBack, "/Content/Images/WebImage/IdCard");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传身份证反面格式不正确，请重新上传，所修改数据未被保留！" });
                        }
                        else
                        {
                            if (str.Keys.Any(c => c == "ImagePath"))
                            {
                                userType.IdCardBack = str["ImagePath"];
                            }
                        }
                    }
                    userType.CreateTime = DateTime.Now;
                    userType.PerId = ID;
                    userType.Type = false;
                    //未审核
                    userType.TypeUser = 0;
                    //添加
                    _userTypeService.CreateUserType(userType);
                }
                else
                {

                    //注册为招聘者
                    //在UserType中添加
                    UserType userType = new UserType();
                    userType.CreateTime = DateTime.Now;
                    userType.PerId = ID;
                    userType.Type = true;
                    userType.TypeUser = 0;
                    //添加用户信息
                    _userTypeService.CreateUserType(userType);
                    //获取添加后的UserType的ID
                    int userTypeId = userType.ID;
                    //添加公司
                    Company company = new Company();
                    company = _mapper.Map<Company>(model);
                    //公司LOGO
                    if(uploadImage.CompanyImg!=null)
                    {
                        str = new Dictionary<string, string>();
                        //上传文件到服务器
                        str = UploadImg(uploadImage.CompanyImg, "/Content/Images/WebImage/Company");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传公司LOGO格式不正确，请重新上传，所修改数据未被保留！" });
                        }
                        else
                        {
                            if (str.Keys.Any(c => c == "ImagePath"))
                            {
                                company.CompanyImg = str["ImagePath"];
                            }
                        }
                    }
                    //营业执照图片
                    if(uploadImage.CompanyLicense!=null)
                    {
                        str = new Dictionary<string, string>();
                        //上传文件到服务器
                        str = UploadImg(uploadImage.CompanyLicense, "/Content/Images/WebImage/Company");
                        if (str["Result"].ToString() == "0")
                        {
                            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Message = "上传营业执照格式不正确，请重新上传，所修改数据未被保留！" });
                        }
                        else
                        {
                            if (str.Keys.Any(c => c == "ImagePath"))
                            {
                                company.CompanyLicense = str["ImagePath"];
                            }
                        }
                    }
                    company.Score = 100;
                    company.CreateTime = DateTime.Now;
                    company.UserTypeId = userTypeId;
                    company.CompanyStatus = 0;
                    _companyService.CreateCompany(company);

                }
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK,Message="注册成功，将跳转到登陆界面", Url = "/ShortWeb/Home/Login" });
            }
            catch(Exception e)
            {
                _logger.Debug("注册时出现错误",e);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError,Message="注册出现错误", Url = "/ShortWeb/System/InternalServerError" });
            }
        }
        [HttpGet]
        public ActionResult GetJsonByName(string name)
        {
            //首先得到名称
            try
            {
                Person person = _personService.GetPersonByName(name);
                if(person!=null)
                {
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.NotFound, Message = $"姓名{name}重复，请重新填写" });
                }
                else
                {
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK });
                }
            }
            catch(Exception e)
            {
                _logger.Debug("注册姓名时出现错误", e);
                return Json(new AjaxJson() { HttpCodeResult=(int)HttpStatusCode.InternalServerError,Url= "/ShortWeb/System/InternalServerError" });
            }
        }
        private Dictionary<string, string> UploadImg(HttpPostedFileBase file, string dir)
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
                Image_path = NewDay + "/" + newfileName + fileExt;
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
    }
}