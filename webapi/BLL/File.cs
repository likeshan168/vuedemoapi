using System;
using System.Collections.Generic;
using System.Data;
using lks.webapi.DAL;
using lks.webapi.Model;
using lks.webapi.Utility;
namespace lks.webapi.BLL
{
    //File
    public partial class FileService
    {

        private readonly FileDAO dal = new FileDAO();
        public FileService()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public dynamic Add(File model)
        {
            try
            {
                dal.Add(model);
                return new FileResponse()
                {
                    Msg = "上传成功",
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new FileResponse()
                {
                    Msg = ex.Message,
                    Code = 500
                };
            }

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(File model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public File GetModel(Guid Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public File GetModelByCache(Guid Id)
        {

            string CacheKey = "FileModel-" + Id;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (File)objModel;
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
        public List<File> QueryList(string strWhere)
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
        public IEnumerable<File> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            return dal.QueryList(index, size, wheres, orderField, isDesc);
        }
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>单条数据项</returns>
        public File QuerySingle(object wheres)
        {
            return dal.QuerySingle(wheres);
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>总数目</returns>
        public int QueryCount(object wheres)
        {
            return dal.QueryCount(wheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<File> DataTableToList(DataTable dt)
        {
            List<File> modelList = new List<File>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                File model;
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