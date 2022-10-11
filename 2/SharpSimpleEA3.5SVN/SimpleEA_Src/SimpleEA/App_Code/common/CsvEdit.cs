using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Microsoft.Office.Interop.Excel;

/// <summary>
///CsvEdit 的摘要说明
/// </summary>
/// 
namespace common
{
    public class CsvEdit
    {
        public string mFilename;
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;
        public CsvEdit()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
       
        public void Create()//创建一个Excel对象
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
        }
        public void Open(string FileName)//打开一个Excel文件
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            //wb = wbs.Open(FileName, 0, true, 5,"", "", true, Excel.XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
            //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Excel.XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            mFilename = FileName;
        }
        public Microsoft.Office.Interop.Excel.Worksheet GetSheet(string SheetName)
        //获取一个工作表
        {
            bool isExist = false;
            for (int i = 1; i <= wb.Worksheets.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[i];
                if (SheetName == ws.Name)
                {
                    isExist = true;
                    break;
                }
            }
            if (isExist)
            {
                Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName];
                return s;
            }
            return null;
        }
        //获取单元格的值
        public string getCellValue(Microsoft.Office.Interop.Excel.Worksheet ws, int x, int y)
        { 
            string ret = "";
            ret = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[x, y]).ToString();
           // ret = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[x, y]).Text;
            return ret;
        }
        public List<string> getSheetData(string sheetName)
        {
            List<string> data = new List<string>();
            Worksheet ws = GetSheet(sheetName);
            if (ws == null)
            {
                return data;
            }
            //取得总记录行数   (包括标题列)
            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            //取得数据范围区域 (不包括标题列) 
           Range rng1 = ws.Cells.get_Range("A2", "C" + rowsint);   //item
 
            object[,] arryItem= (object[,])rng1.Value2;   //get range's value
            //string[,] arry = new string[rowsint-1, 2];

            string item = "";
            for (int i = 1; i <= rowsint - 1; i++)
            {
                //Item_Code列
                StringBuilder sb = new StringBuilder("");
                item =arryItem[i, 1].ToString();
                sb.Append(item);
                sb.Append(',');
                item = arryItem[i, 2].ToString();
                sb.Append(item);
                sb.Append(',');
                item = arryItem[i, 3].ToString();
                sb.Append(item);
                data.Add(sb.ToString());
            }
  
            return data;
        }

        //public List<string> getSheetData(Worksheet ws,  string tbl_st, string tbl_ed, int row_num, int col_num)
        public List<string> getSheetData(Worksheet ws, int col_num)
        {
            List<string> data = new List<string>();
            if (ws == null)
            {
                return data;
            }
            //取得总记录行数   (包括标题列)
            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            if (rowsint == 1)
            {
                return data;
            }

            //int rowsint = row_num; //得到行数
            //取得数据范围区域 (不包括标题列) 
            Range rng1 = ws.Cells.get_Range("A2", "G" + rowsint);   //item

            object[,] arryItem = (object[,])rng1.Value2;   //get range's value
            //string[,] arry = new string[rowsint-1, 2];
            int row = arryItem.GetLength(0);
            string item = "";
            for (int i = 1; i <= row ; i++)
            {
                if (arryItem[i, 1] == null)
                {
                    break;
                }
                //Item_Code列
                StringBuilder sb = new StringBuilder("");
                for (int j = 1; j <= col_num; j++)
                {
                    if (arryItem[i, j] != null)
                    {
                        item = arryItem[i, j].ToString();
                    }
                    else
                    {
                        item = "*";
                    }
                    sb.Append(item);
                    sb.Append(',');
                }
                data.Add(sb.ToString());
            }

            return data;
        }
        public List<string> getSheetData3(Worksheet ws, int col_num)
        {
            List<string> data = new List<string>();
            if (ws == null)
            {
                return data;
            }
            //取得总记录行数   (包括标题列)
            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            //int rowsint = row_num; //得到行数
            //取得数据范围区域 (不包括标题列) 
            Range rng1 = ws.Cells.get_Range("A2", "G" + rowsint);   //item

            object[,] arryItem = (object[,])rng1.Value2;   //get range's value
            //string[,] arry = new string[rowsint-1, 2];
            int row = arryItem.GetLength(0);
            string item = "";
            for (int i = 1; i <= row; i++)
            {
                if (arryItem[i, 1] == null)
                {
                    break;
                }
                //Item_Code列
                StringBuilder sb = new StringBuilder("");
                for (int j = 1; j <= col_num; j++)
                {
                    if (arryItem[i, j] != null)
                    {
                        item = arryItem[i, j].ToString();
                    }
                    else
                    {
                        item = "*";
                    }
                    sb.Append(item);
                    sb.Append(',');
                }
                data.Add(sb.ToString());
            }

            return data;
        }
        public string getSheetValue(Worksheet ws,  string st_pos, string row_pos, int row_num)
        {
            if (ws == null)
            {
                return "";
            }
            //取得总记录行数   (包括标题列)
            //int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            int rowsint = row_num;
            //取得数据范围区域 (不包括标题列) 
            //Range rng1 = ws.Cells.get_Range("A2", "C" + rowsint);   //item
            Range rng1 = ws.Cells.get_Range(st_pos , row_pos);   //item

            object[,] arryItem = (object[,])rng1.Value2;   //get range's value
            //string[,] arry = new string[rowsint-1, 2];

            string item = "";
            StringBuilder sb = new StringBuilder("");
            for (int i = 1; i <= rowsint; i++)
            {
                if (arryItem[i, 1] == null)
                {
                    break;
                }
                //Item_Code列
                item = arryItem[i, 1].ToString();
                sb.Append(item);
                sb.Append(",");
            }

            return sb.ToString();
        }

    
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格插入到Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0;  j<= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX+i, j + startY] = dt.Rows[i][j].ToString();

                }

            }

        }
    


        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格添加到Excel指定工作表的指定位置一
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];

                }

            }

        }
        public void AddTable(System.Data.DataTable dt,Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        //将内存中数据表格添加到Excel指定工作表的指定位置二
        {


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];

                }
            }

        }
       

       
        public bool Save()
        //保存文档
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool SaveAs(object FileName)
        //文档另存为
        {
            try
            {

                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;

            }

            catch (Exception ex)
            {
                return false;

            }
        }
        public void Close()
        //关闭一个Excel对象，销毁对象
        {
            //wb.Save();
            if (wb != null)
            {
                wb.Close(Type.Missing, Type.Missing, Type.Missing);
            }
            if (wbs != null)
            {
                wbs.Close();
            }
            if (app != null)
            {
                app.Quit();
            }
            //释放掉多余的excel进程
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();


        }
    }
}