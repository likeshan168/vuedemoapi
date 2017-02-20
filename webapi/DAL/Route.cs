using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL  
{
	 	//Route
		public partial class RouteDAO
	{
   		     
		public bool Exists(int RouteId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Route");
			strSql.Append(" where ");
			                                       strSql.Append(" RouteId = @RouteId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@RouteId", SqlDbType.Int,4)
			};
			parameters[0].Value = RouteId;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Route model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Route(");			
            strSql.Append("Name,Op,Remark");
			strSql.Append(") values (");
            strSql.Append("@Name,@Op,@Remark");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Op", SqlDbType.NVarChar,10) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,400)             
              
            };
			            
            parameters[0].Value = model.Name;                        
            parameters[1].Value = model.Op;                        
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
		public bool Update(Route model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Route set ");
			                                                
            strSql.Append(" Name = @Name , ");                                    
            strSql.Append(" Op = @Op , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where RouteId=@RouteId ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@RouteId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Op", SqlDbType.NVarChar,10) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,400)             
              
            };
						            
            parameters[0].Value = model.RouteId;                        
            parameters[1].Value = model.Name;                        
            parameters[2].Value = model.Op;                        
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
		public bool Delete(int RouteId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Route ");
			strSql.Append(" where RouteId=@RouteId");
						SqlParameter[] parameters = {
					new SqlParameter("@RouteId", SqlDbType.Int,4)
			};
			parameters[0].Value = RouteId;


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
		public bool BatchDelete(IEnumerable<Route> commissions)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Route where RouteId in(");
			foreach (Route item in commissions)
			{
				strSql.Append($"'{item.RouteId}',");
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
		public bool DeleteList(string RouteIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Route ");
			strSql.Append(" where ID in ("+RouteIdlist + ")  ");
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
		public Route GetModel(int RouteId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select RouteId, Name, Op, Remark  ");			
			strSql.Append("  from Route ");
			strSql.Append(" where RouteId=@RouteId");
						SqlParameter[] parameters = {
					new SqlParameter("@RouteId", SqlDbType.Int,4)
			};
			parameters[0].Value = RouteId;

			
			return SqlHelper.MapEntity<Route>(SqlHelper.ExecuteReader(strSql.ToString(), parameters));
			
		}
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Route GetModel(DataRow row)
        {
            return SqlHelper.MapEntity<Route>(row);
        }
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Route ");
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
			strSql.Append(" FROM Route ");
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
			strSql.Append("select count(RouteId)");			
			strSql.Append("  from Route ");
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
        public IEnumerable<Route> QueryList(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            string sql = SqlHelper.GenerateQuerySql("Route", columns, index, size, wheres, orderField, isDesc);
            return SqlHelper.GetList<Route>(sql, null, out total);
        }
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>指定条件的数据</returns>
        public Route QuerySingle(string wheres)
        {
            string sql = SqlHelper.GenerateQuerySql("Route", null, 1, 1, wheres, "RouteId");
            return SqlHelper.QuerySingle<Route>(sql, null);
        }
   
	}
}

