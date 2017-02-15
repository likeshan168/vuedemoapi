using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL  
{
	 	//Role
		public partial class RoleDAO
	{
   		     
		public bool Exists(int RoleId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Role");
			strSql.Append(" where ");
			                                       strSql.Append(" RoleId = @RoleId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RoleId;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Role(");			
            strSql.Append("RoleName,RouteId,Remark");
			strSql.Append(") values (");
            strSql.Append("@RoleName,@RouteId,@Remark");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@RoleName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@RouteId", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,100)             
              
            };
			            
            parameters[0].Value = model.RoleName;                        
            parameters[1].Value = model.RouteId;                        
            parameters[2].Value = model.Remark;                        
			   
			object obj = SqlHelper.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Role set ");
			                                                
            strSql.Append(" RoleName = @RoleName , ");                                    
            strSql.Append(" RouteId = @RouteId , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where RoleId=@RoleId ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@RoleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@RouteId", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,100)             
              
            };
						            
            parameters[0].Value = model.RoleId;                        
            parameters[1].Value = model.RoleName;                        
            parameters[2].Value = model.RouteId;                        
            parameters[3].Value = model.Remark;                        
            int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int RoleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Role ");
			strSql.Append(" where RoleId=@RoleId");
						SqlParameter[] parameters = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RoleId;


			int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public bool BatchDelete(IEnumerable<Role> commissions)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Role where RoleId in(");
			foreach (Role item in commissions)
			{
				strSql.Append($"'{item.RoleId}',");
			}
			strSql.Remove(strSql.Length - 1, 0);
            strSql.Append(')');

			int rows=SqlHelper.ExecuteSql(strSql.ToString());
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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string RoleIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Role ");
			strSql.Append(" where ID in ("+RoleIdlist + ")  ");
			int rows=SqlHelper.ExecuteSql(strSql.ToString());
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
		public Role GetModel(int RoleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select RoleId, RoleName, RouteId, Remark  ");			
			strSql.Append("  from Role ");
			strSql.Append(" where RoleId=@RoleId");
						SqlParameter[] parameters = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RoleId;

			
			return SqlHelper.MapEntity<Role>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));
			
		}
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Role GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<Role>(row);
        }
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Role ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SqlHelper.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM Role ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
        	StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(RoleId)");			
			strSql.Append("  from Role ");
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
        public IEnumerable<Role> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("Role", columns, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<Role>(sql, null, out total);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public Role QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("Role", null, 1, 1, wheres, "RoleId");
            return SqlHelper.QuerySingle<Role>(sql, null);
        }
   
	}
}

