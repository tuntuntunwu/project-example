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
public class SPLHandler
{
    public SPLHandler()
    {
    }
	
        public String filename;

        public String outputfilename="outpufile.spl";

        public void changeSplFile()
        {
            
            //String filename = "170806122238-NOPDF.spl";
            //FileStream writeStream = File.OpenWrite(filename);// 以写的方式打开
            StreamReader sr = new StreamReader(filename,Encoding.Default);
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
			
			//替换网页打印的文件名
            String replace4 = "PUBLICPDF=OFF";
            String new4 = "PUBLICPDF=ON";
            filestring_hex = filestring_hex.Replace(string2hex(replace3), string2hex(new3));   
			
			
            //String PERMANENCE = "PERMANENCE";
            //String PERMANENCE_HEX = string2hex(PERMANENCE);
            //byte[] bt = HexStringToBytes(filestring_hex);
            string resultfile = hex2string(filestring_hex);
            String newfilename = outputfilename;
            //writeFile(bt, troy);

            writtxt(resultfile, newfilename);     
        }

        public void binaryChangeSplFile()
        {
            FileStream fs = null;
            BinaryReader br = null;
            
            FileStream outfs = null;
            BinaryWriter bw = null;


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
                String str11 = string2hex(replace1);
                String str12 = string2hex(new1);

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
                

                    head = head.Replace(string2hex(replace1), string2hex(new1));
                    //int has4 = head.IndexOf(string2hex(replace1));
                    //int has3 = head.IndexOf(string2hex(new1));

                    String replace2 = "HOLD=OFF";
                    String new2 = "HOLD=ON";
                    head = head.Replace(string2hex(replace2), string2hex(new2));

                    String replace3 = "PUBLICPDF=OFF";
                    String new3 = "PUBLICPDF=ON";
                    head = head.Replace(string2hex(replace3), string2hex(new3));


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

                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        cha = br.ReadByte();
                        bw.Write(cha);
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
                
              
            }  
            catch (EndOfStreamException e)  
            {  
                Console.WriteLine(e.Message);  
                Console.WriteLine("已经读到末尾");  
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

     static String string2hex(string input)
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
            FileStream fileStream = new FileStream( file, FileMode.Append);
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


	
}

