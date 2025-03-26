using System;
using System.Text.RegularExpressions;
using Asm.As.Oib.Common.Utilities;

namespace MaintenanceDataTutorial
{
    internal static class Helper
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <param name="uriKey">The URI key.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="doReplaceHostName">if set to <c>true</c> [do replace host name].</param>
        /// <returns></returns>
         internal static string GetUri(string uriKey, string hostName, bool doReplaceHostName)
        {
            var uri = EndpointHelper.GetAppSettingString(uriKey);
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(uriKey);
            }

            if (doReplaceHostName)
            {
                // look for a host name
                var match = Regex.Match(uri, @"\/\/([\w]+):", RegexOptions.IgnoreCase);

                if (match.Groups.Count > 1)
                {
                    // a host name found, do replace it
                    uri = uri.Replace(match.Groups[1].Value, hostName);
                }
            }

            return uri;
        }
    }
}
