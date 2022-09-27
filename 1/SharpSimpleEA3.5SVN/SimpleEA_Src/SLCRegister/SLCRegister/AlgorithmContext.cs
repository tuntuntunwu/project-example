using System;
using System.Collections.Generic;
using System.Text;

namespace SLCRegister
{
    public class AlgorithmContext
    {
        private IAlgorithm algorithm = null;

        public AlgorithmContext(IAlgorithm tmp)
        {
            algorithm = tmp;
        }

        public string Encrypt(string encryptString)
        {
           return  algorithm.Encrypt(encryptString);
        }

        public string Decrypt(string decryptString)
        {
            return algorithm.Decrypt(decryptString);
        }
    }
}
