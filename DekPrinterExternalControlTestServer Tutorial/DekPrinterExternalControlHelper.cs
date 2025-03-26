#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.Text;
using MesModel = www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;

#endregion

namespace DekPrinterExternalControlTestServer
{
    public static class DekPrinterExternalControlHelper
    {
        public static string GetString(MesModel.VerifyPrinterToolRequest verifyPrinterToolRequest)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SetupPath:'{0}', ", verifyPrinterToolRequest.PrinterTool.SetupPath);
            sb.AppendFormat("LineName:'{0}', ", verifyPrinterToolRequest.PrinterTool.Machine.LineName);
            sb.AppendFormat("MachineName:'{0}', ", verifyPrinterToolRequest.PrinterTool.Machine.MachineName);
            sb.AppendFormat("MachineType:'{0}', ", verifyPrinterToolRequest.PrinterTool.Machine.MachineType);
            sb.AppendFormat("MachineId:'{0}'." + Environment.NewLine, verifyPrinterToolRequest.PrinterTool.Machine.MachineId);

            sb.AppendFormat("PrinterProductInfo: ProductName:'{0}', ", verifyPrinterToolRequest.PrinterTool.ProductInfo.ProductName);
            sb.AppendFormat("ProductId:'{0}', ", verifyPrinterToolRequest.PrinterTool.ProductInfo.ProductId);
            sb.AppendFormat("DateModified:'{0}'." + Environment.NewLine, verifyPrinterToolRequest.PrinterTool.ProductInfo.DateModified);

            sb.AppendFormat("PrinterVerificationItem: Name:'{0}', ", verifyPrinterToolRequest.PrinterTool.Name);
            sb.AppendFormat("VerificationItemId:'{0}', ", verifyPrinterToolRequest.PrinterTool.VerificationItemId);
            sb.AppendFormat("VerificationRequired:'{0}'." + Environment.NewLine, verifyPrinterToolRequest.PrinterTool.VerificationRequired);

            return sb.ToString();
        }

        public static string GetString(MesModel.VerifyPrinterToolResponse verifyPrinterToolResponse)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Verified Tool: Message:'{0}', ", verifyPrinterToolResponse.PrinterTool.Message);
            sb.AppendFormat("VerificationStatus:'{0}'." + Environment.NewLine, verifyPrinterToolResponse.PrinterTool.PrinterValidationStatus);

            return sb.ToString();
        }

        public static string GetString(MesModel.VerifyPrinterMaterialRequest verifyPrinterMaterialRequest)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SetupPath:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.SetupPath);
            sb.AppendFormat("LineName:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.Machine.LineName);
            sb.AppendFormat("MachineName:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.Machine.MachineName);
            sb.AppendFormat("MachineType:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.Machine.MachineType);
            sb.AppendFormat("MachineId:'{0}'." + Environment.NewLine, verifyPrinterMaterialRequest.PrinterMaterial.Machine.MachineId);

            sb.AppendFormat("PrinterProductInfo: ProductName:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.ProductInfo.ProductName);
            sb.AppendFormat("ProductId:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.ProductInfo.ProductId);
            sb.AppendFormat("DateModified:'{0}'." + Environment.NewLine, verifyPrinterMaterialRequest.PrinterMaterial.ProductInfo.DateModified);

            sb.AppendFormat("PrinterVerificationItem: Name:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.Name);
            sb.AppendFormat("VerificationItemId:'{0}', ", verifyPrinterMaterialRequest.PrinterMaterial.VerificationItemId);
            sb.AppendFormat("VerificationRequired:'{0}'." + Environment.NewLine, verifyPrinterMaterialRequest.PrinterMaterial.VerificationRequired);

            sb.AppendFormat("VerificationItemBarcode: '{0}'." + Environment.NewLine, verifyPrinterMaterialRequest.PrinterMaterial.Barcode);

            return sb.ToString();
        }

        public static string GetString(MesModel.VerifyPrinterMaterialResponse verifyPrinterMaterialResponse)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Message:'{0}', ", verifyPrinterMaterialResponse.PrinterMaterial.Message);
            sb.AppendFormat("VerificationStatus:'{0}'." + Environment.NewLine, verifyPrinterMaterialResponse.PrinterMaterial.PrinterValidationStatus);

            return sb.ToString();
        }
    }
}