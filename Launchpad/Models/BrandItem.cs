using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Launchpad.Models
{
    public class BrandItem
    {
        public string ItemID { get; set; }
        public string Size { get; internal set; }
        public string ItemDescEng { get; internal set; }
    }
}