using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class RolePageListViewModel
    {
        public int Total { get ; set ; }
        public List<RoleViewModelIndex> Rows{get ; set ;}
    }
}