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
            this.SuspendLayout();
            // 
            // tblLoPnlPlayerStats
            // 
            this.tblLoPnlPlayerStats.ColumnCount = 1;
            this.tblLoPnlPlayerStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlPlayerStats.Location = new System.Drawing.Point(6, 12);
            this.tblLoPnlPlayerStats.Name = "tblLoPnlPlayerStats";
            this.tblLoPnlPlayerStats.RowCount = 2;
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblLoPnlPlayerStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlPlayerStats.Size = new System.Drawing.Size(1299, 938);
            this.tblLoPnlPlayerStats.TabIndex = 0;
            // 
            // PlayerStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 962);
            this.Controls.Add(this.tblLoPnlPlayerStats);
            this.Name = "PlayerStats";
            this.Padding = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.Text = "PlayerStats";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLoPnlPlayerStats;
    }
}