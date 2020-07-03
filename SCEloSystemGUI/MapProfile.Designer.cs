namespace SCEloSystemGUI
{
    partial class MapProfile
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

            this.raceImageCache.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblLOPnlMapProfile = new System.Windows.Forms.TableLayoutPanel();
            this.lbName = new System.Windows.Forms.Label();
            this.grpBxMapDetails = new System.Windows.Forms.GroupBox();
            this.tblLOPnlMapDetails = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbSpawnPositions = new System.Windows.Forms.Label();
            this.lbTileset = new System.Windows.Forms.Label();
            this.lbSize = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbDescription = new System.Windows.Forms.Label();
            this.grpBxMapImage = new System.Windows.Forms.GroupBox();
            this.picBxMapImage = new System.Windows.Forms.PictureBox();
            this.grpBxMapStats = new System.Windows.Forms.GroupBox();
            this.tblLOPnlMapStats = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbTotalGames = new System.Windows.Forms.Label();
            this.grpBxGames = new System.Windows.Forms.GroupBox();
            this.tblLOPnlGames = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.tblLOPnlMapProfile.SuspendLayout();
            this.grpBxMapDetails.SuspendLayout();
            this.tblLOPnlMapDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpBxMapImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxMapImage)).BeginInit();
            this.grpBxMapStats.SuspendLayout();
            this.tblLOPnlMapStats.SuspendLayout();
            this.grpBxGames.SuspendLayout();
            this.tblLOPnlGames.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlMapProfile
            // 
            this.tblLOPnlMapProfile.BackColor = System.Drawing.Color.Transparent;
            this.tblLOPnlMapProfile.ColumnCount = 4;
            this.tblLOPnlMapProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tblLOPnlMapProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 530F));
            this.tblLOPnlMapProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 815F));
            this.tblLOPnlMapProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapProfile.Controls.Add(this.lbName, 0, 0);
            this.tblLOPnlMapProfile.Controls.Add(this.grpBxMapDetails, 0, 1);
            this.tblLOPnlMapProfile.Controls.Add(this.grpBxMapImage, 1, 1);
            this.tblLOPnlMapProfile.Controls.Add(this.grpBxMapStats, 0, 2);
            this.tblLOPnlMapProfile.Controls.Add(this.grpBxGames, 2, 1);
            this.tblLOPnlMapProfile.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblLOPnlMapProfile.ForeColor = System.Drawing.Color.White;
            this.tblLOPnlMapProfile.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlMapProfile.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlMapProfile.Margin = new System.Windows.Forms.Padding(4);
            this.tblLOPnlMapProfile.Name = "tblLOPnlMapProfile";
            this.tblLOPnlMapProfile.RowCount = 4;
            this.tblLOPnlMapProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tblLOPnlMapProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 544F));
            this.tblLOPnlMapProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tblLOPnlMapProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapProfile.Size = new System.Drawing.Size(1695, 866);
            this.tblLOPnlMapProfile.TabIndex = 0;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.tblLOPnlMapProfile.SetColumnSpan(this.lbName, 3);
            this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbName.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.White;
            this.lbName.Location = new System.Drawing.Point(4, 0);
            this.lbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(1687, 53);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "[NAME]";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBxMapDetails
            // 
            this.grpBxMapDetails.Controls.Add(this.tblLOPnlMapDetails);
            this.grpBxMapDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxMapDetails.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxMapDetails.ForeColor = System.Drawing.Color.White;
            this.grpBxMapDetails.Location = new System.Drawing.Point(3, 56);
            this.grpBxMapDetails.Name = "grpBxMapDetails";
            this.grpBxMapDetails.Size = new System.Drawing.Size(344, 538);
            this.grpBxMapDetails.TabIndex = 1;
            this.grpBxMapDetails.TabStop = false;
            this.grpBxMapDetails.Text = "Map Details";
            // 
            // tblLOPnlMapDetails
            // 
            this.tblLOPnlMapDetails.ColumnCount = 3;
            this.tblLOPnlMapDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLOPnlMapDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblLOPnlMapDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapDetails.Controls.Add(this.label1, 0, 0);
            this.tblLOPnlMapDetails.Controls.Add(this.label2, 0, 1);
            this.tblLOPnlMapDetails.Controls.Add(this.label3, 0, 2);
            this.tblLOPnlMapDetails.Controls.Add(this.label4, 0, 3);
            this.tblLOPnlMapDetails.Controls.Add(this.label5, 1, 0);
            this.tblLOPnlMapDetails.Controls.Add(this.label6, 1, 1);
            this.tblLOPnlMapDetails.Controls.Add(this.label7, 1, 2);
            this.tblLOPnlMapDetails.Controls.Add(this.lbSpawnPositions, 2, 0);
            this.tblLOPnlMapDetails.Controls.Add(this.lbTileset, 2, 1);
            this.tblLOPnlMapDetails.Controls.Add(this.lbSize, 2, 2);
            this.tblLOPnlMapDetails.Controls.Add(this.panel1, 0, 4);
            this.tblLOPnlMapDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlMapDetails.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlMapDetails.Name = "tblLOPnlMapDetails";
            this.tblLOPnlMapDetails.RowCount = 5;
            this.tblLOPnlMapDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlMapDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlMapDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlMapDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblLOPnlMapDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapDetails.Size = new System.Drawing.Size(338, 512);
            this.tblLOPnlMapDetails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Spawn Positions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tileset";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Size";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tblLOPnlMapDetails.SetColumnSpan(this.label4, 3);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 6, 21, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 36);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(123, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = ":";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(123, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 32);
            this.label6.TabIndex = 0;
            this.label6.Text = ":";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(123, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 32);
            this.label7.TabIndex = 0;
            this.label7.Text = ":";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSpawnPositions
            // 
            this.lbSpawnPositions.AutoSize = true;
            this.lbSpawnPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSpawnPositions.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSpawnPositions.Location = new System.Drawing.Point(139, 0);
            this.lbSpawnPositions.Name = "lbSpawnPositions";
            this.lbSpawnPositions.Size = new System.Drawing.Size(196, 32);
            this.lbSpawnPositions.TabIndex = 0;
            this.lbSpawnPositions.Text = "[SpawnPositions]";
            this.lbSpawnPositions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTileset
            // 
            this.lbTileset.AutoSize = true;
            this.lbTileset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTileset.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTileset.Location = new System.Drawing.Point(139, 32);
            this.lbTileset.Name = "lbTileset";
            this.lbTileset.Size = new System.Drawing.Size(196, 32);
            this.lbTileset.TabIndex = 0;
            this.lbTileset.Text = "[TileSet]";
            this.lbTileset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSize
            // 
            this.lbSize.AutoSize = true;
            this.lbSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSize.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSize.Location = new System.Drawing.Point(139, 64);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(196, 32);
            this.lbSize.TabIndex = 0;
            this.lbSize.Text = "[Size]";
            this.lbSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.tblLOPnlMapDetails.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.lbDescription);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 144);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 348);
            this.panel1.TabIndex = 1;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescription.Location = new System.Drawing.Point(3, 6);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.lbDescription.MinimumSize = new System.Drawing.Size(315, 18);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(315, 18);
            this.lbDescription.TabIndex = 0;
            this.lbDescription.Text = "[Description]";
            // 
            // grpBxMapImage
            // 
            this.grpBxMapImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.grpBxMapImage.Controls.Add(this.picBxMapImage);
            this.grpBxMapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxMapImage.Location = new System.Drawing.Point(353, 56);
            this.grpBxMapImage.Name = "grpBxMapImage";
            this.grpBxMapImage.Padding = new System.Windows.Forms.Padding(0);
            this.grpBxMapImage.Size = new System.Drawing.Size(524, 538);
            this.grpBxMapImage.TabIndex = 2;
            this.grpBxMapImage.TabStop = false;
            // 
            // picBxMapImage
            // 
            this.picBxMapImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBxMapImage.Location = new System.Drawing.Point(6, 18);
            this.picBxMapImage.Margin = new System.Windows.Forms.Padding(0);
            this.picBxMapImage.Name = "picBxMapImage";
            this.picBxMapImage.Size = new System.Drawing.Size(512, 512);
            this.picBxMapImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxMapImage.TabIndex = 0;
            this.picBxMapImage.TabStop = false;
            // 
            // grpBxMapStats
            // 
            this.tblLOPnlMapProfile.SetColumnSpan(this.grpBxMapStats, 2);
            this.grpBxMapStats.Controls.Add(this.tblLOPnlMapStats);
            this.grpBxMapStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxMapStats.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxMapStats.ForeColor = System.Drawing.Color.White;
            this.grpBxMapStats.Location = new System.Drawing.Point(3, 600);
            this.grpBxMapStats.Name = "grpBxMapStats";
            this.grpBxMapStats.Size = new System.Drawing.Size(874, 261);
            this.grpBxMapStats.TabIndex = 3;
            this.grpBxMapStats.TabStop = false;
            this.grpBxMapStats.Text = "Statistics";
            // 
            // tblLOPnlMapStats
            // 
            this.tblLOPnlMapStats.ColumnCount = 3;
            this.tblLOPnlMapStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLOPnlMapStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tblLOPnlMapStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapStats.Controls.Add(this.label10, 2, 1);
            this.tblLOPnlMapStats.Controls.Add(this.label9, 0, 1);
            this.tblLOPnlMapStats.Controls.Add(this.label11, 0, 0);
            this.tblLOPnlMapStats.Controls.Add(this.lbTotalGames, 1, 0);
            this.tblLOPnlMapStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlMapStats.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlMapStats.Name = "tblLOPnlMapStats";
            this.tblLOPnlMapStats.RowCount = 3;
            this.tblLOPnlMapStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlMapStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlMapStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMapStats.Size = new System.Drawing.Size(868, 235);
            this.tblLOPnlMapStats.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(468, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(397, 32);
            this.label10.TabIndex = 0;
            this.label10.Text = "Mirror Matchups:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tblLOPnlMapStats.SetColumnSpan(this.label9, 2);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(459, 32);
            this.label9.TabIndex = 0;
            this.label9.Text = "Win Ratios:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 32);
            this.label11.TabIndex = 0;
            this.label11.Text = "Total Games:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotalGames
            // 
            this.lbTotalGames.AutoSize = true;
            this.lbTotalGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTotalGames.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalGames.Location = new System.Drawing.Point(103, 0);
            this.lbTotalGames.Name = "lbTotalGames";
            this.lbTotalGames.Size = new System.Drawing.Size(359, 32);
            this.lbTotalGames.TabIndex = 0;
            this.lbTotalGames.Text = "[total games]";
            this.lbTotalGames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBxGames
            // 
            this.grpBxGames.Controls.Add(this.tblLOPnlGames);
            this.grpBxGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxGames.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxGames.ForeColor = System.Drawing.Color.White;
            this.grpBxGames.Location = new System.Drawing.Point(883, 56);
            this.grpBxGames.Name = "grpBxGames";
            this.tblLOPnlMapProfile.SetRowSpan(this.grpBxGames, 2);
            this.grpBxGames.Size = new System.Drawing.Size(809, 805);
            this.grpBxGames.TabIndex = 4;
            this.grpBxGames.TabStop = false;
            this.grpBxGames.Text = "Games";
            // 
            // tblLOPnlGames
            // 
            this.tblLOPnlGames.ColumnCount = 2;
            this.tblLOPnlGames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271F));
            this.tblLOPnlGames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlGames.Controls.Add(this.label12, 0, 0);
            this.tblLOPnlGames.Controls.Add(this.btnApplyFilter, 1, 1);
            this.tblLOPnlGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlGames.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlGames.Name = "tblLOPnlGames";
            this.tblLOPnlGames.RowCount = 4;
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlGames.Size = new System.Drawing.Size(803, 779);
            this.tblLOPnlGames.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(265, 32);
            this.label12.TabIndex = 0;
            this.label12.Text = "Filters:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyFilter.Enabled = false;
            this.btnApplyFilter.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyFilter.ForeColor = System.Drawing.Color.Black;
            this.btnApplyFilter.Location = new System.Drawing.Point(688, 35);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(112, 36);
            this.btnApplyFilter.TabIndex = 1;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // MapProfile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1701, 864);
            this.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.SpaceBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1715, 886);
            this.Controls.Add(this.tblLOPnlMapProfile);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1731, 924);
            this.Name = "MapProfile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Profile";
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MapProfile_HelpRequested);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapProfile_KeyUp);
            this.tblLOPnlMapProfile.ResumeLayout(false);
            this.tblLOPnlMapProfile.PerformLayout();
            this.grpBxMapDetails.ResumeLayout(false);
            this.tblLOPnlMapDetails.ResumeLayout(false);
            this.tblLOPnlMapDetails.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpBxMapImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBxMapImage)).EndInit();
            this.grpBxMapStats.ResumeLayout(false);
            this.tblLOPnlMapStats.ResumeLayout(false);
            this.tblLOPnlMapStats.PerformLayout();
            this.grpBxGames.ResumeLayout(false);
            this.tblLOPnlGames.ResumeLayout(false);
            this.tblLOPnlGames.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlMapProfile;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.GroupBox grpBxMapDetails;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlMapDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbSpawnPositions;
        private System.Windows.Forms.Label lbTileset;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.GroupBox grpBxMapImage;
        private System.Windows.Forms.PictureBox picBxMapImage;
        private System.Windows.Forms.GroupBox grpBxMapStats;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlMapStats;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbTotalGames;
        private System.Windows.Forms.GroupBox grpBxGames;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlGames;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnApplyFilter;
    }
}