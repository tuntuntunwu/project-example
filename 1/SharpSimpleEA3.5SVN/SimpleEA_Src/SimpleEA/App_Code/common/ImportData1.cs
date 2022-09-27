using System;
using System.Collections.Generic;
using System.Web;
//using Excel;
using Microsoft.Office.Interop.Excel;
using Model;
using common;
using System.IO;
using System.Text;
/// <summary>
///ImportData 的摘要说明
/// </summary>
/// 
namespace common
{
    public class ImportData1
    {

        protected bool IsOkData(string data)
        {
            string[] data_array = data.Split(',');
            bool ret = true;
            if (data_array[1] == "*")
            {
                ret = false;
            }
            return ret;

        }
     //这里是对BeanPlan导入数据的处理
        //public UserModel  ImportExcel(string filename)
        //{
        //        UserModel beanitem = new UserModel(); 
        //        CsvEdit csvEdit = new CsvEdit();
        //    try{
        //        csvEdit.Open(filename);

        //        List<string> rdAnswers = new List<string>();

        //        beanitem = new UserModel();

        //        //string sheetname = Configer.SHEET_MAN_NAME;
        //        string sheetname = "SampleICCard";
        //        Microsoft.Office.Interop.Excel.Worksheet ws = csvEdit.GetSheet(sheetname);
        //        string val;
        //        //LoginName
        //        val = csvEdit.getCellValue(ws, 1, 1);
        //        beanitem.LoginName = val;
        //        //ICCardID
        //        val = csvEdit.getCellValue(ws, 1, 2);
        //        beanitem.ICCardID = val;              
               

        //        return beanitem;
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //    finally
        //    {
        //        csvEdit.Close();
        //    }

        //    return null;
        //}

        //这里是对01读保持线圈状态导入数据的处理


        public UserModel[] ImportExcel11(string filename)
        {
            
            CsvEdit excelEdit = new CsvEdit();
            try
            {
                excelEdit.Open(filename);

                List<string> rdAnswers = new List<string>();
                //01读保持线圈状态
                //string klp_info_st ="A1";
                int col_num = 2;
                string sheetname = "SampleICCard";

                Microsoft.Office.Interop.Excel.Worksheet ws1 = excelEdit.GetSheet(sheetname);
                List<string> klp_info_list = excelEdit.getSheetData(ws1, col_num);
                UserModel[] beanitem = new UserModel[klp_info_list.Count];
                int i = 0;
                string[] klp_info;
                string temp = "";
                for (i = 0; i < klp_info_list.Count; i++)
                {
                    temp = klp_info_list[i];
                    if (IsOkData(temp))
                    {
                        klp_info = temp.Split(',');
                        beanitem[i] = new UserModel();
                        beanitem[i].LoginName = klp_info[0];
                        beanitem[i].ICCardID = klp_info[1];
                    }
                }
                return beanitem;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                excelEdit.Close();
            }

            return null;
        }


        public List<UserModel>  ImportCSV(string filename)
        {
            StreamReader streamReader = new StreamReader(filename, Encoding.Default, false);
             List<UserModel> allDataList = new List<UserModel>();
            try
            {
                string strline;

                List<string> rdAnswers = new List<string>();
                int col_num = 2;
                string sheetname = "SampleICCard";
                while ((strline = streamReader.ReadLine()) != null)
                {
                    string[] array = strline.Split(new char[] { ',' });
                    if (array.Length != 2)
                    {
                        continue;
                    }
                    UserModel row = new UserModel();
                    row.LoginName = array[0];
                    row.ICCardID = array[1] == "" ? null : array[1];
                    allDataList.Add(row);
                }
                return allDataList;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                streamReader.Close();
            }

            return null;
        }
    }   
}
