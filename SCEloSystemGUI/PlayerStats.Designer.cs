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
            this.btnToggleCustomizationVisibility = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabCtrlCustomizations = new System.Windows.Forms.TabControl();
            this.tabPageFilters = new System.Windows.Forms.TabPage();
            this.tabPagePlayerSearch = new System.Windows.Forms.TabPage();
            this.tblLoPnlPlayerStats.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.tblLoPnlFilters.SuspendLayout();
            this.tabCtrlCustomizations.SuspendLayout();
            this.tabPageFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLoPnlPlayerStats
            // 
            this.tblLoPnlPlayerStats.ColumnCount = 1;
            this.tblLoPnlPlayerStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Controls.Add(this.btnToggleCustomizationVisibility, 0, 1);
            this.tblLoPnlPlayerStats.Controls.Add(this.tabCtrlCustomizations, 0, 0);
            this.tblLoPnlPlayerStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlPlayerStats.Location = new System.Drawing.Point(6, 8);
            this.tblLoPnlPlayerStats.Name = "tblLoPnlPlayerStats";
            this.tblLoPnlPlayerStats.RowCount = 3;
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 384F));
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Size = new System.Drawing.Size(1002, 1004);
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
            this.pnlFilters.Size = new System.Drawing.Size(994, 360);
            this.pnlFilters.TabIndex = 1;
            // 
            // tblLoPnlFilters
            // 
            this.tblLoPnlFilters.AutoSize = true;
            this.tblLoPnlFilters.ColumnCount = 4;
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 307F));
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 307F));
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 247F));
            this.tblLoPnlFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLoPnlFilters.Controls.Add(this.btnApply, 3, 0);
            this.tblLoPnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblLoPnlFilters.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlFilters.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlFilters.Name = "tblLoPnlFilters";
            this.tblLoPnlFilters.RowCount = 1;
            this.tblLoPnlFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlFilters.Size = new System.Drawing.Size(979, 42);
            this.tblLoPnlFilters.TabIndex = 0;
            // 
            // btnToggleCustomizationVisibility
            // 
            this.btnToggleCustomizationVisibility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleCustomizationVisibility.Location = new System.Drawing.Point(330, 385);
            this.btnToggleCustomizationVisibility.Margin = new System.Windows.Forms.Padding(330, 1, 330, 1);
            this.btnToggleCustomizationVisibility.Name = "btnToggleCustomizationVisibility";
            this.btnToggleCustomizationVisibility.Size = new System.Drawing.Size(342, 22);
            this.btnToggleCustomizationVisibility.TabIndex = 2;
            this.btnToggleCustomizationVisibility.Text = "Show customization";
            this.btnToggleCustomizationVisibility.UseVisualStyleBackColor = true;
            this.btnToggleCustomizationVisibility.Click += new System.EventHandler(this.btnToggleFilterVisibility_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(864, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(112, 36);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "&Apply filters";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tabCtrlCustomizations
            // 
            this.tabCtrlCustomizations.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabCtrlCustomizations.Controls.Add(this.tabPageFilters);
            this.tabCtrlCustomizations.Controls.Add(this.tabPagePlayerSearch);
            this.tabCtrlCustomizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlCustomizations.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCtrlCustomizations.HotTrack = true;
            this.tabCtrlCustomizations.ItemSize = new System.Drawing.Size(54, 16);
            this.tabCtrlCustomizations.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlCustomizations.Margin = new System.Windows.Forms.Padding(0);
            this.tabCtrlCustomizations.Name = "tabCtrlCustomizations";
            this.tabCtrlCustomizations.SelectedIndex = 0;
            this.tabCtrlCustomizations.Size = new System.Drawing.Size(1002, 384);
            this.tabCtrlCustomizations.TabIndex = 3;
            // 
            // tabPageFilters
            // 
            this.tabPageFilters.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageFilters.Controls.Add(this.pnlFilters);
            this.tabPageFilters.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageFilters.Location = new System.Drawing.Point(4, 4);
            this.tabPageFilters.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageFilters.Name = "tabPageFilters";
            this.tabPageFilters.Size = new System.Drawing.Size(994, 360);
            this.tabPageFilters.TabIndex = 0;
            this.tabPageFilters.Text = "Filters";
            // 
            // tabPagePlayerSearch
            // 
            this.tabPagePlayerSearch.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePlayerSearch.Location = new System.Drawing.Point(4, 4);
            this.tabPagePlayerSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tabPagePlayerSearch.Name = "tabPagePlayerSearch";
            this.tabPagePlayerSearch.Size = new System.Drawing.Size(994, 336);
            this.tabPagePlayerSearch.TabIndex = 1;
            this.tabPagePlayerSearch.Text = "Player Search";
            // 
            // PlayerStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 1022);
            this.Controls.Add(this.tblLoPnlPlayerStats);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "PlayerStats";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Stats";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PlayerStats_KeyPress);
            this.tblLoPnlPlayerStats.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.tblLoPnlFilters.ResumeLayout(false);
            this.tabCtrlCustomizations.ResumeLayout(false);
            this.tabPageFilters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlPlayerStats;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlFilters;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnToggleCustomizationVisibility;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabControl tabCtrlCustomizations;
        private System.Windows.Forms.TabPage tabPageFilters;
        private System.Windows.Forms.TabPage tabPagePlayerSearch;
    }
}