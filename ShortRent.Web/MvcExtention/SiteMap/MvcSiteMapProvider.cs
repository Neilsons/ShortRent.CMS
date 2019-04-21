using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
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
        public MvcSiteMapProvider(IManagerService managerService,IMapper mapper)
        {
            AllNodes =mapper.Map<List<ManagerBread>>(managerService.GetManagers().AsEnumerable());
            NodeTree= mapper.Map<List<ManagerBread>>(managerService.GetTreeViewManagers().AsEnumerable());
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
            if(action!="Home")
            {
                if (current == null)
                {
                    current = AllNodes.SingleOrDefault(node => string.Equals(node.ActionName, "List", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(node.ControllerName, controller, StringComparison.OrdinalIgnoreCase));
                    if (action == "Create")
                        breadcrumb.Add(new ManagerBread() { ClassIcons = "fa fa-plus", Color = "#000000", Name = "创建" });
                    if (action == "Edit")
                        breadcrumb.Add(new ManagerBread() { ClassIcons = "fa fa-pencil", Color = "#000000", Name = "编辑" });
                    if (action == "Detail")
                        breadcrumb.Add(new ManagerBread() { ClassIcons = "fa fa-info-circle", Color = "#000000", Name = "详情" });
                }
            }
            while(current!=null)
            {
                if(current.ControllerName!=null&& current.ActionName!=null)
                {
                    breadcrumb.Insert(0, new ManagerBread()
                    {
                        ClassIcons = current.ClassIcons,
                        Color = current.Color,
                        ControllerName = current.ControllerName,
                        ActionName = current.ActionName,
                        Name=current.Name
                    });
                }
                current = current.Parent;
            }
            breadcrumb.Insert(0, new ManagerBread()
            {
                ClassIcons = "fa fa-home",
                Color = "#000000",
                ControllerName = "Person",
                ActionName = "Home",
                Name="家"
            });
            return breadcrumb;
         
        }

        public IEnumerable<ManagerBread> GetSiteMap(ViewContext context)
        {
            int account = 1;
            string action = context.RouteData.Values["action"] as string;
            string controller = context.RouteData.Values["controller"] as string;
            List<ManagerBread> nodes = CopyAndSetState(NodeTree.ToList(), controller, action);
            return GetAuthorizedNodes(account, nodes);
        }
        private List<ManagerBread> CopyAndSetState(List<ManagerBread> nodes,string controller,string action)
        {
            List<ManagerBread> copies = new List<ManagerBread>();
            foreach(ManagerBread node in nodes)
            {
                ManagerBread copy = new ManagerBread();
                copy.ClassIcons = node.ClassIcons;
                copy.Color = node.Color;

                copy.ControllerName = node.ControllerName;
                copy.ActionName = node.ActionName;
                copy.HasActiveChildren = node.Childrens.Any(child => child.Activity || (child.HasActiveChildren??false));
                copy.Activity = node.Childrens.Any(child => child.Activity && !(child.ControllerName==null&&child.ActionName==null)) || (
                    string.Equals(node.ActionName,action,StringComparison.OrdinalIgnoreCase)&&
                    string.Equals(node.ControllerName,controller,StringComparison.OrdinalIgnoreCase)                    
                    );
                copy.Childrens=CopyAndSetState(node.Childrens,controller,action);
                copies.Add(copy);
            }
            return copies;
        }
        private IEnumerable<ManagerBread> GetAuthorizedNodes(Int32? accountId, IEnumerable<ManagerBread> nodes)
        {
            List<ManagerBread> authorized = new List<ManagerBread>();
            //foreach (MvcSiteMapNode node in nodes)
            //{
            //    node.Children = GetAuthorizedNodes(accountId, node.Children);

            //    if (node.IsMenu && IsAuthorizedToView(accountId, node.Area, node.Controller, node.Action) && !IsEmpty(node))
            //        authorized.Add(node);
            //    else
            //        authorized.AddRange(node.Children);
            //}

            return authorized;
        }
        private bool IsEmpty(ManagerBread node)
        {
            return node.ActionName == null && !node.Childrens.Any();
        }

    }
}
