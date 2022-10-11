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
// OSAPrintOverrideSSLPolicy.cs
//
//

#endregion

using System;
using System.Net;

	/// <summary>
	/// Summary description for OverrideSSLPolicy.
	/// </summary>
public class OSAPrintOverrideSSLPolicy : ICertificatePolicy
{
    public OSAPrintOverrideSSLPolicy()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //This method may be implemented in future. 
    //Actually it should check the certificate with the trust store.
    public bool CheckValidationResult(ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, WebRequest request, int certificateProblem)
    {
        return true;
    }
}
