#region Copyright
// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.
#endregion

#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.Win32;
using MaterialManagerEventsTutorial.Utility;
using www.asm.com.MaterialManager_2018_11_Contracts_Service;

#endregion using

namespace MaterialManagerEventsTutorial.ViewModels
{
    /// <summary>
    /// Represents event data
    /// </summary>
    public class EventData
    {
        #region event data structure

        public string MessageId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public string Message { get; private set; }
        public DateTime ReceiveDateTime { get; private set; }

        #endregion event data structure

        #region command

        private ICommand _saveAsCommand;
        public ICommand SaveAsCommand => _saveAsCommand ?? (_saveAsCommand = new TransmitCommand(SaveAs));

        #endregion command

        private void SaveAs(object obj)
        {
            try
            {
                var dialog = new SaveFileDialog {Filter = "*.xml|*.xml", DefaultExt = "*.xml"};
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(dialog.FileName, Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private EventData(){}

        /// <summary>
        /// Creates EventData collection from event.
        /// </summary>
        /// <param name="args">The <see cref="MaterialManagerEventReceivedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public static List<EventData> FromEvent(MaterialManagerEventReceivedEventArgs args)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(MaterialManagerMessageRequest));
                serializer.WriteObject(stream, args.Data);

                stream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(stream))
                {
                    var result = streamReader.ReadToEnd();
                    var xml = XDocument.Parse(result);
                    var msg = xml.ToString();

                    var rc = new List<EventData>();
                    var msgId = (xml.Root?.Descendants())?.First(d => d.Name.LocalName == "MsgId").Value ?? "n/a";
                    return CollectEventData(rc, msg, msgId, xml.Root, DateTime.Now, null);
                }
            }
        }

        private static List<EventData> CollectEventData(List<EventData> list, string msg, string msgID, XElement node, DateTime reciveDateTime, string keyPrefix)
        {
            if (!node.HasElements && !string.IsNullOrEmpty(node.Value.Trim()))
            {
                list.Add(new EventData()
                {
                    Message = msg,
                    ReceiveDateTime = reciveDateTime,
                    Key = (keyPrefix == null ? "" : keyPrefix + ".") + node.Name.LocalName,
                    Value = node.Value,
                    MessageId = msgID.Contains(":") ? msgID : msgID + ":00"
                });
            }

            bool performIndex = keyPrefix == "MaterialManagerMessageRequest" && node.Name.LocalName == "MaterialManagerEvents";
            int counter = 1;

            foreach (var desc in node.Elements())
            {
                if (performIndex)
                {
                    CollectEventData(list, msg, $"{msgID}:{counter:D2}", desc, reciveDateTime, $"[{counter:D2}]");
                    counter++;
                }
                else
                {
                    CollectEventData(list, msg, msgID, desc, reciveDateTime, (keyPrefix == null ? "" : keyPrefix + ".") + node.Name.LocalName);
                }
            }

            return list;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(Message)}: {Message}, {nameof(ReceiveDateTime)}: {ReceiveDateTime}";
        }
    }
}