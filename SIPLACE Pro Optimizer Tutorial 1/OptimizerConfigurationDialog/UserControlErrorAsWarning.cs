#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace OIB.Tutorial
{
    public partial class UserControlErrorAsWarning : UserControl
    {
        private List<int> m_nArrWarningsAsErrors;
        private const string m_strErrorNumberForWarnings = "2272";

        public event EventHandler TreeUpdate;


        public UserControlErrorAsWarning(int[] nArrayAllWarningsAsErrors)
        {
            InitializeComponent();

            //Help file
            string strHelpFileName = "help//OptErrorDialog." + Thread.CurrentThread.CurrentCulture.Parent.Name + ".chm";
            if (!File.Exists(strHelpFileName))
            {
                strHelpFileName = "help//OptErrorDialog.en.chm";
            }

            m_helpProvider.HelpNamespace = strHelpFileName;

            //Fill List
            foreach (int nErrorNumber in nArrayAllWarningsAsErrors)
            {
                //Add a row for each Warning number even if the text is empty
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewWarnings);
                row.Cells[0].Value = false;
                row.Cells[1].Value = nErrorNumber;
                row.Cells[1].ToolTipText = dataGridViewWarnings.Columns[1].ToolTipText;
                row.Cells[2].Value = string.Empty;
                
                dataGridViewWarnings.Rows.Add(row);
            }

            dataGridViewWarnings.Sort(dataGridViewWarnings.Columns[1], ListSortDirection.Ascending);

            //Paste the error number into the string
            string strLinkLabelText = linkLabel1.Text;
            linkLabel1.Text = string.Format(strLinkLabelText, m_strErrorNumberForWarnings);

            int nStartIndex = linkLabel1.Text.IndexOf(m_strErrorNumberForWarnings, StringComparison.Ordinal);

            if (nStartIndex > 0)
            {
                linkLabel1.LinkArea = new LinkArea(nStartIndex, m_strErrorNumberForWarnings.Length);
                linkLabel1.Links[0].LinkData = m_strErrorNumberForWarnings;
            }
        }

        private void UserControlErrorAsWarning_Load(object sender, EventArgs e)
        {
            //Look for the numbers, which are switched on
            if (m_nArrWarningsAsErrors != null)
            {
                foreach (DataGridViewRow row in dataGridViewWarnings.Rows)
                {
                    foreach (int nErrorNumber in m_nArrWarningsAsErrors)
                    {
                        if (row.Cells[1].Value is int && (int) row.Cells[1].Value == nErrorNumber)
                            row.Cells[0].Value = true;
                    }
                }
            }
            if (TreeUpdate != null) TreeUpdate(this, new EventArgs());

            dataGridViewWarnings.ClearSelection();
        }

        public List<int> DlgParamOptimizeWarningsAsErrors
        {
            get { return m_nArrWarningsAsErrors; }
            set { m_nArrWarningsAsErrors = value; }
        }

        private void dataGridViewWarnings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsANonHeaderLinkCell(e))
            {
                MoveToLink(e);
            }
        }

        private bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        {
            if (cellEvent.RowIndex != -1 &&
                dataGridViewWarnings.Columns[cellEvent.ColumnIndex] is DataGridViewLinkColumn)
            {
                return true;
            }
            return false;
        }

        private void MoveToLink(DataGridViewCellEventArgs e)
        {
            //string strLink = dataGridViewPrecedences.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
            if (e.ColumnIndex == 1)
            {
                int nErrorNr;
                try
                {
                    nErrorNr = Convert.ToInt32(dataGridViewWarnings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                catch
                {
                    return;
                }

                if (nErrorNr > 0)
                {
                    Help.ShowHelp(this, m_helpProvider.HelpNamespace, HelpNavigator.KeywordIndex, nErrorNr.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private void dataGridViewWarnings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewWarnings.CurrentCell is DataGridViewCheckBoxCell && dataGridViewWarnings.IsCurrentCellDirty)
                dataGridViewWarnings.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewWarnings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            if (e.RowIndex < 0) return;

            DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell) dataGridViewWarnings.Rows[e.RowIndex].Cells[e.ColumnIndex];

            int nWarningNbr = (int) dataGridViewWarnings.Rows[e.RowIndex].Cells[1].Value;

            if ((bool) checkCell.Value)
            {
                if (!m_nArrWarningsAsErrors.Contains(nWarningNbr))
                    m_nArrWarningsAsErrors.Add(nWarningNbr);
            }
            else
            {
                if (m_nArrWarningsAsErrors.Contains(nWarningNbr))
                    m_nArrWarningsAsErrors.Remove(nWarningNbr);
            }

            dataGridViewWarnings.InvalidateCell(e.ColumnIndex, e.RowIndex);
            if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strErrorNr = e.Link.LinkData as string;
            Help.ShowHelp(this, m_helpProvider.HelpNamespace, HelpNavigator.KeywordIndex, strErrorNr);
        }
    }
}