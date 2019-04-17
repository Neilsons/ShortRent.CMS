using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using ShortRent.Service;
using ShortRent.Web.Models;

namespace ShortRent.Web.MvcExtention
{
    public class MvcSiteMapProvider : IMvcSiteMapProvider
    {
        #region  Field
        private IEnumerable<ManagerBread> AllNodes { get; set; }
        private IEnumerable<ManagerBread> NodeTree { get; set; }
        #endregion
        #region Contructor
        public MvcSiteMapProvider(IManagerService managerService)
        {
           
        }
        #endregion
        /// <summary>
        /// 返回面包屑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ManagerBread> GetBreadCrumb(ViewContext context)
        {
            string action = context.RouteData.Values["action"] as string;
            string controller = context.RouteData.Values["controller"] as string;
            ManagerBread current = AllNodes.SingleOrDefault(node => string.Equals(node.ActionName, action, StringComparison.OrdinalIgnoreCase)
            && string.Equals(node.ControllerName, controller, StringComparison.OrdinalIgnoreCase));
            List<ManagerBread> breadcrumb = new List<ManagerBread>();
            while(current!=null)
            {
                breadcrumb.Insert(0,new ManagerBread() {
                    ClassIcons = current.ClassIcons,
                    ControllerName = current.ControllerName,
                    ActionName = current.ActionName
                });
                current=current;
            }
            return breadcrumb;
         
        }

        public IEnumerable<MvcSiteMapNode> GetSiteMap(ViewContext context)
        {
            //string action = context.RouteData.Values["action"] as string;
            //string controller = context.RouteData.Values["controller"] as string;
            //IEnumerable<MvcSiteMapNode> nodes = CopyAndSetState(NodeTree,controller,action);
            throw new NotImplementedException();
        }
        private IEnumerable<MvcSiteMapNode> ToList(IEnumerable<MvcSiteMapNode> nodes)
        {
            List<MvcSiteMapNode> list = new List<MvcSiteMapNode>();
            foreach (MvcSiteMapNode node in nodes)
            {
                list.Add(node);
                list.AddRange(ToList(node.Children));
            }
            return list;
        }
        private IEnumerable<MvcSiteMapNode> CopyAndSetState(IEnumerable<MvcSiteMapNode> nodes,string controller,string action)
        {
            List<MvcSiteMapNode> copies = new List<MvcSiteMapNode>();
            foreach(MvcSiteMapNode node in nodes)
            {
                MvcSiteMapNode copy = new MvcSiteMapNode();
                copy.IconClass = node.IconClass;
                copy.Color = node.Color;
                copy.IsMenu = node.IsMenu;

                copy.Controller = node.Controller;
                copy.Action = node.Action;
                copy.HasActiveChildren = node.Children.Any(child => child.IsActive || child.HasActiveChildren);
                copy.IsActive = copy.Children.Any(child => child.IsActive && !child.IsMenu) || (
                    string.Equals(node.Action,action,StringComparison.OrdinalIgnoreCase)&&
                    string.Equals(node.Controller,controller,StringComparison.OrdinalIgnoreCase)                    
                    );
                copy.Children=CopyAndSetState(node.Children,controller,action);
                copies.Add(copy);
            }
            return copies;
        }
        private bool IsEmpty(MvcSiteMapNode node)
        {
            return node.Action == null && !node.Children.Any();
        }

    }
}
