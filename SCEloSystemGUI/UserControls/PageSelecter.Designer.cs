namespace SCEloSystemGUI.UserControls
{
    partial class PageSelecter
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
            this.tblLOPnlPageSelecter = new System.Windows.Forms.TableLayoutPanel();
            this.maxPageNumberDisplay = new System.Windows.Forms.Label();
            this.txtBxCurrentPage = new System.Windows.Forms.TextBox();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.pnlFirstPage = new System.Windows.Forms.Panel();
            this.pnlPrevPage = new System.Windows.Forms.Panel();
            this.pnlNextPage = new System.Windows.Forms.Panel();
            this.pnlLastPage = new System.Windows.Forms.Panel();
            this.tblLOPnlPageSelecter.SuspendLayout();
            this.pnlFirstPage.SuspendLayout();
            this.pnlPrevPage.SuspendLayout();
            this.pnlNextPage.SuspendLayout();
            this.pnlLastPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlPageSelecter
            // 
            this.tblLOPnlPageSelecter.ColumnCount = 6;
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLOPnlPageSelecter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlPageSelecter.Controls.Add(this.maxPageNumberDisplay, 3, 0);
            this.tblLOPnlPageSelecter.Controls.Add(this.txtBxCurrentPage, 2, 0);
            this.tblLOPnlPageSelecter.Controls.Add(this.pnlFirstPage, 0, 0);
            this.tblLOPnlPageSelecter.Controls.Add(this.pnlPrevPage, 1, 0);
            this.tblLOPnlPageSelecter.Controls.Add(this.pnlNextPage, 4, 0);
            this.tblLOPnlPageSelecter.Controls.Add(this.pnlLastPage, 5, 0);
            this.tblLOPnlPageSelecter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblLOPnlPageSelecter.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlPageSelecter.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlPageSelecter.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlPageSelecter.Name = "tblLOPnlPageSelecter";
            this.tblLOPnlPageSelecter.RowCount = 1;
            this.tblLOPnlPageSelecter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPageSelecter.Size = new System.Drawing.Size(190, 36);
            this.tblLOPnlPageSelecter.TabIndex = 0;
            // 
            // maxPageNumberDisplay
            // 
            this.maxPageNumberDisplay.AutoEllipsis = true;
            this.maxPageNumberDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxPageNumberDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.maxPageNumberDisplay.Location = new System.Drawing.Point(98, 6);
            this.maxPageNumberDisplay.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.maxPageNumberDisplay.Name = "maxPageNumberDisplay";
            this.maxPageNumberDisplay.Size = new System.Drawing.Size(40, 24);
            this.maxPageNumberDisplay.TabIndex = 0;
            this.maxPageNumberDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBxCurrentPage
            // 
            this.txtBxCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxCurrentPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxCurrentPage.Location = new System.Drawing.Point(52, 6);
            this.txtBxCurrentPage.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.txtBxCurrentPage.Name = "txtBxCurrentPage";
            this.txtBxCurrentPage.Size = new System.Drawing.Size(40, 24);
            this.txtBxCurrentPage.TabIndex = 0;
            this.txtBxCurrentPage.Text = "1";
            this.txtBxCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBxCurrentPage.FontChanged += new System.EventHandler(this.txtBxCurrentPage_FontChanged);
            this.txtBxCurrentPage.TextChanged += new System.EventHandler(this.txtBxCurrentPage_TextChanged);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFirstPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFirstPage.FlatAppearance.BorderSize = 0;
            this.btnFirstPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.Location = new System.Drawing.Point(0, 0);
            this.btnFirstPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(28, 23);
            this.btnFirstPage.TabIndex = 1;
            this.btnFirstPage.UseVisualStyleBackColor = true;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrevPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrevPage.FlatAppearance.BorderSize = 0;
            this.btnPrevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.Location = new System.Drawing.Point(0, 7);
            this.btnPrevPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(21, 23);
            this.btnPrevPage.TabIndex = 2;
            this.btnPrevPage.UseVisualStyleBackColor = true;
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNextPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnNextPage.Location = new System.Drawing.Point(0, 13);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(21, 23);
            this.btnNextPage.TabIndex = 3;
            this.btnNextPage.UseVisualStyleBackColor = true;
            // 
            // btnLastPage
            // 
            this.btnLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLastPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLastPage.FlatAppearance.BorderSize = 0;
            this.btnLastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnLastPage.Location = new System.Drawing.Point(0, 6);
            this.btnLastPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(21, 23);
            this.btnLastPage.TabIndex = 4;
            this.btnLastPage.UseVisualStyleBackColor = true;
            // 
            // pnlFirstPage
            // 
            this.pnlFirstPage.Controls.Add(this.btnFirstPage);
            this.pnlFirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirstPage.Location = new System.Drawing.Point(0, 0);
            this.pnlFirstPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFirstPage.Name = "pnlFirstPage";
            this.pnlFirstPage.Size = new System.Drawing.Size(28, 36);
            this.pnlFirstPage.TabIndex = 1;
            // 
            // pnlPrevPage
            // 
            this.pnlPrevPage.Controls.Add(this.btnPrevPage);
            this.pnlPrevPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrevPage.Location = new System.Drawing.Point(28, 0);
            this.pnlPrevPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPrevPage.Name = "pnlPrevPage";
            this.pnlPrevPage.Size = new System.Drawing.Size(21, 36);
            this.pnlPrevPage.TabIndex = 1;
            // 
            // pnlNextPage
            // 
            this.pnlNextPage.Controls.Add(this.btnNextPage);
            this.pnlNextPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNextPage.Location = new System.Drawing.Point(141, 0);
            this.pnlNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlNextPage.Name = "pnlNextPage";
            this.pnlNextPage.Size = new System.Drawing.Size(21, 36);
            this.pnlNextPage.TabIndex = 1;
            // 
            // pnlLastPage
            // 
            this.pnlLastPage.Controls.Add(this.btnLastPage);
            this.pnlLastPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLastPage.Location = new System.Drawing.Point(162, 0);
            this.pnlLastPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLastPage.Name = "pnlLastPage";
            this.pnlLastPage.Size = new System.Drawing.Size(28, 36);
            this.pnlLastPage.TabIndex = 1;
            // 
            // PageSelecter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tblLOPnlPageSelecter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PageSelecter";
            this.Size = new System.Drawing.Size(190, 36);
            this.FontChanged += new System.EventHandler(this.PageSelecter_FontChanged);
            this.tblLOPnlPageSelecter.ResumeLayout(false);
            this.tblLOPnlPageSelecter.PerformLayout();
            this.pnlFirstPage.ResumeLayout(false);
            this.pnlPrevPage.ResumeLayout(false);
            this.pnlNextPage.ResumeLayout(false);
            this.pnlLastPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlPageSelecter;
        private System.Windows.Forms.Label maxPageNumberDisplay;
        private System.Windows.Forms.TextBox txtBxCurrentPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Panel pnlFirstPage;
        private System.Windows.Forms.Panel pnlPrevPage;
        private System.Windows.Forms.Panel pnlNextPage;
        private System.Windows.Forms.Panel pnlLastPage;
    }
}
