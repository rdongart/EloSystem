using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    partial class ContentFilter<T> where T : EloSystemContent
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
            this.tblLoPnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lbHeader = new System.Windows.Forms.Label();
            this.cbAllowEmpty = new System.Windows.Forms.CheckBox();
            this.tblLoPnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlMain
            // 
            this.tblLoPnlMain.ColumnCount = 2;
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tblLoPnlMain.Controls.Add(this.btnDeselectAll, 1, 3);
            this.tblLoPnlMain.Controls.Add(this.btnSelectAll, 1, 2);
            this.tblLoPnlMain.Controls.Add(this.lbHeader, 0, 0);
            this.tblLoPnlMain.Controls.Add(this.cbAllowEmpty, 1, 1);
            this.tblLoPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlMain.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlMain.Name = "tblLoPnlMain";
            this.tblLoPnlMain.RowCount = 4;
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblLoPnlMain.Size = new System.Drawing.Size(295, 300);
            this.tblLoPnlMain.TabIndex = 0;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeselectAll.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectAll.Location = new System.Drawing.Point(208, 262);
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
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSelectAll.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(208, 218);
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
            this.lbHeader.Size = new System.Drawing.Size(289, 28);
            this.lbHeader.TabIndex = 1;
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAllowEmpty
            // 
            this.cbAllowEmpty.AutoSize = true;
            this.cbAllowEmpty.Checked = true;
            this.cbAllowEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAllowEmpty.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllowEmpty.Location = new System.Drawing.Point(208, 31);
            this.cbAllowEmpty.Name = "cbAllowEmpty";
            this.cbAllowEmpty.Size = new System.Drawing.Size(84, 20);
            this.cbAllowEmpty.TabIndex = 2;
            this.cbAllowEmpty.Text = "Allow empty";
            this.cbAllowEmpty.UseVisualStyleBackColor = true;
            this.cbAllowEmpty.CheckedChanged += new System.EventHandler(this.cbAllowEmpty_CheckedChanged);
            // 
            // ContentFilter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLoPnlMain);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ContentFilter";
            this.Size = new System.Drawing.Size(295, 300);
            this.tblLoPnlMain.ResumeLayout(false);
            this.tblLoPnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlMain;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.CheckBox cbAllowEmpty;
    }
}
