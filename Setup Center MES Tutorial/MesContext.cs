#region Copyright
//-----------------------------------------------------------------------
// <copyright file="MesContext.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

#region Namespaces
using System;
using System.Data;
#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// The current MES context
    /// </summary>
    public class MesContext
    {
        public static string PackagingUnitDataXmlFilePath = @"PackagingUnits.xml";
        public static string ComponentStatusDataXmlFilePath = @"ComponentStatus.xml";

        public static DataSet PackagingUnitsDataSet;
        public static DataSet ComponentStatusDataSet;

        public static bool ThrowExceptionEnabled;
        public static bool SleepEnabled;
        public static bool ModifyQuantityEnabled;
        public static bool ModifyOriginalQuantityEnabled;
        public static bool ModifyRohsEnabled;
        public static bool ModifyMsdEnabled;
        public static bool ModifySiplaceProComponentNameEnabled;

        public static int Sleep;
        public static int NewQuantity;
        public static int NewOriginalQuantity;
        public static int NewRohs;
        public static int NewMsdLevel;
        public static DateTime NewMsdOpenDate;
        public static string NewSiplaceProComponentName;
    }
}
