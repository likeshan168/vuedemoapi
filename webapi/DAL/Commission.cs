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

        public bool Exists(string 工作号)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Commission");
            strSql.Append(" where ");
            strSql.Append(" 工作号 = @工作号  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作号", SqlDbType.NVarChar,50)         };
            parameters[0].Value = 工作号;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Commission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Commission(");
            strSql.Append("工作号,业务员,委托人简称,利润,应收折合,未收折合,收款日期,超期日期,月数,超期回款资金成本,金额,工作单日期,KB");
            strSql.Append(") values (");
            strSql.Append("@工作号,@业务员,@委托人简称,@利润,@应收折合,@未收折合,@收款日期,@超期日期,@月数,@超期回款资金成本,@金额,@工作单日期,@KB");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@工作号", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@业务员", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@委托人简称", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@利润", SqlDbType.Decimal,9) ,
                        new SqlParameter("@应收折合", SqlDbType.Decimal,9) ,
                        new SqlParameter("@未收折合", SqlDbType.Decimal,9) ,
                        new SqlParameter("@收款日期", SqlDbType.Date,3) ,
                        new SqlParameter("@超期日期", SqlDbType.Date,3) ,
                        new SqlParameter("@月数", SqlDbType.Int,4) ,
                        new SqlParameter("@超期回款资金成本", SqlDbType.Decimal,9) ,
                        new SqlParameter("@金额", SqlDbType.Decimal,9) ,
                        new SqlParameter("@工作单日期", SqlDbType.Date,3) ,
                        new SqlParameter("@KB", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.工作号;
            parameters[1].Value = model.业务员;
            parameters[2].Value = model.委托人简称;
            parameters[3].Value = model.利润;
            parameters[4].Value = model.应收折合;
            parameters[5].Value = model.未收折合;
            parameters[6].Value = model.收款日期;
            parameters[7].Value = model.超期日期;
            parameters[8].Value = model.月数;
            parameters[9].Value = model.超期回款资金成本;
            parameters[10].Value = model.金额;
            parameters[11].Value = model.工作单日期;
            parameters[12].Value = model.KB;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        public void AddAndUpdate(IList<Commission> models, int columnCount)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (Commission item in models)
            {
                strSql.AppendFormat($"SELECT 工作号 FROM dbo.Commission WHERE 工作号='{item.工作号}' if @@ROWCOUNT>0 ");
                if (columnCount == 5)
                {
                    strSql.AppendFormat($"update Commission set 委托人简称 = '{item.委托人简称}', 应收折合={item.应收折合},利润={item.利润},未收折合={item.未收折合} where 工作号='{item.工作号}' ");
                    strSql.AppendFormat($"else insert Commission(工作号,委托人简称,应收折合,利润,未收折合) values('{item.工作号}','{item.委托人简称}',{item.应收折合},{item.利润},{item.未收折合});");
                }
                else if (columnCount == 4)
                {
                    strSql.AppendFormat($"update Commission set 业务员 = '{item.业务员}', 工作单日期='{item.工作单日期}',收款日期='{item.收款日期}' where 工作号='{item.工作号}' ");
                    strSql.AppendFormat($"else insert Commission(工作号,业务员,工作单日期,收款日期) values('{item.工作号}','{item.业务员}','{item.工作单日期}','{item.收款日期}');");
                }
                else if (columnCount == 2)
                {
                    strSql.AppendFormat($"update Commission set KB = {item.KB} where 工作号='{item.工作号}' ");
                    strSql.AppendFormat($"else insert Commission(工作号,KB) values('{item.工作号}',{item.KB});");
                }
                else if (columnCount == 0)
                {
                    strSql.AppendFormat($"update Commission set 业务员 = '{item.业务员}',委托人简称 = '{item.委托人简称}',利润={item.利润},应收折合={item.应收折合},未收折合={item.未收折合}, 收款日期='{item.收款日期}',超期日期='{item.超期日期}',月数={item.月数},超期回款资金成本={item.超期回款资金成本},金额 = {item.金额},工作单日期 = '{item.工作单日期}',KB = {item.KB} where 工作号='{item.工作号}' ");
                    strSql.AppendFormat($"else insert Commission(工作号 ,业务员 ,委托人简称 ,利润 ,应收折合 ,未收折合 ,收款日期 ,超期日期 ,月数 ,超期回款资金成本 ,金额 ,工作单日期 ,KB) values('{item.工作号}','{item.业务员}','{item.委托人简称}',{item.利润},{item.应收折合},{item.未收折合},'{item.收款日期}','{item.超期日期}',{item.月数},{item.超期回款资金成本},{item.金额},'{item.工作单日期}',{item.KB});");
                }
            }

            SqlHelper.ExecuteSql(strSql.ToString());
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Commission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Commission set ");

            strSql.Append(" 工作号 = @工作号 , ");
            strSql.Append(" 业务员 = @业务员 , ");
            strSql.Append(" 委托人简称 = @委托人简称 , ");
            strSql.Append(" 利润 = @利润 , ");
            strSql.Append(" 应收折合 = @应收折合 , ");
            strSql.Append(" 未收折合 = @未收折合 , ");
            strSql.Append(" 收款日期 = @收款日期 , ");
            strSql.Append(" 超期日期 = @超期日期 , ");
            strSql.Append(" 月数 = @月数 , ");
            strSql.Append(" 超期回款资金成本 = @超期回款资金成本 , ");
            strSql.Append(" 金额 = @金额 , ");
            strSql.Append(" 工作单日期 = @工作单日期 , ");
            strSql.Append(" KB = @KB  ");
            strSql.Append(" where 工作号=@工作号  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@工作号", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@业务员", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@委托人简称", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@利润", SqlDbType.Decimal,9) ,
                        new SqlParameter("@应收折合", SqlDbType.Decimal,9) ,
                        new SqlParameter("@未收折合", SqlDbType.Decimal,9) ,
                        new SqlParameter("@收款日期", SqlDbType.Date,3) ,
                        new SqlParameter("@超期日期", SqlDbType.Date,3) ,
                        new SqlParameter("@月数", SqlDbType.Int,4) ,
                        new SqlParameter("@超期回款资金成本", SqlDbType.Decimal,9) ,
                        new SqlParameter("@金额", SqlDbType.Decimal,9) ,
                        new SqlParameter("@工作单日期", SqlDbType.Date,3) ,
                        new SqlParameter("@KB", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.工作号;
            parameters[1].Value = model.业务员;
            parameters[2].Value = model.委托人简称;
            parameters[3].Value = model.利润;
            parameters[4].Value = model.应收折合;
            parameters[5].Value = model.未收折合;
            parameters[6].Value = model.收款日期;
            parameters[7].Value = model.超期日期;
            parameters[8].Value = model.月数;
            parameters[9].Value = model.超期回款资金成本;
            parameters[10].Value = model.金额;
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
        public bool Delete(string 工作号)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Commission ");
            strSql.Append(" where 工作号=@工作号 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作号", SqlDbType.NVarChar,50)         };
            parameters[0].Value = 工作号;


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

        public bool BatchDelete(IEnumerable<Commission> commissions)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Commission where 工作号 in(");
            foreach (Commission item in commissions)
            {
                strSql.Append($"'{item.工作号}',");
            }
            strSql.Remove(strSql.Length - 1, 1);
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
        public Commission GetModel(string 工作号)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 工作号, 业务员, 委托人简称, 利润, 应收折合, 未收折合, 收款日期, 超期日期, 月数, 超期回款资金成本, 金额, 工作单日期, KB  ");
            strSql.Append("  from Commission ");
            strSql.Append(" where 工作号=@工作号 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工作号", SqlDbType.NVarChar,50)         };
            parameters[0].Value = 工作号;


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
        public int QueryCount(string wheres)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(工作号)");
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
        public IEnumerable<Commission> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("Commission", columns, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<Commission>(sql, null, out total);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public Commission QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("Commission", null, 1, 1, wheres, "工作号");
            return SqlHelper.QuerySingle<Commission>(sql, null);
        }

    }
}

