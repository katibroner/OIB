//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerHelper.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <email>oib-support.siplace@asmpt.com</email>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------


#region Namespace

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;

using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Service;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Data;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// Service locator helper managing the service locator proxy using http protocol. Use the pattern:
    /// 
    /// using (ConfigurationManagerHelper configurationManagerHelper = new ConfigurationManagerHelper(configurationManagerMexUri))
    /// {
    ///     ...
    /// } // end of using closes open connections (or aborts them if they are faulted.
    /// </summary>
    public class ConfigurationManagerHelper : ServiceHelper<IConfigurationManager>
    {
        #region Constructor

        public ConfigurationManagerHelper(Uri mexUri) : base(mexUri)
        {
        }

        #endregion

        #region Methods
        
        public void ResetConfiguration()
        {
            if (this.Disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            // 1. Load the configuration manager which holds the OIB configuration
            ConfigurationManager configurationManager = this.Proxy.Load();

            // 2. Rename the enterprise object
            Enterprise enterprise = configurationManager.Enterprise;
            enterprise.Name = "Enterprise";
            enterprise.Sites.Clear();   // delte old configuration, if any, to be on the save side.
            this.Proxy.Save(configurationManager);
        }

        public void CreateTutorialFactoryLayout()
        {
            if (this.Disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            // 1. Load the configuration manager which holds the OIB configuration
            ConfigurationManager configurationManager = this.Proxy.Load();

            // 2. Rename the enterprise object
            Enterprise enterprise = configurationManager.Enterprise;
            enterprise.Name = "Nano Placement Inc.";
            enterprise.Sites.Clear();   // delte old configuration, if any, to be on the save side.

            // 3. Create site 'Munich' and link it to the enterprise element
            Site site = new Site();
            site.Name = "Munich";
            site.ProductionLines = new ProductionLineCollection();
            enterprise.Sites.Add(site);

            // 4. Create production line 'Line 1' and link it to the site 'Munich'
            ProductionLine line1 = new ProductionLine();
            line1.Name = "Line 1";
            site.ProductionLines.Add(line1);

            // 5. Create production line 'Line 2' and link it to the site 'Munich'
            ProductionLine line2 = new ProductionLine();
            line2.Name = "Line 2";
            site.ProductionLines.Add(line2);

            // 6. overwrite old config with new configuration.
            this.Proxy.Save(configurationManager);
        }

        #endregion

        /// <summary>
        /// Finds the parent of the given factory layout element.
        /// </summary>
        /// <param name="configurationManager">The full factory layout</param>
        /// <param name="isa95Base">The factory layout element</param>
        /// <returns>The parent of the given factory layout element or null, on no parent was found.</returns>
        public static Isa95Base GetParent(ConfigurationManager configurationManager, Isa95Base isa95Base)
        {
            if (configurationManager == null)
                throw new ArgumentNullException("configurationManager");

            if (isa95Base == null)
                throw new ArgumentNullException("isa95Base");

            if (configurationManager.Enterprise == null)
            {
                return null;
            }

            foreach (Site site in configurationManager.Enterprise.Sites)
            {
                if (site.ID == isa95Base.ID)
                    return configurationManager.Enterprise;

                foreach (Area area in site.Areas)
                {
                    if (area.ID == isa95Base.ID)
                        return site;

                    foreach (ProductionLine productionLine in area.ProductionLines)
                    {
                        if (productionLine.ID == isa95Base.ID)
                            return area;

                        foreach (WorkCell workCell in productionLine.WorkCells)
                        {
                            if (workCell.ID == isa95Base.ID)
                            {
                                return productionLine;
                            }

                            foreach (WorkCell subWorkCell in workCell.SubWorkCells)
                            {
                                if (subWorkCell.ID == isa95Base.ID)
                                    return workCell;
                            }
                        }
                    }
                }
                foreach (ProductionLine productionLine in site.ProductionLines)
                {
                    if (productionLine.ID == isa95Base.ID)
                        return site;

                    foreach (WorkCell workCell in productionLine.WorkCells)
                    {
                        if (workCell.ID == isa95Base.ID)
                        {
                            return productionLine;
                        }

                        foreach (WorkCell subWorkCell in workCell.SubWorkCells)
                        {
                            if (subWorkCell.ID == isa95Base.ID)
                                return workCell;
                        }
                    }
                }
            }

            return null;
        }
    }
}
