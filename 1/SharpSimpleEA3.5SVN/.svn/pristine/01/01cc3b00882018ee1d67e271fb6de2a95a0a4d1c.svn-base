using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileCommon
{
   public class FileProcess
    {
       public static string getModulePath()
       {
           return Application.StartupPath;
       }
        public static bool writeCommentFile(string filename, string content)
        {
            try
            {
                FileStream myFS = new FileStream(filename, FileMode.Append | FileMode.OpenOrCreate);
                StreamWriter mySW = new StreamWriter(myFS);
                mySW.WriteLine(content);
                mySW.Close();
                myFS.Close();
            }
            catch (IOException ex)
            {
                return false;
            }
            return true;
        }
        public static bool readCommentFile(string filename, List<string> lines)
        {
            try
            {
                FileStream myFS = new FileStream(filename, FileMode.Open);
                StreamReader mySR = new StreamReader(myFS);
                string strTemp;
                while ((strTemp = mySR.ReadLine()) != null)
                {
                    lines.Add(strTemp);
                }
                mySR.Close();
                myFS.Close();
            }
            catch (IOException ex)
            {
                return false;
            }
            return true;
        }
        public static void checkAndDeleteFile(string filepath)
        {
            FileInfo finfo = new FileInfo(filepath);
            if (finfo.Exists)
            {
                File.Delete(filepath);
            }

        }

    public static  bool DeleteDir(string strPath) 
    { 
        try
        { 
            strPath = @strPath.Trim().ToString(); // 判断文件夹是否存在 
            if (System.IO.Directory.Exists(strPath)) 
            { // 获得文件夹数组 
                string[] strDirs = System.IO.Directory.GetDirectories(strPath); // 获得文件数组 
                string[] strFiles = System.IO.Directory.GetFiles(strPath); // 遍历所有子文件夹 
                foreach (string strFile in strFiles)            
                { // 删除文件夹 
                    System.IO.File.Delete(strFile);             
                } // 遍历所有文件 
                foreach (string strdir in strDirs)
                { // 删除文件 
                    System.IO.Directory.Delete(strdir, true);
                }
            } // 成功 
            return true;
        } 
        catch (Exception Exp) // 异常处理       
        { // 异常信息 
                System.Diagnostics.Debug.Write(Exp.Message.ToString());
            return false;         
        }     
     } 
    public static bool FileExist(string filename)
    {
                if (File.Exists(filename))
                    return true;
                else
                    return false;
    }
    public static void DeleteFile(string filename)
    {
        if (File.Exists(filename))
        {
            System.IO.File.Delete(filename);
        }
    }
    public static bool FolderExist(string foldername)
    {
        if (Directory.Exists(foldername))
            return true;
        else
            return false;
    }
    public static void FileCopy(string orignFile, string NewFile)
    {
        File.Copy(orignFile, NewFile, true);
    }
    public static void copyFileToFolder(string orignFile, string folder)
    {
        if (!System.IO.Directory.Exists(folder))
        {
            // 目录不存在，建立目录
            System.IO.Directory.CreateDirectory(folder);
        }
        String sourcePath = orignFile;
        String filename = Path.GetFileName(orignFile);
        String targetPath = Path.Combine(folder, filename);
        bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
        System.IO.File.Copy(sourcePath, targetPath, isrewrite);
    }

    public static void FolderCreate(string orignFolder, string NewFloder)
    {
        FolderCreate(orignFolder);
        Directory.SetCurrentDirectory(orignFolder);
        Directory.CreateDirectory(NewFloder);
    }
    public static void FolderCreate(string foldername)
    {

        if (!FolderExist(foldername))
        {
            Directory.CreateDirectory(foldername);
        }
    }

    public static void MoveFile(string Frompath, string directoryPath)
    {
        //string Frompath = @"D:\Test\OutPut";
        //string directoryPath = @"D:\report";
        try
        {
            string[] pdfList = Directory.GetFiles(Frompath, "*.pdf");  //PDF文件
            
            foreach (string f in pdfList)
            {
                string fName = f.Substring(Frompath.Length + 1);
                try
                {
                    if (!File.Exists(System.IO.Path.Combine(directoryPath, fName)))
                    {
                        File.Copy(System.IO.Path.Combine(Frompath, fName), System.IO.Path.Combine(directoryPath, fName));
                    }
                }
                catch (IOException copyError)
                {
                    //MessageBox.Show(copyError.Message);
                    continue;
                }
            }
            //删除原始文件夹里的文件
            foreach (string f in pdfList)
            {
                try
                {
                    File.Delete(f);
                }
                catch (Exception delError)
                {
                    //MessageBox.Show(copyError.Message);
                    continue;
                }
            }

        }
        catch (DirectoryNotFoundException dirNotFound)
        {
            //MessageBox.Show(dirNotFound.Message);
        }
    }


  }
    
}
