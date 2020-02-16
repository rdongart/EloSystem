namespace SCEloSystemGUI
{
    partial class HeadToHeadSelector
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
            this.tblLOPnlPlayerSelector = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblLOPnlPlayerSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlPlayerSelector
            // 
            this.tblLOPnlPlayerSelector.ColumnCount = 2;
            this.tblLOPnlPlayerSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLOPnlPlayerSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLOPnlPlayerSelector.Controls.Add(this.btnOK, 0, 1);
            this.tblLOPnlPlayerSelector.Controls.Add(this.btnCancel, 1, 1);
            this.tblLOPnlPlayerSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPlayerSelector.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlPlayerSelector.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.tblLOPnlPlayerSelector.Name = "tblLOPnlPlayerSelector";
            this.tblLOPnlPlayerSelector.RowCount = 2;
            this.tblLOPnlPlayerSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLOPnlPlayerSelector.Size = new System.Drawing.Size(874, 342);
            this.tblLOPnlPlayerSelector.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(321, 300);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 8, 16, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 36);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(453, 300);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(16, 8, 4, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HeadToHeadSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 342);
            this.Controls.Add(this.tblLOPnlPlayerSelector);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HeadToHeadSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Player";
            this.tblLOPnlPlayerSelector.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayerSelector;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}