using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

/// <summary>
///Class1 的摘要说明
/// </summary>
/// 
/*how to call splhandler
 
            SPLHandler splhandler = new SPLHandler();
            splhandler.filename = @"E:\SPL\170806122238-NOPDF.spl";
            splhandler.outputfilename = @"E:\SPL\output.spl";
            splhandler.binaryChangeSplFile();
 
 */
namespace common
{
    public class SPLHandler
    {
        public SPLHandler()
        {
        }



        public String filename;
        public String outputfilename = "outpufile.spl";

        public  void changeSplFile(string filepath)
        {
            filename = filepath;
            //2017-08-16 add for pdf 留底 开始
            string bkfilepath = filepath + "-bk";
            //FileInfo fi = new FileInfo(filepath);
            //fi.MoveTo(bkfilepath);

            //filename = bkfilepath;
            outputfilename = bkfilepath;
            
            bool ret = binaryChangeSplFile();

            //if (File.Exists(bkfilepath))
            //{
            //    System.IO.File.Delete(bkfilepath);
            //}
            //2017-08-16 add for pdf 留底 结束


            //处理完后删除-bw文件
            try
            {
                if (ret == true)
                {
                    if (File.Exists(bkfilepath))
                    {
                        File.Delete(filename);
                        FileInfo fi = new FileInfo(bkfilepath);
                        fi.MoveTo(filename);

                    }
                    if (File.Exists(bkfilepath))
                    {
                        File.Delete(bkfilepath);
                    }
                }
                else
                {
                    if (File.Exists(bkfilepath))
                    {
                        File.Delete(bkfilepath);
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }


        public void changeSplFile()
        {

            //String filename = "170806122238-NOPDF.spl";
            //FileStream writeStream = File.OpenWrite(filename);// 以写的方式打开
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            String line;
            String filestring = "";
            while ((line = sr.ReadLine()) != null)
            {
                filestring += line;
            }
            String filestring_hex = string2hex(filestring);

            String replace1 = "FILING=OFF";
            String new1 = "FILING=PERMANENCE";


            String str11 = string2hex(replace1);
            String str12 = string2hex(new1);

            int hasi = filestring_hex.IndexOf("46494C494E473D4F4646");
            int has2 = filestring_hex.IndexOf(string2hex("HOLD=OFF"));


            filestring_hex = filestring_hex.Replace(string2hex(replace1), string2hex(new1));
            int has4 = filestring_hex.IndexOf(string2hex(replace1));
            int has3 = filestring_hex.IndexOf(string2hex(new1));

            String replace2 = "HOLD=OFF";
            String new2 = "HOLD=ON";
            filestring_hex = filestring_hex.Replace(string2hex(replace2), string2hex(new2));

            String replace3 = "PUBLICPDF=OFF";
            String new3 = "PUBLICPDF=ON";
            filestring_hex = filestring_hex.Replace(string2hex(replace3), string2hex(new3));

            //检查路径中有没有非法字符
            //查找@PJL SET JOBNAME=
            //查找@PJL SET JOBNAMEW=
            //String jobname_hex = string2hex("@PJL SET JOBNAME=");
            //String jobnamew_hex = string2hex("@PJL SET JOBNAMEW=");
            //int st = filestring_hex.IndexOf(jobname_hex);
            //int ed = filestring_hex.IndexOf(jobnamew_hex);
            //String find_jobname = filestring_hex.Substring(st, ed-st);
            //int flg = hasIllegalChar(find_jobname);
            //if( flg == 1)
            //{
            //     String new_jobname = "EASafePrint";
            //     filestring_hex = filestring_hex.Replace(string2hex(find_jobname), string2hex(new_jobname));   
            //}

            //String PERMANENCE = "PERMANENCE";
            //String PERMANENCE_HEX = string2hex(PERMANENCE);
            //byte[] bt = HexStringToBytes(filestring_hex);
            string resultfile = hex2string(filestring_hex);
            String newfilename = outputfilename;
            //writeFile(bt, troy);

            writtxt(resultfile, newfilename);
        }

        private static int hasIllegalChar(String checkdata)
        {
            int flg = 0;
            int len = checkdata.Length;
            for (int k = 0; k < len; k = k + 2)
            {
                string sub = checkdata.Substring(k, 2);
                if (sub.Equals("5C")) // \
                {
                    flg = 1;
                }
                if (sub.Equals("2F")) // /
                {
                    flg = 1;
                }
                if (sub.Equals("3A")) // :
                {
                    flg = 1;
                }
                if (sub.Equals("2A")) // *
                {
                    flg = 1;
                }
                if (sub.Equals("3F")) // ?
                {
                    flg = 1;
                }
                if (sub.Equals("22")) // \
                {
                    flg = 1;
                }
                if (sub.Equals("3C")) // <
                {
                    flg = 1;
                }
                if (sub.Equals("3E")) // >
                {
                    flg = 1;
                }
                if (sub.Equals("7C")) // /
                {
                    flg = 1;
                }
            }
            return flg;
        }
        public bool binaryChangeSplFile()
        {
            FileStream fs = null;
            BinaryReader br = null;

            FileStream outfs = null;
            BinaryWriter bw = null;


            byte cha;
            int index = 0;
            string head = "";
            string tail = "";
            bool changeflg = true;
            try
            {

                fs = File.Open(filename, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);

                outfs = new FileStream(outputfilename, FileMode.Create); //初始化FileStream对象
                bw = new BinaryWriter(outfs); //创建BinaryWriter对象

                br.BaseStream.Seek(0, SeekOrigin.Begin);

                //int count = 0;
                while (br.BaseStream.Position < br.BaseStream.Length)
                {

                    cha = br.ReadByte();
                    string temp = cha.ToString("X2");
                    head += temp;
                    index++;
                    //if (temp.Equals("29"))
                    //{
                    //    break;
                    //}  
                    if (index > 2160)
                    {
                        break;
                    }
                }


                String replace1 = "FILING=OFF";
                String new1 = "FILING=PERMANENCE";
                //String str11 = string2hex(replace1);
                //String str12 = string2hex(new1);

                String replace2 = "HOLD=OFF";
                String new2 = "HOLD=ON";

                String replace3 = "PUBLICPDF=OFF";
                String new3 = "PUBLICPDF=ON";

                //int hasi = head.IndexOf("46494C494E473D4F4646");
                //int has2 = head.IndexOf(string2hex("HOLD=OFF"));
                //int has2 = head.IndexOf(string2hex(new1));
                //if (has2 > 0)
                //{
                //    fs.Close();
                //    br.Close();
                //    bw.Close(); //关闭BinaryWriter对象
                //    outfs.Close(); 
                //    File.Copy(filename, outputfilename, true);
                //    return;
                //}

                //20190723 update start

                if (head.IndexOf(string2hex(replace1)) < 0)
                {
                    changeflg = false;
                    //return false;
                    fs.Close();
                    br.Close();
                    bw.Close(); 
                    outfs.Close();
                    return changeflg;
                }
                if (head.IndexOf(string2hex(replace2)) < 0)
                {
                    changeflg = false;
                    //return false;
                    fs.Close();
                    br.Close();
                    bw.Close();
                    outfs.Close();
                    return changeflg;
                }

                if (head.IndexOf(string2hex(replace3)) < 0)
                {
                    changeflg = false;
                    //return false ;
                    fs.Close();
                    br.Close();
                    bw.Close();
                    outfs.Close();
                    return changeflg;
                }

                //20190723 update end
                head = head.Replace(string2hex(replace1), string2hex(new1));
                //int has4 = head.IndexOf(string2hex(replace1));
                //int has3 = head.IndexOf(string2hex(new1));

                //String replace2 = "HOLD=OFF";
                //String new2 = "HOLD=ON";


                head = head.Replace(string2hex(replace2), string2hex(new2));

                //String replace3 = "PUBLICPDF=OFF";
                //String new3 = "PUBLICPDF=ON";


                head = head.Replace(string2hex(replace3), string2hex(new3));


                /////////////////////////////////////// 20171230
                //检查路径中有没有非法字符
                //查找@PJL SET JOBNAME=
                //查找@PJL SET JOBNAMEW=
                String jobname_hex = string2hex("@PJL SET JOBNAME=\"");
                String end_hex = "220D0A" + string2hex("@PJL SET JOBNAMEW=");
                int st = head.IndexOf(jobname_hex) + 36;
                int ed = head.IndexOf(end_hex, st);

                //20190809-add start
                if (st < 0 || ed < 0 || st >= ed)
                {
                    changeflg = false;
                    fs.Close();
                    br.Close();
                    bw.Close();
                    outfs.Close();
                    return changeflg;
                }
                //20190809-add end


                String head1 = head.Substring(0, st);
                String find_jobname = head.Substring(st, ed - st);
                String head2 = head.Substring(ed, head.Length - ed);
                int flg = hasIllegalChar(find_jobname);
                if (flg == 1)
                {
                    String new_jobname = "EASafePrint";
                    //head = head.Replace(find_jobname, string2hex(new_jobname));
                    head = head1 + string2hex(new_jobname) + head2;
                }


                String jobnamew_hex = string2hex("@PJL SET JOBNAMEW=\"");
                end_hex = "220D0A" + string2hex("@PJL SET SPOOLTIME=");
                st = head.IndexOf(jobnamew_hex) + 38;
                ed = head.IndexOf(end_hex, st);
                //20190809-add start
                if (st < 0 || ed < 0 || st >= ed)
                {
                    changeflg = false;
                    fs.Close();
                    br.Close();
                    bw.Close();
                    outfs.Close();
                    return changeflg;
                }
                //20190809-add end

                //if (st < 0 || ed < 0)
                //{
                //    changeflg = false;
                //    //return false;
                //}

                head1 = head.Substring(0, st);
                find_jobname = head.Substring(st, ed - st);
                head2 = head.Substring(ed, head.Length - ed);
                flg = hasIllegalChar(find_jobname);
                if (flg == 1)
                {
                    String new_jobname = "EASafePrint";
                    //head = head.Replace(find_jobname, string2hex(new_jobname));
                    head = head1 + string2hex(new_jobname) + head2;

                }
                ///////////////////////////////////////

                //BinaryWriter bw = new BinaryWriter(outfs, Encoding.UTF8);
                //bw.Write(head);
                //byte newchar = br.ReadByte();
                //string strr = newchar.ToString("X2");



                string[] headsplits = split2(head);
                foreach (string bt in headsplits)
                {
                    if (bt == null)
                    {
                        break;
                    }
                    bw.Write((byte)Convert.ToInt32(bt, 16));
                }

                //while (br.BaseStream.Position < br.BaseStream.Length)
                //{
                //    cha = br.ReadByte();
                //    bw.Write(cha);
                //}

                int size = 1024 * 1000;
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    if (br.BaseStream.Length - br.BaseStream.Position >= size)
                    {
                        byte[] buffer = new byte[size];
                        buffer = br.ReadBytes(size);
                        bw.Write(buffer);
                    }
                    else
                    {
                        size = (int)(br.BaseStream.Length - br.BaseStream.Position);
                        byte[] buffer = new byte[size];
                        buffer = br.ReadBytes(size);
                        bw.Write(buffer);
                    }
                }


                //string strSpace = "000000000000000000";
                //string[] endsplits = split2(strSpace);
                //foreach (string bt in endsplits)
                //{
                //    if (bt == null)
                //    {
                //        break;
                //    }
                //    bw.Write((byte)Convert.ToInt32(bt, 16));
                //}

                //bw.Close(); //关闭BinaryWriter对象
                //outfs.Close(); 

                changeflg = true;
            }
            catch (EndOfStreamException e)
            {
                //Console.WriteLine(e.Message);
                //Console.WriteLine("已经读到末尾");
                changeflg = false;
            }
            finally
            {
                //Console.ReadKey();
                fs.Close();
                br.Close();
                bw.Close(); //关闭BinaryWriter对象
                outfs.Close();
            }
            return changeflg;
        }

        String string2hex(string input)
        {
            //String PERMANENCE = "PERMANENCE";
            // PERMANENCE = PERMANENCE.ToString("x8");// 转成16进制
            Console.Out.WriteLine(input);

            String hex_input = "";
            //string input = "Hello World!";
            char[] values = input.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                hex_input += hexOutput;
                //Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
            }
            return hex_input;
        }

        static String hex2string(string input)
        {
            string result = "";
            //string[] hexValuesSplit = input.Split(' ');
            string[] hexValuesSplit = Regex.Split(input, "(?<=\\G.{2})");
            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                result += charValue;
                // Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",hex, value, stringValue, charValue);

            }
            return result;
        }



        public static string writtxt(string html, string file)
        {
            //FileStream fileStream = new FileStream(Environment.CurrentDirectory + "\\" + file, FileMode.Append);
            FileStream fileStream = new FileStream(file, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.Write(html + "\r\n");
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            return "ture";
        }

        public static string[] split2(string head)
        {
            string[] strs = new String[10000];
            int i = 0;


            for (i = 0; i < head.Length / 2; i++)
            {
                char[] temp = { 'a', 'b' };
                temp[0] = head[i * 2];
                temp[1] = head[i * 2 + 1];
                //string strrrrr = new String(temp);
                strs[i] = new String(temp);
            }
            return strs;

        }


        //强制黑白处理
        public void forcePrintBWSplFile(string filepath)
        {
            filename = filepath;
            string bkfilepath = filepath + "-bw";
            FileInfo fi = new FileInfo(filepath);
            fi.MoveTo(bkfilepath);

            filename = bkfilepath;
            outputfilename = filepath;
            binaryChangePrintBWSplFile();

            //处理完后删除-bw文件
            File.Delete(bkfilepath);
        }

        public void binaryChangePrintBWSplFile()
        {
            FileStream fs = null;
            BinaryReader br = null;

            FileStream outfs = null;
            BinaryWriter bw = null;

            //int headpos = 2160;
            int headendpos = 8100;
            byte cha;
            int index = 0;
            string head = "";
            string tail = "";
            try
            {

                fs = File.Open(filename, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);

                outfs = new FileStream(outputfilename, FileMode.Create); //初始化FileStream对象
                bw = new BinaryWriter(outfs); //创建BinaryWriter对象

                br.BaseStream.Seek(0, SeekOrigin.Begin);

                //int count = 0;
                while (br.BaseStream.Position < br.BaseStream.Length)
                {

                    cha = br.ReadByte();
                    string temp = cha.ToString("X2");
                    head += temp;
                    index++;

                    if (index > headendpos)
                    {
                        break;
                    }
                }


                String replace1 = "COLORMODE=AUTO";
                String new1 = "COLORMODE=BW";
                String str11 = string2hex(replace1);
                String str12 = string2hex(new1);

                head = head.Replace(string2hex(replace1), string2hex(new1));

                String replace2 = "COLORMODE=COLOR";
                String new2 = "COLORMODE=BW";
                head = head.Replace(string2hex(replace2), string2hex(new2));


                //对PS打印语言强制黑白打印的实现
                String replace3 = "{/setcolormode where {pop 0 setcolormode}";
                String new3 = "{/setcolormode where {pop 2 setcolormode}";
                head = head.Replace(string2hex(replace3), string2hex(new3));

                //对PS打印语言强制黑白打印的实现
                String replace5 = "{/setcolormode where {pop 1 setcolormode}";
                String new5 = "{/setcolormode where {pop 2 setcolormode}";
                head = head.Replace(string2hex(replace5), string2hex(new5));

                //对PS打印语言强制黑白打印的实现
                String replace4 = "/ProcessColorModel /DeviceCMYK";
                String new4 = "/ProcessColorModel /DeviceGray";
                head = head.Replace(string2hex(replace4), string2hex(new4));


                string[] headsplits = split2(head);
                foreach (string bt in headsplits)
                {
                    if (bt == null)
                    {
                        break;
                    }
                    bw.Write((byte)Convert.ToInt32(bt, 16));
                }

                //while (br.BaseStream.Position < br.BaseStream.Length)
                //{
                //    cha = br.ReadByte();
                //    bw.Write(cha);
                //}
                int size = 1024*1000;
                while(br.BaseStream.Position < br.BaseStream.Length)
                {
                    if (br.BaseStream.Length - br.BaseStream.Position >= size)
                    {
                        byte[] buffer = new byte[size];
                        buffer = br.ReadBytes(size);
                        bw.Write(buffer);
                    }
                    else
                    {
                        size = (int)(br.BaseStream.Length - br.BaseStream.Position);
                        byte[] buffer = new byte[size];
                        buffer = br.ReadBytes(size);
                        bw.Write(buffer);
                    }
                }


            }
            catch (EndOfStreamException e)
            {
                //Console.WriteLine(e.Message);
                //Console.WriteLine("已经读到末尾");
            }
            finally
            {
                //Console.ReadKey();
                fs.Close();
                br.Close();
                bw.Close(); //关闭BinaryWriter对象
                outfs.Close();
            }
        }
    }

}