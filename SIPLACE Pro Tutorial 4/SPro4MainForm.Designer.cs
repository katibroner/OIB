using System.Windows.Forms;

namespace OIB.Tutorial.SiplacePro._4
{
    partial class SPro4MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBoxLeftSession;
        private GroupBox groupBoxRightSession;
        private Common.Logging.LoggerListView loggerListView;


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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loggerListView = new OIB.Tutorial.Common.Logging.LoggerListView();
            this.groupBoxLeftSession = new System.Windows.Forms.GroupBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.buttonLeftUnsubscribePropertiesUpdates = new System.Windows.Forms.Button();
            this.buttonLeftSubscribePropertiesUpdates = new System.Windows.Forms.Button();
            this.labelLeftSiplaceProEndpoint = new System.Windows.Forms.Label();
            this.buttonLeftDeleteComponent = new System.Windows.Forms.Button();
            this.buttonLeftUpdateComponent = new System.Windows.Forms.Button();
            this.buttonLeftReadComponent = new System.Windows.Forms.Button();
            this.buttonLeftCreateComponent = new System.Windows.Forms.Button();
            this.buttonLeftConnectSiplacePro = new System.Windows.Forms.Button();
            this.textBoxLeftSiplaceProAdapterAddress = new System.Windows.Forms.TextBox();
            this.groupBoxRightSession = new System.Windows.Forms.GroupBox();
            this.buttonRightUnsubscribePropertiesUpdates = new System.Windows.Forms.Button();
            this.labelRightSiplaceProEndpoint = new System.Windows.Forms.Label();
            this.buttonRightSubscribePropertiesUpdates = new System.Windows.Forms.Button();
            this.buttonRightDeleteComponent = new System.Windows.Forms.Button();
            this.buttonRightConnectSiplacePro = new System.Windows.Forms.Button();
            this.buttonRightUpdateComponent = new System.Windows.Forms.Button();
            this.textBoxRightSiplaceProAdapterAddress = new System.Windows.Forms.TextBox();
            this.buttonRightReadComponent = new System.Windows.Forms.Button();
            this.buttonRightCreateComponent = new System.Windows.Forms.Button();
            this.splitterLogMessageWindow = new System.Windows.Forms.Splitter();
            this.groupBoxLeftSession.SuspendLayout();
            this.groupBoxRightSession.SuspendLayout();
            this.SuspendLayout();
            // 
            // loggerListView
            // 
            this.loggerListView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loggerListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loggerListView.Location = new System.Drawing.Point(0, 300);
            this.loggerListView.Margin = new System.Windows.Forms.Padding(4);
            this.loggerListView.Name = "loggerListView";
            this.loggerListView.Size = new System.Drawing.Size(1004, 178);
            this.loggerListView.TabIndex = 0;
            // 
            // groupBoxLeftSession
            // 
            this.groupBoxLeftSession.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftUnsubscribePropertiesUpdates);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftSubscribePropertiesUpdates);
            this.groupBoxLeftSession.Controls.Add(this.labelLeftSiplaceProEndpoint);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftDeleteComponent);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftUpdateComponent);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftReadComponent);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftCreateComponent);
            this.groupBoxLeftSession.Controls.Add(this.buttonLeftConnectSiplacePro);
            this.groupBoxLeftSession.Controls.Add(this.textBoxLeftSiplaceProAdapterAddress);
            this.groupBoxLeftSession.Location = new System.Drawing.Point(21, 12);
            this.groupBoxLeftSession.Name = "groupBoxLeftSession";
            this.groupBoxLeftSession.Size = new System.Drawing.Size(472, 276);
            this.groupBoxLeftSession.TabIndex = 1;
            this.groupBoxLeftSession.TabStop = false;
            this.groupBoxLeftSession.Text = "SIPLACE Pro Adapter Left";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(29, 69);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 9;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // buttonLeftUnsubscribePropertiesUpdates
            // 
            this.buttonLeftUnsubscribePropertiesUpdates.Enabled = false;
            this.buttonLeftUnsubscribePropertiesUpdates.Location = new System.Drawing.Point(189, 158);
            this.buttonLeftUnsubscribePropertiesUpdates.Name = "buttonLeftUnsubscribePropertiesUpdates";
            this.buttonLeftUnsubscribePropertiesUpdates.Size = new System.Drawing.Size(155, 23);
            this.buttonLeftUnsubscribePropertiesUpdates.TabIndex = 8;
            this.buttonLeftUnsubscribePropertiesUpdates.Text = "Unsubscribe Properties Updates";
            this.buttonLeftUnsubscribePropertiesUpdates.UseVisualStyleBackColor = true;
            this.buttonLeftUnsubscribePropertiesUpdates.Click += new System.EventHandler(this.buttonLeftUnsubscribePropertiesUpdates_Click);
            // 
            // buttonLeftSubscribePropertiesUpdates
            // 
            this.buttonLeftSubscribePropertiesUpdates.Location = new System.Drawing.Point(189, 129);
            this.buttonLeftSubscribePropertiesUpdates.Name = "buttonLeftSubscribePropertiesUpdates";
            this.buttonLeftSubscribePropertiesUpdates.Size = new System.Drawing.Size(155, 23);
            this.buttonLeftSubscribePropertiesUpdates.TabIndex = 7;
            this.buttonLeftSubscribePropertiesUpdates.Text = "Subscribe Properties Updates";
            this.buttonLeftSubscribePropertiesUpdates.UseVisualStyleBackColor = true;
            this.buttonLeftSubscribePropertiesUpdates.Click += new System.EventHandler(this.buttonLeftSubscribePropertiesUpdates_Click);
            // 
            // labelLeftSiplaceProEndpoint
            // 
            this.labelLeftSiplaceProEndpoint.AutoSize = true;
            this.labelLeftSiplaceProEndpoint.Location = new System.Drawing.Point(6, 22);
            this.labelLeftSiplaceProEndpoint.Name = "labelLeftSiplaceProEndpoint";
            this.labelLeftSiplaceProEndpoint.Size = new System.Drawing.Size(151, 13);
            this.labelLeftSiplaceProEndpoint.TabIndex = 6;
            this.labelLeftSiplaceProEndpoint.Text = "SIPLACE Pro Adapter Address";
            // 
            // buttonLeftDeleteComponent
            // 
            this.buttonLeftDeleteComponent.Location = new System.Drawing.Point(29, 216);
            this.buttonLeftDeleteComponent.Name = "buttonLeftDeleteComponent";
            this.buttonLeftDeleteComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonLeftDeleteComponent.TabIndex = 5;
            this.buttonLeftDeleteComponent.Text = "Delete Compoent";
            this.buttonLeftDeleteComponent.UseVisualStyleBackColor = true;
            this.buttonLeftDeleteComponent.Click += new System.EventHandler(this.buttonLeftDeleteComponent_Click);
            // 
            // buttonLeftUpdateComponent
            // 
            this.buttonLeftUpdateComponent.Location = new System.Drawing.Point(29, 187);
            this.buttonLeftUpdateComponent.Name = "buttonLeftUpdateComponent";
            this.buttonLeftUpdateComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonLeftUpdateComponent.TabIndex = 4;
            this.buttonLeftUpdateComponent.Text = "Update Component";
            this.buttonLeftUpdateComponent.UseVisualStyleBackColor = true;
            this.buttonLeftUpdateComponent.Click += new System.EventHandler(this.buttonLeftUpdateComponent_Click);
            // 
            // buttonLeftReadComponent
            // 
            this.buttonLeftReadComponent.Location = new System.Drawing.Point(29, 158);
            this.buttonLeftReadComponent.Name = "buttonLeftReadComponent";
            this.buttonLeftReadComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonLeftReadComponent.TabIndex = 3;
            this.buttonLeftReadComponent.Text = "Read Component";
            this.buttonLeftReadComponent.UseVisualStyleBackColor = true;
            this.buttonLeftReadComponent.Click += new System.EventHandler(this.buttonLeftReadComponent_Click);
            // 
            // buttonLeftCreateComponent
            // 
            this.buttonLeftCreateComponent.Location = new System.Drawing.Point(29, 129);
            this.buttonLeftCreateComponent.Name = "buttonLeftCreateComponent";
            this.buttonLeftCreateComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonLeftCreateComponent.TabIndex = 2;
            this.buttonLeftCreateComponent.Text = "Create Component";
            this.buttonLeftCreateComponent.UseVisualStyleBackColor = true;
            this.buttonLeftCreateComponent.Click += new System.EventHandler(this.buttonLeftCreateComponent_Click);
            // 
            // buttonLeftConnectSiplacePro
            // 
            this.buttonLeftConnectSiplacePro.Location = new System.Drawing.Point(29, 92);
            this.buttonLeftConnectSiplacePro.Name = "buttonLeftConnectSiplacePro";
            this.buttonLeftConnectSiplacePro.Size = new System.Drawing.Size(144, 23);
            this.buttonLeftConnectSiplacePro.TabIndex = 1;
            this.buttonLeftConnectSiplacePro.Text = "Connect SIPLACE Pro";
            this.buttonLeftConnectSiplacePro.UseVisualStyleBackColor = true;
            this.buttonLeftConnectSiplacePro.Click += new System.EventHandler(this.buttonLeftConnectSiplacePro_Click);
            // 
            // textBoxLeftSiplaceProAdapterAddress
            // 
            this.textBoxLeftSiplaceProAdapterAddress.Location = new System.Drawing.Point(6, 38);
            this.textBoxLeftSiplaceProAdapterAddress.Name = "textBoxLeftSiplaceProAdapterAddress";
            this.textBoxLeftSiplaceProAdapterAddress.Size = new System.Drawing.Size(460, 20);
            this.textBoxLeftSiplaceProAdapterAddress.TabIndex = 0;
            this.textBoxLeftSiplaceProAdapterAddress.Text = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro/Secure";
            // 
            // groupBoxRightSession
            // 
            this.groupBoxRightSession.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRightSession.Controls.Add(this.buttonRightUnsubscribePropertiesUpdates);
            this.groupBoxRightSession.Controls.Add(this.labelRightSiplaceProEndpoint);
            this.groupBoxRightSession.Controls.Add(this.buttonRightSubscribePropertiesUpdates);
            this.groupBoxRightSession.Controls.Add(this.buttonRightDeleteComponent);
            this.groupBoxRightSession.Controls.Add(this.buttonRightConnectSiplacePro);
            this.groupBoxRightSession.Controls.Add(this.buttonRightUpdateComponent);
            this.groupBoxRightSession.Controls.Add(this.textBoxRightSiplaceProAdapterAddress);
            this.groupBoxRightSession.Controls.Add(this.buttonRightReadComponent);
            this.groupBoxRightSession.Controls.Add(this.buttonRightCreateComponent);
            this.groupBoxRightSession.Location = new System.Drawing.Point(520, 12);
            this.groupBoxRightSession.Name = "groupBoxRightSession";
            this.groupBoxRightSession.Size = new System.Drawing.Size(472, 276);
            this.groupBoxRightSession.TabIndex = 2;
            this.groupBoxRightSession.TabStop = false;
            this.groupBoxRightSession.Text = "SIPLACE Pro Adapter Right";
            // 
            // buttonRightUnsubscribePropertiesUpdates
            // 
            this.buttonRightUnsubscribePropertiesUpdates.Enabled = false;
            this.buttonRightUnsubscribePropertiesUpdates.Location = new System.Drawing.Point(194, 144);
            this.buttonRightUnsubscribePropertiesUpdates.Name = "buttonRightUnsubscribePropertiesUpdates";
            this.buttonRightUnsubscribePropertiesUpdates.Size = new System.Drawing.Size(155, 23);
            this.buttonRightUnsubscribePropertiesUpdates.TabIndex = 10;
            this.buttonRightUnsubscribePropertiesUpdates.Text = "Unsubscribe Properties Updates";
            this.buttonRightUnsubscribePropertiesUpdates.UseVisualStyleBackColor = true;
            this.buttonRightUnsubscribePropertiesUpdates.Click += new System.EventHandler(this.buttonRightUnsubscribePropertiesUpdates_Click);
            // 
            // labelRightSiplaceProEndpoint
            // 
            this.labelRightSiplaceProEndpoint.AutoSize = true;
            this.labelRightSiplaceProEndpoint.Location = new System.Drawing.Point(6, 22);
            this.labelRightSiplaceProEndpoint.Name = "labelRightSiplaceProEndpoint";
            this.labelRightSiplaceProEndpoint.Size = new System.Drawing.Size(151, 13);
            this.labelRightSiplaceProEndpoint.TabIndex = 10;
            this.labelRightSiplaceProEndpoint.Text = "SIPLACE Pro Adapter Address";
            // 
            // buttonRightSubscribePropertiesUpdates
            // 
            this.buttonRightSubscribePropertiesUpdates.Location = new System.Drawing.Point(194, 115);
            this.buttonRightSubscribePropertiesUpdates.Name = "buttonRightSubscribePropertiesUpdates";
            this.buttonRightSubscribePropertiesUpdates.Size = new System.Drawing.Size(155, 23);
            this.buttonRightSubscribePropertiesUpdates.TabIndex = 9;
            this.buttonRightSubscribePropertiesUpdates.Text = "Subscribe Properties Updates";
            this.buttonRightSubscribePropertiesUpdates.UseVisualStyleBackColor = true;
            this.buttonRightSubscribePropertiesUpdates.Click += new System.EventHandler(this.buttonRightSubscribePropertiesUpdates_Click);
            // 
            // buttonRightDeleteComponent
            // 
            this.buttonRightDeleteComponent.Location = new System.Drawing.Point(34, 202);
            this.buttonRightDeleteComponent.Name = "buttonRightDeleteComponent";
            this.buttonRightDeleteComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonRightDeleteComponent.TabIndex = 9;
            this.buttonRightDeleteComponent.Text = "Delete Compoent";
            this.buttonRightDeleteComponent.UseVisualStyleBackColor = true;
            this.buttonRightDeleteComponent.Click += new System.EventHandler(this.buttonRightDeleteComponent_Click);
            // 
            // buttonRightConnectSiplacePro
            // 
            this.buttonRightConnectSiplacePro.Location = new System.Drawing.Point(34, 78);
            this.buttonRightConnectSiplacePro.Name = "buttonRightConnectSiplacePro";
            this.buttonRightConnectSiplacePro.Size = new System.Drawing.Size(144, 23);
            this.buttonRightConnectSiplacePro.TabIndex = 2;
            this.buttonRightConnectSiplacePro.Text = "Connect SIPLACE Pro";
            this.buttonRightConnectSiplacePro.UseVisualStyleBackColor = true;
            this.buttonRightConnectSiplacePro.Click += new System.EventHandler(this.buttonRightConnectSiplacePro_Click);
            // 
            // buttonRightUpdateComponent
            // 
            this.buttonRightUpdateComponent.Location = new System.Drawing.Point(34, 173);
            this.buttonRightUpdateComponent.Name = "buttonRightUpdateComponent";
            this.buttonRightUpdateComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonRightUpdateComponent.TabIndex = 8;
            this.buttonRightUpdateComponent.Text = "Update Component";
            this.buttonRightUpdateComponent.UseVisualStyleBackColor = true;
            this.buttonRightUpdateComponent.Click += new System.EventHandler(this.buttonRightUpdateComponent_Click);
            // 
            // textBoxRightSiplaceProAdapterAddress
            // 
            this.textBoxRightSiplaceProAdapterAddress.Location = new System.Drawing.Point(6, 38);
            this.textBoxRightSiplaceProAdapterAddress.Name = "textBoxRightSiplaceProAdapterAddress";
            this.textBoxRightSiplaceProAdapterAddress.Size = new System.Drawing.Size(460, 20);
            this.textBoxRightSiplaceProAdapterAddress.TabIndex = 1;
            this.textBoxRightSiplaceProAdapterAddress.Text = "net.tcp://ART09:9500/Asm.As.Oib.SiplacePro/Secure";
            // 
            // buttonRightReadComponent
            // 
            this.buttonRightReadComponent.Location = new System.Drawing.Point(34, 144);
            this.buttonRightReadComponent.Name = "buttonRightReadComponent";
            this.buttonRightReadComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonRightReadComponent.TabIndex = 7;
            this.buttonRightReadComponent.Text = "Read Component";
            this.buttonRightReadComponent.UseVisualStyleBackColor = true;
            this.buttonRightReadComponent.Click += new System.EventHandler(this.buttonRightReadComponent_Click);
            // 
            // buttonRightCreateComponent
            // 
            this.buttonRightCreateComponent.Location = new System.Drawing.Point(34, 115);
            this.buttonRightCreateComponent.Name = "buttonRightCreateComponent";
            this.buttonRightCreateComponent.Size = new System.Drawing.Size(144, 23);
            this.buttonRightCreateComponent.TabIndex = 6;
            this.buttonRightCreateComponent.Text = "Create Component";
            this.buttonRightCreateComponent.UseVisualStyleBackColor = true;
            this.buttonRightCreateComponent.Click += new System.EventHandler(this.buttonRightCreateComponent_Click);
            // 
            // splitterLogMessageWindow
            // 
            this.splitterLogMessageWindow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterLogMessageWindow.Enabled = false;
            this.splitterLogMessageWindow.Location = new System.Drawing.Point(0, 297);
            this.splitterLogMessageWindow.Name = "splitterLogMessageWindow";
            this.splitterLogMessageWindow.Size = new System.Drawing.Size(1004, 3);
            this.splitterLogMessageWindow.TabIndex = 3;
            this.splitterLogMessageWindow.TabStop = false;
            // 
            // SPro4MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 478);
            this.Controls.Add(this.splitterLogMessageWindow);
            this.Controls.Add(this.groupBoxRightSession);
            this.Controls.Add(this.groupBoxLeftSession);
            this.Controls.Add(this.loggerListView);
            this.MinimumSize = new System.Drawing.Size(1020, 516);
            this.Name = "SPro4MainForm";
            this.Text = "SIPLACE Pro Tutorial 4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPro4MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.SPro4MainForm_Resize_1);
            this.groupBoxLeftSession.ResumeLayout(false);
            this.groupBoxLeftSession.PerformLayout();
            this.groupBoxRightSession.ResumeLayout(false);
            this.groupBoxRightSession.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonLeftConnectSiplacePro;
        private TextBox textBoxLeftSiplaceProAdapterAddress;
        private Button buttonRightConnectSiplacePro;
        private TextBox textBoxRightSiplaceProAdapterAddress;
        private Button buttonLeftCreateComponent;
        private Button buttonLeftReadComponent;
        private Button buttonLeftDeleteComponent;
        private Button buttonLeftUpdateComponent;
        private Button buttonRightDeleteComponent;
        private Button buttonRightUpdateComponent;
        private Button buttonRightReadComponent;
        private Button buttonRightCreateComponent;
        private Label labelLeftSiplaceProEndpoint;
        private Label labelRightSiplaceProEndpoint;
        private Button buttonLeftUnsubscribePropertiesUpdates;
        private Button buttonLeftSubscribePropertiesUpdates;
        private Button buttonRightUnsubscribePropertiesUpdates;
        private Button buttonRightSubscribePropertiesUpdates;
        private Splitter splitterLogMessageWindow;
        private CheckBox _cbUseClientAuthentication;
    }
}

