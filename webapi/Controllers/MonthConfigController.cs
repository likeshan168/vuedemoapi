using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lks.webapi.BLL;
using lks.webapi.Model;

namespace webapi.Controllers
{
    [RoutePrefix("api/monthconfig")]
    public class MonthConfigController : ApiController
    {
        private readonly MonthConfigService _bllService = new MonthConfigService();
        [Route("getMonthConfig"), HttpPost]
        public dynamic GetMonthConfig(PagePara para)
        {
            int total = 0;
            return _bllService.GetList(para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total);
        }
        [Route("updateMonthConfig"), HttpPost]
        public dynamic UpdateMonthConfig(IEnumerable<MonthConfig> configs)
        {
            return _bllService.AddAndUpdate(configs);
        }
        [Route("deleteMonthConfig"), HttpPost]
        public dynamic DeleteMonthConfig(string 委托人简称)
        {
            return _bllService.Delete(委托人简称);
        }
    }
}
