#region Copyright
//-----------------------------------------------------------------------
// <copyright file="LoggerListView.Designer.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

namespace OIB.Tutorial.Common.Logging
{
    partial class LoggerListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerListView));
            this.listViewLogger = new System.Windows.Forms.ListView();
            this.columnHeaderLoggerTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLoggerMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListLogger = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewLogger
            // 
            this.listViewLogger.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLoggerTime,
            this.columnHeaderLoggerMessage});
            this.listViewLogger.ContextMenuStrip = this.contextMenuStrip;
            this.listViewLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLogger.FullRowSelect = true;
            this.listViewLogger.Location = new System.Drawing.Point(0, 0);
            this.listViewLogger.MultiSelect = false;
            this.listViewLogger.Name = "listViewLogger";
            this.listViewLogger.Size = new System.Drawing.Size(707, 178);
            this.listViewLogger.SmallImageList = this.imageListLogger;
            this.listViewLogger.TabIndex = 1;
            this.listViewLogger.UseCompatibleStateImageBehavior = false;
            this.listViewLogger.View = System.Windows.Forms.View.Details;
            this.listViewLogger.SizeChanged += new System.EventHandler(this.listViewLogger_SizeChanged);
            this.listViewLogger.DoubleClick += new System.EventHandler(this.listViewLogger_DoubleClick);
            // 
            // columnHeaderLoggerTime
            // 
            this.columnHeaderLoggerTime.Text = "Time";
            this.columnHeaderLoggerTime.Width = 141;
            // 
            // columnHeaderLoggerMessage
            // 
            this.columnHeaderLoggerMessage.Text = "Message";
            this.columnHeaderLoggerMessage.Width = 538;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(157, 26);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All Entries";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // imageListLogger
            // 
            this.imageListLogger.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLogger.ImageStream")));
            this.imageListLogger.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageListLogger.Images.SetKeyName(0, "Information.bmp");
            this.imageListLogger.Images.SetKeyName(1, "Warning.bmp");
            this.imageListLogger.Images.SetKeyName(2, "Critical.bmp");
            this.imageListLogger.Images.SetKeyName(3, "FormulaEvaluator.bmp");
            // 
            // LoggerListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewLogger);
            this.Name = "LoggerListView";
            this.Size = new System.Drawing.Size(707, 178);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewLogger;
        private System.Windows.Forms.ColumnHeader columnHeaderLoggerTime;
        private System.Windows.Forms.ColumnHeader columnHeaderLoggerMessage;
        private System.Windows.Forms.ImageList imageListLogger;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
    }
}
