using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ee.ls.Utility.Npoi
{
    public class NpoiHelper
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public static void ToExcel<T>(List<T> dataSource, string path, List<string> attries, List<string> headers)
        {
            if (dataSource == null || !dataSource.Any())
            {
                throw new Exception("dataSource is null.");
            }

            HSSFWorkbook wb = new HSSFWorkbook();//创建一个工作薄

            HSSFSheet sheet = wb.CreateSheet() as HSSFSheet;//在工作薄中创建一个工作表
            HSSFRow rw = sheet.CreateRow(0) as HSSFRow;
            var patriarch = sheet.CreateDrawingPatriarch();
            for (int i = 0; i < headers.Count; i++) //循环一个表头来创建第一行的表头
            {
                rw.CreateCell(i).SetCellValue(headers[i]);
            }
            Type t = typeof(T); //获取得泛型集合中的实体， 返回T的类型
            PropertyInfo[] properties = t.GetProperties(); //返回当前获得实体后 实体类型中的所有公共属性
            for (int i = 0; i < dataSource.Count; i++)//循环实体泛型集合
            {
                rw = sheet.CreateRow(i + 1) as HSSFRow;//创建一个新行，把传入集合中的每条数据创建一行
                foreach (PropertyInfo property in properties)//循环得到的所有属性（想要把里面指定的属性值导出到Excel文件中）
                {
                    for (int j = 0; j < attries.Count; j++)//循环需要导出属性值 的 属性名
                    {
                        string attry = attries[j];//获得一个需要导入的属性名；
                        if (string.Compare(property.Name.ToUpper(), attry.ToUpper()) == 0)//如果需要导出的属性名和当前循环实体的属性名一样，
                        {
                            object objValue = property.GetValue(dataSource[i], null);//获取当前循环的实体属性在当前实体对象（arr[i]）的值
                            if (objValue != null && (objValue.GetType().Name == "Bitmap" || objValue.GetType().Name == "Image"))
                            {
                                //- 插入图片到 Excel，并返回一个图片的标识   
                                var handle = (objValue as Bitmap).GetHbitmap();
                                using (Bitmap newBmp = Image.FromHbitmap(handle))
                                {
                                    MemoryStream ms = new MemoryStream();
                                    newBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byte[] bytes = ms.GetBuffer();
                                    ms.Close();
                                    var pictureIdx = wb.AddPicture(bytes, PictureType.JPEG);
                                    //- 创建图片的位置                      
                                    var anchor = new HSSFClientAnchor(
                                        0, 0,//- 上左 到 上右 的位置，是基于下面的行列位置   
                                        0, 0, //- 下左 到 下右 的位置，是基于下面的行列位置      
                                        j, i + 1,
                                        j + 1, i + 2);
                                    //- 图片输出的位置这么计算的： 
                                    //- 假设我们要将图片放置于第 5(E) 列的第 2 行  
                                    //- 对应索引为是 4 : 1 （默认位置）         
                                    //- 放置的位置就等于（默认位置）到（默认位置各自加上一行、一列）    
                                    var pic = patriarch.CreatePicture(anchor, pictureIdx);//- 使用绘画器绘画图片

                                    sheet.SetColumnWidth(j, 100 * 36);
                                    rw.HeightInPoints = 100 * 0.75f;
                                    bytes = null;

                                }
                                DeleteObject(handle);
                            }
                            else
                            {
                                rw.CreateCell(j).SetCellValue((objValue == null) ? string.Empty : objValue.ToString());//创建单元格并进行赋值
                                sheet.AutoSizeColumn(j);
                            }
                        }
                    }
                }
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wb.Write(fs);
            }

        }

        /// <summary>  
        /// 根据datatable获得列名  
        /// </summary>  
        /// <param name="dt">表对象</param>  
        /// <returns>返回结果的数据列数组</returns>  
        public static List<string> GetColumnsByDataTable(DataTable dt)
        {
            List<string> strColumns = new List<string>();

            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count; ;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns.Add(dt.Columns[i].ColumnName);
                }
            }
            return strColumns;
        }

        private string fileName = null; //文件名  
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public NpoiHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        public NpoiHelper()
        {
            disposed = false;
        }

        /// <summary>  
        /// 将DataTable数据导入到excel中  
        /// </summary>  
        /// <param name="data">要导入的数据</param>  
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>  
        /// <param name="sheetName">要导入的excel的sheet的名称</param>  
        /// <returns>导入数据行数(包含列名那一行)</returns>  
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
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
        /// <returns>返回的DataTable</returns>  
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                    workbook = new HSSFWorkbook(fs);

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
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号  
                    int rowCount = sheet.LastRowNum;
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
        /// 将excel中的数据导入到dataTable中
        /// </summary>
        /// <param name="stream">EXCEL文件流</param>
        /// <param name="sheetName">表名</param>
        /// <param name="isFirstRowColumn">第一张是不是列头</param>
        /// <param name="is2007">是否为2007版EXCEL</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(Stream stream,string sheetName, bool isFirstRowColumn,bool is2007)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (is2007) // 2007版本  
                    workbook = new XSSFWorkbook(stream);
                else  // 2003版本  
                    workbook = new HSSFWorkbook(stream);

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
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号  
                    int rowCount = sheet.LastRowNum;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
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

    }
}
