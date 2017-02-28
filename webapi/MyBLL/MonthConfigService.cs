using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lks.webapi.Model;

namespace lks.webapi.BLL
{
    public partial class MonthConfigService
    {
        public dynamic GetList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            total = 0;
            try
            {
                var monthConfigs = dal.QueryList(columns, index, size, wheres, orderField, out total, isDesc);
                return new
                {
                    MonthConfigs = monthConfigs,
                    Total = total,
                    Code = 200,
                    Msg = "请求成功"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    MonthConfigs = new List<MonthConfig>(),
                    Total = total,
                    Code = 500,
                    Msg = ex.Message
                };
            }
        }

        public dynamic AddAndUpdate(IEnumerable<MonthConfig> models)
        {
            try
            {
                dal.AddAndUpdate(models);
                return new
                {
                    Code = 200,
                    Msg = "保存成功"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Code = 500,
                    Msg = ex.Message
                };
            }
        }
    }
}