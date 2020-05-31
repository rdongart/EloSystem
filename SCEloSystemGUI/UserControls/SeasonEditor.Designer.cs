namespace SCEloSystemGUI.UserControls
{
    partial class SeasonEditor
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
            this.cmbBxSeasons = new System.Windows.Forms.ComboBox();
            this.lbHeading = new System.Windows.Forms.Label();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSelectHeading = new System.Windows.Forms.Label();
            this.tblLOPnlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tblLoPnlMain.SuspendLayout();
            this.tblLOPnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlMain
            // 
            this.tblLoPnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLoPnlMain.ColumnCount = 4;
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Controls.Add(this.cmbBxSeasons, 1, 2);
            this.tblLoPnlMain.Controls.Add(this.lbHeading, 0, 0);
            this.tblLoPnlMain.Controls.Add(this.txtBxName, 1, 3);
            this.tblLoPnlMain.Controls.Add(this.label1, 0, 3);
            this.tblLoPnlMain.Controls.Add(this.lbSelectHeading, 0, 2);
            this.tblLoPnlMain.Controls.Add(this.tblLOPnlButtons, 1, 4);
            this.tblLoPnlMain.Controls.Add(this.label2, 0, 1);
            this.tblLoPnlMain.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblLoPnlMain.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlMain.Name = "tblLoPnlMain";
            this.tblLoPnlMain.RowCount = 7;
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Size = new System.Drawing.Size(360, 190);
            this.tblLoPnlMain.TabIndex = 10;
            // 
            // cmbBxSeasons
            // 
            this.cmbBxSeasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBxSeasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxSeasons.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxSeasons.FormattingEnabled = true;
            this.cmbBxSeasons.Location = new System.Drawing.Point(146, 63);
            this.cmbBxSeasons.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cmbBxSeasons.Name = "cmbBxSeasons";
            this.cmbBxSeasons.Size = new System.Drawing.Size(188, 26);
            this.cmbBxSeasons.TabIndex = 1;
            this.cmbBxSeasons.SelectedIndexChanged += new System.EventHandler(this.cmbBxSeasons_SelectedIndexChanged);
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
            this.lbHeading.Text = "Edit Season";
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBxName
            // 
            this.txtBxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxName.Enabled = false;
            this.txtBxName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxName.Location = new System.Drawing.Point(146, 93);
            this.txtBxName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.Size = new System.Drawing.Size(188, 26);
            this.txtBxName.TabIndex = 2;
            this.txtBxName.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSelectHeading
            // 
            this.lbSelectHeading.AutoSize = true;
            this.lbSelectHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelectHeading.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectHeading.Location = new System.Drawing.Point(3, 60);
            this.lbSelectHeading.Name = "lbSelectHeading";
            this.lbSelectHeading.Size = new System.Drawing.Size(134, 30);
            this.lbSelectHeading.TabIndex = 8;
            this.lbSelectHeading.Text = "Season";
            this.lbSelectHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLOPnlButtons
            // 
            this.tblLOPnlButtons.ColumnCount = 2;
            this.tblLOPnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLOPnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLOPnlButtons.Controls.Add(this.btnEdit, 1, 0);
            this.tblLOPnlButtons.Controls.Add(this.btnRemove, 0, 0);
            this.tblLOPnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlButtons.Location = new System.Drawing.Point(140, 120);
            this.tblLOPnlButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlButtons.Name = "tblLOPnlButtons";
            this.tblLOPnlButtons.RowCount = 1;
            this.tblLOPnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLOPnlButtons.Size = new System.Drawing.Size(200, 50);
            this.tblLOPnlButtons.TabIndex = 11;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(107, 14);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(7, 14, 7, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 32);
            this.btnEdit.TabIndex = 3;
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
            this.btnRemove.Size = new System.Drawing.Size(86, 32);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tournament";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SeasonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblLoPnlMain);
            this.Name = "SeasonEditor";
            this.Size = new System.Drawing.Size(360, 190);
            this.tblLoPnlMain.ResumeLayout(false);
            this.tblLoPnlMain.PerformLayout();
            this.tblLOPnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlMain;
        private System.Windows.Forms.ComboBox cmbBxSeasons;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSelectHeading;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlButtons;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label2;
    }
}
