using System;
using System.Collections.Generic;
using System.Data;
using lks.webapi.DAL;
using lks.webapi.Model;
using lks.webapi.Utility;
namespace lks.webapi.BLL
{
    //Role
    public partial class RoleService
    {

        private readonly RoleDAO dal = new RoleDAO();
        public RoleService()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RoleId)
        {
            return dal.Exists(RoleId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Role model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Role model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RoleId)
        {
            return dal.Delete(RoleId);
        }
        /// <summary>
        ///  批量删除
        /// </summary>
        public bool BatchDelete(IEnumerable<Role> commissions)
        {
            return dal.BatchDelete(commissions);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string RoleIdlist)
        {
            return dal.DeleteList(RoleIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Role GetModel(int RoleId)
        {
            return dal.GetModel(RoleId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Role GetModelByCache(int RoleId)
        {
            string CacheKey = "RoleModel-" + RoleId;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RoleId);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Role)objModel;
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
        public List<Role> QueryList(string strWhere)
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
        public IEnumerable<Role> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            return dal.QueryList(columns, index, size, wheres, orderField, out total, isDesc);
        }
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>单条数据项</returns>
        public Role QuerySingle(string wheres)
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
        public List<Role> DataTableToList(DataTable dt)
        {
            List<Role> modelList = new List<Role>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Role model;
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