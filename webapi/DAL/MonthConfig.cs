using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    //MonthConfig
    public partial class MonthConfigDAO
    {

        public bool Exists(string 委托人简称)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonthConfig");
            strSql.Append(" where ");
            strSql.Append(" 委托人简称 = @委托人简称  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@委托人简称", SqlDbType.VarChar,100)
            };
            parameters[0].Value = 委托人简称;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MonthConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MonthConfig(");
            strSql.Append("委托人简称,月数");
            strSql.Append(") values (");
            strSql.Append("@委托人简称,@月数");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@委托人简称", SqlDbType.VarChar,100) ,
                        new SqlParameter("@月数", SqlDbType.Int,4)

            };

            parameters[0].Value = model.委托人简称;
            parameters[1].Value = model.月数;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MonthConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MonthConfig set ");

            strSql.Append(" 委托人简称 = @委托人简称 , ");
            strSql.Append(" 月数 = @月数  ");
            strSql.Append(" where 委托人简称=@委托人简称  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@委托人简称", SqlDbType.VarChar,100) ,
                        new SqlParameter("@月数", SqlDbType.Int,4)

            };

            parameters[0].Value = model.委托人简称;
            parameters[1].Value = model.月数;
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
        public bool Delete(string 委托人简称)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MonthConfig ");
            strSql.Append(" where 委托人简称=@委托人简称 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@委托人简称", SqlDbType.VarChar,100)           };
            parameters[0].Value = 委托人简称;


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
        ///  批量删除
        /// </summary>
        public bool BatchDelete(IEnumerable<MonthConfig> commissions)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MonthConfig where 委托人简称 in(");
            foreach (MonthConfig item in commissions)
            {
                strSql.Append($"'{item.委托人简称}',");
            }
            strSql.Remove(strSql.Length - 1, 0);
            strSql.Append(')');

            int rows = SqlHelper.ExecuteSql(strSql.ToString());
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
        public MonthConfig GetModel(string 委托人简称)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 委托人简称, 月数  ");
            strSql.Append("  from MonthConfig ");
            strSql.Append(" where 委托人简称=@委托人简称 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@委托人简称", SqlDbType.VarChar,100)           };
            parameters[0].Value = 委托人简称;


            return SqlHelper.MapEntity<MonthConfig>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MonthConfig GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<MonthConfig>(row);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM MonthConfig ");
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
            strSql.Append(" FROM MonthConfig ");
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
            strSql.Append("select count(委托人简称)");
            strSql.Append("  from MonthConfig ");
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
        public IEnumerable<MonthConfig> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("MonthConfig", columns, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<MonthConfig>(sql, null, out total);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public MonthConfig QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("MonthConfig", null, 1, 1, wheres, "委托人简称");
            return SqlHelper.QuerySingle<MonthConfig>(sql, null);
        }

    }
}

