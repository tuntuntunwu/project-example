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
// OSAResponseFilter.cs 
// 
#endregion

using System;
using System.Web;
using System.IO;

	/// <summary>
	/// This file specifies custom filter for response object.
	/// This enables writing response sent back to the MFP (or browser)
	/// into a log file.
	/// </summary>
public class OSAResponseFilter : Stream
{
    private Stream m_sink;
    private FileStream m_fs;
    private string m_sFile;

    public OSAResponseFilter(HttpResponse response, string sFile)
    {
        m_sink = response.Filter;
        m_sFile = sFile;

        // create log file and put heading with time stamp
        m_fs = new FileStream(m_sFile, FileMode.Append, FileAccess.Write);

        string sTextLine =
            "********** RESPONSE AT " +
            DateTime.Now.ToLongTimeString() +
            " " +
            DateTime.Now.ToLongDateString() +
            " **********";

        StreamWriter sw = new StreamWriter(m_fs);
        sw.WriteLine(sTextLine);
        sw.Close();

        m_fs.Close();
    }

    // The following members of Stream must be overridden.
    public override bool CanRead
    { get { return m_sink.CanRead; } }

    public override bool CanSeek
    { get { return m_sink.CanSeek; } }

    public override bool CanWrite
    { get { return m_sink.CanWrite; } }

    public override long Length
    { get { return m_sink.Length; } }

    public override long Position
    {
        get { return m_sink.Position; }
        set { m_sink.Position = value; }
    }

    public override long Seek(long offset, System.IO.SeekOrigin origin)
    {
        return 0;
    }

    public override void SetLength(long value)
    {
        m_sink.SetLength(value);
    }

    public override void Close()
    {
        m_sink.Close();
    }

    public override void Flush()
    {
        m_sink.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return m_sink.Read(buffer, offset, count);
    }

    // Override the Write method to filter Response to a file. 
    public override void Write(byte[] buffer, int offset, int count)
    {
        // Write out the response to the browser.
        m_sink.Write(buffer, 0, count);

        // Write out the response to the file.
        m_fs = new FileStream(m_sFile, FileMode.Append, FileAccess.Write);
        m_fs.Write(buffer, 0, count);
        m_fs.Close();
    }
}
