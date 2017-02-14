using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace lks.webapi.Extension
{
    public static class MyExt
    {
        #region DataTable 转换为Json 字符串
        /// <summary>
        /// DataTable 对象 转换为Json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = int.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {

                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                Type t;
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    t = dataColumn.DataType;
                    if (t == typeof(int))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<int>(dataColumn.ColumnName));
                    }
                    else if (t == typeof(decimal))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<decimal>(dataColumn.ColumnName));
                    }
                    else if (t == typeof(DateTime))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<DateTime>(dataColumn.ColumnName));
                    }
                    else if (t == typeof(string))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<string>(dataColumn.ColumnName));
                    }
                    else if (t == typeof(long))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<long>(dataColumn.ColumnName));
                    }
                    else if (t == typeof(double))
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow.Field<double>(dataColumn.ColumnName));
                    }
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }

        public static string ToJson2(this DataTable dt)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow dataRow in dt.Rows)
            {
                sb.Append("{");

                Type t;
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    t = dataColumn.DataType;
                    if (t == typeof(int))
                    {
                        sb.Append($"\"{dataColumn.ColumnName}\":{(dataRow.IsNull(dataColumn.ColumnName) ? 0 : dataRow.Field<int>(dataColumn.ColumnName))},");
                    }
                    else if (t == typeof(decimal))
                    {
                        sb.Append($"\"{dataColumn.ColumnName}\":{(dataRow.IsNull(dataColumn.ColumnName) ? 0 : dataRow.Field<decimal>(dataColumn.ColumnName))},");
                    }
                    else if (t == typeof(DateTime))
                    {
                        if (dataRow.IsNull(dataColumn.ColumnName))
                        {
                            sb.Append($"\"{dataColumn.ColumnName}\":{-2174803200000},");
                        }
                        else
                        {
                            sb.Append($"\"{dataColumn.ColumnName}\":{GetTimeLikeJS(dataRow.Field<DateTime>(dataColumn.ColumnName))},");
                        }
                    }
                    else if (t == typeof(string))
                    {
                        sb.Append($"\"{dataColumn.ColumnName}\":\"{(dataRow.IsNull(dataColumn.ColumnName) ? string.Empty : dataRow.Field<string>(dataColumn.ColumnName))}\",");
                    }
                    else if (t == typeof(long))
                    {
                        sb.Append($"\"{dataColumn.ColumnName}\":{(dataRow.IsNull(dataColumn.ColumnName) ? 0 : dataRow.Field<long>(dataColumn.ColumnName))},");
                    }
                    else if (t == typeof(double))
                    {
                        sb.Append($"\"{dataColumn.ColumnName}\":{(dataRow.IsNull(dataColumn.ColumnName) ? 0 : dataRow.Field<double>(dataColumn.ColumnName))},");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
            }
            if (sb.Length > 0)
            {
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            else
            {
                sb.Append("]");
            }

            return sb.ToString();

        }
        #endregion

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion

        #region 转换为string字符串类型
        /// <summary>
        ///  转换为string字符串类型
        /// </summary>
        /// <param name="s">获取需要转换的值</param>
        /// <param name="format">需要格式化的位数</param>
        /// <returns>返回一个新的字符串</returns>
        public static string ToStr(this object s, string format = "")
        {
            string result = "";
            try
            {
                if (format == "")
                {
                    result = s.ToString();
                }
                else
                {
                    result = string.Format("{0:" + format + "}", s);
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion

        private static long lLeft = 621355968000000000;

        //将数字变成时间
        public static string GetTimeFromInt(long ltime)
        {
            long Eticks = (long)(ltime * 10000000) + lLeft;
            DateTime dt = new DateTime(Eticks).ToLocalTime();
            return dt.ToString();
        }
        //将时间变成数字
        public static long GetIntFromTime(DateTime dt)
        {
            DateTime dt1 = dt.ToUniversalTime();
            long Sticks = (dt1.Ticks - lLeft) / 10000000;
            return Sticks;
        }

        public static long GetTimeLikeJS(DateTime dt)
        {
            long lLeft = 621355968000000000;
            long Sticks = (dt.Ticks - lLeft) / 10000;
            return Sticks;
        }
    }
}