using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ShortRent.Web.MvcExtention
{
    public class MvcSiteMapProvider : IMvcSiteMapProvider
    {
        #region  Field
        private IEnumerable<MvcSiteMapNode> AllNodes { get; set; }
        private IEnumerable<MvcSiteMapNode> NodeTree { get; set; }
        #endregion
        #region Contructor
        public MvcSiteMapProvider(string path, IMvcSiteMapParser parse)
        {
            XElement siteMap = XElement.Load(path);
            NodeTree = parse.GetNodeTree(siteMap);
            AllNodes = ToList(NodeTree);
        }
        #endregion
        /// <summary>
        /// 返回面包屑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<MvcSiteMapNode> GetBreadCrumb(ViewContext context)
        {
            string action = context.RouteData.Values["action"] as string;
            string controller = context.RouteData.Values["controller"] as string;
            MvcSiteMapNode current = AllNodes.SingleOrDefault(node => string.Equals(node.Action, action, StringComparison.OrdinalIgnoreCase)
            && string.Equals(node.Controller, controller, StringComparison.OrdinalIgnoreCase));
            List<MvcSiteMapNode> breadcrumb = new List<MvcSiteMapNode>();
            while(current!=null)
            {
                breadcrumb.Insert(0,new MvcSiteMapNode() {
                    IconClass=current.IconClass,
                    Controller=current.Controller,
                    Action=current.Action
                });
                current = current.Parent;
            }
            return breadcrumb;
         
        }

        public IEnumerable<MvcSiteMapNode> GetSiteMap(ViewContext context)
        {
            string action = context.RouteData.Values["action"] as string;
            string controller = context.RouteData.Values["controller"] as string;
            IEnumerable<MvcSiteMapNode> nodes = CopyAndSetState(NodeTree,controller,action);
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
