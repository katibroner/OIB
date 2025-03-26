
namespace OIB.Tutorial
{
    partial class mainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.textBoxFactoryLayoutElementType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCreateTutorialConfiguration = new System.Windows.Forms.Button();
            this.textBoxFactoryLayoutElementName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.labelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.buttonUnLock = new System.Windows.Forms.Button();
            this.buttonLock = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxStartQuantity = new System.Windows.Forms.TextBox();
            this.textBoxComponent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.groupBoxTest.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 423);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(825, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxTest.Controls.Add(this.buttonTestConnection);
            this.groupBoxTest.Controls.Add(this.textBoxFactoryLayoutElementType);
            this.groupBoxTest.Controls.Add(this.label2);
            this.groupBoxTest.Controls.Add(this.buttonCreateTutorialConfiguration);
            this.groupBoxTest.Controls.Add(this.textBoxFactoryLayoutElementName);
            this.groupBoxTest.Controls.Add(this.label1);
            this.groupBoxTest.Controls.Add(this.textBoxDiscoveryEndpoint);
            this.groupBoxTest.Controls.Add(this.labelServiceLocatorMexCaption);
            this.groupBoxTest.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(801, 193);
            this.groupBoxTest.TabIndex = 17;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Configuration";
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(6, 156);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(323, 23);
            this.buttonTestConnection.TabIndex = 14;
            this.buttonTestConnection.Text = "Test connection to Setup Center";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.ButtonTestConnection_Click);
            // 
            // textBoxFactoryLayoutElementType
            // 
            this.textBoxFactoryLayoutElementType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFactoryLayoutElementType.Location = new System.Drawing.Point(245, 81);
            this.textBoxFactoryLayoutElementType.Name = "textBoxFactoryLayoutElementType";
            this.textBoxFactoryLayoutElementType.Size = new System.Drawing.Size(539, 20);
            this.textBoxFactoryLayoutElementType.TabIndex = 11;
            this.textBoxFactoryLayoutElementType.Text = "ProductionLine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Factory Layout Element Type";
            // 
            // buttonCreateTutorialConfiguration
            // 
            this.buttonCreateTutorialConfiguration.Location = new System.Drawing.Point(6, 127);
            this.buttonCreateTutorialConfiguration.Name = "buttonCreateTutorialConfiguration";
            this.buttonCreateTutorialConfiguration.Size = new System.Drawing.Size(323, 23);
            this.buttonCreateTutorialConfiguration.TabIndex = 0;
            this.buttonCreateTutorialConfiguration.Text = "Create Tutorial Factory Layout and Service Configuration";
            this.buttonCreateTutorialConfiguration.UseVisualStyleBackColor = true;
            this.buttonCreateTutorialConfiguration.Click += new System.EventHandler(this.ButtonCreateTutorialConfiguration_Click);
            // 
            // textBoxFactoryLayoutElementName
            // 
            this.textBoxFactoryLayoutElementName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFactoryLayoutElementName.Location = new System.Drawing.Point(245, 55);
            this.textBoxFactoryLayoutElementName.Name = "textBoxFactoryLayoutElementName";
            this.textBoxFactoryLayoutElementName.Size = new System.Drawing.Size(539, 20);
            this.textBoxFactoryLayoutElementName.TabIndex = 9;
            this.textBoxFactoryLayoutElementName.Text = "Line 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Factory Layout Element";
            // 
            // textBoxDiscoveryEndpoint
            // 
            this.textBoxDiscoveryEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDiscoveryEndpoint.Location = new System.Drawing.Point(245, 16);
            this.textBoxDiscoveryEndpoint.Name = "textBoxDiscoveryEndpoint";
            this.textBoxDiscoveryEndpoint.Size = new System.Drawing.Size(539, 20);
            this.textBoxDiscoveryEndpoint.TabIndex = 7;
            this.textBoxDiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // labelServiceLocatorMexCaption
            // 
            this.labelServiceLocatorMexCaption.AutoSize = true;
            this.labelServiceLocatorMexCaption.Location = new System.Drawing.Point(6, 19);
            this.labelServiceLocatorMexCaption.Name = "labelServiceLocatorMexCaption";
            this.labelServiceLocatorMexCaption.Size = new System.Drawing.Size(123, 13);
            this.labelServiceLocatorMexCaption.TabIndex = 6;
            this.labelServiceLocatorMexCaption.Text = "OIB service locator MEX";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxComment);
            this.groupBox1.Controls.Add(this.buttonUnLock);
            this.groupBox1.Controls.Add(this.buttonLock);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxQuantity);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxStartQuantity);
            this.groupBox1.Controls.Add(this.textBoxComponent);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.buttonUpdate);
            this.groupBox1.Controls.Add(this.buttonRead);
            this.groupBox1.Controls.Add(this.buttonCreate);
            this.groupBox1.Location = new System.Drawing.Point(12, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 201);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Comment";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(260, 135);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(220, 20);
            this.textBoxComment.TabIndex = 16;
            this.textBoxComment.Text = "PU created/changed by OIB Client";
            // 
            // buttonUnLock
            // 
            this.buttonUnLock.Location = new System.Drawing.Point(6, 164);
            this.buttonUnLock.Name = "buttonUnLock";
            this.buttonUnLock.Size = new System.Drawing.Size(126, 23);
            this.buttonUnLock.TabIndex = 15;
            this.buttonUnLock.Text = "UnLock";
            this.buttonUnLock.UseVisualStyleBackColor = true;
            this.buttonUnLock.Click += new System.EventHandler(this.ButtonUnLock_Click);
            // 
            // buttonLock
            // 
            this.buttonLock.Location = new System.Drawing.Point(6, 135);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(126, 23);
            this.buttonLock.TabIndex = 14;
            this.buttonLock.Text = "Lock";
            this.buttonLock.UseVisualStyleBackColor = true;
            this.buttonLock.Click += new System.EventHandler(this.ButtonLock_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(189, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Quantity";
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Location = new System.Drawing.Point(260, 109);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(220, 20);
            this.textBoxQuantity.TabIndex = 12;
            this.textBoxQuantity.Text = "1000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(189, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Start Quantity";
            // 
            // textBoxStartQuantity
            // 
            this.textBoxStartQuantity.Location = new System.Drawing.Point(260, 80);
            this.textBoxStartQuantity.Name = "textBoxStartQuantity";
            this.textBoxStartQuantity.Size = new System.Drawing.Size(220, 20);
            this.textBoxStartQuantity.TabIndex = 10;
            this.textBoxStartQuantity.Text = "1000";
            // 
            // textBoxComponent
            // 
            this.textBoxComponent.Location = new System.Drawing.Point(260, 51);
            this.textBoxComponent.Name = "textBoxComponent";
            this.textBoxComponent.Size = new System.Drawing.Size(220, 20);
            this.textBoxComponent.TabIndex = 8;
            this.textBoxComponent.Text = "CP 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Component";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(260, 19);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(220, 20);
            this.textBoxId.TabIndex = 6;
            this.textBoxId.Text = "1001";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "ID";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(6, 106);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(126, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(6, 77);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(126, 23);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(6, 48);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(126, 23);
            this.buttonRead.TabIndex = 2;
            this.buttonRead.Text = "Read";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.ButtonRead_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(6, 19);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(126, 23);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(7, 104);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 15;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 445);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxTest);
            this.Controls.Add(this.statusStrip1);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBoxTest;
        private System.Windows.Forms.TextBox textBoxDiscoveryEndpoint;
        private System.Windows.Forms.Label labelServiceLocatorMexCaption;
        private System.Windows.Forms.TextBox textBoxFactoryLayoutElementName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFactoryLayoutElementType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCreateTutorialConfiguration;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxStartQuantity;
        private System.Windows.Forms.TextBox textBoxComponent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.Button buttonUnLock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

