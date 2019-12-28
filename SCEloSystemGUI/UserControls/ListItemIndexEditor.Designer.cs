namespace SCEloSystemGUI.UserControls
{
    partial class ListItemIndexEditor
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
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnAcceptChanges = new System.Windows.Forms.Button();
            this.btnDecreaseIndex = new System.Windows.Forms.Button();
            this.btnIncreaseIndex = new System.Windows.Forms.Button();
            this.tLPMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            this.tLPMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tLPMain.ColumnCount = 3;
            this.tLPMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tLPMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tLPMain.Controls.Add(this.lbHeader, 0, 0);
            this.tLPMain.Controls.Add(this.btnAcceptChanges, 2, 1);
            this.tLPMain.Controls.Add(this.btnDecreaseIndex, 1, 1);
            this.tLPMain.Controls.Add(this.btnIncreaseIndex, 1, 2);
            this.tLPMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLPMain.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLPMain.Location = new System.Drawing.Point(0, 0);
            this.tLPMain.Margin = new System.Windows.Forms.Padding(0);
            this.tLPMain.Name = "tLPMain";
            this.tLPMain.RowCount = 3;
            this.tLPMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tLPMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tLPMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPMain.Size = new System.Drawing.Size(828, 168);
            this.tLPMain.TabIndex = 0;
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.tLPMain.SetColumnSpan(this.lbHeader, 3);
            this.lbHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeader.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.Location = new System.Drawing.Point(0, 3);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(822, 22);
            this.lbHeader.TabIndex = 0;
            // 
            // btnAcceptChanges
            // 
            this.btnAcceptChanges.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAcceptChanges.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcceptChanges.Location = new System.Drawing.Point(734, 32);
            this.btnAcceptChanges.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.btnAcceptChanges.Name = "btnAcceptChanges";
            this.btnAcceptChanges.Size = new System.Drawing.Size(90, 50);
            this.btnAcceptChanges.TabIndex = 1;
            this.btnAcceptChanges.Text = "Accept Changes";
            this.btnAcceptChanges.UseVisualStyleBackColor = true;
            this.btnAcceptChanges.Click += new System.EventHandler(this.btnAcceptChanges_Click);
            // 
            // btnDecreaseIndex
            // 
            this.btnDecreaseIndex.Image = global::SCEloSystemGUI.Properties.Resources.ArrowUp;
            this.btnDecreaseIndex.Location = new System.Drawing.Point(692, 38);
            this.btnDecreaseIndex.Margin = new System.Windows.Forms.Padding(4, 10, 4, 6);
            this.btnDecreaseIndex.Name = "btnDecreaseIndex";
            this.btnDecreaseIndex.Size = new System.Drawing.Size(32, 54);
            this.btnDecreaseIndex.TabIndex = 2;
            this.btnDecreaseIndex.UseVisualStyleBackColor = true;
            this.btnDecreaseIndex.Click += new System.EventHandler(this.btnDecreaseIndex_Click);
            // 
            // btnIncreaseIndex
            // 
            this.btnIncreaseIndex.Image = global::SCEloSystemGUI.Properties.Resources.ArrowDwn;
            this.btnIncreaseIndex.Location = new System.Drawing.Point(692, 104);
            this.btnIncreaseIndex.Margin = new System.Windows.Forms.Padding(4, 6, 4, 10);
            this.btnIncreaseIndex.Name = "btnIncreaseIndex";
            this.btnIncreaseIndex.Size = new System.Drawing.Size(32, 54);
            this.btnIncreaseIndex.TabIndex = 2;
            this.btnIncreaseIndex.UseVisualStyleBackColor = true;
            this.btnIncreaseIndex.Click += new System.EventHandler(this.btnIncreaseIndex_Click);
            // 
            // ListItemIndexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPMain);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ListItemIndexEditor";
            this.Size = new System.Drawing.Size(828, 168);
            this.tLPMain.ResumeLayout(false);
            this.tLPMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Button btnAcceptChanges;
        private System.Windows.Forms.Button btnDecreaseIndex;
        private System.Windows.Forms.Button btnIncreaseIndex;
    }
}
