using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    //Commission
    public partial class CommissionDAO
    {

        public bool Exists(string 工作单号)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Commission");
            strSql.Append(" where ");
            strSql.Append(" 工作单号 = @工作单号  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作单号", SqlDbType.NVarChar,50)            };
            parameters[0].Value = 工作单号;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Commission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Commission(");
            strSql.Append("工作单号,业务员,委托人简称,利润(RMB),应收折合(RMB),未收折合RMB,收款日期,超期日期,月数,超期回款资金成本,金额列,工作单日期,KB");
            strSql.Append(") values (");
            strSql.Append("@工作单号,@业务员,@委托人简称,@利润(RMB),@应收折合(RMB),@未收折合RMB,@收款日期,@超期日期,@月数,@超期回款资金成本,@金额列,@工作单日期,@KB");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@工作单号", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@业务员", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@委托人简称", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@利润(RMB)", SqlDbType.Decimal,9) ,
                        new SqlParameter("@应收折合(RMB)", SqlDbType.Decimal,9) ,
                        new SqlParameter("@未收折合RMB", SqlDbType.Decimal,9) ,
                        new SqlParameter("@收款日期", SqlDbType.Date,3) ,
                        new SqlParameter("@超期日期", SqlDbType.Date,3) ,
                        new SqlParameter("@月数", SqlDbType.Int,4) ,
                        new SqlParameter("@超期回款资金成本", SqlDbType.Decimal,9) ,
                        new SqlParameter("@金额列", SqlDbType.Decimal,9) ,
                        new SqlParameter("@工作单日期", SqlDbType.Date,3) ,
                        new SqlParameter("@KB", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.工作单号;
            parameters[1].Value = model.业务员;
            parameters[2].Value = model.委托人简称;
            parameters[3].Value = model.利润;
            parameters[4].Value = model.应收折合;
            parameters[5].Value = model.未收折合;
            parameters[6].Value = model.收款日期;
            parameters[7].Value = model.超期日期;
            parameters[8].Value = model.月数;
            parameters[9].Value = model.超期回款资金成本;
            parameters[10].Value = model.金额列;
            parameters[11].Value = model.工作单日期;
            parameters[12].Value = model.KB;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Commission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Commission set ");

            strSql.Append(" 工作单号 = @工作单号 , ");
            strSql.Append(" 业务员 = @业务员 , ");
            strSql.Append(" 委托人简称 = @委托人简称 , ");
            strSql.Append(" 利润(RMB) = @利润(RMB) , ");
            strSql.Append(" 应收折合(RMB) = @应收折合(RMB) , ");
            strSql.Append(" 未收折合RMB = @未收折合RMB , ");
            strSql.Append(" 收款日期 = @收款日期 , ");
            strSql.Append(" 超期日期 = @超期日期 , ");
            strSql.Append(" 月数 = @月数 , ");
            strSql.Append(" 超期回款资金成本 = @超期回款资金成本 , ");
            strSql.Append(" 金额列 = @金额列 , ");
            strSql.Append(" 工作单日期 = @工作单日期 , ");
            strSql.Append(" KB = @KB  ");
            strSql.Append(" where 工作单号=@工作单号  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@工作单号", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@业务员", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@委托人简称", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@利润(RMB)", SqlDbType.Decimal,9) ,
                        new SqlParameter("@应收折合(RMB)", SqlDbType.Decimal,9) ,
                        new SqlParameter("@未收折合RMB", SqlDbType.Decimal,9) ,
                        new SqlParameter("@收款日期", SqlDbType.Date,3) ,
                        new SqlParameter("@超期日期", SqlDbType.Date,3) ,
                        new SqlParameter("@月数", SqlDbType.Int,4) ,
                        new SqlParameter("@超期回款资金成本", SqlDbType.Decimal,9) ,
                        new SqlParameter("@金额列", SqlDbType.Decimal,9) ,
                        new SqlParameter("@工作单日期", SqlDbType.Date,3) ,
                        new SqlParameter("@KB", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.工作单号;
            parameters[1].Value = model.业务员;
            parameters[2].Value = model.委托人简称;
            parameters[3].Value = model.利润;
            parameters[4].Value = model.应收折合;
            parameters[5].Value = model.未收折合;
            parameters[6].Value = model.收款日期;
            parameters[7].Value = model.超期日期;
            parameters[8].Value = model.月数;
            parameters[9].Value = model.超期回款资金成本;
            parameters[10].Value = model.金额列;
            parameters[11].Value = model.工作单日期;
            parameters[12].Value = model.KB;
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
        public bool Delete(string 工作单号)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Commission ");
            strSql.Append(" where 工作单号=@工作单号 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作单号", SqlDbType.NVarChar,50)            };
            parameters[0].Value = 工作单号;


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
        public Commission GetModel(string 工作单号)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 工作单号, 业务员, 委托人简称, 利润(RMB), 应收折合(RMB), 未收折合RMB, 收款日期, 超期日期, 月数, 超期回款资金成本, 金额列, 工作单日期, KB  ");
            strSql.Append("  from Commission ");
            strSql.Append(" where 工作单号=@工作单号 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作单号", SqlDbType.NVarChar,50)            };
            parameters[0].Value = 工作单号;


            return SqlHelper.MapEntity<Commission>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Commission GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<Commission>(row);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Commission ");
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
            strSql.Append(" FROM Commission ");
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
        public int QueryCount(object wheres)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(工作单号)");
            strSql.Append("  from Commission ");
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
        public IEnumerable<Commission> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("Commission", null, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<Commission>(sql, null);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public Commission QuerySingle(object wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("Commission", null, 1, 1, wheres, "工作单号");
            return SqlHelper.QuerySingle<Commission>(sql, null);
        }

    }
}

