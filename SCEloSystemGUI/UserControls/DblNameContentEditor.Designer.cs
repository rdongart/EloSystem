namespace SCEloSystemGUI.UserControls
{
    partial class DblNameContentEditor<T> 
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
            this.tblLoPnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.lbFileName = new System.Windows.Forms.Label();
            this.txtBxNameShort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxNameLong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSelectHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.picBxCurrentImage = new System.Windows.Forms.PictureBox();
            this.chckBxRemoveCurrentImage = new System.Windows.Forms.CheckBox();
            this.toolTipEditor = new System.Windows.Forms.ToolTip(this.components);
            this.tblLoPnlMain.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCurrentImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLoPnlMain
            // 
            this.tblLoPnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLoPnlMain.ColumnCount = 4;
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Controls.Add(this.lbHeading, 0, 0);
            this.tblLoPnlMain.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tblLoPnlMain.Controls.Add(this.btnEdit, 1, 6);
            this.tblLoPnlMain.Controls.Add(this.tableLayoutPanel3, 1, 5);
            this.tblLoPnlMain.Controls.Add(this.txtBxNameShort, 1, 2);
            this.tblLoPnlMain.Controls.Add(this.label1, 0, 2);
            this.tblLoPnlMain.Controls.Add(this.label3, 0, 3);
            this.tblLoPnlMain.Controls.Add(this.txtBxNameLong, 1, 3);
            this.tblLoPnlMain.Controls.Add(this.label4, 0, 4);
            this.tblLoPnlMain.Controls.Add(this.lbSelectHeading, 0, 1);
            this.tblLoPnlMain.Controls.Add(this.tableLayoutPanel4, 1, 4);
            this.tblLoPnlMain.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlMain.Name = "tblLoPnlMain";
            this.tblLoPnlMain.RowCount = 9;
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlMain.Size = new System.Drawing.Size(380, 280);
            this.tblLoPnlMain.TabIndex = 7;
            // 
            // lbHeading
            // 
            this.lbHeading.AutoSize = true;
            this.tblLoPnlMain.SetColumnSpan(this.lbHeading, 2);
            this.lbHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeading.Location = new System.Drawing.Point(3, 0);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(354, 30);
            this.lbHeading.TabIndex = 7;
            this.lbHeading.Text = "Edit";
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBrowse, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 180);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 30);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "New Image";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(94, 4);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(62, 22);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse...";
            this.toolTipEditor.SetToolTip(this.btnBrowse, "Browse to select a new image to replace the current image");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(234, 224);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 32);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "&Save changes";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Controls.Add(this.btnRemoveImage, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbFileName, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(166, 183);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(188, 24);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveImage.Enabled = false;
            this.btnRemoveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemoveImage.Location = new System.Drawing.Point(164, 1);
            this.btnRemoveImage.Margin = new System.Windows.Forms.Padding(4, 1, 3, 1);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(21, 22);
            this.btnRemoveImage.TabIndex = 6;
            this.btnRemoveImage.Text = "x";
            this.btnRemoveImage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTipEditor.SetToolTip(this.btnRemoveImage, "Remove selected image");
            this.btnRemoveImage.UseVisualStyleBackColor = true;
            // 
            // lbFileName
            // 
            this.lbFileName.AutoEllipsis = true;
            this.lbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileName.Location = new System.Drawing.Point(0, 3);
            this.lbFileName.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(154, 18);
            this.lbFileName.TabIndex = 11;
            this.lbFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbFileName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtBxNameShort
            // 
            this.txtBxNameShort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxNameShort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxNameShort.Location = new System.Drawing.Point(166, 63);
            this.txtBxNameShort.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxNameShort.Name = "txtBxNameShort";
            this.txtBxNameShort.Size = new System.Drawing.Size(188, 23);
            this.txtBxNameShort.TabIndex = 0;
            this.txtBxNameShort.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name - short";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Name - long";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBxNameLong
            // 
            this.txtBxNameLong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxNameLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxNameLong.Location = new System.Drawing.Point(166, 93);
            this.txtBxNameLong.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxNameLong.Name = "txtBxNameLong";
            this.txtBxNameLong.Size = new System.Drawing.Size(188, 23);
            this.txtBxNameLong.TabIndex = 1;
            this.txtBxNameLong.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 60);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current image";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSelectHeading
            // 
            this.lbSelectHeading.AutoSize = true;
            this.lbSelectHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelectHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectHeading.Location = new System.Drawing.Point(3, 30);
            this.lbSelectHeading.Name = "lbSelectHeading";
            this.lbSelectHeading.Size = new System.Drawing.Size(154, 30);
            this.lbSelectHeading.TabIndex = 8;
            this.lbSelectHeading.Text = "Select";
            this.lbSelectHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel4.Controls.Add(this.picBxCurrentImage, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chckBxRemoveCurrentImage, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(163, 123);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 54);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // picBxCurrentImage
            // 
            this.picBxCurrentImage.Location = new System.Drawing.Point(0, 0);
            this.picBxCurrentImage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.picBxCurrentImage.Name = "picBxCurrentImage";
            this.picBxCurrentImage.Size = new System.Drawing.Size(163, 54);
            this.picBxCurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxCurrentImage.TabIndex = 10;
            this.picBxCurrentImage.TabStop = false;
            // 
            // chckBxRemoveCurrentImage
            // 
            this.chckBxRemoveCurrentImage.AutoSize = true;
            this.chckBxRemoveCurrentImage.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chckBxRemoveCurrentImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chckBxRemoveCurrentImage.Enabled = false;
            this.chckBxRemoveCurrentImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckBxRemoveCurrentImage.Location = new System.Drawing.Point(169, 18);
            this.chckBxRemoveCurrentImage.Name = "chckBxRemoveCurrentImage";
            this.chckBxRemoveCurrentImage.Size = new System.Drawing.Size(22, 33);
            this.chckBxRemoveCurrentImage.TabIndex = 11;
            this.chckBxRemoveCurrentImage.Text = " X";
            this.chckBxRemoveCurrentImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipEditor.SetToolTip(this.chckBxRemoveCurrentImage, "Remove current image");
            this.chckBxRemoveCurrentImage.UseVisualStyleBackColor = true;
            this.chckBxRemoveCurrentImage.CheckedChanged += new System.EventHandler(this.chckBxRemoveCurrentImage_CheckedChanged);
            // 
            // DblNameContentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLoPnlMain);
            this.Name = "DblNameContentEditor";
            this.Size = new System.Drawing.Size(380, 280);
            this.tblLoPnlMain.ResumeLayout(false);
            this.tblLoPnlMain.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCurrentImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlMain;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.TextBox txtBxNameShort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxNameLong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBxCurrentImage;
        private System.Windows.Forms.Label lbSelectHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox chckBxRemoveCurrentImage;
        private System.Windows.Forms.ToolTip toolTipEditor;
    }
}
