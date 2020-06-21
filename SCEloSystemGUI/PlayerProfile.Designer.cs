namespace SCEloSystemGUI
{
    partial class PlayerProfile
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
            this.components = new System.ComponentModel.Container();
            this.tblLOPnlPlayerProfile = new System.Windows.Forms.TableLayoutPanel();
            this.lbName = new System.Windows.Forms.Label();
            this.grpBxPlayerInfo = new System.Windows.Forms.GroupBox();
            this.tblLOPnlPlayerDetails = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBxPlayerPhoto = new System.Windows.Forms.PictureBox();
            this.lstViewAliases = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.lbIRLName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbDateOfBirth = new System.Windows.Forms.Label();
            this.picBxRank = new System.Windows.Forms.PictureBox();
            this.picBxRace = new System.Windows.Forms.PictureBox();
            this.picBxCountry = new System.Windows.Forms.PictureBox();
            this.picBxTeam = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPlayerInfoName = new System.Windows.Forms.Label();
            this.grpBxPerformance = new System.Windows.Forms.GroupBox();
            this.tblLOPnlPerformance = new System.Windows.Forms.TableLayoutPanel();
            this.cmbBxSetDevInterval = new System.Windows.Forms.ComboBox();
            this.pnlRatingDev = new System.Windows.Forms.Panel();
            this.grpBxResults = new System.Windows.Forms.GroupBox();
            this.tblLOPnlResults = new System.Windows.Forms.TableLayoutPanel();
            this.tabCtrlResults = new System.Windows.Forms.TabControl();
            this.tabPageMatchResults = new System.Windows.Forms.TabPage();
            this.tblLOPnlMatches = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageSingleGames = new System.Windows.Forms.TabPage();
            this.tblLOPnlGames = new System.Windows.Forms.TableLayoutPanel();
            this.tblLOPnlOpponentSelection = new System.Windows.Forms.TableLayoutPanel();
            this.rdBtnAny = new System.Windows.Forms.RadioButton();
            this.rdBtnZerg = new System.Windows.Forms.RadioButton();
            this.rdBtnTerran = new System.Windows.Forms.RadioButton();
            this.rdBtnProtoss = new System.Windows.Forms.RadioButton();
            this.rdBtnRandom = new System.Windows.Forms.RadioButton();
            this.tabPageMaps = new System.Windows.Forms.TabPage();
            this.lbTeamName = new System.Windows.Forms.Label();
            this.lbCountryName = new System.Windows.Forms.Label();
            this.toolTipPlayerProfile = new System.Windows.Forms.ToolTip(this.components);
            this.tblLOPnlPlayerProfile.SuspendLayout();
            this.grpBxPlayerInfo.SuspendLayout();
            this.tblLOPnlPlayerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxPlayerPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxRank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxRace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxTeam)).BeginInit();
            this.grpBxPerformance.SuspendLayout();
            this.tblLOPnlPerformance.SuspendLayout();
            this.grpBxResults.SuspendLayout();
            this.tblLOPnlResults.SuspendLayout();
            this.tabCtrlResults.SuspendLayout();
            this.tabPageMatchResults.SuspendLayout();
            this.tabPageSingleGames.SuspendLayout();
            this.tblLOPnlGames.SuspendLayout();
            this.tblLOPnlOpponentSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlPlayerProfile
            // 
            this.tblLOPnlPlayerProfile.BackColor = System.Drawing.Color.Transparent;
            this.tblLOPnlPlayerProfile.ColumnCount = 3;
            this.tblLOPnlPlayerProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tblLOPnlPlayerProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 945F));
            this.tblLOPnlPlayerProfile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerProfile.Controls.Add(this.lbName, 0, 0);
            this.tblLOPnlPlayerProfile.Controls.Add(this.grpBxPlayerInfo, 0, 1);
            this.tblLOPnlPlayerProfile.Controls.Add(this.grpBxPerformance, 0, 2);
            this.tblLOPnlPlayerProfile.Controls.Add(this.grpBxResults, 1, 1);
            this.tblLOPnlPlayerProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPlayerProfile.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlPlayerProfile.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlPlayerProfile.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlPlayerProfile.Name = "tblLOPnlPlayerProfile";
            this.tblLOPnlPlayerProfile.RowCount = 4;
            this.tblLOPnlPlayerProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblLOPnlPlayerProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tblLOPnlPlayerProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 652F));
            this.tblLOPnlPlayerProfile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerProfile.Size = new System.Drawing.Size(1554, 956);
            this.tblLOPnlPlayerProfile.TabIndex = 0;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.Color.Transparent;
            this.tblLOPnlPlayerProfile.SetColumnSpan(this.lbName, 2);
            this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbName.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.White;
            this.lbName.Location = new System.Drawing.Point(3, 3);
            this.lbName.Margin = new System.Windows.Forms.Padding(3);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(1539, 32);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "[player name]";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBxPlayerInfo
            // 
            this.grpBxPlayerInfo.BackColor = System.Drawing.Color.Transparent;
            this.grpBxPlayerInfo.Controls.Add(this.tblLOPnlPlayerDetails);
            this.grpBxPlayerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxPlayerInfo.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxPlayerInfo.ForeColor = System.Drawing.Color.White;
            this.grpBxPlayerInfo.Location = new System.Drawing.Point(3, 41);
            this.grpBxPlayerInfo.Name = "grpBxPlayerInfo";
            this.grpBxPlayerInfo.Size = new System.Drawing.Size(594, 234);
            this.grpBxPlayerInfo.TabIndex = 2;
            this.grpBxPlayerInfo.TabStop = false;
            this.grpBxPlayerInfo.Text = "Player Info";
            // 
            // tblLOPnlPlayerDetails
            // 
            this.tblLOPnlPlayerDetails.AutoSize = true;
            this.tblLOPnlPlayerDetails.BackColor = System.Drawing.Color.Transparent;
            this.tblLOPnlPlayerDetails.ColumnCount = 8;
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tblLOPnlPlayerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLOPnlPlayerDetails.Controls.Add(this.label1, 0, 3);
            this.tblLOPnlPlayerDetails.Controls.Add(this.picBxPlayerPhoto, 7, 0);
            this.tblLOPnlPlayerDetails.Controls.Add(this.lstViewAliases, 2, 3);
            this.tblLOPnlPlayerDetails.Controls.Add(this.label4, 0, 1);
            this.tblLOPnlPlayerDetails.Controls.Add(this.lbIRLName, 2, 1);
            this.tblLOPnlPlayerDetails.Controls.Add(this.label3, 0, 2);
            this.tblLOPnlPlayerDetails.Controls.Add(this.lbDateOfBirth, 2, 2);
            this.tblLOPnlPlayerDetails.Controls.Add(this.picBxRank, 6, 0);
            this.tblLOPnlPlayerDetails.Controls.Add(this.picBxRace, 5, 0);
            this.tblLOPnlPlayerDetails.Controls.Add(this.picBxCountry, 3, 0);
            this.tblLOPnlPlayerDetails.Controls.Add(this.picBxTeam, 4, 0);
            this.tblLOPnlPlayerDetails.Controls.Add(this.label2, 1, 1);
            this.tblLOPnlPlayerDetails.Controls.Add(this.label5, 1, 2);
            this.tblLOPnlPlayerDetails.Controls.Add(this.label6, 1, 3);
            this.tblLOPnlPlayerDetails.Controls.Add(this.lbPlayerInfoName, 0, 0);
            this.tblLOPnlPlayerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPlayerDetails.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlPlayerDetails.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlPlayerDetails.Name = "tblLOPnlPlayerDetails";
            this.tblLOPnlPlayerDetails.RowCount = 4;
            this.tblLOPnlPlayerDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblLOPnlPlayerDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlPlayerDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblLOPnlPlayerDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblLOPnlPlayerDetails.Size = new System.Drawing.Size(588, 208);
            this.tblLOPnlPlayerDetails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 142);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aliases";
            // 
            // picBxPlayerPhoto
            // 
            this.picBxPlayerPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBxPlayerPhoto.Location = new System.Drawing.Point(493, 4);
            this.picBxPlayerPhoto.Margin = new System.Windows.Forms.Padding(5, 4, 5, 10);
            this.picBxPlayerPhoto.Name = "picBxPlayerPhoto";
            this.tblLOPnlPlayerDetails.SetRowSpan(this.picBxPlayerPhoto, 2);
            this.picBxPlayerPhoto.Size = new System.Drawing.Size(90, 90);
            this.picBxPlayerPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxPlayerPhoto.TabIndex = 17;
            this.picBxPlayerPhoto.TabStop = false;
            // 
            // lstViewAliases
            // 
            this.lstViewAliases.AutoArrange = false;
            this.lstViewAliases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstViewAliases.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.SpaceBackground;
            this.lstViewAliases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstViewAliases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.tblLOPnlPlayerDetails.SetColumnSpan(this.lstViewAliases, 5);
            this.lstViewAliases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewAliases.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstViewAliases.ForeColor = System.Drawing.Color.White;
            this.lstViewAliases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstViewAliases.HideSelection = false;
            this.lstViewAliases.Location = new System.Drawing.Point(119, 142);
            this.lstViewAliases.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lstViewAliases.MultiSelect = false;
            this.lstViewAliases.Name = "lstViewAliases";
            this.lstViewAliases.ShowGroups = false;
            this.lstViewAliases.Size = new System.Drawing.Size(366, 63);
            this.lstViewAliases.TabIndex = 16;
            this.lstViewAliases.UseCompatibleStateImageBehavior = false;
            this.lstViewAliases.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 184;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "IRL Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbIRLName
            // 
            this.lbIRLName.AutoSize = true;
            this.tblLOPnlPlayerDetails.SetColumnSpan(this.lbIRLName, 5);
            this.lbIRLName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIRLName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIRLName.Location = new System.Drawing.Point(119, 75);
            this.lbIRLName.Margin = new System.Windows.Forms.Padding(3);
            this.lbIRLName.Name = "lbIRLName";
            this.lbIRLName.Size = new System.Drawing.Size(366, 26);
            this.lbIRLName.TabIndex = 0;
            this.lbIRLName.Text = "[IRL Name]";
            this.lbIRLName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Date of birth";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDateOfBirth
            // 
            this.lbDateOfBirth.AutoSize = true;
            this.tblLOPnlPlayerDetails.SetColumnSpan(this.lbDateOfBirth, 5);
            this.lbDateOfBirth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDateOfBirth.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDateOfBirth.Location = new System.Drawing.Point(119, 107);
            this.lbDateOfBirth.Margin = new System.Windows.Forms.Padding(3);
            this.lbDateOfBirth.Name = "lbDateOfBirth";
            this.lbDateOfBirth.Size = new System.Drawing.Size(366, 26);
            this.lbDateOfBirth.TabIndex = 0;
            this.lbDateOfBirth.Text = "[Date of birth]";
            this.lbDateOfBirth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBxRank
            // 
            this.picBxRank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBxRank.Location = new System.Drawing.Point(420, 4);
            this.picBxRank.Margin = new System.Windows.Forms.Padding(6, 4, 6, 6);
            this.picBxRank.Name = "picBxRank";
            this.picBxRank.Size = new System.Drawing.Size(62, 62);
            this.picBxRank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBxRank.TabIndex = 18;
            this.picBxRank.TabStop = false;
            this.toolTipPlayerProfile.SetToolTip(this.picBxRank, "Overall rank");
            // 
            // picBxRace
            // 
            this.picBxRace.BackColor = System.Drawing.Color.Transparent;
            this.picBxRace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBxRace.Location = new System.Drawing.Point(346, 4);
            this.picBxRace.Margin = new System.Windows.Forms.Padding(6, 4, 6, 6);
            this.picBxRace.Name = "picBxRace";
            this.picBxRace.Size = new System.Drawing.Size(62, 62);
            this.picBxRace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxRace.TabIndex = 17;
            this.picBxRace.TabStop = false;
            this.toolTipPlayerProfile.SetToolTip(this.picBxRace, "Main race");
            // 
            // picBxCountry
            // 
            this.picBxCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBxCountry.Location = new System.Drawing.Point(198, 4);
            this.picBxCountry.Margin = new System.Windows.Forms.Padding(6, 4, 6, 6);
            this.picBxCountry.Name = "picBxCountry";
            this.picBxCountry.Size = new System.Drawing.Size(62, 62);
            this.picBxCountry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxCountry.TabIndex = 17;
            this.picBxCountry.TabStop = false;
            // 
            // picBxTeam
            // 
            this.picBxTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBxTeam.Location = new System.Drawing.Point(272, 4);
            this.picBxTeam.Margin = new System.Windows.Forms.Padding(6, 4, 6, 6);
            this.picBxTeam.Name = "picBxTeam";
            this.picBxTeam.Size = new System.Drawing.Size(62, 62);
            this.picBxTeam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxTeam.TabIndex = 17;
            this.picBxTeam.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(103, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = ":";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(103, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = ":";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(103, 142);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 63);
            this.label6.TabIndex = 0;
            this.label6.Text = ":";
            // 
            // lbPlayerInfoName
            // 
            this.lbPlayerInfoName.AutoSize = true;
            this.tblLOPnlPlayerDetails.SetColumnSpan(this.lbPlayerInfoName, 3);
            this.lbPlayerInfoName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayerInfoName.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlayerInfoName.Location = new System.Drawing.Point(3, 3);
            this.lbPlayerInfoName.Margin = new System.Windows.Forms.Padding(3);
            this.lbPlayerInfoName.Name = "lbPlayerInfoName";
            this.lbPlayerInfoName.Size = new System.Drawing.Size(186, 66);
            this.lbPlayerInfoName.TabIndex = 0;
            this.lbPlayerInfoName.Text = "IRL Name";
            this.lbPlayerInfoName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBxPerformance
            // 
            this.grpBxPerformance.Controls.Add(this.tblLOPnlPerformance);
            this.grpBxPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxPerformance.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxPerformance.ForeColor = System.Drawing.Color.White;
            this.grpBxPerformance.Location = new System.Drawing.Point(3, 281);
            this.grpBxPerformance.Name = "grpBxPerformance";
            this.grpBxPerformance.Size = new System.Drawing.Size(594, 646);
            this.grpBxPerformance.TabIndex = 3;
            this.grpBxPerformance.TabStop = false;
            this.grpBxPerformance.Text = "Performance";
            // 
            // tblLOPnlPerformance
            // 
            this.tblLOPnlPerformance.AutoScroll = true;
            this.tblLOPnlPerformance.ColumnCount = 2;
            this.tblLOPnlPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 580F));
            this.tblLOPnlPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPerformance.Controls.Add(this.cmbBxSetDevInterval, 0, 2);
            this.tblLOPnlPerformance.Controls.Add(this.pnlRatingDev, 0, 1);
            this.tblLOPnlPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPerformance.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlPerformance.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlPerformance.Name = "tblLOPnlPerformance";
            this.tblLOPnlPerformance.RowCount = 3;
            this.tblLOPnlPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tblLOPnlPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLOPnlPerformance.Size = new System.Drawing.Size(588, 620);
            this.tblLOPnlPerformance.TabIndex = 0;
            // 
            // cmbBxSetDevInterval
            // 
            this.cmbBxSetDevInterval.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbBxSetDevInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxSetDevInterval.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxSetDevInterval.FormattingEnabled = true;
            this.cmbBxSetDevInterval.Location = new System.Drawing.Point(6, 586);
            this.cmbBxSetDevInterval.Margin = new System.Windows.Forms.Padding(6);
            this.cmbBxSetDevInterval.Name = "cmbBxSetDevInterval";
            this.cmbBxSetDevInterval.Size = new System.Drawing.Size(150, 26);
            this.cmbBxSetDevInterval.TabIndex = 0;
            this.cmbBxSetDevInterval.SelectedIndexChanged += new System.EventHandler(this.cmbBxSetDevInterval_SelectedIndexChanged);
            // 
            // pnlRatingDev
            // 
            this.pnlRatingDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRatingDev.Location = new System.Drawing.Point(0, 142);
            this.pnlRatingDev.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRatingDev.Name = "pnlRatingDev";
            this.pnlRatingDev.Size = new System.Drawing.Size(580, 438);
            this.pnlRatingDev.TabIndex = 1;
            // 
            // grpBxResults
            // 
            this.grpBxResults.Controls.Add(this.tblLOPnlResults);
            this.grpBxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBxResults.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxResults.ForeColor = System.Drawing.Color.White;
            this.grpBxResults.Location = new System.Drawing.Point(603, 41);
            this.grpBxResults.Name = "grpBxResults";
            this.tblLOPnlPlayerProfile.SetRowSpan(this.grpBxResults, 2);
            this.grpBxResults.Size = new System.Drawing.Size(939, 886);
            this.grpBxResults.TabIndex = 4;
            this.grpBxResults.TabStop = false;
            this.grpBxResults.Text = "Results";
            // 
            // tblLOPnlResults
            // 
            this.tblLOPnlResults.ColumnCount = 2;
            this.tblLOPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 930F));
            this.tblLOPnlResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlResults.Controls.Add(this.tabCtrlResults, 0, 1);
            this.tblLOPnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlResults.Location = new System.Drawing.Point(3, 23);
            this.tblLOPnlResults.Name = "tblLOPnlResults";
            this.tblLOPnlResults.RowCount = 2;
            this.tblLOPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 336F));
            this.tblLOPnlResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlResults.Size = new System.Drawing.Size(933, 860);
            this.tblLOPnlResults.TabIndex = 0;
            // 
            // tabCtrlResults
            // 
            this.tabCtrlResults.Controls.Add(this.tabPageMatchResults);
            this.tabCtrlResults.Controls.Add(this.tabPageSingleGames);
            this.tabCtrlResults.Controls.Add(this.tabPageMaps);
            this.tabCtrlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlResults.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCtrlResults.Location = new System.Drawing.Point(3, 339);
            this.tabCtrlResults.Name = "tabCtrlResults";
            this.tabCtrlResults.SelectedIndex = 0;
            this.tabCtrlResults.Size = new System.Drawing.Size(924, 518);
            this.tabCtrlResults.TabIndex = 0;
            this.tabCtrlResults.SelectedIndexChanged += new System.EventHandler(this.tabCtrlResults_SelectedIndexChanged);
            // 
            // tabPageMatchResults
            // 
            this.tabPageMatchResults.Controls.Add(this.tblLOPnlMatches);
            this.tabPageMatchResults.Location = new System.Drawing.Point(4, 24);
            this.tabPageMatchResults.Name = "tabPageMatchResults";
            this.tabPageMatchResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatchResults.Size = new System.Drawing.Size(916, 490);
            this.tabPageMatchResults.TabIndex = 0;
            this.tabPageMatchResults.Text = "Match Results";
            this.tabPageMatchResults.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlMatches
            // 
            this.tblLOPnlMatches.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.SpaceBackground;
            this.tblLOPnlMatches.ColumnCount = 1;
            this.tblLOPnlMatches.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMatches.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlMatches.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlMatches.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlMatches.Name = "tblLOPnlMatches";
            this.tblLOPnlMatches.RowCount = 2;
            this.tblLOPnlMatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLOPnlMatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMatches.Size = new System.Drawing.Size(910, 484);
            this.tblLOPnlMatches.TabIndex = 0;
            // 
            // tabPageSingleGames
            // 
            this.tabPageSingleGames.Controls.Add(this.tblLOPnlGames);
            this.tabPageSingleGames.Location = new System.Drawing.Point(4, 24);
            this.tabPageSingleGames.Name = "tabPageSingleGames";
            this.tabPageSingleGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingleGames.Size = new System.Drawing.Size(916, 478);
            this.tabPageSingleGames.TabIndex = 1;
            this.tabPageSingleGames.Text = "Single Games";
            this.tabPageSingleGames.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlGames
            // 
            this.tblLOPnlGames.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.SpaceBackground;
            this.tblLOPnlGames.ColumnCount = 2;
            this.tblLOPnlGames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlGames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tblLOPnlGames.Controls.Add(this.tblLOPnlOpponentSelection, 0, 0);
            this.tblLOPnlGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlGames.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlGames.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlGames.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlGames.Name = "tblLOPnlGames";
            this.tblLOPnlGames.RowCount = 2;
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLOPnlGames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlGames.Size = new System.Drawing.Size(910, 472);
            this.tblLOPnlGames.TabIndex = 0;
            // 
            // tblLOPnlOpponentSelection
            // 
            this.tblLOPnlOpponentSelection.ColumnCount = 5;
            this.tblLOPnlOpponentSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLOPnlOpponentSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLOPnlOpponentSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLOPnlOpponentSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLOPnlOpponentSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLOPnlOpponentSelection.Controls.Add(this.rdBtnAny, 0, 0);
            this.tblLOPnlOpponentSelection.Controls.Add(this.rdBtnZerg, 1, 0);
            this.tblLOPnlOpponentSelection.Controls.Add(this.rdBtnTerran, 2, 0);
            this.tblLOPnlOpponentSelection.Controls.Add(this.rdBtnProtoss, 3, 0);
            this.tblLOPnlOpponentSelection.Controls.Add(this.rdBtnRandom, 4, 0);
            this.tblLOPnlOpponentSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlOpponentSelection.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLOPnlOpponentSelection.Location = new System.Drawing.Point(4, 3);
            this.tblLOPnlOpponentSelection.Margin = new System.Windows.Forms.Padding(4, 3, 100, 3);
            this.tblLOPnlOpponentSelection.Name = "tblLOPnlOpponentSelection";
            this.tblLOPnlOpponentSelection.RowCount = 1;
            this.tblLOPnlOpponentSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlOpponentSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlOpponentSelection.Size = new System.Drawing.Size(406, 30);
            this.tblLOPnlOpponentSelection.TabIndex = 0;
            // 
            // rdBtnAny
            // 
            this.rdBtnAny.AutoSize = true;
            this.rdBtnAny.Checked = true;
            this.rdBtnAny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnAny.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdBtnAny.Location = new System.Drawing.Point(3, 3);
            this.rdBtnAny.Name = "rdBtnAny";
            this.rdBtnAny.Size = new System.Drawing.Size(75, 24);
            this.rdBtnAny.TabIndex = 0;
            this.rdBtnAny.TabStop = true;
            this.rdBtnAny.Text = "Any";
            this.rdBtnAny.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rdBtnAny.UseMnemonic = false;
            this.rdBtnAny.UseVisualStyleBackColor = true;
            this.rdBtnAny.CheckedChanged += new System.EventHandler(this.OnOpponentRaceChanged);
            // 
            // rdBtnZerg
            // 
            this.rdBtnZerg.AutoSize = true;
            this.rdBtnZerg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdBtnZerg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnZerg.Image = global::SCEloSystemGUI.Properties.Resources.Zicon;
            this.rdBtnZerg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdBtnZerg.Location = new System.Drawing.Point(84, 3);
            this.rdBtnZerg.Name = "rdBtnZerg";
            this.rdBtnZerg.Size = new System.Drawing.Size(75, 24);
            this.rdBtnZerg.TabIndex = 0;
            this.rdBtnZerg.TabStop = true;
            this.rdBtnZerg.Text = "vs";
            this.rdBtnZerg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rdBtnZerg.UseMnemonic = false;
            this.rdBtnZerg.UseVisualStyleBackColor = true;
            this.rdBtnZerg.CheckedChanged += new System.EventHandler(this.OnOpponentRaceChanged);
            // 
            // rdBtnTerran
            // 
            this.rdBtnTerran.AutoSize = true;
            this.rdBtnTerran.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdBtnTerran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnTerran.Image = global::SCEloSystemGUI.Properties.Resources.Ticon;
            this.rdBtnTerran.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdBtnTerran.Location = new System.Drawing.Point(165, 3);
            this.rdBtnTerran.Name = "rdBtnTerran";
            this.rdBtnTerran.Size = new System.Drawing.Size(75, 24);
            this.rdBtnTerran.TabIndex = 0;
            this.rdBtnTerran.TabStop = true;
            this.rdBtnTerran.Text = "vs";
            this.rdBtnTerran.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rdBtnTerran.UseMnemonic = false;
            this.rdBtnTerran.UseVisualStyleBackColor = true;
            this.rdBtnTerran.CheckedChanged += new System.EventHandler(this.OnOpponentRaceChanged);
            // 
            // rdBtnProtoss
            // 
            this.rdBtnProtoss.AutoSize = true;
            this.rdBtnProtoss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdBtnProtoss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnProtoss.Image = global::SCEloSystemGUI.Properties.Resources.Picon;
            this.rdBtnProtoss.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdBtnProtoss.Location = new System.Drawing.Point(246, 3);
            this.rdBtnProtoss.Name = "rdBtnProtoss";
            this.rdBtnProtoss.Size = new System.Drawing.Size(75, 24);
            this.rdBtnProtoss.TabIndex = 0;
            this.rdBtnProtoss.TabStop = true;
            this.rdBtnProtoss.Text = "vs";
            this.rdBtnProtoss.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rdBtnProtoss.UseMnemonic = false;
            this.rdBtnProtoss.UseVisualStyleBackColor = true;
            this.rdBtnProtoss.CheckedChanged += new System.EventHandler(this.OnOpponentRaceChanged);
            // 
            // rdBtnRandom
            // 
            this.rdBtnRandom.AutoSize = true;
            this.rdBtnRandom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdBtnRandom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnRandom.Image = global::SCEloSystemGUI.Properties.Resources.Ricon;
            this.rdBtnRandom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdBtnRandom.Location = new System.Drawing.Point(327, 3);
            this.rdBtnRandom.Name = "rdBtnRandom";
            this.rdBtnRandom.Size = new System.Drawing.Size(76, 24);
            this.rdBtnRandom.TabIndex = 0;
            this.rdBtnRandom.TabStop = true;
            this.rdBtnRandom.Text = "vs";
            this.rdBtnRandom.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rdBtnRandom.UseMnemonic = false;
            this.rdBtnRandom.UseVisualStyleBackColor = true;
            this.rdBtnRandom.CheckedChanged += new System.EventHandler(this.OnOpponentRaceChanged);
            // 
            // tabPageMaps
            // 
            this.tabPageMaps.Location = new System.Drawing.Point(4, 24);
            this.tabPageMaps.Name = "tabPageMaps";
            this.tabPageMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMaps.Size = new System.Drawing.Size(916, 478);
            this.tabPageMaps.TabIndex = 2;
            this.tabPageMaps.Text = "Maps";
            this.tabPageMaps.UseVisualStyleBackColor = true;
            // 
            // lbTeamName
            // 
            this.lbTeamName.AutoSize = true;
            this.lbTeamName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTeamName.Location = new System.Drawing.Point(363, 39);
            this.lbTeamName.Margin = new System.Windows.Forms.Padding(3);
            this.lbTeamName.Name = "lbTeamName";
            this.lbTeamName.Size = new System.Drawing.Size(0, 18);
            this.lbTeamName.TabIndex = 0;
            this.lbTeamName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCountryName
            // 
            this.lbCountryName.AutoSize = true;
            this.lbCountryName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountryName.Location = new System.Drawing.Point(363, 3);
            this.lbCountryName.Margin = new System.Windows.Forms.Padding(3);
            this.lbCountryName.Name = "lbCountryName";
            this.lbCountryName.Size = new System.Drawing.Size(0, 18);
            this.lbCountryName.TabIndex = 0;
            this.lbCountryName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerProfile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1545, 903);
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.SpaceBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1554, 956);
            this.Controls.Add(this.tblLOPnlPlayerProfile);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1570, 994);
            this.Name = "PlayerProfile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Profile";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PlayerProfile_KeyUp);
            this.tblLOPnlPlayerProfile.ResumeLayout(false);
            this.tblLOPnlPlayerProfile.PerformLayout();
            this.grpBxPlayerInfo.ResumeLayout(false);
            this.grpBxPlayerInfo.PerformLayout();
            this.tblLOPnlPlayerDetails.ResumeLayout(false);
            this.tblLOPnlPlayerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxPlayerPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxRank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxRace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxTeam)).EndInit();
            this.grpBxPerformance.ResumeLayout(false);
            this.tblLOPnlPerformance.ResumeLayout(false);
            this.grpBxResults.ResumeLayout(false);
            this.tblLOPnlResults.ResumeLayout(false);
            this.tabCtrlResults.ResumeLayout(false);
            this.tabPageMatchResults.ResumeLayout(false);
            this.tabPageSingleGames.ResumeLayout(false);
            this.tblLOPnlGames.ResumeLayout(false);
            this.tblLOPnlOpponentSelection.ResumeLayout(false);
            this.tblLOPnlOpponentSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayerProfile;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayerDetails;
        private System.Windows.Forms.Label lbIRLName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstViewAliases;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBxCountry;
        private System.Windows.Forms.ToolTip toolTipPlayerProfile;
        private System.Windows.Forms.PictureBox picBxTeam;
        private System.Windows.Forms.Label lbTeamName;
        private System.Windows.Forms.Label lbCountryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbDateOfBirth;
        private System.Windows.Forms.PictureBox picBxPlayerPhoto;
        private System.Windows.Forms.PictureBox picBxRace;
        private System.Windows.Forms.GroupBox grpBxPlayerInfo;
        private System.Windows.Forms.GroupBox grpBxPerformance;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlPerformance;
        private System.Windows.Forms.ComboBox cmbBxSetDevInterval;
        private System.Windows.Forms.Panel pnlRatingDev;
        private System.Windows.Forms.GroupBox grpBxResults;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlResults;
        private System.Windows.Forms.TabControl tabCtrlResults;
        private System.Windows.Forms.TabPage tabPageMatchResults;
        private System.Windows.Forms.TabPage tabPageSingleGames;
        private System.Windows.Forms.PictureBox picBxRank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbPlayerInfoName;
        private System.Windows.Forms.TabPage tabPageMaps;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlMatches;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlGames;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlOpponentSelection;
        private System.Windows.Forms.RadioButton rdBtnAny;
        private System.Windows.Forms.RadioButton rdBtnZerg;
        private System.Windows.Forms.RadioButton rdBtnTerran;
        private System.Windows.Forms.RadioButton rdBtnProtoss;
        private System.Windows.Forms.RadioButton rdBtnRandom;
    }
}