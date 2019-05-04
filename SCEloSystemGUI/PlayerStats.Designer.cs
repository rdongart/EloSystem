namespace SCEloSystemGUI
{
    partial class PlayerStats
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
            this.tblLoPnlPlayerStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.tblLoPnlFilters = new System.Windows.Forms.TableLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnToggleFilterVisibility = new System.Windows.Forms.Button();
            this.tblLoPnlPlayerStats.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.tblLoPnlFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlPlayerStats
            // 
            this.tblLoPnlPlayerStats.ColumnCount = 1;
            this.tblLoPnlPlayerStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Controls.Add(this.pnlFilters, 0, 0);
            this.tblLoPnlPlayerStats.Controls.Add(this.btnToggleFilterVisibility, 0, 1);
            this.tblLoPnlPlayerStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlPlayerStats.Location = new System.Drawing.Point(6, 12);
            this.tblLoPnlPlayerStats.Name = "tblLoPnlPlayerStats";
            this.tblLoPnlPlayerStats.RowCount = 3;
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Size = new System.Drawing.Size(837, 998);
            this.tblLoPnlPlayerStats.TabIndex = 0;
            // 
            // pnlFilters
            // 
            this.pnlFilters.AutoScroll = true;
            this.pnlFilters.Controls.Add(this.tblLoPnlFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlFilters.Size = new System.Drawing.Size(837, 320);
            this.pnlFilters.TabIndex = 1;
            // 
            // tblLoPnlFilters
            // 
            this.tblLoPnlFilters.AutoSize = true;
            this.tblLoPnlFilters.ColumnCount = 3;
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 307F));
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLoPnlFilters.Controls.Add(this.btnApply, 2, 0);
            this.tblLoPnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblLoPnlFilters.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlFilters.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlFilters.Name = "tblLoPnlFilters";
            this.tblLoPnlFilters.RowCount = 1;
            this.tblLoPnlFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlFilters.Size = new System.Drawing.Size(822, 42);
            this.tblLoPnlFilters.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(725, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 36);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "&Apply filters";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnToggleFilterVisibility
            // 
            this.btnToggleFilterVisibility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleFilterVisibility.Location = new System.Drawing.Point(330, 322);
            this.btnToggleFilterVisibility.Margin = new System.Windows.Forms.Padding(330, 2, 330, 2);
            this.btnToggleFilterVisibility.Name = "btnToggleFilterVisibility";
            this.btnToggleFilterVisibility.Size = new System.Drawing.Size(177, 22);
            this.btnToggleFilterVisibility.TabIndex = 2;
            this.btnToggleFilterVisibility.Text = "Show filters";
            this.btnToggleFilterVisibility.UseVisualStyleBackColor = true;
            this.btnToggleFilterVisibility.Click += new System.EventHandler(this.btnToggleFilterVisibility_Click);
            // 
            // PlayerStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 1022);
            this.Controls.Add(this.tblLoPnlPlayerStats);
            this.DoubleBuffered = true;
            this.Name = "PlayerStats";
            this.Padding = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Stats";
            this.tblLoPnlPlayerStats.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.tblLoPnlFilters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlPlayerStats;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlFilters;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnToggleFilterVisibility;
    }
}