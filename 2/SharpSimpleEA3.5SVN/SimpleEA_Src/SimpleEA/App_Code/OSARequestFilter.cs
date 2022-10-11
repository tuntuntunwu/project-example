#region Copyright SHARP Corporation
//
//	SHARP OSA SDK
//	DirectOsaPrint Application
//
//	Copyright (c) 2005-2011 SHARP CORPORATION. All rights reserved.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
//
// OSARequestFilter.cs 
// 
#endregion

using System;
using System.Web;
using System.IO;

//namespace DirectOsaPrint
//{
	/// <summary>
	/// This file specifies custom filter for request object.
	/// This enables reading of request object into
	/// a log file.
	/// </summary>
	public class OSARequestFilter : Stream
	{
		private Stream m_source;
		private FileStream m_fs;
		private string m_sFile;

		public OSARequestFilter(HttpRequest request, string sFile)
		{
			m_source = request.Filter;
			m_sFile = sFile;
			string sTempFile = Path.GetTempPath() + "temp.txt";

			// save the entire request object into temporary file
			request.SaveAs(sTempFile, true);

			// read from temporary file just made
			FileStream tfs = 
				new FileStream(sTempFile, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(tfs);
			string sRequest = sr.ReadToEnd();
			sr.Close();

			// create log file and populate the entire contents of request
			m_fs =  new FileStream(m_sFile, FileMode.Append, FileAccess.Write);

			string sTextLine =
				"********** REQUEST  AT " + 
				DateTime.Now.ToLongTimeString() + 
				" " +
				DateTime.Now.ToLongDateString() +
				" **********";

			StreamWriter sw = new StreamWriter(m_fs);
			sw.WriteLine(sTextLine);
			sw.Write(sRequest);
			sw.Close();

			m_fs.Close();
		}

		// The following members of Stream must be overridden.
		public override bool CanRead
		{get { return m_source.CanRead; }}

		public override bool CanSeek
		{get { return m_source.CanSeek; }}

		public override bool CanWrite
		{get { return m_source.CanWrite; }}

		public override long Length
		{get { return m_source.Length; }}

		public override long Position
		{
			get { return m_source.Position; }
			set { m_source.Position = value; }
		}

        public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
            return m_source.Seek(offset, origin);
		}

        public override void SetLength(long value)
		{
            m_source.SetLength(value);
		}

		public override void Close()
		{
			m_source.Close();
		}

		public override void Flush()
		{
			m_source.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{      
			return m_source.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{		
			m_source.Write(buffer, 0, count);
		}
	}
//}
