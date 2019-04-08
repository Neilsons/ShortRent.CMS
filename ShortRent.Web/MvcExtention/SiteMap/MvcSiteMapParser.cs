using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ShortRent.Web.MvcExtention
{
    public class MvcSiteMapParser : IMvcSiteMapParser
    {
        public IEnumerable<MvcSiteMapNode> GetNodeTree(XElement sitemap)
        {
            return GetNodes(sitemap,null);
        }
        private IEnumerable<MvcSiteMapNode> GetNodes(XElement siteMaop,MvcSiteMapNode Parent)
        {
            List<MvcSiteMapNode> nodes = new List<MvcSiteMapNode>();
            foreach (XElement element in siteMaop.Elements("siteMapNode"))
            {
                MvcSiteMapNode node = new MvcSiteMapNode();

                node.IsMenu = (bool?)element.Attribute("menu")==true;
                node.Controller=(string)element.Attribute("controller");
                node.IconClass = (string)element.Attribute("icon");
                node.Color = (string)element.Attribute("color");
                node.Action = (string)element.Attribute("action");
                node.Children=GetNodes(element,node);
                node.Parent=Parent;
                nodes.Add(node);
            }
            return nodes;
        }
    }
}