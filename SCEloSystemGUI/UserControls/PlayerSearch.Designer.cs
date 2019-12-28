namespace SCEloSystemGUI.UserControls
{
    partial class PlayerSearch
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
            this.tblLOPnlPlayerSearch = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBxFilter = new System.Windows.Forms.TextBox();
            this.lbHeader = new System.Windows.Forms.Label();
            this.tblLOPnlPlayerSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlPlayerSearch
            // 
            this.tblLOPnlPlayerSearch.ColumnCount = 2;
            this.tblLOPnlPlayerSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLOPnlPlayerSearch.Controls.Add(this.btnSearch, 1, 1);
            this.tblLOPnlPlayerSearch.Controls.Add(this.txtBxFilter, 0, 1);
            this.tblLOPnlPlayerSearch.Controls.Add(this.lbHeader, 0, 0);
            this.tblLOPnlPlayerSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPlayerSearch.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlPlayerSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlPlayerSearch.Name = "tblLOPnlPlayerSearch";
            this.tblLOPnlPlayerSearch.RowCount = 3;
            this.tblLOPnlPlayerSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlPlayerSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlPlayerSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerSearch.Size = new System.Drawing.Size(798, 300);
            this.tblLOPnlPlayerSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.Search;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(765, 35);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(27, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBxFilter
            // 
            this.txtBxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxFilter.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxFilter.Location = new System.Drawing.Point(6, 35);
            this.txtBxFilter.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxFilter.Name = "txtBxFilter";
            this.txtBxFilter.Size = new System.Drawing.Size(750, 25);
            this.txtBxFilter.TabIndex = 0;
            this.txtBxFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBxFilter_KeyDown);
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.tblLOPnlPlayerSearch.SetColumnSpan(this.lbHeader, 2);
            this.lbHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeader.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.Location = new System.Drawing.Point(6, 0);
            this.lbHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(786, 32);
            this.lbHeader.TabIndex = 13;
            this.lbHeader.Text = "Player Search";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLOPnlPlayerSearch);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PlayerSearch";
            this.Size = new System.Drawing.Size(798, 300);
            this.tblLOPnlPlayerSearch.ResumeLayout(false);
            this.tblLOPnlPlayerSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayerSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBxFilter;
        private System.Windows.Forms.Label lbHeader;
    }
}
