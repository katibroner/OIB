#region Copyright
//-----------------------------------------------------------------------
// <copyright file="LoggerListView.cs" company="ASM Assembly Systems GmbH & Co. KG">
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
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
#endregion

namespace OIB.Tutorial.Common.Logging
{
    public partial class LoggerListView : UserControl, ILog
    {
        #region Delegates
        private delegate void AddLogItemDelegate(List<ListViewItem> item);
        #endregion

        #region Constructor
        public LoggerListView()
        {
            InitializeComponent();
        }
        #endregion

        #region Implementation of ILog
        public void InfoMessage(string message)
        {
            var item = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 0 };
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, message));

            AddLogItem(new List<ListViewItem> { item });
        }

        public void ErrorMessage(string message, Exception ex = null)
        {
            var itemsList = new List<ListViewItem>();
            var msgItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 2 };
            msgItem.SubItems.Add(new ListViewItem.ListViewSubItem(msgItem, message));
            itemsList.Add(msgItem);

            if (ex != null)
            {
                var exItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 2 };
                exItem.SubItems.Add(new ListViewItem.ListViewSubItem(exItem, ex.Message));
                itemsList.Add(exItem);
                Console.WriteLine(@"ERR (with Exception): {0}\nException: {1}", message, ex);
            }
            AddLogItem(itemsList);
        }

        public void WarnMessage(string message, Exception ex = null)
        {
            var itemsList = new List<ListViewItem>();
            var msgItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 1 };
            msgItem.SubItems.Add(new ListViewItem.ListViewSubItem(msgItem, message));
            itemsList.Add(msgItem);

            if (ex != null)
            {
                var exItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 1 };
                exItem.SubItems.Add(new ListViewItem.ListViewSubItem(exItem, ex.Message));
                itemsList.Add(exItem);
                Console.WriteLine(@"WARN (with Exception): {0}\nException: {1}", message, ex);
            }
            AddLogItem(itemsList);
        }

        public void DebugMessage(string message, Exception ex = null)
        {
            var itemsList = new List<ListViewItem>();
            var msgItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 3 };
            msgItem.SubItems.Add(new ListViewItem.ListViewSubItem(msgItem, message));
            itemsList.Add(msgItem);

            if (ex != null)
            {
                var exItem = new ListViewItem(DateTime.Now.ToString(CultureInfo.InvariantCulture)) { ImageIndex = 3 };
                exItem.SubItems.Add(new ListViewItem.ListViewSubItem(exItem, ex.Message));
                itemsList.Add(exItem);
                Console.WriteLine(@"DBG (with Exception): {0}\nException: {1}", message, ex);
            }
            AddLogItem(itemsList);
        }
        #endregion

        #region Private Methods
        private void AddLogItem(List<ListViewItem> items)
        {
            if (listViewLogger.InvokeRequired)
            {
                listViewLogger.Invoke(new AddLogItemDelegate(AddLogItem), new object[] { items });
                return;
            }
            listViewLogger.BeginUpdate();

            items.ForEach(item => listViewLogger.Items.Add(item));
            
            listViewLogger.EnsureVisible(listViewLogger.Items.Count - 1);

            listViewLogger.EndUpdate();
        }
        #endregion

        #region Event Handlers
        private void listViewLogger_DoubleClick(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            
            if (listView == null) 
                return;
            if (listView.SelectedItems.Count <= 0) 
                return;
            
            var form = new DetailedMessageForm(listView.SelectedItems[0].SubItems[1].Text) { StartPosition = FormStartPosition.CenterParent };
            form.ShowDialog();
        }

        private void listViewLogger_SizeChanged(object sender, EventArgs e)
        {
            listViewLogger.BeginUpdate();
            columnHeaderLoggerMessage.Width = (Width - columnHeaderLoggerTime.Width) - 30;
            listViewLogger.EndUpdate();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewLogger.BeginUpdate();
            listViewLogger.Items.Clear();
            listViewLogger.EndUpdate();
        }
        #endregion
    }
}
