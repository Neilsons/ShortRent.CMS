using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class PagedListViewModel<T>
    {
        public int Total { get; set; }
        public List<T> Rows { get; set; }
    }
}