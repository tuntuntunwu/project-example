
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Threading;
using System.Web;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web.Hosting;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CommonOSA
{

    public abstract class BaseHelper
    {
        //--- Session information ---
        private static Dictionary<string, BaseDeviceSession> sessionData = new Dictionary<string, BaseDeviceSession>();
        private static ReaderWriterLock rwLock = new ReaderWriterLock();

        private static string appData = "App_Data";
        private static string sessionFileName = "session_{0}.data";
        private static string save_session_file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, appData + Path.AltDirectorySeparatorChar + sessionFileName);

        //--- Specify the class to deserialize ---
        public enum DeSerializeType
        {
            GetJobSettableElementsResponse = 0,
            GetPrintFileSettableElelementsResponse = 1,
            GetSendFileSettableElementsResponse = 2
        }

        // Get the application setting from WebConfig
        public static string GetAppSettingString(string key)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                return value;
            }
            value = ConfigurationManager.AppSettings.Get(key).ToString();

            return value;
        }

        // Create session information.
        public static bool Create(OsaRequestInfo req, string physicalApplicationPath, ref string errPage, BaseDeviceSession s)
        {
            if (string.IsNullOrEmpty(physicalApplicationPath))
            {
                return false;
            }
            if (null == req)
            {
                return false;
            }

            // Check whether the browser is in the extended OSA mode.
            if (req.GetUserAgent().StartsWith("OpenSystems"))
            {
                if (null == req.GetAlternative())
                {
                    errPage = "FatalErrorForm.aspx?code=INITIALIZATION";
                    return false;
                }
                else
                {
                    errPage = "InitializationError.aspx?code=INITIALIZATION";
                    return false;
                }
            }
            else
            {
                errPage = "InitializationError.aspx";
            }

            // Load a license file.
            string path = Path.Combine(physicalApplicationPath, GetAppSettingString("LicFile"));
            if (!File.Exists(path))
            {
                // License file is not exist.
                errPage += string.Format("?code={0}", "LICENSE");
                return false;
            }

            //--- Check AppWeb port existence ---
            if (string.IsNullOrEmpty(req.GetAppWebPort()) || "0" == req.GetAppWebPort())
            {
                errPage += string.Format("?code=INITIALIZATION");
                // Port number is '0' or null or empty.
                return false;
            }

            //--- Check folder permission ---
            if (!CheckForPermission(appData))
            {
                string code = "ACCESS_FOLDER";
                errPage += string.Format("?code={0}&msg={1}", code, appData);
                return false;
            }

            // Check the session infomation file, and delete it exists.
            try
            {
                string deviceId = req.GetDeviceId();
                string saveFileName = string.Format(save_session_file, deviceId);

                string appDataPath = Path.GetDirectoryName(saveFileName);
                if (!System.IO.Directory.Exists(appDataPath))
                {
                    System.IO.Directory.CreateDirectory(appDataPath);
                }
                using (FileStream fs = new FileStream(saveFileName, FileMode.Create))
                {
                    // Test writting.
                    fs.WriteByte(byte.Parse("1"));
                }
                if (File.Exists(saveFileName))
                {
                    File.Delete(saveFileName);
                }
            }
            catch (UnauthorizedAccessException)
            {
                string code = "ACCESS_FILE";
                string saveFileName = string.Format(sessionFileName, req.GetDeviceId());
                errPage += string.Format("?code={0}&msg={1}", code, saveFileName);
                return false;
            }

            s.req = req;

            // Get a vendor key from the license file.
            XmlDocument licXmlDoc = new XmlDocument();
            try
            {
                licXmlDoc.Load(path);
                XmlNodeList elemList = licXmlDoc.GetElementsByTagName("VendorKey");
                if (elemList.Count > 0)
                {
                    s.vKey = GetAes(req, elemList[0].InnerXml);
                }
            }
            catch (XmlException e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        // Get AES encrypted string.
        private static string GetAes(OsaRequestInfo req, string vKey)
        {
            string uisessionId = req.GetUiSessionId();
            string result = string.Empty;

            string encryKey = uisessionId.Substring(uisessionId.Length - 16);
            byte[] encryKeyBytes = System.Text.Encoding.UTF8.GetBytes(encryKey);

            RijndaelManaged aes = new RijndaelManaged();
            // set a key size, encrypt key and initialize vector.
            aes.KeySize = 128;
            aes.Key = ResizeBytesArray(encryKeyBytes, aes.Key.Length);
            aes.IV = ResizeBytesArray(encryKeyBytes, aes.IV.Length);

            // Convert the vendor key to byte array.
            byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(vKey);

            // Prepare the encrypted data with base 64 encoding.
            using (MemoryStream msOut = new MemoryStream())
            {
                ICryptoTransform aesdecrypt = aes.CreateEncryptor();

                using (CryptoStream cryptStreem = new CryptoStream(msOut, aesdecrypt, CryptoStreamMode.Write))
                {
                    cryptStreem.Write(bytesIn, 0, bytesIn.Length);
                    cryptStreem.FlushFinalBlock();
                    byte[] bytesOut = msOut.ToArray();
                    result = HttpUtility.UrlEncode(Convert.ToBase64String(bytesOut));
                }
            }

            return result;

        }

        // Resize a byte array
        private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
        {
            byte[] newBytes = new byte[newSize];
            int loopCount = (bytes.Length <= newSize) ? bytes.Length : newSize;
            for (int i = 0; i < loopCount; i++)
            {
                newBytes[i] = bytes[i];
            }
            return newBytes;
        }
        
        // Set the session information
        public static void SetSession(string Key, BaseDeviceSession session)
        {
            sessionData[Key] = session;
        }

        // Get the session information
        public static BaseDeviceSession GetSession(string key)
        {
            return (BaseDeviceSession)sessionData[key];
        }

        // Save the session information to a file
        public static void Save(HttpRequest request)
        {
            string deviceId = OsaRequestInfo.GetDeviceId(request);
            Save(deviceId);
        }

        // Save the session information to a file
        public static void Save(string deviceId)
        {
            if (null == sessionData)
            {
                return;
            }
            if (sessionData.Count < 1)
            {
                return;
            }

            try
            {
                // Get writer lock.
                rwLock.AcquireWriterLock(Timeout.Infinite); // 

                string saveFileName = string.Format(save_session_file, deviceId);
                using (FileStream fs = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        bf.Serialize(fs, sessionData[deviceId]);
                    }
                    catch (SerializationException ex)
                    {
                        Debug.WriteLine(ex.StackTrace);
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                    catch (SecurityException ex)
                    {
                        Debug.WriteLine(ex.StackTrace);
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                // Release writer lock.
                rwLock.ReleaseWriterLock(); // 
            }

        }

        // Load a session information from a file
        public static void Load(HttpRequest request)
        {
            string deviceId = OsaRequestInfo.GetDeviceId(request);
            Load(deviceId);
        }

        // Load a session information from a file
        public static void Load(string deviceId)
        {
            string saveFileName = string.Format(save_session_file, deviceId);

            if (!File.Exists(saveFileName))
            {
                return;
            }
            try
            {
                // Get reader lock.
                rwLock.AcquireReaderLock(Timeout.Infinite);
                using (FileStream fs = new FileStream(saveFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        BaseDeviceSession deviceSession = (BaseDeviceSession)bf.Deserialize(fs);
                        if (sessionData.ContainsKey(deviceId))
                        {
                            sessionData[deviceId] = deviceSession;
                        }
                        else
                        {
                            sessionData.Add(deviceId, deviceSession);
                        }
                    }
                    catch (SerializationException ex)
                    {
                        // session data must be broken.
                        Debug.WriteLine(ex.StackTrace);
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                    catch (SecurityException ex)
                    {
                        // Access denied.
                        Debug.WriteLine(ex.StackTrace);
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
            finally
            {
                // Release reader lock.
                rwLock.ReleaseReaderLock(); // 
            }
        }

        //--- Deserialize to json character ---
        public static void JsonDeSerialize(string targetData, BaseDeviceSession s)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            object serData = ser.DeserializeObject(targetData);
            GetJob.Xmldocout res = ConvertToXmlDocDsc(serData);

            s.xmlDocDsc = res;
            s.currentData = GetCurrentData(s.xmlDocDsc);
        }

        //--- Xmldocout is made from json object ---
        private static GetJob.Xmldocout ConvertToXmlDocDsc(object obj)
        {
            GetJob.Xmldocout res = new GetJob.Xmldocout();
            Dictionary<string, object> xmlDocout = DictionaryCast(obj);
            res.deviceinfo = new GetJob.Deviceinfo();

            foreach (string key in xmlDocout.Keys)
            {
                switch (key)
                {
                    case CommonConst.FILE_ID:
                        res.fileId = xmlDocout[key].ToString();
                        break;
                    case CommonConst.DEVICE_INFO:
                        Dictionary<string, object> deviceinfo = DictionaryCast(xmlDocout[key]);
                        foreach (FieldInfo info in res.deviceinfo.GetType().GetFields())
                        {
                            info.SetValue(res.deviceinfo, GetStringValue(deviceinfo, GetSerializeName(info)));
                        }
                        break;
                    default:
                        CreateComplex(res, DictionaryCast(xmlDocout[key]), key);
                        break;

                }
            }

            return res;

        }

        //--- complex is made from json object ---
        private static void CreateComplex(GetJob.Xmldocout res, Dictionary<string, object> obj, string keyName)
        {
            int index = 0;
            if (null == res.complex)
            {
                res.complex = new GetJob.complex[1];
                index = 0;
            }
            else
            {
                Array.Resize<GetJob.complex>(ref res.complex, res.complex.Length + 1);
                index = res.complex.Length - 1;
            }

            res.complex[index] = new GetJob.complex();
            res.complex[index].sysname = keyName;
            res.complex[index].property = new GetJob.Property[obj.Count];

            int i = 0;

            foreach (string key in obj.Keys)
            {
                CreateProperty(ref res.complex[index].property[i], DictionaryCast(obj[key]), key);
                i++;
            }

        }

        //--- Property is made from json object ---
        private static void CreateProperty(ref GetJob.Property res, Dictionary<string, object> obj, string keyName)
        {
            res = new GetJob.Property();
            res.sysname = keyName;
            res.minOccurs = GetIntValue(obj, CommonConst.MIN_OCCURS);
            res.maxOccurs = GetIntValue(obj, CommonConst.MAX_OCCURS);
            res.isType = GetStringValue(obj, CommonConst.IS_TYPE);
            res.value = GetStringValue(obj, CommonConst.VALUE);

            switch(res.isType)
            {       
                case CommonConst.isType.typeList:
                    int len = ((object[])obj[CommonConst.isType.typeList]).Length;
                    res.allowedValueList = new string[len];
                    int i = 0;

                    foreach (string val in (object[])obj[CommonConst.isType.typeList])
                    {
                        res.allowedValueList[i] = val;
                        i++;
                    }
                    break;
                case CommonConst.isType.typeString:

                    res.appInfo = new GetJob.PropertyAppInfo[2];
                    res.appInfo[0] = new GetJob.PropertyAppInfo();
                    res.appInfo[0].name = CommonConst.MIN_LENGTH;
                    res.appInfo[0].Value = GetStringValue(obj, CommonConst.MIN_LENGTH);

                    res.appInfo[1] = new GetJob.PropertyAppInfo();
                    res.appInfo[1].name = CommonConst.MAX_LENGTH;
                    res.appInfo[1].Value = GetStringValue(obj, CommonConst.MAX_LENGTH);
                    break;
                case CommonConst.isType.typeInteger:

                    res.appInfo = new GetJob.PropertyAppInfo[2];
                    res.appInfo[0] = new GetJob.PropertyAppInfo();
                    res.appInfo[0].name = CommonConst.MIN_VALUE;
                    res.appInfo[0].Value = GetStringValue(obj, CommonConst.MIN_VALUE);

                    res.appInfo[1] = new GetJob.PropertyAppInfo();
                    res.appInfo[1].name = CommonConst.MAX_VALUE;
                    res.appInfo[1].Value = GetStringValue(obj, CommonConst.MAX_VALUE);
                    break;
                default:
                    break;
            }

        }

        //--- Cast to Dictionary<string, object> ---
        private static Dictionary<string, object> DictionaryCast(object obj)
        {
            return (Dictionary<string, object>)obj;
        }

        //--- Get serialize name ---
        private static string GetSerializeName(object info)
        {
            string resName = string.Empty;
            if (info is _MemberInfo)
            {
                resName = ((_MemberInfo)info).Name;
            }

            if (info is ICustomAttributeProvider)
            {
                foreach (object ser in ((ICustomAttributeProvider)info).GetCustomAttributes(false))
                {
                    if (ser is XmlElementAttribute)
                    {
                        resName = ((XmlElementAttribute)ser).ElementName;
                    }
                }
            }

            return resName;
        
        }

        //--- Get appropriate character to key ---
        private static string GetStringValue(Dictionary<string, object> target, string key)
        {
            if (target.ContainsKey(key))
            {
                if (target[key] is bool)
                {
                    if ((bool)target[key])
                    {
                        return CommonConst.TRUE;
                    }
                    else
                    {
                        return CommonConst.FALSE;
                    }
                }
                else
                {
                    return target[key].ToString();
                }
            }
            return string.Empty;
        }

        //--- Get appropriate number to key ---
        private static string GetIntValue(Dictionary<string, object> target, string key)
        {
            int val = 0;
            if (target.ContainsKey(key))
            {
                val = (int)target[key];

            }
            return val.ToString();
        }

        // Serialize
        public static string Serialize(object targetData)
        {
            string serialObject = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                // --- XML
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(SetJob.SetJobElements));
                ser.Serialize(ms, targetData);

                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    serialObject = sr.ReadToEnd();
                }
            }
            return serialObject;
        }

        //--- Create display property values class from MFP parameter ---
        protected static Dictionary<string, List<string>> GetCurrentData(GetJob.Xmldocout xmlDocDsc)
        {

            Dictionary<string, List<string>> currentData = new Dictionary<string, List<string>>();
            List<string> curVal = new List<string>();

            foreach (GetJob.Property property in xmlDocDsc.complex[0].property)
            {
                curVal = new List<string>();
                curVal.Add(property.value);
                currentData.Add(property.sysname, curVal);
            }
            return currentData;
        
        }

        // Convert job parameters for Web API
        public static SetJob.SetJobElements ConvertToSetJobElements(BaseDeviceSession s)
        {
            SetJob.SetJobElements xmlDocSet = new SetJob.SetJobElements();
            xmlDocSet.generic = null;
            xmlDocSet.complex = ConvertToComplex(s.xmlDocDsc.complex, s.currentData);
            return xmlDocSet;
        }

        // Sub function for converting job parameters for Web API
        public static SetJob.complex[] ConvertToComplex(GetJob.complex[] GetComplex, Dictionary<string, List<string>> currentData)
        {

            List<SetJob.complex> listComplex = new List<SetJob.complex>();
            List<SetJob.Property> listProperty = new List<SetJob.Property>();
            SetJob.complex setComplex = null;

            //--- Property ----
            //--- The property key loop ---
            foreach (string key in currentData.Keys)
            {
                //--- Get display property value ---
                List<string> curVal = currentData[key];
                //--- Get the appropriate MFP parameters for the property key ---
                GetJob.Property[] ret = Array.FindAll(GetComplex[0].property, delegate(GetJob.Property pro) { return pro.sysname == key; });
                //--- Get MFP property value ---
                string defVal = ret[0].value;

                //--- If there is a difference between display property values and MFP property values ---
                if (curVal.Count > 1 || defVal != curVal[0])
                {
                    //--- Loop display property values ---
                    foreach (string value in curVal)
                    {
                        //--- Create properties for job execution ---
                        SetJob.Property setPro = new SetJob.Property();
                        setPro.sysname = key;
                        setPro.Value = value;
                        listProperty.Add(setPro);
                    }
                }
            }


            setComplex = new SetJob.complex();
            setComplex.property = listProperty.ToArray();
            setComplex.sysname = GetComplex[0].sysname;
            listComplex.Add(setComplex);


            //--- Event ----
            listProperty.Clear();
            int count = Array.FindAll(GetComplex[1].property, delegate(GetJob.Property pro) { return pro.value != string.Empty; }).Length;

            if (0 == count)
            {
                return listComplex.ToArray();
            }

            //--- The property loop ---
            foreach (GetJob.Property pro in GetComplex[1].property)
            {

                if (!string.IsNullOrEmpty(pro.value))
                {
                    //--- Create properties for job execution ---
                    SetJob.Property setPro = new SetJob.Property();
                    setPro.sysname = pro.sysname;
                    setPro.Value = pro.value;
                    listProperty.Add(setPro);
                }

            }

            setComplex = new SetJob.complex();
            setComplex.property = listProperty.ToArray();
            setComplex.sysname = GetComplex[1].sysname;
            listComplex.Add(setComplex);

            return listComplex.ToArray();

        }

        // Set a job event for Web API
        public static void SetEventValue(BaseDeviceSession s, string key, string val)
        {

            GetJob.Property[] properties = s.xmlDocDsc.complex[1].property;

            if (null != properties)
            {
                GetJob.Property[] retPro = Array.FindAll(properties, delegate(GetJob.Property pro) { return pro.sysname == key; });
                if (1 == retPro.Length)
                {
                    //--- Superscription when property exists ---
                    retPro[0].value = val;
                }
                else
                {
                    //--- Addition when property doesn't exist ---
                    Array.Resize<GetJob.Property>(ref properties, properties.Length + 1);
                    properties[properties.Length - 1] = new GetJob.Property();
                    properties[properties.Length - 1].sysname = key;
                    properties[properties.Length - 1].value = val;
                }
            }
            s.xmlDocDsc.complex[1].property = properties;
        }


        // Set a job parameter for Web API
        public static void SetPropertyValue(SetJob.SetJobElements elements, string key, string val)
        {

            SetJob.Property[] properties = elements.complex[0].property;
            if (null != properties)
            {
                SetJob.Property[] retPro = Array.FindAll(properties, delegate(SetJob.Property pro) { return pro.sysname == key; });
                if (1 == retPro.Length)
                {
                    //--- Superscription when property exists ---
                    retPro[0].Value = val;
                }
                else
                {
                    //--- Addition when property doesn't exist ---
                    Array.Resize<SetJob.Property>(ref properties, properties.Length + 1);
                    properties[properties.Length - 1] = new SetJob.Property();
                    properties[properties.Length - 1].sysname = key;
                    properties[properties.Length - 1].Value = val;
                }
            }
            elements.complex[0].property = properties;
        }

        //--- It is checked whether there is a writing authority in the specified folder ---
        public static bool CheckForPermission(string appdatafolder)
        {

            string curappdir = string.Empty;
            curappdir = HostingEnvironment.ApplicationPhysicalPath + appdatafolder;
            try
            {

                DirectorySecurity dirSec = Directory.GetAccessControl(curappdir);

                //--- Get Folder Permission ---
                System.Security.AccessControl.AuthorizationRuleCollection authRule = dirSec.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (FileSystemAccessRule fsar in authRule)
                {
                    //--- Whether user name exists in Folder Permission is checked ---
                    if (fsar.IdentityReference == WindowsIdentity.GetCurrent().User)
                    {
                        //--- It is checked whether there is a writing authority ---
                        if (((int)FileSystemRights.WriteData & (int)fsar.FileSystemRights) == (int)FileSystemRights.WriteData)
                        {
                            return true;
                        }
                    }

                    //--- Whether group name exists in Folder Permission is checked ---
                    if (WindowsIdentity.GetCurrent().Groups.Contains(fsar.IdentityReference))
                    {
                        //--- It is checked whether there is a writing authority ---
                        if (((int)FileSystemRights.WriteData & (int)fsar.FileSystemRights) == (int)FileSystemRights.WriteData)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SecurityException secEx)
            {
                Debug.WriteLine(secEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                Debug.WriteLine(argNullEx.Message);
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine(ioEx.Message);
            }
            catch (UnauthorizedAccessException unAuthEx)
            {
                Debug.WriteLine(unAuthEx.Message);
            }
            return false;
        }

    }
}
