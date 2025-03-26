#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;

#endregion

namespace DekPrinterExternalControlTestServer
{
    public class VerifyMaterialValues
    {
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }
        public string PartNumber { get; set; }
        public int PotLife { get; set; }
        public DateTime PotOpenDate { get; set; }
        public int Quantity { get; set; }
        public string SerialNumber { get; set; }
        public int VerificationStatus { get; set; }
    }
}