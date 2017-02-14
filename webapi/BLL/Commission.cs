using System;
using System.Collections.Generic;
using System.Data;
using lks.webapi.DAL;
using lks.webapi.Model;
using lks.webapi.Utility;
namespace lks.webapi.BLL
{
    //Commission
    public partial class CommissionService
    {

        private readonly CommissionDAO dal = new CommissionDAO();
        public CommissionService()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string 工作号)
        {
            return dal.Exists(工作号);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Commission model)
        {
            dal.Add(model);

        }
        public void AddAndUpdate(IList<Commission> models, int columnCount)
        {
            dal.AddAndUpdate(models, columnCount);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Commission model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string 工作号)
        {

            return dal.Delete(工作号);
        }

        public bool BatchDelete(IEnumerable<Commission> commissions)
        {
            return dal.BatchDelete(commissions);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Commission GetModel(string 工作号)
        {

            return dal.GetModel(工作号);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Commission GetModelByCache(string 工作号)
        {

            string CacheKey = "CommissionModel-" + 工作号;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(工作号);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Commission)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Commission> QueryList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获取指定页的数据列表
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每一页显示的条数</param>
        /// <param name="wheres">查询条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isDesc">是否降序</param>
        /// <returns>数据列表</returns>
        public IEnumerable<Commission> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            return dal.QueryList(columns, index, size, wheres, orderField, out total, isDesc);
        }
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>单条数据项</returns>
        public Commission QuerySingle(string wheres)
        {
            return dal.QuerySingle(wheres);
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>总数目</returns>
        public int QueryCount(string wheres)
        {
            return dal.QueryCount(wheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Commission> DataTableToList(DataTable dt)
        {
            List<Commission> modelList = new List<Commission>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Commission model;
                foreach (DataRow row in dt.Rows)
                {
                    model = dal.GetModel(row);
                    if (model != null)
                        modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

    }
}