using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MFPOSAautosubmit
{
    /// <summary>
    /// Ｉ／Ｏ
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class IOUtils
    {
        #region BufferedReader
        /// <summary>
        /// BufferedReader
        /// </summary>
        /// <remarks></remarks>
        public class BufferedReader
        {
            #region 成员变量
            int readCnt;
            int buffferCnt;

            StreamReader reader;

            Queue<string> readRecs;
            #endregion

            #region BufferedReader
            /// <summary>
            /// BufferedReader
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="buffsize"></param>
            public BufferedReader(StreamReader reader, int buffsize)
            {
                this.reader = reader;
                this.buffferCnt = buffsize;
                this.readRecs = new Queue<string>(buffsize + 1);
                this.readCnt = 0;
            }
            #endregion

            #region IO

            /// <summary>
            /// EndOfStream
            /// </summary>
            /// <remarks></remarks>
            public bool EndOfStream
            {
                get
                {
                    return (reader.EndOfStream && readRecs.Count == 0);
                }
            }

            /// <summary>
            /// BufferSize(固定值)
            /// </summary>
            public int BufferSize
            {
                get { return this.buffferCnt; }
            }

            /// <summary>
            /// StreamReader计数
            /// </summary>
            public int ReadCount
            {
                get { return this.readCnt; }
            }

            /// <summary>
            /// </summary>
            public int RemainCount
            {
                get { return this.readRecs.Count; }
            }

            /// <summary>
            /// BufferedStrings
            /// </summary>
            /// <remarks></remarks>
            public string[] BufferedStrings
            {
                get
                {
                    return readRecs.ToArray();
                }
            }

            #endregion

            #region Close
            /// <summary>
            /// BufferedReader关闭
            /// </summary>
            public void Close()
            {
                this.reader.Close();
                this.reader = null;
                this.readRecs = null;
                this.buffferCnt = 0;
                this.readCnt = 0;
            }
            #endregion

            #region PreRead
            /// <summary>
            /// </summary>
            /// <returns></returns>
            public int PreRead()
            {
                string rec;
                while (!this.reader.EndOfStream)
                {
                    this.readCnt++;
                    rec = reader.ReadLine();
                    this.readRecs.Enqueue(rec);

                    if (this.readCnt >= this.buffferCnt)
                        break;
                }
                return this.readCnt;
            }
            #endregion

            #region ReadLine
            /// <summary>
            /// </summary>
            /// <returns></returns>
            public string ReadLine()
            {
                if (this.readRecs.Count >= 1)
                {
                    return this.readRecs.Dequeue();
                }
                else
                {
                    this.readCnt++;
                    return this.reader.ReadLine();
                }
            }
            #endregion
        }
        #endregion

        #region CreateBufferedFileStream
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static BufferedStream CreateBufferedFileStream(string filepath, FileMode mode)
        {
            BufferedStream fs = new BufferedStream(new FileStream(filepath, mode));
            return fs;
        }
        #endregion

        #region CreateFileStreamWriter
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mode"></param>
        /// <returns>r</returns>
        public static StreamWriter CreateFileStreamWriter(string filepath, FileMode mode, Encoding encoding)
        {
            FileStream fs = new FileStream(filepath, mode);
            return new StreamWriter(fs, encoding);
        }
        #endregion

        #region CreateFileStreamWriter
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mode"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        public static StreamWriter CreateFileStreamWriter(string filepath, FileMode mode, FileShare share, Encoding encoding)
        {
            FileAccess access = FileAccess.Write;
            if (mode == FileMode.OpenOrCreate || mode == FileMode.Append || mode == FileMode.Open)
            {
                access = FileAccess.ReadWrite;
            }

            FileStream fs = new FileStream(filepath, mode, access, share);
            return new StreamWriter(fs, encoding);
        }
        #endregion

        #region CreateFileStreamWriter
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static StreamWriter CreateFileStreamWriter(string filepath)
        {
            return CreateFileStreamWriter(filepath, FileMode.Create, Encoding.GetEncoding("shift_jis"));
        }
        #endregion

        #region CreateFileStreamReader
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static StreamReader CreateFileStreamReader(string filepath, FileMode mode, Encoding encoding)
        {
            FileStream fs = new FileStream(filepath, mode);
            return new StreamReader(fs, encoding);
        }
        #endregion

        #region CreateFileStreamReader
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mode"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        public static StreamReader CreateFileStreamReader(string filepath, FileMode mode, FileShare share, Encoding encoding)
        {
            FileAccess access = FileAccess.Read;
            if (mode == FileMode.OpenOrCreate || mode == FileMode.Append)
            {
                access = FileAccess.ReadWrite;
            }

            FileStream fs = new FileStream(filepath, mode, access, share);
            return new StreamReader(fs, encoding);
        }
        #endregion

        #region CreateFileStreamReader
        /// <summary>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>r</returns>
        public static StreamReader CreateFileStreamReader(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            return new StreamReader(fs, Encoding.GetEncoding("shift_jis"));
        }
        #endregion

        #region ConvertToByteArray
        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(string s, Encoding encoding)
        {
            byte[] byteBuff = new byte[s.Length * 3];
            MemoryStream ostream = new MemoryStream(byteBuff);
            StreamWriter writer = new StreamWriter(ostream, encoding);
            writer.Write(s);
            writer.Flush();
            byte[] bytes = new byte[ostream.Position];
            Array.Copy(byteBuff, bytes, bytes.Length);
            return bytes;
        }
        #endregion

        #region ConvertFromByteArray
        /// <summary>
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertFromByteArray(byte[] bytes, Encoding encoding)
        {
            StreamReader reader = new StreamReader(new MemoryStream(bytes), encoding);
            return reader.ReadToEnd();
        }
        #endregion

        #region ConvertToBase64ByteArray
        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ConvertToBase64ByteArray(string s)
        {
            byte[] bytes = ConvertToByteArray(s, Encoding.GetEncoding("shift_jis"));
            string s64 = Convert.ToBase64String(bytes);

            List<byte> outBuff = new List<byte>(s64.Length * 3);
            for (int i = 0; i < s64.Length; i++)
            {
                outBuff.Add((byte)s64[i]);
            }
            return outBuff.ToArray();
        }
        #endregion

        #region ConvertFromBase64ByteArray
        /// <summary>
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertFromBase64ByteArray(byte[] bytes)
        {
            char[] charBuff = new char[bytes.Length];
            for (int i = 0, cnt = bytes.Length; i < cnt; i++)
            {
                charBuff[i] = (char)bytes[i];
            }
            byte[] deArray = Convert.FromBase64CharArray(charBuff, 0, charBuff.Length);

            string retval = ConvertFromByteArray(deArray, Encoding.GetEncoding("shift_jis"));
            return retval;
        }
        #endregion

    }
}
