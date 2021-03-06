﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    //UserInfo
    public partial class UserInfoDAO
    {

        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
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
        public void Add(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("Id,Name,Password,Email,PhoneNumber,RoleId,Remark");
            strSql.Append(") values (");
            strSql.Append("@Id,@Name,@Password,@Email,@PhoneNumber,@RoleId,@Remark");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Email", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,1000)

            };

            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.PhoneNumber;
            parameters[5].Value = model.RoleId;
            parameters[6].Value = model.Remark;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");

            strSql.Append(" Id = @Id , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Password = @Password , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" PhoneNumber = @PhoneNumber , ");
            strSql.Append(" RoleId = @RoleId , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where Id=@Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Email", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,1000)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.PhoneNumber;
            parameters[5].Value = model.RoleId;
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
            strSql.Append("delete from UserInfo ");
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
        ///  批量删除
        /// </summary>
        public bool BatchDelete(IEnumerable<UserInfo> commissions)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo where Id in(");
            foreach (UserInfo item in commissions)
            {
                strSql.Append($"'{item.Id}',");
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
        public UserInfo GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Name, Password, Email, PhoneNumber, RoleId, Remark  ");
            strSql.Append("  from UserInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = Id;


            return SqlHelper.MapEntity<UserInfo>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserInfo GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<UserInfo>(row);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM UserInfo ");
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
            strSql.Append(" FROM UserInfo ");
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
            strSql.Append("  from UserInfo ");
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
        public IEnumerable<UserInfo> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("UserInfo", columns, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<UserInfo>(sql, null, out total);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public UserInfo QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("UserInfo", null, 1, 1, wheres, "Id");
            return SqlHelper.QuerySingle<UserInfo>(sql, null);
        }

    }
}

