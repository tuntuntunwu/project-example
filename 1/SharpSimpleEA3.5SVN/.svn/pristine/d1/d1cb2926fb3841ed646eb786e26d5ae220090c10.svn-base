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
    public class DalJobTypeInfo : CommonDal
    {

        protected SqlParameter[] CreateParamList(JobTypeInfoEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,beanitem.ID),
                sqlHelper.CreateInParam("@JobName",SqlDbType.NVarChar,50,beanitem.JobName),
                sqlHelper.CreateInParam("@JobNameDisp",SqlDbType.NVarChar,50,beanitem.JobNameDisp),
                sqlHelper.CreateInParam("@Comment",SqlDbType.NVarChar,50,beanitem.Comment),
            };
            return ParamList;
        }

        protected JobTypeInfoEntry Bind(SqlDataReader rec)
        {
            JobTypeInfoEntry beanitem = new JobTypeInfoEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.JobName = ConvertObjToString(rec["JobName"]);
            beanitem.JobNameDisp = ConvertObjToString(rec["JobNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);
            rec.Close();

            return beanitem;
        }

        protected JobTypeInfoEntry Bind(DataRow rec)
        {
            JobTypeInfoEntry beanitem = new JobTypeInfoEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.JobName = ConvertObjToString(rec["JobName"]);
            beanitem.JobNameDisp = ConvertObjToString(rec["JobNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);

            return beanitem;

        }

        protected JobTypeInfoModel BindM(DataRow rec)
        {

            JobTypeInfoModel beanitem = new JobTypeInfoModel();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.JobName = ConvertObjToString(rec["JobName"]);
            beanitem.JobNameDisp = ConvertObjToString(rec["JobNameDisp"]);
            beanitem.Comment = ConvertObjToString(rec["Comment"]);

            return beanitem;

        }

        /// <summary>
        /// 通过ID查JobName
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public JobTypeInfoEntry GetJobNameByID(int ID)
        {
            JobTypeInfoEntry entry = new JobTypeInfoEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@ID", SqlDbType.Int, 4, ID) };
            try
            {
                SqlDataReader rec;
                string sql = "select JobName from JobTypeInformation  where ID=@ID ";
                sqlHelper.RunSQL(sql, ParamList, out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}