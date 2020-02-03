namespace SCEloSystemGUI.UserControls
{
    partial class ResultsFilters
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
            this.tblLOPnlMatchListFilter = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbWinRate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbResultsZerg = new System.Windows.Forms.Label();
            this.lbResultsTerran = new System.Windows.Forms.Label();
            this.lbResultsProtoss = new System.Windows.Forms.Label();
            this.lbResultsRandom = new System.Windows.Forms.Label();
            this.lbResultsTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbOpponent = new System.Windows.Forms.Label();
            this.btnSelectOpponent = new System.Windows.Forms.Button();
            this.picBxPlayer = new System.Windows.Forms.PictureBox();
            this.picBxMap = new System.Windows.Forms.PictureBox();
            this.btnRemovePlayer = new System.Windows.Forms.Button();
            this.cmbBxMapSelection = new System.Windows.Forms.ComboBox();
            this.toolTipMatchListFilter = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.tblLoPnlResults = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbMapStatsHeader = new System.Windows.Forms.Label();
            this.lbRaceVsZerg = new System.Windows.Forms.Label();
            this.lbRaceVsTerran = new System.Windows.Forms.Label();
            this.lbRaceVsProtoss = new System.Windows.Forms.Label();
            this.tblLOPnlMatchListFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxMap)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tblLoPnlResults.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlMatchListFilter
            // 
            this.tblLOPnlMatchListFilter.ColumnCount = 3;
            this.tblLOPnlMatchListFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tblLOPnlMatchListFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tblLOPnlMatchListFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMatchListFilter.Controls.Add(this.panel5, 2, 1);
            this.tblLOPnlMatchListFilter.Controls.Add(this.label8, 0, 0);
            this.tblLOPnlMatchListFilter.Controls.Add(this.panel4, 1, 1);
            this.tblLOPnlMatchListFilter.Controls.Add(this.panel2, 0, 1);
            this.tblLOPnlMatchListFilter.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblLOPnlMatchListFilter.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlMatchListFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlMatchListFilter.Name = "tblLOPnlMatchListFilter";
            this.tblLOPnlMatchListFilter.RowCount = 3;
            this.tblLOPnlMatchListFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlMatchListFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tblLOPnlMatchListFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMatchListFilter.Size = new System.Drawing.Size(927, 178);
            this.tblLOPnlMatchListFilter.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tblLoPnlResults.SetColumnSpan(this.label3, 4);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(455, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Filtered results, total:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Zerg";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWinRate
            // 
            this.lbWinRate.AutoSize = true;
            this.lbWinRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWinRate.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWinRate.Location = new System.Drawing.Point(118, 94);
            this.lbWinRate.Name = "lbWinRate";
            this.lbWinRate.Size = new System.Drawing.Size(109, 23);
            this.lbWinRate.TabIndex = 0;
            this.lbWinRate.Text = "Terran";
            this.lbWinRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(233, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Protoss";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(348, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Random";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbResultsZerg
            // 
            this.lbResultsZerg.AutoSize = true;
            this.lbResultsZerg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultsZerg.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultsZerg.Location = new System.Drawing.Point(3, 117);
            this.lbResultsZerg.Name = "lbResultsZerg";
            this.lbResultsZerg.Size = new System.Drawing.Size(109, 23);
            this.lbResultsZerg.TabIndex = 0;
            this.lbResultsZerg.Text = "<result>";
            this.lbResultsZerg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbResultsTerran
            // 
            this.lbResultsTerran.AutoSize = true;
            this.lbResultsTerran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultsTerran.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultsTerran.Location = new System.Drawing.Point(118, 117);
            this.lbResultsTerran.Name = "lbResultsTerran";
            this.lbResultsTerran.Size = new System.Drawing.Size(109, 23);
            this.lbResultsTerran.TabIndex = 0;
            this.lbResultsTerran.Text = "<result>";
            this.lbResultsTerran.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbResultsProtoss
            // 
            this.lbResultsProtoss.AutoSize = true;
            this.lbResultsProtoss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultsProtoss.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultsProtoss.Location = new System.Drawing.Point(233, 117);
            this.lbResultsProtoss.Name = "lbResultsProtoss";
            this.lbResultsProtoss.Size = new System.Drawing.Size(109, 23);
            this.lbResultsProtoss.TabIndex = 0;
            this.lbResultsProtoss.Text = "<result>";
            this.lbResultsProtoss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbResultsRandom
            // 
            this.lbResultsRandom.AutoSize = true;
            this.lbResultsRandom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultsRandom.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultsRandom.Location = new System.Drawing.Point(348, 117);
            this.lbResultsRandom.Name = "lbResultsRandom";
            this.lbResultsRandom.Size = new System.Drawing.Size(110, 23);
            this.lbResultsRandom.TabIndex = 0;
            this.lbResultsRandom.Text = "<result>";
            this.lbResultsRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbResultsTotal
            // 
            this.lbResultsTotal.AutoSize = true;
            this.tblLoPnlResults.SetColumnSpan(this.lbResultsTotal, 4);
            this.lbResultsTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultsTotal.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultsTotal.Location = new System.Drawing.Point(3, 23);
            this.lbResultsTotal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.lbResultsTotal.Name = "lbResultsTotal";
            this.lbResultsTotal.Size = new System.Drawing.Size(455, 24);
            this.lbResultsTotal.TabIndex = 0;
            this.lbResultsTotal.Text = "<results total>";
            this.lbResultsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tblLoPnlResults.SetColumnSpan(this.label4, 4);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(455, 35);
            this.label4.TabIndex = 0;
            this.label4.Text = "Filtered results, vs.:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tblLOPnlMatchListFilter.SetColumnSpan(this.label8, 3);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(921, 27);
            this.label8.TabIndex = 0;
            this.label8.Text = "Filters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Head-to-head filter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbOpponent
            // 
            this.lbOpponent.AutoSize = true;
            this.lbOpponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOpponent.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpponent.Location = new System.Drawing.Point(3, 26);
            this.lbOpponent.Name = "lbOpponent";
            this.lbOpponent.Size = new System.Drawing.Size(142, 70);
            this.lbOpponent.TabIndex = 0;
            this.lbOpponent.Text = "<no player selected>";
            this.lbOpponent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectOpponent
            // 
            this.btnSelectOpponent.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnSelectOpponent.Location = new System.Drawing.Point(38, 8);
            this.btnSelectOpponent.Margin = new System.Windows.Forms.Padding(6, 6, 12, 6);
            this.btnSelectOpponent.Name = "btnSelectOpponent";
            this.btnSelectOpponent.Size = new System.Drawing.Size(60, 30);
            this.btnSelectOpponent.TabIndex = 1;
            this.btnSelectOpponent.Text = "Browse";
            this.btnSelectOpponent.UseVisualStyleBackColor = true;
            this.btnSelectOpponent.Click += new System.EventHandler(this.btnSelectPlayer_Click);
            // 
            // picBxPlayer
            // 
            this.picBxPlayer.Location = new System.Drawing.Point(151, 31);
            this.picBxPlayer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.picBxPlayer.Name = "picBxPlayer";
            this.picBxPlayer.Size = new System.Drawing.Size(64, 60);
            this.picBxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxPlayer.TabIndex = 3;
            this.picBxPlayer.TabStop = false;
            // 
            // picBxMap
            // 
            this.picBxMap.Location = new System.Drawing.Point(3, 31);
            this.picBxMap.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.picBxMap.Name = "picBxMap";
            this.tableLayoutPanel3.SetRowSpan(this.picBxMap, 4);
            this.picBxMap.Size = new System.Drawing.Size(64, 60);
            this.picBxMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxMap.TabIndex = 3;
            this.picBxMap.TabStop = false;
            // 
            // btnRemovePlayer
            // 
            this.btnRemovePlayer.Enabled = false;
            this.btnRemovePlayer.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnRemovePlayer.Location = new System.Drawing.Point(122, 8);
            this.btnRemovePlayer.Margin = new System.Windows.Forms.Padding(12, 6, 6, 6);
            this.btnRemovePlayer.Name = "btnRemovePlayer";
            this.btnRemovePlayer.Size = new System.Drawing.Size(60, 30);
            this.btnRemovePlayer.TabIndex = 1;
            this.btnRemovePlayer.Text = "Clear";
            this.toolTipMatchListFilter.SetToolTip(this.btnRemovePlayer, "Clear Player");
            this.btnRemovePlayer.UseVisualStyleBackColor = true;
            this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click);
            // 
            // cmbBxMapSelection
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.cmbBxMapSelection, 2);
            this.cmbBxMapSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBxMapSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxMapSelection.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxMapSelection.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbBxMapSelection.FormattingEnabled = true;
            this.cmbBxMapSelection.Location = new System.Drawing.Point(8, 105);
            this.cmbBxMapSelection.Margin = new System.Windows.Forms.Padding(8, 9, 8, 3);
            this.cmbBxMapSelection.Name = "cmbBxMapSelection";
            this.cmbBxMapSelection.Size = new System.Drawing.Size(204, 26);
            this.cmbBxMapSelection.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Location = new System.Drawing.Point(4, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 140);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.picBxPlayer, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbOpponent, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(218, 138);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.btnRemovePlayer);
            this.panel3.Controls.Add(this.btnSelectOpponent);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 96);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(218, 44);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tableLayoutPanel3);
            this.panel4.Location = new System.Drawing.Point(232, 34);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(220, 140);
            this.panel4.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lbRaceVsZerg, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbBxMapSelection, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.picBxMap, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbMapStatsHeader, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbRaceVsTerran, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lbRaceVsProtoss, 1, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(218, 138);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label9, 2);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(214, 26);
            this.label9.TabIndex = 0;
            this.label9.Text = "Map filter";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLoPnlResults
            // 
            this.tblLoPnlResults.ColumnCount = 4;
            this.tblLoPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLoPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLoPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLoPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLoPnlResults.Controls.Add(this.lbResultsZerg, 0, 4);
            this.tblLoPnlResults.Controls.Add(this.label5, 2, 3);
            this.tblLoPnlResults.Controls.Add(this.lbResultsTerran, 1, 4);
            this.tblLoPnlResults.Controls.Add(this.label6, 0, 3);
            this.tblLoPnlResults.Controls.Add(this.lbResultsProtoss, 2, 4);
            this.tblLoPnlResults.Controls.Add(this.label3, 0, 0);
            this.tblLoPnlResults.Controls.Add(this.lbResultsRandom, 3, 4);
            this.tblLoPnlResults.Controls.Add(this.lbResultsTotal, 0, 1);
            this.tblLoPnlResults.Controls.Add(this.label4, 0, 2);
            this.tblLoPnlResults.Controls.Add(this.lbWinRate, 1, 3);
            this.tblLoPnlResults.Controls.Add(this.label7, 3, 3);
            this.tblLoPnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlResults.Location = new System.Drawing.Point(0, 0);
            this.tblLoPnlResults.Margin = new System.Windows.Forms.Padding(0);
            this.tblLoPnlResults.Name = "tblLoPnlResults";
            this.tblLoPnlResults.RowCount = 6;
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblLoPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlResults.Size = new System.Drawing.Size(461, 138);
            this.tblLoPnlResults.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tblLoPnlResults);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(460, 34);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(463, 140);
            this.panel5.TabIndex = 3;
            // 
            // lbMapStatsHeader
            // 
            this.lbMapStatsHeader.AutoSize = true;
            this.lbMapStatsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMapStatsHeader.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMapStatsHeader.Location = new System.Drawing.Point(73, 26);
            this.lbMapStatsHeader.Name = "lbMapStatsHeader";
            this.lbMapStatsHeader.Size = new System.Drawing.Size(144, 19);
            this.lbMapStatsHeader.TabIndex = 0;
            this.lbMapStatsHeader.Text = "Map\'s matchup stats:";
            this.lbMapStatsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRaceVsZerg
            // 
            this.lbRaceVsZerg.AutoSize = true;
            this.lbRaceVsZerg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRaceVsZerg.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRaceVsZerg.Location = new System.Drawing.Point(73, 45);
            this.lbRaceVsZerg.Name = "lbRaceVsZerg";
            this.lbRaceVsZerg.Size = new System.Drawing.Size(144, 17);
            this.lbRaceVsZerg.TabIndex = 0;
            this.lbRaceVsZerg.Text = "<result>";
            this.lbRaceVsZerg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRaceVsTerran
            // 
            this.lbRaceVsTerran.AutoSize = true;
            this.lbRaceVsTerran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRaceVsTerran.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRaceVsTerran.Location = new System.Drawing.Point(73, 62);
            this.lbRaceVsTerran.Name = "lbRaceVsTerran";
            this.lbRaceVsTerran.Size = new System.Drawing.Size(144, 17);
            this.lbRaceVsTerran.TabIndex = 0;
            this.lbRaceVsTerran.Text = "<result>";
            this.lbRaceVsTerran.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRaceVsProtoss
            // 
            this.lbRaceVsProtoss.AutoSize = true;
            this.lbRaceVsProtoss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRaceVsProtoss.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRaceVsProtoss.Location = new System.Drawing.Point(73, 79);
            this.lbRaceVsProtoss.Name = "lbRaceVsProtoss";
            this.lbRaceVsProtoss.Size = new System.Drawing.Size(144, 17);
            this.lbRaceVsProtoss.TabIndex = 0;
            this.lbRaceVsProtoss.Text = "<result>";
            this.lbRaceVsProtoss.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ResultsFilters
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tblLOPnlMatchListFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ResultsFilters";
            this.Size = new System.Drawing.Size(927, 181);
            this.tblLOPnlMatchListFilter.ResumeLayout(false);
            this.tblLOPnlMatchListFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxMap)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tblLoPnlResults.ResumeLayout(false);
            this.tblLoPnlResults.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlMatchListFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbOpponent;
        private System.Windows.Forms.Button btnSelectOpponent;
        private System.Windows.Forms.ComboBox cmbBxMapSelection;
        private System.Windows.Forms.PictureBox picBxPlayer;
        private System.Windows.Forms.PictureBox picBxMap;
        private System.Windows.Forms.Button btnRemovePlayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbWinRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbResultsZerg;
        private System.Windows.Forms.Label lbResultsTerran;
        private System.Windows.Forms.Label lbResultsProtoss;
        private System.Windows.Forms.Label lbResultsRandom;
        private System.Windows.Forms.Label lbResultsTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTipMatchListFilter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlResults;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbRaceVsZerg;
        private System.Windows.Forms.Label lbMapStatsHeader;
        private System.Windows.Forms.Label lbRaceVsTerran;
        private System.Windows.Forms.Label lbRaceVsProtoss;
    }
}
