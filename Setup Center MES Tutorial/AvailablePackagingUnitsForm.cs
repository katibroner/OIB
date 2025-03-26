#region Copyright
//-----------------------------------------------------------------------
// <copyright file="AvailablePackagingUnitsForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

using System.Data;
using System.Windows.Forms;

namespace OIB.Tutorial
{
	public partial class AvailablePackagingUnitsForm : Form
	{
		public AvailablePackagingUnitsForm(DataTable dt)
		{
			var bindingSource = new BindingSource();

			InitializeComponent();

            // Set up the DataGridView.
            dataGridView.Dock = DockStyle.Fill;

            // Automatically generate the DataGridView columns.
            dataGridView.AutoGenerateColumns = true;

			// Automatically resize the visible rows.
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Set the DataGridView control's border.
            dataGridView.BorderStyle = BorderStyle.Fixed3D;

			// Set up the data source.
			bindingSource.DataSource = dt;
			dataGridView.DataSource = bindingSource;

            dataGridView.DataError += dataGridView_DataError;
		}

        void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }
	}
}
