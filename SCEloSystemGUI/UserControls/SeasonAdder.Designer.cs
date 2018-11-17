namespace SCEloSystemGUI.UserControls
{
    partial class SeasonAdder
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
            this.tblLOPnlSeasonAdder = new System.Windows.Forms.TableLayoutPanel();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.lbHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tblLOPnlSeasonAdder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlSeasonAdder
            // 
            this.tblLOPnlSeasonAdder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLOPnlSeasonAdder.ColumnCount = 4;
            this.tblLOPnlSeasonAdder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblLOPnlSeasonAdder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLOPnlSeasonAdder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlSeasonAdder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlSeasonAdder.Controls.Add(this.txtBxName, 1, 2);
            this.tblLOPnlSeasonAdder.Controls.Add(this.lbHeading, 0, 0);
            this.tblLOPnlSeasonAdder.Controls.Add(this.label1, 0, 2);
            this.tblLOPnlSeasonAdder.Controls.Add(this.btnAdd, 1, 3);
            this.tblLOPnlSeasonAdder.Controls.Add(this.label2, 0, 1);
            this.tblLOPnlSeasonAdder.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlSeasonAdder.Name = "tblLOPnlSeasonAdder";
            this.tblLOPnlSeasonAdder.RowCount = 6;
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlSeasonAdder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlSeasonAdder.Size = new System.Drawing.Size(360, 160);
            this.tblLOPnlSeasonAdder.TabIndex = 2;
            // 
            // txtBxName
            // 
            this.txtBxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxName.Enabled = false;
            this.txtBxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxName.Location = new System.Drawing.Point(146, 63);
            this.txtBxName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.Size = new System.Drawing.Size(188, 23);
            this.txtBxName.TabIndex = 1;
            this.txtBxName.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            // 
            // lbHeading
            // 
            this.lbHeading.AutoSize = true;
            this.tblLOPnlSeasonAdder.SetColumnSpan(this.lbHeading, 2);
            this.lbHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeading.Location = new System.Drawing.Point(3, 0);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(334, 30);
            this.lbHeading.TabIndex = 1;
            this.lbHeading.Text = "Add Season";
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(189, 104);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(145, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add to &tournament";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Tournament";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SeasonAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLOPnlSeasonAdder);
            this.Name = "SeasonAdder";
            this.Size = new System.Drawing.Size(358, 158);
            this.tblLOPnlSeasonAdder.ResumeLayout(false);
            this.tblLOPnlSeasonAdder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlSeasonAdder;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
    }
}
