using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace MFPCheckService
{
    class Status
    {
        /// <summary>
        /// DeviceStatus;
        /// </summary>
        public int deviceStatus { get; set; }
        /// <summary>
        /// PrinterStatus;
        /// </summary>
        public int printerStatus { get; set; }

        /// <summary>
        /// DetectedErrorState;
        /// </summary>
        public string errorState { get; set; }

        /// <summary>
        /// Cause of Down or Warning;
        /// </summary>
        public string causeOfDown { get; set; }
    }


}
