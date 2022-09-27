using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SimpleEACommon
{
    public class Common
    {
        // Caption
        internal const string Caption = "Simple EA Application";
        // Install Flg
        public static bool installflg = false;

        #region "Public const"
        // Access error
        public const string MSG_ACCESS = "Drive or directory access denied.\nPlease run this program as administrator account.";
        // Access error
        public const string MSG_SYSTEMERROR = "System Error, please contract the Administrator.";
        #endregion

        #region "Public Static Function"
        #region "ExecuteReader"
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <Date>2010.08.07</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static SqlDataReader ExecuteReader(string sql, string DBConnectionStrings)
        {

            SqlConnection con = new SqlConnection(DBConnectionStrings);
            con.Open();

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
        }
        #endregion

        #region "ExecuteDataTable"
        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="DBConnectionStrings"></param>
        /// <returns></returns>
        /// <Date>2011.01.24</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DataTable ExecuteDataTable(string sql, string DBConnectionStrings)
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(DBConnectionStrings))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        #endregion


        #region "Alert"
        /// <summary>
        /// Alert
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DialogResult Alert(string msg)
        {
            DialogResult rt;
            if (installflg)
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return rt;
        }
        #endregion

        #region "Confirm"
        /// <summary>
        /// Confirm
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DialogResult Confirm(string msg)
        {
            DialogResult rt;
            if (installflg)
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }

            return rt;
        }
        #endregion

        #region "Confirm"
        /// <summary>
        /// Confirm
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DialogResult Confirm(string msg , MessageBoxButtons button)
        {
            DialogResult rt;
            if (installflg)
            {
                rt = MessageBox.Show(msg, Caption, button, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                rt = MessageBox.Show(msg, Caption, button, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
            return rt;
        }
        #endregion


        #region "Error"
        /// <summary>
        /// Alert
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DialogResult Error(string msg)
        {
            DialogResult rt;
            if (installflg)
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                rt = MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rt;

        }
        #endregion

        #endregion

    }
}
