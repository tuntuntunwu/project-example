using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Model
{
    public class JobTypeInfoCSVModel
    {
        #region Model

        public string JobType = "";
        public int JobTypeID = 0;

        public double AllMoney=0;
        public string getReportData(int Dsp_Count_mode)
        {
            return UtilCommon.doubleToMoney(AllMoney, Dsp_Count_mode);
        }
        #endregion Model
    }
}
