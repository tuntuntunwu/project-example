using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Text;
using SimpleEACommon;
namespace DAL
{
	/// <summary>
	/// SQLHelper类封装对SQL Server数据库的添加、删除、修改和选择等操作
	/// </summary>
	public class SQLAuthHelper
	{
        //protected  string servIp;
        //protected  string port;
        //protected  string userId;
        //protected  string passWd;
        //protected  string databaseName;
        protected string connectString = "";
		/// 连接数据源
		private SqlConnection myConnection = null;
		private readonly string RETURNVALUE = "RETURNVALUE";
        /// <summary>
        /// 打开数据库连接.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        public SQLAuthHelper(string connectstring)
        {
            connectString = connectstring;
        }
        public SQLAuthHelper()
        {
                   
        }
        public  void setDBServerInfo(string _connectstring)
        {
            connectString = _connectstring;
        }

        public  SqlConnection getDBConnection()
        {
            //string format3 = "server={0};database={1};uid={2};pwd={3}";
            //string format4 = "server={0}:{1};database={2};uid={3};pwd={4}";
            //string connectionString = "";
            //if (port.Trim().Equals(""))
            //{
            //    connectionString = String.Format(format3, servIp, databaseName, userId, passWd);
            //}
            //else
            //{
            //    connectionString = String.Format(format4, servIp, port, databaseName, userId, passWd);
            //}
            string connectionString = this.connectString;
            //string connectstring = ConfigurationManager.AppSettings["SimpleEAConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    //打开数据库连接
                    conn.Open();

                }
                catch (Exception ex)
                {

                    //ClassError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);

                }
                finally
                {
                    //关闭已经打开的数据库连接				
                }
            }
            return conn;
        }
       
        public void closeDBConnection(SqlConnection conn)
        {
            //判断连接是否已经创建
            if (conn != null)
            {
                //判断连接的状态是否打开
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
		/// <summary>
		/// 打开数据库连接.
		/// </summary>
		private void Open() 
		{
            //string connectstring = ConfigurationManager.ConnectionStrings["SimpleEAConnectionString"].ConnectionString;
            //string format3 = "server={0};database={1};uid={2};pwd={3}";
            //string format4 = "server={0}:{1};database={2};uid={3};pwd={4}";
            //string connectionString = "";
            //if (port.Trim().Equals(""))
            //{
            //    connectionString = String.Format(format3, servIp, databaseName, userId, passWd);
            //}
            //else
            //{
            //    connectionString = String.Format(format4, servIp, port, databaseName, userId, passWd);
            //}
            string connectionString = this.connectString;
            myConnection = new SqlConnection(connectionString);
            if (myConnection.State == ConnectionState.Closed)
			{   
				try
				{
					//打开数据库连接
					myConnection.Open();
				}
				catch(Exception ex)
				{
                    
					//ClassError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);
				}
				finally
				{
					//关闭已经打开的数据库连接				
				}
			}
		}

		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		public void Close() 
		{
			//判断连接是否已经创建
			if(myConnection != null)
			{
				//判断连接的状态是否打开
				if(myConnection.State == ConnectionState.Open)
				{
					myConnection.Close();
				}
			}
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose() 
		{
			// 确认连接是否已经关闭
			if (myConnection != null) 
			{
				myConnection.Dispose();
				myConnection = null;
			}				
		}
		
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <returns>返回存储过程返回值</returns>
		public int RunProc(string procName) 
		{
			SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 180;
			try
			{
				//执行存储过程
				cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();
			}
			
			//返回存储过程的参数值
			return (int)cmd.Parameters[RETURNVALUE].Value;
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程名称</param>
		/// <param name="prams">存储过程所需参数</param>
		/// <returns>返回存储过程返回值</returns>
		public int RunProc(string procName, SqlParameter[] prams) 
		{
            prams = FilterSqlParameter(prams);
			SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 180;
			try
			{
				//执行存储过程
				cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();
			}
			
			//返回存储过程的参数值
			return (int)cmd.Parameters[RETURNVALUE].Value;
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="dataReader">返回存储过程返回值</param>
		public void RunProc(string procName, out SqlDataReader dataReader) 
		{
			//创建Command
			SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 180;
			try
			{
				//读取数据
				dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);	
			}
			catch(Exception ex)
			{
				dataReader = null;
				//记录错误日志
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="prams">存储过程所需参数</param>
        /// <param name="dataReader">返回DataReader对象</param>
		public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader) 
		{
            prams = FilterSqlParameter(prams);
			//创建Command
			SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 180;
			
			try
			{
				//读取数据
				dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch(Exception ex)
			{
				dataReader = null;
				//记录错误日志
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
		}	
	
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="dataSet">返回DataSet对象</param>
		public void RunProc(string procName, ref DataSet dataSet) 
		{
			if(dataSet == null)
			{
				dataSet = new DataSet();
			}
			//创建SqlDataAdapter
			SqlDataAdapter da = CreateProcDataAdapter(procName,null);
			
			try
			{
				//读取数据
				da.Fill(dataSet);
			}
			catch(Exception ex)
			{
				//记录错误日志
               
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
			}
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="prams">存储过程所需参数</param>
		/// <param name="dataSet">返回DataSet对象</param>
		public void RunProc(string procName, SqlParameter[] prams,ref DataSet dataSet) 
		{
            prams = FilterSqlParameter(prams);
			if(dataSet == null)
			{
				dataSet = new DataSet();
			}
			//创建SqlDataAdapter
			SqlDataAdapter da = CreateProcDataAdapter(procName,prams);
			
			try
			{
				//读取数据
				da.Fill(dataSet);
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
			}
		}
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="prams">存储过程所需参数</param>
        /// <param name="datatable">返回DataTable对象</param>
        public void RunProc(string procName, SqlParameter[] prams, ref DataTable dt)
        {
            prams = FilterSqlParameter(prams);
            if (dt == null)
            {
                dt = new DataTable();
            }
            //创建SqlDataAdapter
            SqlDataAdapter da = CreateProcDataAdapter(procName, prams);

            try
            {
                //读取数据
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();
            }
        }
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <returns>返回值</returns>
		public int RunSQL(string cmdText) 
		{
            int ret;
            ret = 0;
			SqlCommand cmd = CreateSQLCommand(cmdText, null);
            cmd.CommandTimeout = 180;
			try
			{
				//执行存储过程
				ret=cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
               
			}
			
			//返回存储过程的参数值
            return ret;
		}
        /// <summary>
        /// 执行SQL语句,返回第一行，第一列的值
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>返回值</returns>
        public int RunSelectSQLToScalar(string cmdText)
        {
           
            int ret;
            ret = 0;
            SqlCommand cmd = CreateSQLCommand(cmdText, null);
            cmd.CommandTimeout = 180;
            try
            {
                //执行存储过程
                ret = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //记录错误日志
               
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();

            }
            //返回存储过程的参数值
            return ret;
        }

        /// <summary>
        /// 执行SQL语句,返回第一行，第一列的值
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>返回值</returns>
        public Object  RunScalar(string cmdText)
        {

            Object ret;
            ret = 0;
            SqlCommand cmd = CreateSQLCommand(cmdText, null);
            cmd.CommandTimeout = 180;
            try
            {
                //执行存储过程
                ret = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();

            }
            //返回存储过程的参数值
            return ret;
        }
        
        /// <summary>
        /// 执行SQL语句,返回第一行，第一列的值
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="prams">SQL语句参数</param>
        /// <returns>返回值</returns>
        public int RunSelectSQLToScalar(string cmdText, SqlParameter[] prams)
        {
            prams = FilterSqlParameter(prams);
            int ret;
            ret = 0;
           
            SqlCommand cmd = CreateSQLCommand(cmdText, prams);
            cmd.CommandTimeout = 180;
            try
            {
                //执行存储过程
                ret = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //记录错误日志
               
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();

            }

            //返回存储过程的参数值
            return ret;
        }
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="prams">SQL语句所需参数</param>
		/// <returns>返回值</returns>
		public int RunSQL(string cmdText, SqlParameter[] prams) 
		{            
			SqlCommand cmd = CreateSQLCommand(cmdText,prams);
            cmd.CommandTimeout = 180;
            int returnvalue = 0;
			try
			{
				//执行存储过程
                returnvalue=cmd.ExecuteNonQuery();
              
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
			}
			
			//返回存储过程的参数值
            return returnvalue;
		}	
		
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>		
		/// <param name="dataReader">返回DataReader对象</param>
		public void RunSQL(string cmdText, out SqlDataReader dataReader) 
		{
			//创建Command
			SqlCommand cmd = CreateSQLCommand(cmdText, null);
            cmd.CommandTimeout = 180;
			try
            {
			
				//读取数据
				dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);	
			}
			catch(Exception ex)
			{
				dataReader = null;
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
		}
       
        public void RunSQL(string cmdText, SqlConnection conn, out SqlDataReader dataReader)
        {
            //创建Command
            SqlCommand cmd = CreateSQLCommand(cmdText, conn, null);
            cmd.CommandTimeout = 180;
            try
            {

                //读取数据
                dataReader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                dataReader = null;
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="prams">SQL语句所需参数</param>
		/// <param name="dataReader">返回DataReader对象</param>
		public void RunSQL(string cmdText, SqlParameter[] prams, out SqlDataReader dataReader) 
		{
			//创建Command
			SqlCommand cmd = CreateSQLCommand(cmdText, prams);
            cmd.CommandTimeout = 180;
			try
			{
				//读取数据
				dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch(Exception ex)
			{
				dataReader = null;
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="dataSet">返回DataSet对象</param>
		public void RunSQL(string cmdText, ref DataSet dataSet) 
		{
			if(dataSet == null)
			{
				dataSet = new DataSet();
			}
			//创建SqlDataAdapter
			SqlDataAdapter da = CreateSQLDataAdapter(cmdText,null);
			
			try
			{
				//读取数据
				da.Fill(dataSet);
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
			}
		}
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dataSet">返回DataSet对象</param>
        public DataSet GetDataSet(string cmdText)
        {

            DataSet dataSet = new DataSet();

            try
            {
               
                //创建SqlDataAdapter
                SqlDataAdapter da = CreateSQLDataAdapter(cmdText, null);
                //读取数据
                da.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();
            }
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dataSet">返回DataSet对象</param>
        public DataSet GetDataSet(string cmdText,SqlParameter[] prams)
        {

            DataSet dataSet = new DataSet();

            try
            {

                //创建SqlDataAdapter
                SqlDataAdapter da = CreateSQLDataAdapter(cmdText,  prams);
                //读取数据
                da.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();
            }
        }
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="prams">SQL语句所需参数</param>
		/// <param name="dataSet">返回DataSet对象</param>
		public void RunSQL(string cmdText, SqlParameter[] prams,ref DataSet dataSet) 
		{
			if(dataSet == null)
			{
				dataSet = new DataSet();
			}
			//创建SqlDataAdapter
			SqlDataAdapter da = CreateProcDataAdapter(cmdText,prams);
			
			try
			{
				//读取数据
				da.Fill(dataSet);
			}
			catch(Exception ex)
			{
				//记录错误日志
                
				//ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
			}
			finally
			{
				//关闭数据库的连接
				Close();	
			}
		}

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dataSet">返回DataSet对象</param>
        /// <param name="tablename">SQL语句所需参数</param>
       
        public void RunSQL(string cmdText,  ref DataSet dataSet,string tablename)
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
            }
            //创建SqlDataAdapter
            SqlDataAdapter da = CreateSQLDataAdapter(cmdText, null);

            try
            {
                //读取数据
                da.Fill(dataSet,tablename);
            }
            catch (Exception ex)
            {
                //记录错误日志

                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //关闭数据库的连接
                Close();
            }
        }
		/// <summary>
		/// 创建一个SqlCommand对象以此来执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="prams">存储过程所需参数</param>
		/// <returns>返回SqlCommand对象</returns>
		private SqlCommand CreateProcCommand(string procName, SqlParameter[] prams) 
		{
			//打开数据库连接
			Open();
			
			//设置Command
			SqlCommand cmd = new SqlCommand(procName, myConnection);
			cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 180;
			//添加把存储过程的参数
			if (prams != null) 
			{
				foreach (SqlParameter parameter in prams)
				{
					cmd.Parameters.Add(parameter);
				}
			}
			
			//添加返回参数ReturnValue
			cmd.Parameters.Add(
				new SqlParameter(RETURNVALUE, SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty, DataRowVersion.Default,null));

			//返回创建的SqlCommand对象
			return cmd;
		}

		/// <summary>
		/// 创建一个SqlCommand对象以此来执行存储过程
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="prams">SQL语句所需参数</param>
		/// <returns>返回SqlCommand对象</returns>
		private SqlCommand CreateSQLCommand(string cmdText, SqlParameter[] prams) 
		{
			//打开数据库连接
			Open();
			
			//设置Command
			SqlCommand cmd = new SqlCommand(cmdText,myConnection);
            cmd.CommandTimeout = 180;
			//添加把存储过程的参数
			if (prams != null) 
			{
				foreach (SqlParameter parameter in prams)
				{
					cmd.Parameters.Add(parameter);
				}
			}
			
			//添加返回参数ReturnValue
			cmd.Parameters.Add(
				new SqlParameter(RETURNVALUE, SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty, DataRowVersion.Default,null));

			//返回创建的SqlCommand对象
			return cmd;
		}
        //chen add
        private SqlCommand CreateSQLCommand(string cmdText, SqlConnection conn, SqlParameter[] prams)
        {
            //设置Command
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandTimeout = 180;
            //添加把存储过程的参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            //添加返回参数ReturnValue
            cmd.Parameters.Add(
                new SqlParameter(RETURNVALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));

            //返回创建的SqlCommand对象
            return cmd;
        }
        //
		/// <summary>
		/// 创建一个SqlDataAdapter对象，用此来执行存储过程
		/// </summary>
		/// <param name="procName">存储过程的名称</param>
		/// <param name="prams">存储过程所需参数</param>
		/// <returns>返回SqlDataAdapter对象</returns>
		private SqlDataAdapter CreateProcDataAdapter(string procName,SqlParameter[] prams)
		{
			//打开数据库连接
			Open();
			
			//设置SqlDataAdapter对象
			SqlDataAdapter da = new SqlDataAdapter(procName,myConnection);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;			

			//添加把存储过程的参数
			if (prams != null) 
			{
				foreach (SqlParameter parameter in prams)
				{
					da.SelectCommand.Parameters.Add(parameter);
				}
			}
			
			//添加返回参数ReturnValue
			da.SelectCommand.Parameters.Add(
				new SqlParameter(RETURNVALUE, SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty, DataRowVersion.Default,null));

			//返回创建的SqlDataAdapter对象
			return da;
		}

		/// <summary>
		/// 创建一个SqlDataAdapter对象，用此来执行SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="prams">SQL语句所需参数</param>
		/// <returns>返回SqlDataAdapter对象</returns>
		private SqlDataAdapter CreateSQLDataAdapter(string cmdText,SqlParameter[] prams)
		{
			//打开数据库连接
			Open();
			
			//设置SqlDataAdapter对象
			SqlDataAdapter da = new SqlDataAdapter(cmdText,myConnection);					

			//添加把存储过程的参数
			if (prams != null) 
			{
				foreach (SqlParameter parameter in prams)
				{
					da.SelectCommand.Parameters.Add(parameter);
				}
			}
			
			//添加返回参数ReturnValue
			da.SelectCommand.Parameters.Add(
				new SqlParameter(RETURNVALUE, SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty, DataRowVersion.Default,null));

			//返回创建的SqlDataAdapter对象
			return da;
		}
		
		/// <summary>
		/// 生成存储过程参数
		/// </summary>
		/// <param name="ParamName">存储过程名称</param>
		/// <param name="DbType">参数类型</param>
		/// <param name="Size">参数大小</param>
		/// <param name="Direction">参数方向</param>
		/// <param name="Value">参数值</param>
		/// <returns>新的 parameter 对象</returns>
		public SqlParameter CreateParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value) 
		{
			SqlParameter param;

			//当参数大小为0时，不使用该参数大小值
			if(Size > 0)
			{
				param = new SqlParameter(ParamName, DbType, Size);
			}
			else
			{
				//当参数大小为0时，不使用该参数大小值
				param = new SqlParameter(ParamName, DbType);
			}

			//创建输出类型的参数
			param.Direction = Direction;
			if (!(Direction == ParameterDirection.Output && Value == null))
			{
				param.Value = Value;
			}

			//返回创建的参数
			return param;
		}

		/// <summary>
		/// 传入输入参数
		/// </summary>
		/// <param name="ParamName">存储过程名称</param>
		/// <param name="DbType">参数类型</param>
		/// <param name="Size">参数大小</param>
		/// <param name="Value">参数值</param>
		/// <returns>新的parameter 对象</returns>
		public SqlParameter CreateInParam(string ParamName, SqlDbType DbType, int Size, object Value) 
		{
			return CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
		}		

		/// <summary>
		/// 传入返回值参数
		/// </summary>
		/// <param name="ParamName">存储过程名称</param>
		/// <param name="DbType">参数类型</param>
		/// <param name="Size">参数大小</param>
		/// <returns>新的 parameter 对象</returns>
		public SqlParameter CreateOutParam(string ParamName, SqlDbType DbType, int Size) 
		{
			return CreateParam(ParamName, DbType, Size, ParameterDirection.Output, null);
		}		

		/// <summary>
		/// 传入返回值参数
		/// </summary>
		/// <param name="ParamName">存储过程名称</param>
		/// <param name="DbType">参数类型</param>
		/// <param name="Size">参数大小</param>
		/// <returns>新的 parameter 对象</returns>
		public SqlParameter CreateReturnParam(string ParamName, SqlDbType DbType, int Size) 
		{
			return CreateParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
		}
        /// <summary>
        /// 将DataReader转为DataTable
        /// </summary>
        /// <param name="dataReader">DataReader</param>
        public DataTable ConvertDataReaderToDataTable(SqlDataReader dataReader)
        {
            //定义DataTable
            DataTable datatable = new DataTable();

            try
            {	//动态添加表的数据列
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    DataColumn myDataColumn = new DataColumn();
                    myDataColumn.DataType = dataReader.GetFieldType(i);
                    myDataColumn.ColumnName = dataReader.GetName(i);
                    datatable.Columns.Add(myDataColumn);
                }

                //添加表的数据
                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }
                    datatable.Rows.Add(myDataRow);
                    myDataRow = null;
                }
                //关闭数据读取器
                dataReader.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                //抛出类型转换错误
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        private SqlParameter[] FilterSqlParameter(SqlParameter[] prams)
        {
            //chen
            if (prams == null) return null;
            //
            int i = 0;
            foreach (SqlParameter pram in prams)
            {
                if (pram.DbType == System.Data.DbType.String && pram.Value == null)
                    prams[i].Value = "";
                if (pram.DbType == System.Data.DbType.Binary)
                    prams[i].DbType = System.Data.DbType.Boolean;
                if (pram.DbType == System.Data.DbType.DateTime && pram.Value.ToString() == "0001/1/1 0:00:00")
                    prams[i].Value = DateTime.Today.ToString();
                ++i;
            }
            return prams; ;
        }
	}
}
