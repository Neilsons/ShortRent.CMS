using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Areas.ShortWeb.Models
{
    public class UploadWebImage
    {
        /// <summary>
        /// 个人头像图片
        /// </summary>
        public HttpPostedFileBase HeadPhoto { get; set; }
        /// <summary>
        /// 身份证正面
        /// </summary>
        public HttpPostedFileBase IdCardFront { get; set; }
        /// <summary>
        /// 身份证反面
        /// </summary>
        public HttpPostedFileBase IdCardBack { get; set; }
        /// <summary>
        /// 公司LOGO
        /// </summary>
        public HttpPostedFileBase CompanyImg { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public HttpPostedFileBase CompanyLicense { get; set; }

    }
}