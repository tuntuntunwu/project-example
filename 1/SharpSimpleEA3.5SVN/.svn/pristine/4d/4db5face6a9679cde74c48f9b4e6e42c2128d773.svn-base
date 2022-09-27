using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SimpleEACommon;
using Model;

/// <summary>
/// ProductSQL 的摘要说明
/// </summary>
namespace DAL
{
    public class DalFunctionTypeInfo : CommonDal
    {
        protected SqlParameter[] CreateParamList(FunctionTypeInfoEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@JobID",SqlDbType.Int,4,beanitem.JobID),
                sqlHelper.CreateInParam("@FunctionID",SqlDbType.Int,4,beanitem.FunctionID),
                sqlHelper.CreateInParam("@FunctionName",SqlDbType.NVarChar,50,beanitem.FunctionName),
                sqlHelper.CreateInParam("@FunctionNameDisp",SqlDbType.NVarChar,50,beanitem.FunctionNameDisp),
                sqlHelper.CreateInParam("@Comment",SqlDbType.NVarChar,50,beanitem.Comment),
            };
            return ParamList;
        }

        protected FunctionTypeInfoEntry Bind(SqlDataReader rec)
        {
            FunctionTypeInfoEntry beanitem = new FunctionTypeInfoEntry();
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.FunctionName = ConvertObjToString(rec["FunctionName"]);
            beanitem.FunctionNameDisp = ConvertObjToString(rec["FunctionNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);
            rec.Close();

            return beanitem;
        }

        protected FunctionTypeInfoEntry Bind(DataRow rec)
        {
            FunctionTypeInfoEntry beanitem = new FunctionTypeInfoEntry();
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.FunctionName = ConvertObjToString(rec["FunctionName"]);
            beanitem.FunctionNameDisp = ConvertObjToString(rec["FunctionNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);

            return beanitem;

        }

        protected FunctionTypeInfoModel BindM(DataRow rec)
        {

            FunctionTypeInfoModel beanitem = new FunctionTypeInfoModel();
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.FunctionName = ConvertObjToString(rec["FunctionName"]);
            beanitem.FunctionNameDisp = ConvertObjToString(rec["FunctionNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);

            return beanitem;

        }

        /// <summary>
        /// 通过UID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public FunctionTypeInfoEntry GetInfoByID(string SerialNumber)
        {
            FunctionTypeInfoEntry entry = new FunctionTypeInfoEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@SerialNumber", SqlDbType.NVarChar, 10, SerialNumber) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from FunctionTypeInformation  where SerialNumber=@SerialNumber ";
                sqlHelper.RunSQL(sql, ParamList, out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
