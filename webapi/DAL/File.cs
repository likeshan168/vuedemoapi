using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    //File
    public partial class FileDAO
    {

        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from File");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = Id;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(File model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into File(");
            strSql.Append("Id,FileName,FileSize,UploadDate,UploadBy,ImportToDb,Remark");
            strSql.Append(") values (");
            strSql.Append("@Id,@FileName,@FileSize,@UploadDate,@UploadBy,@ImportToDb,@Remark");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@FileName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@FileSize", SqlDbType.Int,4) ,
                        new SqlParameter("@UploadDate", SqlDbType.DateTime) ,
                        new SqlParameter("@UploadBy", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ImportToDb", SqlDbType.Bit,1) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,1000)

            };

            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.FileSize;
            parameters[3].Value = model.UploadDate;
            parameters[4].Value = model.UploadBy;
            parameters[5].Value = model.ImportToDb;
            parameters[6].Value = model.Remark;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(File model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update File set ");

            strSql.Append(" Id = @Id , ");
            strSql.Append(" FileName = @FileName , ");
            strSql.Append(" FileSize = @FileSize , ");
            strSql.Append(" UploadDate = @UploadDate , ");
            strSql.Append(" UploadBy = @UploadBy , ");
            strSql.Append(" ImportToDb = @ImportToDb , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where Id=@Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@FileName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@FileSize", SqlDbType.Int,4) ,
                        new SqlParameter("@UploadDate", SqlDbType.DateTime) ,
                        new SqlParameter("@UploadBy", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ImportToDb", SqlDbType.Bit,1) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,1000)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.FileSize;
            parameters[3].Value = model.UploadDate;
            parameters[4].Value = model.UploadBy;
            parameters[5].Value = model.ImportToDb;
            parameters[6].Value = model.Remark;
            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from File ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = Id;


            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public File GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, FileName, FileSize, UploadDate, UploadBy, ImportToDb, Remark  ");
            strSql.Append("  from File ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = Id;


            return SqlHelper.MapEntity<File>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public File GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<File>(row);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM File ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM File ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>返回条数</returns>
        public int QueryCount(string wheres)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(Id)");
            strSql.Append("  from File ");
            string where = SqlHelper.GetWhere(wheres);
            strSql.Append(where);
            return SqlHelper.ExecuteScalar<int>(strSql.ToString());
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
        public IEnumerable<File> QueryList(int index, int size, string wheres, string orderField, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("File", null, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<File>(sql, null);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public File QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("File", null, 1, 1, wheres, "Id");
            return SqlHelper.QuerySingle<File>(sql, null);
        }

    }
}

