using System;
using System.Collections.Generic;
using System.Text;

namespace SLCRegister
{
    public abstract class IAlgorithm
    {
        // 偏移量
        protected byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        // 密钥
        protected string encryptKey = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        public abstract string Encrypt(string encryptString);
        public abstract string Decrypt(string decryptString);
    }
}
