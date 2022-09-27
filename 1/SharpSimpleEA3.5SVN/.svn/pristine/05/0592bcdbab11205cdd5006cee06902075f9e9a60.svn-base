#region Copyright SHARP Corporation
//
//	SHARP OSA SDK
//	DirectOsaPrint Application
//
//	Copyright (c) 2005-2011 SHARP CORPORATION. All rights reserved.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
//
// OSAMetadata.cs 
// 
#endregion

using System;
using System.Data;
using System.Data.OleDb;

	/// <summary>
	/// OSAMetadata class provides isolated layer of matadata retrieval, entry,
	/// and validation functions.  Metadata configuration is loaded from xml file,
	/// and saved into individual metadata file alongside printted file.
	/// Note that most of functionalities of this class relies on DateSet and
	/// it's delived object, and can easily generalized by rewriting Load() and
	/// Save() methods.  For example, by modifying Load() and Save() methods,
	/// data can be retrieved and save to DB without changing any other methods.
	/// Load() must be the first method to be called after instantiating this class object. 
	/// Load() method populates DataSet.
	/// </summary>
	public class OSAMetadata : IDisposable
	{
		// currently these values are public access
		// for testing in WebForm2 page, need to be private
		public DataSet m_objSrcDataSet = null;
		public DataTable m_objSrcFieldTable = null;
		public DataTable m_objSrcRangeTable = null;
		public DataTable m_objSrcChoiceTable = null;
		public DataTable m_objSrcOptionTable = null;

		// constructor
		public OSAMetadata()
		{
			// simply create DataSet object
			m_objSrcDataSet = new DataSet();
		}

		// load DataSet from metadata configuration file from metadata configuration
		// file in xml format.  Prior to loading configuration file, schema file
		// for configuration file is loaded so that DataSet loaded will be
		// appropriately interpretted.
		public void Load(string strMetadataConfigFile, string strMetadataSchemaFile)
		{
			// load schama for configuration file first,
			// then load configuration file into source Data Set
			// object
			m_objSrcDataSet.ReadXmlSchema(strMetadataSchemaFile);
			m_objSrcDataSet.ReadXml(strMetadataConfigFile);

			// check to see if all appropriate tables
			// contained in the data set actually is present

			// indicator to see if input config file
			// is in good form
			bool bIsGoodForm = true;

			m_objSrcFieldTable = m_objSrcDataSet.Tables["field"];
			if(m_objSrcFieldTable == null)
			{
				bIsGoodForm = false;
			}

			m_objSrcRangeTable = m_objSrcDataSet.Tables["range"];
			if(m_objSrcRangeTable == null)
			{
				bIsGoodForm = false;
			}

			m_objSrcChoiceTable = m_objSrcDataSet.Tables["choice"];
			if(m_objSrcChoiceTable == null)
			{
				bIsGoodForm = false;
			}

			m_objSrcOptionTable = m_objSrcDataSet.Tables["option"];
			if(m_objSrcOptionTable == null)
			{
				bIsGoodForm = false;
			}

			if(!bIsGoodForm)
			{
				throw new Exception("MATADATA CONFIG FILE IS NOT IN CORRECT FORMAT.");
			}
		}

		// save current data set information into a metadata output
		// file specific for a particular metadata file.  This meta
		// data file will be associated with printted (reference) file
		public void Save(string strMetadataFile, string strRefFile)
		{
			// create brand new data set object that will be
			// populated with subset of data in source data set
			DataSet objDataSet = new DataSet("metadata");

			// construct file info table and its columns where
			// path to printted file information resides
			DataTable objFileInfoTable = new DataTable("fileinfo");
			objFileInfoTable.Columns.Add("path");
			DataRow objRow = objFileInfoTable.NewRow();
			objRow["path"] = strRefFile;
			objFileInfoTable.Rows.Add(objRow);
			objDataSet.Tables.Add(objFileInfoTable);

			// construct field table and its columns where
			// name value pairs of metadata will be stored
			DataTable objFieldTable = new DataTable("field");
			objFieldTable.Columns.Add("name");
			objFieldTable.Columns.Add("value");

			// populate field table from source data set
			for(int i = 0; i < m_objSrcFieldTable.Rows.Count; i++)
			{
				DataRow objNewRow = objFieldTable.NewRow();
				objNewRow["name"] = m_objSrcFieldTable.Rows[i]["name"].ToString();
				objNewRow["value"] = m_objSrcFieldTable.Rows[i]["value"].ToString();
				objFieldTable.Rows.Add(objNewRow);
			}
			objDataSet.Tables.Add(objFieldTable);

			// write all information into destination metadata
			// for a particular printted file
			objDataSet.WriteXml(strMetadataFile);
		}

		// gets number of fields in field table
		public int GetNumOfFields()
		{
			if(m_objSrcFieldTable == null)
			{
				return 0;
			}
			return m_objSrcFieldTable.Rows.Count;
		}

		// gets name of specified field
		public string GetFieldName(int nField)
		{
			if(IsFieldInRange(nField))
			{
				return m_objSrcFieldTable.Rows[nField]["name"].ToString();
			}
			return "";
		}

		// gets type of specified field
		public string GetFieldType(int nField)
		{
			if(IsFieldInRange(nField))
			{
				return m_objSrcFieldTable.Rows[nField]["type"].ToString();
			}
			return "";
		}

		// gets value of specified field
		public string GetFieldValue(int nField)
		{
			if(IsFieldInRange(nField))
			{
				return m_objSrcFieldTable.Rows[nField]["value"].ToString();
			}
			return "";
		}

		// gets range of values for a specified field
		// Note1: this is only for fields that have range of values specified
		// (i.e. <range><min>25</min><max>100</max></range>
		// Note2: only fields with type="number" have fields
		// Note3: if range is not specified, this method returns false
		public bool GetRangeValues(int nField, ref string strMin, ref string strMax)
		{
			try
			{
				if(IsFieldInRange(nField))
				{
					// field_Id in field table associates field_Id in
					// range table
					string strFieldId =
						m_objSrcFieldTable.Rows[nField]["field_Id"].ToString();

					// It iterates through the range table to match the "field_Id" string. 
					// If it matches then returns the min and max value also function returns true. 
					// Otherwise function returns false.
					for(int i = 0; i < m_objSrcRangeTable.Rows.Count; i++)
					{
						DataRow objRow = m_objSrcRangeTable.Rows[i];
						if(objRow["field_Id"].ToString() == strFieldId)
						{
							strMin = objRow["min"].ToString();
							strMax = objRow["max"].ToString();
							return true;
						}
					}
				}
				return false;
			}
			catch(Exception)
			{
				return false;
			}
		}

		// gets values for enumerated fields
		// Note1: this only applies in fiedls with type="enum".
		// (i.e. <type>enum</type> ... 
		// <choice><option>A</option><option>B</option> ... </choice>
		// If enum field does not have options specified, this method
		// returns false
		public bool GetEnumValues(int nField, ref string[] saEnumValues)
		{
			try
			{
				if(!IsFieldInRange(nField))
				{
					return false;
				}

				// field_Id in field table associates field_Id in
				// choice table
				string strFieldId =
					m_objSrcFieldTable.Rows[nField]["field_Id"].ToString();

				// in choice table, find corresponding choice_Id
				// This choice_Id is referenced in option table
				string strChoiceId = "";
				for(int i = 0; i < m_objSrcChoiceTable.Rows.Count; i++)
				{
					DataRow objRow = m_objSrcChoiceTable.Rows[i];
					if(objRow["field_Id"].ToString() == strFieldId)
					{
						strChoiceId = objRow["choice_Id"].ToString();
						break;
					}
				}

				if(strChoiceId == "")
				{
					return false;
				}

				// in option table, find match of choice_Id
				// field.  Matched rows are values of options

				int nItemCount = 0;
				int nRowCount = m_objSrcOptionTable.Rows.Count;

				// first find out number of matches
				for(int j = 0; j < nRowCount; j++)
				{
					DataRow objRow = m_objSrcOptionTable.Rows[j];
					if(objRow["choice_Id"].ToString() == strChoiceId)
					{
						nItemCount++;
					}
				}

				if(nItemCount == 0)
				{
					return false;
				}

				// once number of matches are found,
				// array of strings to contain option values
				// can be created
				saEnumValues = new string[nItemCount];
				
				// populate saEnumValues with option's text contents
				int nCount = 0;
				for(int k = 0; k < nRowCount; k++)
				{
					DataRow objRow = m_objSrcOptionTable.Rows[k];
					if(objRow["choice_Id"].ToString() == strChoiceId)
					{
						saEnumValues[nCount] = 
							objRow["option_Text"].ToString();
						nCount++;
					}
				}

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		// sets value for a specified field
		public bool SetFieldValue(int nField, string strNewValue)
		{
			// quietly validate first
			if(!IsValid(nField, strNewValue))
			{
				return false;
			}

			m_objSrcFieldTable.Rows[nField]["value"] = strNewValue;
			return true;
		}

		// examines to see whether a value specified for a specified
		// field, but this method does not set value
		public bool IsValid(int nField, string strValue)
		{
			if(IsFieldInRange(nField))
			{
				string strType =
					m_objSrcFieldTable.Rows[nField]["type"].ToString();
				
				// depending on type of field, different
				// processes are required to find out validity
				bool bValid = false;
				switch(strType)
				{
					case "text":
						bValid = IsValidText(nField, strValue);
						break;
					case "number":
						bValid = IsValidNumber(nField, strValue);
						break;
					case "enum":
						bValid = IsValidEnum(nField, strValue);
						break;
					case "date":
						bValid = IsValidDate(nField, strValue);
						break;
					default:
						break;
				}

				return bValid;
			}
			return false;
		}

		// examines validity of text field
		private bool IsValidText(int nField, string strVal)
		{
			// currently any text is valid entry
			return true;
		}
		
		// examines validity of number field
		private bool IsValidNumber(int nField, string strVal)
		{
			try
			{
				// if a string is not null, try to validate it as number.
				if (!strVal.Equals(string.Empty))
				{
					double dVal = Double.Parse(strVal);

					string strFieldId =
						m_objSrcFieldTable.Rows[nField]["field_Id"].ToString();

					// if range is specified, this value must be within
					// specified range, otherwise it is invalid
					for(int i = 0; i < m_objSrcRangeTable.Rows.Count; i++)
					{
						DataRow objRow = m_objSrcRangeTable.Rows[i];
						if(objRow["field_Id"].ToString() == strFieldId)
						{
							double dMin = Double.Parse(objRow["min"].ToString());
							double dMax = Double.Parse(objRow["max"].ToString());
							if(dVal >= dMin && dVal <= dMax)
							{
								return true;
							}
							return false;
						}
					}
				}

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		// examines validity of enum field 
		private bool IsValidEnum(int nField, string strVal)
		{
			try
			{
				string strFieldId =
					m_objSrcFieldTable.Rows[nField]["field_Id"].ToString();
				string strChoiceId = "";

				// refer to choice table where its choice_Id associates
				// with that in option table
				for(int i = 0; i < m_objSrcChoiceTable.Rows.Count; i++)
				{
					DataRow objRow = m_objSrcChoiceTable.Rows[i];
					if(objRow["field_Id"].ToString() == strFieldId)
					{
						strChoiceId = objRow["choice_Id"].ToString();
						break;
					}
				}

				if(strChoiceId == "")
				{
					return false;
				}

				// in option table, find matched entry.
				// If match found, it is valid value, otherwise
				// it is invalid.
				for(int j = 0; j < m_objSrcOptionTable.Rows.Count; j++)
				{
					DataRow objRow = m_objSrcOptionTable.Rows[j];
					if(objRow["choice_Id"].ToString() == strChoiceId)
					{
						if(objRow["option_Text"].ToString() == strVal)
						{
							return true;
						}
					}
				}

				return false;
			}
			catch(Exception)
			{
				return false;
			}
		}

		// examines validity of data field
		private bool IsValidDate(int nField, string strVal)
		{
			try
			{
				// if a string is not null, try to validate it as date.
				if (!strVal.Equals(string.Empty))
				{
					// if it can be successfully parsed as Date,
					// it is valid.
					DateTime dtVal = DateTime.Parse(strVal);
				}

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		// examines to see if specified field index is
		// in fact within a range
		private bool IsFieldInRange(int nField)
		{
			if(m_objSrcFieldTable == null)
			{
				return false;
			}

			int nFieldCount = m_objSrcFieldTable.Rows.Count;
			if(nField >= 0 && nField < nFieldCount)
			{
				return true;
			}

			return false;
		}

        #region IDisposable Members

        public void Dispose()
        {
            m_objSrcDataSet.Dispose();
            m_objSrcFieldTable.Dispose();
            m_objSrcRangeTable.Dispose();
            m_objSrcChoiceTable.Dispose();
            m_objSrcOptionTable.Dispose();
        }

        #endregion
    }

