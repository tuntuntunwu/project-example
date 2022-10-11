using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace PrintCopyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunCopyPrint runCopy = new RunCopyPrint();
            Thread copyprintthread = new Thread(runCopy.TaskStart);
            copyprintthread.Start();  
            Console.WriteLine("留底文件复制中......");
        }
    }
}
