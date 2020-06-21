namespace SCEloSystemGUI.UserControls
{
    partial class GameByPlayerFilter
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

            flagsCache.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblLoPnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lbHeader = new System.Windows.Forms.Label();
            this.tblLoPnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlMain
            // 
            this.tblLoPnlMain.ColumnCount = 2;
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tblLoPnlMain.Controls.Add(this.btnDeselectAll, 0, 2);
            this.tblLoPnlMain.Controls.Add(this.btnSelectAll, 1, 2);
            this.tblLoPnlMain.Controls.Add(this.lbHeader, 0, 0);
            this.tblLoPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLoPnlMain.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlMain.MinimumSize = new System.Drawing.Size(180, 100);
            this.tblLoPnlMain.Name = "tblLoPnlMain";
            this.tblLoPnlMain.RowCount = 3;
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblLoPnlMain.Size = new System.Drawing.Size(180, 316);
            this.tblLoPnlMain.TabIndex = 2;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAll.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectAll.ForeColor = System.Drawing.Color.Black;
            this.btnDeselectAll.Location = new System.Drawing.Point(3, 278);
            this.btnDeselectAll.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(84, 32);
            this.btnDeselectAll.TabIndex = 0;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectAll.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.ForeColor = System.Drawing.Color.Black;
            this.btnSelectAll.Location = new System.Drawing.Point(93, 278);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(84, 32);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.tblLoPnlMain.SetColumnSpan(this.lbHeader, 2);
            this.lbHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeader.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.Location = new System.Drawing.Point(3, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(174, 28);
            this.lbHeader.TabIndex = 1;
            this.lbHeader.Text = "Participant Filter:";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GameByPlayerFilter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLoPnlMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(180, 110);
            this.Name = "GameByPlayerFilter";
            this.Size = new System.Drawing.Size(180, 316);
            this.tblLoPnlMain.ResumeLayout(false);
            this.tblLoPnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlMain;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label lbHeader;
    }
}
