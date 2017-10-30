using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace lks.webapi.Utility
{
    public class ExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;
        private string extension;
        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }
        public ExcelHelper(FileStream fs, string extension)
        {
            this.fs = fs;
            disposed = false;
            this.extension = extension;
        }
        /// <summary>
        /// 讲DataTable中的数据导出excel
        /// </summary>
        /// <param name="dt">需要导出的数据</param>
        /// <param name="excelDir">导出的路径</param>
        /// <param name="excelName">导出的excel文件名</param>
        /// <param name="sheetName">导出的sheet表名</param>
        /// <returns></returns>
        public int OutputExcel(DataTable dt, string excelDir, string excelName, string sheetName = null)
        {
            if (!Directory.Exists(excelDir))
            {
                throw new DirectoryNotFoundException($"{excelDir}文件路径不存在");
            }

            if (!excelDir.EndsWith("\\"))
            {
                excelDir = excelDir + "\\";
            }

            if (!excelName.EndsWith(".xls") && !excelName.EndsWith(".xlsx"))
            {
                excelName = excelName + ".xls";
            }
            string filePath = excelDir + excelName;

            string ext = Path.GetExtension(excelName);
            if (ext.Equals(".xls", StringComparison.OrdinalIgnoreCase))
            {
                //output the excel97-2003
                workbook = new HSSFWorkbook();
            }

            if (ext.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                //output the excel2007+
                workbook = new XSSFWorkbook();
            }

            ISheet sheet = workbook.CreateSheet(sheetName == null ? DateTime.Now.ToString("yyyy-MM-dd") : sheetName);
            IRow row = null;
            ICell cell = null;

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;

            #region write the column name

            row = sheet.CreateRow(0);
            for (int i = 0; i < columnCount; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            #endregion

            if (rowCount > 0)
            {
                #region write the data

                for (int i = 0; i < rowCount; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < columnCount; j++)
                    {
                        cell = row.CreateCell(j);

                        object value = dt.Rows[i][j];

                        #region set the cell value based on the type

                        switch (value.GetType().ToString())
                        {
                            case "string":
                            case "String":
                                cell.SetCellValue(value.ToString());
                                break;
                            case "decimal":
                            case "Decimal":
                            case "double":
                            case "Double":
                                cell.SetCellValue((double)value);
                                break;
                            case "int":
                            case "Int":
                                cell.SetCellValue((int)value);
                                break;
                            case "Guid":
                                cell.SetCellValue(((Guid)value).ToString());
                                break;
                            case "DateTime":
                                cell.SetCellValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
                                break;
                            default:
                                cell.SetCellValue(value.ToString());
                                break;
                        }
                        #endregion
                    }
                }

                #endregion 
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                ms.WriteTo(fs);
            }

            workbook = null;
            sheet = null;

            return rowCount;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten = true)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <param name="skipPreRowCount">跳过的前面几行</param>
        /// <param name="skipLastRowCount">忽略后面几行</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn = true, int skipPreRowCount = 0, int skipLastRowCount = 0)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (extension.Equals(".xlsx")) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (extension.Equals(".xls")) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                else
                    return null;

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(skipPreRowCount - 1);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = firstRow.RowNum + 1;
                    }
                    else
                    {
                        startRow = firstRow.RowNum;
                    }

                    //最后一列的标号

                    int rowCount = sheet.LastRowNum - skipLastRowCount;

                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 非通用版本
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            //List<string> cols = new List<string>();
            Dictionary<string, string> cols = new Dictionary<string, string>();
            List<int> colIndexes = new List<int>();

            int startRow = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (extension.Equals(".xlsx")) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (extension.Equals(".xls")) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                else
                    return null;

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    if (cellCount == 25)
                    {
                        //去除前面7行和后面5行
                        firstRow = sheet.GetRow(7);
                        rowCount = rowCount - 5;
                    }
                    else if (cellCount == 28 || cellCount == 27)
                    {
                        //去除前面一行
                        firstRow = sheet.GetRow(1);
                        ICell cell = firstRow.GetCell(0);

                        if (string.IsNullOrWhiteSpace(cell.StringCellValue))
                        {
                            firstRow = sheet.GetRow(7);
                            rowCount = rowCount - 5;
                            //范例1excel表中包含的列
                            cols.Add("工作号", "string");
                            cols.Add("委托人简称", "string");
                            cols.Add("应收折合", "decimal");
                            cols.Add("利润", "decimal");
                            cols.Add("未收折合", "decimal");
                            //cols.AddRange(new List<string>
                            //{
                            //    "工作号",
                            //    "委托人简称",
                            //    "应收折合",
                            //    "利润",
                            //    "未收折合"
                            //});
                        }
                        else
                        {
                            //范例2excel表中包含的列
                            cols.Add("工作号", "string");
                            cols.Add("业务员", "string");
                            cols.Add("工作单日期", "date");
                            cols.Add("收款日期", "date");
                            //cols.AddRange(new List<string>
                            //    {
                            //        "工作号",
                            //        "业务员",
                            //        "工作单日期",
                            //        "收款日期"
                            //    });
                        }
                    }
                    else if (cellCount == 30)
                    {
                        // 范例2excel表中包含的列 这个又是导入一种不同列数的表，我都疯了
                        cols.Add("工作号", "string");
                        cols.Add("业务员", "string");
                        cols.Add("工作单日期", "date");
                        cols.Add("收款日期", "date");
                    }
                    else if (cellCount == 31)
                    {
                        //去除前面7行和后面2行
                        firstRow = sheet.GetRow(7);
                        rowCount = rowCount - 2;
                        //范例3excel表中包含的列
                        cols.Add("工作号", "string");
                        cols.Add("kb", "decimal");
                        //cols.AddRange(new List<string>
                        //    {
                        //        "工作号",
                        //        "KB"
                        //    });
                    }
                    string newColumnName = string.Empty;
                    Type type;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null && IsColumnMatched(cols, cellValue, out newColumnName, out type))
                                {
                                    colIndexes.Add(i);
                                    DataColumn column = new DataColumn(newColumnName, type);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        //针对范例3excel表中的列，其中导出的excel列有点问题
                        if (cellCount == 31)
                        {
                            DataColumn column = new DataColumn("kb", typeof(decimal));
                            data.Columns.Add(column);
                            colIndexes.Add(19);
                            startRow = firstRow.RowNum + 2;

                        }
                        else
                            startRow = firstRow.RowNum + 1;
                    }
                    else
                    {
                        startRow = firstRow.RowNum;
                    }



                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        //for (int j = row.FirstCellNum; j < cellCount; ++j)
                        //{
                        //    if (row.GetCell(j) != null ) //同理，没有数据的单元格都默认是null
                        //        dataRow[j] = row.GetCell(j).ToString();
                        //}
                        int j = 0;
                        foreach (int index in colIndexes)
                        {
                            if (row.GetCell(index) != null && !string.IsNullOrWhiteSpace(row.GetCell(index).ToString())) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j++] = row.GetCell(index).ToString();
                            }
                        }

                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }

        private bool IsColumnMatched(Dictionary<string, string> columns, string columnName, out string newColumnName, out Type type)
        {
            newColumnName = string.Empty;
            type = typeof(string);
            foreach (KeyValuePair<string, string> column in columns)
            {
                if (columnName.StartsWith(column.Key))
                {
                    newColumnName = column.Key;
                    switch (column.Value)
                    {
                        case "int":
                            type = typeof(int);
                            break;
                        case "string":
                            type = typeof(string);
                            break;
                        case "date":
                            type = typeof(DateTime);
                            break;
                        case "decimal":
                            type = typeof(decimal);
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                else
                    continue;
            }

            return false;
        }
    }
}