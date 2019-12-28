namespace SCEloSystemGUI.UserControls
{
    partial class ActivityFilter 
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
            this.tblLOPnlActivityFilter = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numUDGamesTotal = new System.Windows.Forms.NumericUpDown();
            this.numUDGamesRecent = new System.Windows.Forms.NumericUpDown();
            this.numUDRecentMonths = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkBxDisableRecencyFilter = new System.Windows.Forms.CheckBox();
            this.tblLOPnlActivityFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGamesTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGamesRecent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRecentMonths)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLOPnlActivityFilter
            // 
            this.tblLOPnlActivityFilter.ColumnCount = 2;
            this.tblLOPnlActivityFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblLOPnlActivityFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlActivityFilter.Controls.Add(this.label1, 0, 0);
            this.tblLOPnlActivityFilter.Controls.Add(this.label2, 0, 1);
            this.tblLOPnlActivityFilter.Controls.Add(this.label3, 0, 2);
            this.tblLOPnlActivityFilter.Controls.Add(this.label4, 0, 3);
            this.tblLOPnlActivityFilter.Controls.Add(this.numUDGamesTotal, 1, 1);
            this.tblLOPnlActivityFilter.Controls.Add(this.numUDGamesRecent, 1, 2);
            this.tblLOPnlActivityFilter.Controls.Add(this.numUDRecentMonths, 1, 3);
            this.tblLOPnlActivityFilter.Controls.Add(this.label5, 0, 4);
            this.tblLOPnlActivityFilter.Controls.Add(this.chkBxDisableRecencyFilter, 1, 4);
            this.tblLOPnlActivityFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlActivityFilter.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlActivityFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlActivityFilter.Name = "tblLOPnlActivityFilter";
            this.tblLOPnlActivityFilter.RowCount = 6;
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLOPnlActivityFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlActivityFilter.Size = new System.Drawing.Size(235, 146);
            this.tblLOPnlActivityFilter.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tblLOPnlActivityFilter.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Activity Filter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Games Played, total";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "Games Played, recent";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Recent period, months";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numUDGamesTotal
            // 
            this.numUDGamesTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDGamesTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDGamesTotal.Location = new System.Drawing.Point(153, 35);
            this.numUDGamesTotal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUDGamesTotal.Name = "numUDGamesTotal";
            this.numUDGamesTotal.Size = new System.Drawing.Size(79, 23);
            this.numUDGamesTotal.TabIndex = 0;
            this.numUDGamesTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDGamesTotal.ThousandsSeparator = true;
            this.numUDGamesTotal.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numUDGamesTotal.ValueChanged += new System.EventHandler(this.activityFilter_ValueChanged);
            // 
            // numUDGamesRecent
            // 
            this.numUDGamesRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDGamesRecent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDGamesRecent.Location = new System.Drawing.Point(153, 61);
            this.numUDGamesRecent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUDGamesRecent.Name = "numUDGamesRecent";
            this.numUDGamesRecent.Size = new System.Drawing.Size(79, 23);
            this.numUDGamesRecent.TabIndex = 1;
            this.numUDGamesRecent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDGamesRecent.ThousandsSeparator = true;
            this.numUDGamesRecent.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDGamesRecent.ValueChanged += new System.EventHandler(this.activityFilter_ValueChanged);
            // 
            // numUDRecentMonths
            // 
            this.numUDRecentMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDRecentMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDRecentMonths.Location = new System.Drawing.Point(153, 87);
            this.numUDRecentMonths.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numUDRecentMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDRecentMonths.Name = "numUDRecentMonths";
            this.numUDRecentMonths.Size = new System.Drawing.Size(79, 23);
            this.numUDRecentMonths.TabIndex = 2;
            this.numUDRecentMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDRecentMonths.ThousandsSeparator = true;
            this.numUDRecentMonths.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numUDRecentMonths.ValueChanged += new System.EventHandler(this.activityFilter_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(3, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "Disable recent activity filter";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBxDisableRecencyFilter
            // 
            this.chkBxDisableRecencyFilter.AutoSize = true;
            this.chkBxDisableRecencyFilter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBxDisableRecencyFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBxDisableRecencyFilter.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxDisableRecencyFilter.Location = new System.Drawing.Point(153, 113);
            this.chkBxDisableRecencyFilter.Name = "chkBxDisableRecencyFilter";
            this.chkBxDisableRecencyFilter.Size = new System.Drawing.Size(79, 30);
            this.chkBxDisableRecencyFilter.TabIndex = 3;
            this.chkBxDisableRecencyFilter.UseVisualStyleBackColor = true;
            this.chkBxDisableRecencyFilter.CheckedChanged += new System.EventHandler(this.activityFilter_ValueChanged);
            // 
            // ActivityFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLOPnlActivityFilter);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ActivityFilter";
            this.Size = new System.Drawing.Size(235, 146);
            this.tblLOPnlActivityFilter.ResumeLayout(false);
            this.tblLOPnlActivityFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGamesTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDGamesRecent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRecentMonths)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlActivityFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUDGamesTotal;
        private System.Windows.Forms.NumericUpDown numUDGamesRecent;
        private System.Windows.Forms.NumericUpDown numUDRecentMonths;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkBxDisableRecencyFilter;
    }
}
