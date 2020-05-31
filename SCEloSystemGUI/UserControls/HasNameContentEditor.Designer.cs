namespace SCEloSystemGUI.UserControls
{
    partial class HasNameContentEditor<T> 
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
            this.cmbBxContent = new System.Windows.Forms.ComboBox();
            this.lbHeading = new System.Windows.Forms.Label();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSelectHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tblLoPnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlMain
            // 
            this.tblLoPnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLoPnlMain.ColumnCount = 4;
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Controls.Add(this.cmbBxContent, 1, 1);
            this.tblLoPnlMain.Controls.Add(this.lbHeading, 0, 0);
            this.tblLoPnlMain.Controls.Add(this.txtBxName, 1, 2);
            this.tblLoPnlMain.Controls.Add(this.label1, 0, 2);
            this.tblLoPnlMain.Controls.Add(this.lbSelectHeading, 0, 1);
            this.tblLoPnlMain.Controls.Add(this.tableLayoutPanel1, 1, 3);
            this.tblLoPnlMain.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblLoPnlMain.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlMain.Name = "tblLoPnlMain";
            this.tblLoPnlMain.RowCount = 6;
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Size = new System.Drawing.Size(360, 160);
            this.tblLoPnlMain.TabIndex = 9;
            // 
            // cmbBxContent
            // 
            this.cmbBxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBxContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxContent.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxContent.FormattingEnabled = true;
            this.cmbBxContent.Location = new System.Drawing.Point(116, 33);
            this.cmbBxContent.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cmbBxContent.Name = "cmbBxContent";
            this.cmbBxContent.Size = new System.Drawing.Size(218, 26);
            this.cmbBxContent.TabIndex = 10;
            this.cmbBxContent.SelectedIndexChanged += new System.EventHandler(this.cmbBxContent_SelectedIndexChanged);
            // 
            // lbHeading
            // 
            this.lbHeading.AutoSize = true;
            this.tblLoPnlMain.SetColumnSpan(this.lbHeading, 2);
            this.lbHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeading.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeading.Location = new System.Drawing.Point(3, 0);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(334, 30);
            this.lbHeading.TabIndex = 7;
            this.lbHeading.Text = "Edit";
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBxName
            // 
            this.txtBxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxName.Enabled = false;
            this.txtBxName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxName.Location = new System.Drawing.Point(116, 63);
            this.txtBxName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.Size = new System.Drawing.Size(218, 26);
            this.txtBxName.TabIndex = 0;
            this.txtBxName.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSelectHeading
            // 
            this.lbSelectHeading.AutoSize = true;
            this.lbSelectHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelectHeading.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectHeading.Location = new System.Drawing.Point(3, 30);
            this.lbSelectHeading.Name = "lbSelectHeading";
            this.lbSelectHeading.Size = new System.Drawing.Size(104, 30);
            this.lbSelectHeading.TabIndex = 8;
            this.lbSelectHeading.Text = "Select";
            this.lbSelectHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(110, 90);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(230, 50);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(122, 14);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(7, 14, 7, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(101, 32);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "&Keep edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Enabled = false;
            this.btnRemove.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(7, 14);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(7, 14, 7, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(101, 32);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // HasNameContentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblLoPnlMain);
            this.Name = "HasNameContentEditor";
            this.Size = new System.Drawing.Size(360, 160);
            this.tblLoPnlMain.ResumeLayout(false);
            this.tblLoPnlMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlMain;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSelectHeading;
        private System.Windows.Forms.ComboBox cmbBxContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRemove;
    }
}
