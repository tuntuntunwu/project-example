using System;
using System.Collections.Generic;
using System.Text;

namespace SLCRegister
{
    public class AlgorithmFacade
    {
        public static string Encrypt(string encryptString, string typeID)
        {
            IAlgorithm alg = AlgorithmFactory.GetAlgorithm(Helper.GetXmlAlgorithm(typeID));
            AlgorithmContext context = new AlgorithmContext(alg);
            string result = context.Encrypt(encryptString);
            return result;
        }

        public static string Decrypt(string decryptString, string typeID)
        {
            IAlgorithm alg = AlgorithmFactory.GetAlgorithm(Helper.GetXmlAlgorithm(typeID));
            AlgorithmContext context = new AlgorithmContext(alg);
            string result = context.Decrypt(decryptString);
            return result;
        }
    }
}
