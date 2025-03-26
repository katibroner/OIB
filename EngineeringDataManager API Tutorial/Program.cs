#region Copyright

// ASM Engineering Data Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Asm.As.EDM.API.DataTransfer;
using Asm.As.EDM.API.DataTransfer.Proxy;

#endregion

namespace SimpleConsoleTest
{
    public class Program
    {
        #region Fields

        private static List<string> s_PendingPushes = new List<string>();

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            #region DOC_CreatePushAPI
            string strMachine = args.Length > 0 ? args[0] : Environment.MachineName;
            using (var pushClient = new Asm.As.EDM.API.DataTransfer.Proxy.EdmPushApiProxy(strMachine))
            {
                #endregion
                #region DOC_Events
                pushClient.PushStatusUpdate += PushClient_PushStatusUpdate;
                pushClient.PushFinished += PushClient_PushFinished;
                #endregion

                #region DOC_Settings
                var settings = new PushSettings
                {
                    AllowReject = false,
                    UseClearingPoolObjectListProtection = true,
                    RequireConfirmation = false
                };

                var parameter = new PushParameter
                {
                    // Required: SIPLACE Pro full path names
                    // Please make sure that all objects share the same ServerObjectType (e.g. "Component:")!
                    // Objects = new[] { @"ComponentShape:System\100" },
                    Objects = new[] {@"Component:ComponentA"},

                    // All other parameters are optional, but setting a display name (and or comment) is recommended,
                    // to improve the readability in the push status monitor...
                    Comment = "Simple Test",
                    DisplayName = "EDM Push Test #1",
                    ObjectTypesToOverwrite = new[] {"Component", "ComponentShape"},
                    Settings = settings
                };
                #endregion

                #region DOC_TriggerPush
                //  Sample using EDM-Line alias, SPI Line and Hostname(all have to be configured in the EDM Line Settings)to push to different lines
                PushHandle handle = pushClient.StartDataPush(
                    parameter, // PushParameter
                    "Line2", // sample: EDM line alias
                    "Line:Line_1", // sample: SIPLACE Pro Full Path name of a line 
                    "Host:HOSTNAMEOFCOMPUTER" // sample: Hostname
                );
                List<string> ids = handle.GetAllPushIds().ToList();
                s_PendingPushes = ids;
                #endregion 

                Console.WriteLine("Pushing data ({0} pushes) - Press enter to exit...", ids.Count);
                Console.ReadLine();
            }
        }

        #region DOC_EventHandler
        private static void PushClient_PushFinished(object sender, PushFinishedEventArgs e)
        {
            PushResult result = e.Result;
            s_PendingPushes.Remove(e.Result.Id);

            Console.WriteLine(
                $"*** Finished: '{result.StatusInfo.DisplayName}' ({result.Id}){Environment.NewLine}" +
                $"\t{(result.Success ? "success" : "failure")} ({result.Duration}){Environment.NewLine}" +
                $"\t[{result.StatusInfo.AdditionalInformation}]{Environment.NewLine}" +
                $"\t<{(s_PendingPushes.Count > 0 ? $"{s_PendingPushes.Count} still running" : "all done!")}>"
            );
        }

        private static void PushClient_PushStatusUpdate(object sender, PushStatusUpdateEventArgs e)
        {
            PushStatusInformation status = e.Status;

            Console.WriteLine(
                $"+++ Status changed for '{status.DisplayName}' ({status.Id}){Environment.NewLine}" +
                $"\tPush to line '{status.EdmLineAlias}'{Environment.NewLine}" +
                $"\tStatus: {status.Status}{Environment.NewLine}" +
                $"\tFinished: {(status.IsFinished ? "yes (" + (status.IsSuccess ? "success" : "failure") + ")" : "no")}"
            );
        }
        #endregion

        #endregion
    }
}