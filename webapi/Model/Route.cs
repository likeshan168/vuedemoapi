using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lks.webapi.Model
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Op { get; set; }
        public string Remark { get; set; }
    }
}