#region Copyright SHARP Corporation
//	Copyright (c) 2011 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace UserDataBackUp
{
    /// <summary>
    /// ResUpdate
    /// </summary>
    /// <remarks>
    /// Update Flg:
    ///     0: Nothing;
    ///     1: Update;
    ///     2: Insert;
    /// </remarks>
    public class ResUpdate
    {
        private DataTable resTable = null;
        private DataTable resDetailTable = null;
        private DataTable resExistdata = null;
        private DataTable resExistDetaildata = null;
        private bool updateflg = false;

        public DataTable RestrictionInfo
        {
            get
            {
                resExistdata.TableName = "RestrictionInfo"; 
                return resExistdata;
            }
        }

        public DataTable RestrictionInformation
        {
            get
            {
                resExistDetaildata.TableName = "RestrictionInformation";
                return resExistDetaildata;
            }
        }

        /// <summary>
        /// RestrictionInformation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public RestrictionInformation RestrictionInformationRow(int i)
        {
            RestrictionInformation res = new RestrictionInformation(resExistDetaildata.Rows[i]);
            return res;
        }

        /// <summary>
        /// RestrictionInfoRow
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public RestrictionInfo RestrictionInfoRow(int i)
        {
            RestrictionInfo res = new RestrictionInfo(resExistdata.Rows[i]);
            return res;
        }

        /// <summary>
        /// RestrictionInfoCount
        /// </summary>
        public int RestrictionInfoCount
        {
            get
            {
                return resExistdata.Rows.Count;
            }
        }

        /// <summary>
        /// RestrictionInformationCount
        /// </summary>
        public int RestrictionInformationCount
        {
            get
            {
                return resExistDetaildata.Rows.Count;
            }
        }


        

        /// <summary>
        /// ResUpdate
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="con"></param>
        /// <param name="updateflg">Update flg</param>
        public ResUpdate( bool _updateflg)
        {
            updateflg = _updateflg;

            // Create RestrictionInfo Table
            CreateRestrictionInfo();

            // Create RestrictionInformation Table
            CreateRestrictionInformation();

        }


        #region "RestrictionInfo Table Config"
        /// <summary>
        /// CreateRestrictionInfo
        /// </summary>
        private void CreateRestrictionInfo()
        {
            // New RestrictionInfo
            resTable = new DataTable("RestrictionInfo");
            DataColumn colInt32 = new DataColumn("ID");
            colInt32.DataType = System.Type.GetType("System.Int32");
            resTable.Columns.Add(colInt32);

            DataColumn colString = new DataColumn("RestrictionName");
            colString.DataType = System.Type.GetType("System.String");
            resTable.Columns.Add(colString);

            DataColumn colOld = new DataColumn("OldID");
            colOld.DataType = System.Type.GetType("System.Int32");
            resTable.Columns.Add(colOld);
            
            return;
        }

        /// <summary>
        /// AddRestrictionInfo
        /// </summary>
        /// <param name="line"></param>
        public void AddRestrictionInfo(string line)
        {
            RestrictionInfo res = new RestrictionInfo(line);
            resTable.Rows.Add(res.ID, res.RestrictionName, res.ID);
        }

        /// <summary>
        /// AddRestrictionInfo
        /// </summary>
        /// <param name="item"></param>
        public void AddRestrictionInfo(string[] item)
        {
            RestrictionInfo res = new RestrictionInfo(item);
            resTable.Rows.Add(res.ID, res.RestrictionName, res.ID);
        }

        #endregion

        #region "Config RestrictionInformation"
        /// <summary>
        /// CreateRestrictionInformation
        /// </summary>
        private void CreateRestrictionInformation()
        {
            // New RestrictionInfo
            resDetailTable = new DataTable("RestrictionInformation");

            //_RestrictionID = Convert.ToInt32(item[0]);
            //_JobId = item[1];
            //_FunctionId = item[2];
            //_Status = item[3];
            //_LimitNum = item[4];

            DataColumn RestrictionID = new DataColumn("RestrictionID");
            RestrictionID.DataType = System.Type.GetType("System.Int32");
            resDetailTable.Columns.Add(RestrictionID);

            DataColumn JobId = new DataColumn("JobId");
            JobId.DataType = System.Type.GetType("System.Int32");
            resDetailTable.Columns.Add(JobId);

            DataColumn FunctionId = new DataColumn("FunctionId");
            FunctionId.DataType = System.Type.GetType("System.Int32");
            resDetailTable.Columns.Add(FunctionId);

            DataColumn Status = new DataColumn("Status");
            Status.DataType = System.Type.GetType("System.Int32");
            resDetailTable.Columns.Add(Status);

            DataColumn LimitNum = new DataColumn("LimitNum");
            LimitNum.DataType = System.Type.GetType("System.String");
            resDetailTable.Columns.Add(LimitNum);
            return;
        }

        /// <summary>
        /// AddRestrictionInformation
        /// </summary>
        /// <param name="line"></param>
        public void AddRestrictionInformation(string line)
        {
            RestrictionInformation detail = new RestrictionInformation(line);
            resDetailTable.Rows.Add(detail.RestrictionID, detail.JobId, detail.FunctionId, detail.Status, detail.LimitNum);
        }

        /// <summary>
        /// AddRestrictionInformation
        /// </summary>
        /// <param name="item"></param>
        public void AddRestrictionInformation(string[] item)
        {
            RestrictionInformation detail = new RestrictionInformation(item);
            resDetailTable.Rows.Add(detail.RestrictionID, detail.JobId, detail.FunctionId, detail.Status, detail.LimitNum);
        }

        #endregion


        #region "Update Restrication"
        /// <summary>
        /// Update Restrication
        /// </summary>
        /// <param name="strDBConn"></param>
        /// <returns></returns>
        public void  UpdateRes(String strDBConn)
        {

            // Get the Exist RestrictionInfo Data
            GetExistRestrictionInfoData(strDBConn);
            // Get the Exist RestrictionInformation Data
            GetExistRestrictionInformationData(strDBConn);


            ArrayList rowlist = new ArrayList();
            if (updateflg)
            {
                // Update the Old Data
                foreach (DataRow row in resExistdata.Rows)
                {
                    RestrictionInfo res = new RestrictionInfo(row);
                    // Find the same row
                    string strSql = "RestrictionName = '{0}'";
                    DataRow[] result = resTable.Select(string.Format(strSql, res.RestrictionName));
                    if (result.Length > 0)
                    {
                        // Add the Exist Row to the list (for delete)
                        res = new RestrictionInfo(result[0]);
                        rowlist.Add(result[0]);


                        // Delete the Exist Row in the DB DataTable.
                        res = new RestrictionInfo(row);
                        strSql = "RestrictionID = {0}";
                        int intExistId = res.ID;
                        DataRow[] detailExistDeleteRow = resExistDetaildata.Select(string.Format(strSql, intExistId));

                        if (detailExistDeleteRow.Length > 0)
                        {
                            row["UpdateFlg"] = "1";
                        }
                        
                        foreach (DataRow deleterow in detailExistDeleteRow)
                        {
                            resExistDetaildata.Rows.Remove(deleterow);
                        }

                        // Update the RestricationInformation in the File DataTable. 
                        res = new RestrictionInfo(result[0]);
                        DataRow[] detailUpdateRow = resDetailTable.Select(string.Format(strSql, res.ID));

                        // Add Updated RestricationInformation to the DB DataTable.
                        foreach (DataRow insertrow in detailUpdateRow)
                        {
                            RestrictionInformation resdetail = new RestrictionInformation(insertrow);
                            if (string.IsNullOrEmpty(resdetail.LimitNum))
                            {
                                resExistDetaildata.Rows.Add(intExistId, resdetail.JobId, resdetail.FunctionId, resdetail.Status, null);
                            }
                            else
                            {
                                resExistDetaildata.Rows.Add(intExistId, resdetail.JobId, resdetail.FunctionId, resdetail.Status, resdetail.LimitNum);
                            }
                        }
                    }
                }
            }
            else
            {
                // Hold Old Data, do not Update those data.
                foreach (DataRow row in resTable.Rows)
                {
                    RestrictionInfo res = new RestrictionInfo(row);
                    string strSql = "RestrictionName = '{0}'";
                    DataRow[] result = resExistdata.Select(string.Format(strSql, res.RestrictionName));
                    if (result.Length > 0)
                    {
                        // Add the Exist Row to the list (for delete)
                        rowlist.Add(row);
                    }
                }
            }

            // Delete the exist rows in the File DataTable.
            foreach (DataRow row in rowlist)
            {
                resTable.Rows.Remove(row);
            }

            // Insert row from File DataTable to the DB DataTable.
            // Get The MaxID
            int id = GetMaxID(resExistdata);
            foreach (DataRow row in resTable.Rows)
            {
                RestrictionInfo res = new RestrictionInfo(row);
                resExistdata.Rows.Add(id, res.RestrictionName , "2");
                // Update the File DataTable with new ID.
                row["ID"] = id;
                id = id + 1;
            }

            // Update the RestrictionInfor
            foreach (DataRow row in resTable.Rows)
            {
                // Based the RestrictionInfo File DataTable.
                int oldint = Convert.ToInt32(row["OldID"]);
                RestrictionInfo res = new RestrictionInfo(row);
                string strSql = "RestrictionID = {0}";
                // Get the Olid Detail Information.
                DataRow[] detail = resDetailTable.Select(string.Format(strSql,oldint));

                // Added the detailrow.
                foreach (DataRow insertrow in detail)
                {
                    RestrictionInformation resdetail = new RestrictionInformation(insertrow);
                    if (string.IsNullOrEmpty(resdetail.LimitNum))
                    {
                        resExistDetaildata.Rows.Add(res.ID, resdetail.JobId, resdetail.FunctionId, resdetail.Status, null);
                    }
                    else
                    {
                        resExistDetaildata.Rows.Add(res.ID, resdetail.JobId, resdetail.FunctionId, resdetail.Status, resdetail.LimitNum);
                    }
                }

            }

        }


        private int GetMaxID(DataTable resExistdata)
        {
            int id = 0;
            int nowid = 0;
            foreach (DataRow row in resExistdata.Rows)
            {
                nowid = Convert.ToInt32(row[0]);
                if (nowid > id)
                {
                    id = nowid;
                }
            }

            return id + 1;
        }
        #endregion

        #region "Get Exist Data"
        /// <summary>
        /// GetExistData
        /// </summary>
        /// <param name="strDBConn"></param>
        private void GetExistRestrictionInfoData(string strDBConn)
        {
            try
            {
                string sql = "SELECT ID,RestrictionName,'0' AS UpdateFlg FROM [SimpleEA].[dbo].[RestrictionInfo] WHERE [RestrictionInfo].ID <> 0 ORDER BY ID ";
                resExistdata = SimpleEACommon.Common.ExecuteDataTable(sql, strDBConn);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void GetExistRestrictionInformationData(string strDBConn)
        {
            try
            {
                string sql = "SELECT RestrictionID,JobId,FunctionId,Status,LimitNum FROM [SimpleEA].[dbo].[RestrictionInformation] WHERE [RestrictionInformation].RestrictionID <> 0 ORDER BY RestrictionID";
                resExistDetaildata = SimpleEACommon.Common.ExecuteDataTable(sql, strDBConn);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

    }

    /// <summary>
    /// RestrictionInfo
    /// </summary>
    public class RestrictionInfo
    {
        private int _ID;
        public int ID
        {
            get
            {
                return _ID;
            }
        }

        private string _RestrictionName;
        public string RestrictionName
        {
            get
            {
                return _RestrictionName;
            }
        }

        private ResStatus _Status = ResStatus.Nothing;
        public ResStatus Status
        {
            get
            {
                return _Status;
            }
        }

        public RestrictionInfo(string line)
        {
            string[] item = line.Split(',');
            ConfigRestrictionInfo(item);
        }

        public RestrictionInfo(string[] item)
        {
            ConfigRestrictionInfo(item);
        }

        public RestrictionInfo(DataRow row)
        {
            _ID = Convert.ToInt32(row[0]);
            _RestrictionName = row[1].ToString();
            try
            {
                string strFlg = row[2].ToString();
                if (strFlg == "0")
                {
                    _Status = ResStatus.Nothing;
                }
                else if ("1".Equals(strFlg))
                {
                    _Status = ResStatus.Update;
                }
                else if ("2".Equals(strFlg))
                {
                    _Status = ResStatus.Insert;
                }
                else
                {
                    _Status = ResStatus.Nothing;
                }
                
            }
            catch
            {
                _Status = ResStatus.Nothing;
            }
        }

        private void ConfigRestrictionInfo(string[] item)
        {
            _ID = Convert.ToInt32(item[0]);
            _RestrictionName = item[1];
            try
            {
                string strFlg = item[2];
                if (strFlg == "0")
                {
                    _Status = ResStatus.Nothing;
                }
                else if ("1".Equals(strFlg))
                {
                    _Status = ResStatus.Update;
                }
                else if ("2".Equals(strFlg))
                {
                    _Status = ResStatus.Insert;
                }
                else
                {
                    _Status = ResStatus.Nothing;
                }

            }
            catch
            {
                _Status = ResStatus.Nothing;
            }
            return;
        }
    }

    /// <summary>
    /// RestrictionInformation
    /// </summary>
    public class RestrictionInformation
    {
        private int _RestrictionID;
        public int RestrictionID
        {
            get
            {
                return _RestrictionID;
            }
        }

        private int _JobId;
        public int JobId
        {
            get
            {
                return _JobId;
            }
        }

        private int _FunctionId;
        public int FunctionId
        {
            get
            {
                return _FunctionId;
            }
        }

        private int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }
        }

        private String _LimitNum;
        public String LimitNum
        {
            get
            {
                return _LimitNum;
            }
        }

        public RestrictionInformation(string line)
        {
            string[] item = line.Split(',');
            ConfigRestrictionInformation(item);
        }

        public RestrictionInformation(string[] item)
        {
            ConfigRestrictionInformation(item);
        }

        public RestrictionInformation(DataRow row)
        {
            _RestrictionID = Convert.ToInt32(row[0]);
            _JobId = Convert.ToInt32(row[1]);
            _FunctionId = Convert.ToInt32(row[2]);
            _Status = Convert.ToInt32(row[3]);
            _LimitNum = row[4].ToString();
        }


        private void ConfigRestrictionInformation(string[] item)
        {

            _RestrictionID = Convert.ToInt32(item[0]);
            _JobId = Convert.ToInt32(item[1]);
            _FunctionId = Convert.ToInt32(item[2]);
            _Status = Convert.ToInt32(item[3]);
            _LimitNum = item[4].ToString();
            return;
        }
    }

    /// <summary>
    /// ResStatus
    /// </summary>
    public enum ResStatus
    {
        Update,
        Insert,
        Nothing
    }
}
