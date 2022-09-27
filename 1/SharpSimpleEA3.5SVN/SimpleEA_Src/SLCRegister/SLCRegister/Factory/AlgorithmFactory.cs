using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SLCRegister
{
    public class AlgorithmFactory
    {
        public static IAlgorithm GetAlgorithm(string algorithmName)
        {
            IAlgorithm alg =null ;
            try
            {
                alg = (IAlgorithm)Assembly.Load("SLCRegister").CreateInstance("SLCRegister." + algorithmName);
            }
            catch(Exception e)
            {
                alg = new DESAlgorithm();
                throw e;
            }
            return alg;
        }
    }
}
