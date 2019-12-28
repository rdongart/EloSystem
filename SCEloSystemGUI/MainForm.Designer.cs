namespace SCEloSystemGUI
{
    public partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlayerStats = new System.Windows.Forms.ToolStripMenuItem();
            this.mapStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageReportMatch = new System.Windows.Forms.TabPage();
            this.tabPageAddMap = new System.Windows.Forms.TabPage();
            this.tblLOPnlMaps = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageAddCountry = new System.Windows.Forms.TabPage();
            this.tblLOPnlCountries = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageAddTeam = new System.Windows.Forms.TabPage();
            this.tblLOPnlTeams = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageAddPlayer = new System.Windows.Forms.TabPage();
            this.tblLOPnlPlayers = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageEditTournaments = new System.Windows.Forms.TabPage();
            this.tblLOPnlTournaments = new System.Windows.Forms.TableLayoutPanel();
            this.menuStripMainForm.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageAddMap.SuspendLayout();
            this.tabPageAddCountry.SuspendLayout();
            this.tabPageAddTeam.SuspendLayout();
            this.tabPageAddPlayer.SuspendLayout();
            this.tabPageEditTournaments.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.showToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(1892, 24);
            this.menuStripMainForm.TabIndex = 0;
            this.menuStripMainForm.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "&Close";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPlayerStats,
            this.mapStatsToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.showToolStripMenuItem.Text = "&Stats";
            // 
            // toolStripMenuItemPlayerStats
            // 
            this.toolStripMenuItemPlayerStats.Name = "toolStripMenuItemPlayerStats";
            this.toolStripMenuItemPlayerStats.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripMenuItemPlayerStats.Size = new System.Drawing.Size(175, 22);
            this.toolStripMenuItemPlayerStats.Text = "&Player Stats";
            this.toolStripMenuItemPlayerStats.Click += new System.EventHandler(this.toolStripMenuItemPlayerStats_Click);
            // 
            // mapStatsToolStripMenuItem
            // 
            this.mapStatsToolStripMenuItem.Name = "mapStatsToolStripMenuItem";
            this.mapStatsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mapStatsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.mapStatsToolStripMenuItem.Text = "&Map Stats";
            this.mapStatsToolStripMenuItem.Click += new System.EventHandler(this.mapStatsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            this.contentsToolStripMenuItem.Visible = false;
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            this.indexToolStripMenuItem.Visible = false;
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            this.searchToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(119, 6);
            this.toolStripSeparator6.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageReportMatch);
            this.tabControlMain.Controls.Add(this.tabPageAddMap);
            this.tabControlMain.Controls.Add(this.tabPageAddCountry);
            this.tabControlMain.Controls.Add(this.tabPageAddTeam);
            this.tabControlMain.Controls.Add(this.tabPageAddPlayer);
            this.tabControlMain.Controls.Add(this.tabPageEditTournaments);
            this.tabControlMain.Location = new System.Drawing.Point(10, 36);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(12, 16, 12, 16);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1866, 950);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageReportMatch
            // 
            this.tabPageReportMatch.AutoScroll = true;
            this.tabPageReportMatch.AutoScrollMargin = new System.Drawing.Size(3, 3);
            this.tabPageReportMatch.Location = new System.Drawing.Point(4, 22);
            this.tabPageReportMatch.Name = "tabPageReportMatch";
            this.tabPageReportMatch.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageReportMatch.Size = new System.Drawing.Size(1858, 924);
            this.tabPageReportMatch.TabIndex = 0;
            this.tabPageReportMatch.Text = "Report Match";
            this.tabPageReportMatch.UseVisualStyleBackColor = true;
            // 
            // tabPageAddMap
            // 
            this.tabPageAddMap.Controls.Add(this.tblLOPnlMaps);
            this.tabPageAddMap.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddMap.Name = "tabPageAddMap";
            this.tabPageAddMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddMap.Size = new System.Drawing.Size(1858, 924);
            this.tabPageAddMap.TabIndex = 1;
            this.tabPageAddMap.Text = "Add Map";
            this.tabPageAddMap.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlMaps
            // 
            this.tblLOPnlMaps.ColumnCount = 3;
            this.tblLOPnlMaps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tblLOPnlMaps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tblLOPnlMaps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlMaps.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlMaps.Margin = new System.Windows.Forms.Padding(10);
            this.tblLOPnlMaps.Name = "tblLOPnlMaps";
            this.tblLOPnlMaps.Padding = new System.Windows.Forms.Padding(16);
            this.tblLOPnlMaps.RowCount = 2;
            this.tblLOPnlMaps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 390F));
            this.tblLOPnlMaps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlMaps.Size = new System.Drawing.Size(1852, 918);
            this.tblLOPnlMaps.TabIndex = 0;
            // 
            // tabPageAddCountry
            // 
            this.tabPageAddCountry.Controls.Add(this.tblLOPnlCountries);
            this.tabPageAddCountry.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddCountry.Name = "tabPageAddCountry";
            this.tabPageAddCountry.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddCountry.Size = new System.Drawing.Size(1858, 924);
            this.tabPageAddCountry.TabIndex = 2;
            this.tabPageAddCountry.Text = "Add Country";
            this.tabPageAddCountry.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlCountries
            // 
            this.tblLOPnlCountries.ColumnCount = 2;
            this.tblLOPnlCountries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tblLOPnlCountries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlCountries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlCountries.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlCountries.Margin = new System.Windows.Forms.Padding(10);
            this.tblLOPnlCountries.Name = "tblLOPnlCountries";
            this.tblLOPnlCountries.Padding = new System.Windows.Forms.Padding(16);
            this.tblLOPnlCountries.RowCount = 2;
            this.tblLOPnlCountries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tblLOPnlCountries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlCountries.Size = new System.Drawing.Size(1852, 918);
            this.tblLOPnlCountries.TabIndex = 1;
            // 
            // tabPageAddTeam
            // 
            this.tabPageAddTeam.Controls.Add(this.tblLOPnlTeams);
            this.tabPageAddTeam.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddTeam.Name = "tabPageAddTeam";
            this.tabPageAddTeam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddTeam.Size = new System.Drawing.Size(1858, 924);
            this.tabPageAddTeam.TabIndex = 3;
            this.tabPageAddTeam.Text = "Add Team";
            this.tabPageAddTeam.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlTeams
            // 
            this.tblLOPnlTeams.ColumnCount = 2;
            this.tblLOPnlTeams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tblLOPnlTeams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlTeams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlTeams.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlTeams.Margin = new System.Windows.Forms.Padding(10);
            this.tblLOPnlTeams.Name = "tblLOPnlTeams";
            this.tblLOPnlTeams.Padding = new System.Windows.Forms.Padding(16);
            this.tblLOPnlTeams.RowCount = 2;
            this.tblLOPnlTeams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tblLOPnlTeams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlTeams.Size = new System.Drawing.Size(1852, 918);
            this.tblLOPnlTeams.TabIndex = 1;
            // 
            // tabPageAddPlayer
            // 
            this.tabPageAddPlayer.Controls.Add(this.tblLOPnlPlayers);
            this.tabPageAddPlayer.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddPlayer.Name = "tabPageAddPlayer";
            this.tabPageAddPlayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddPlayer.Size = new System.Drawing.Size(1858, 924);
            this.tabPageAddPlayer.TabIndex = 4;
            this.tabPageAddPlayer.Text = "Add Player";
            this.tabPageAddPlayer.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlPlayers
            // 
            this.tblLOPnlPlayers.ColumnCount = 2;
            this.tblLOPnlPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1062F));
            this.tblLOPnlPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlPlayers.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlPlayers.Margin = new System.Windows.Forms.Padding(10);
            this.tblLOPnlPlayers.Name = "tblLOPnlPlayers";
            this.tblLOPnlPlayers.Padding = new System.Windows.Forms.Padding(16);
            this.tblLOPnlPlayers.RowCount = 2;
            this.tblLOPnlPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 510F));
            this.tblLOPnlPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayers.Size = new System.Drawing.Size(1852, 918);
            this.tblLOPnlPlayers.TabIndex = 1;
            // 
            // tabPageEditTournaments
            // 
            this.tabPageEditTournaments.Controls.Add(this.tblLOPnlTournaments);
            this.tabPageEditTournaments.Location = new System.Drawing.Point(4, 22);
            this.tabPageEditTournaments.Name = "tabPageEditTournaments";
            this.tabPageEditTournaments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditTournaments.Size = new System.Drawing.Size(1858, 924);
            this.tabPageEditTournaments.TabIndex = 5;
            this.tabPageEditTournaments.Text = "Add Tournament & Seasons";
            this.tabPageEditTournaments.UseVisualStyleBackColor = true;
            // 
            // tblLOPnlTournaments
            // 
            this.tblLOPnlTournaments.ColumnCount = 2;
            this.tblLOPnlTournaments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 392F));
            this.tblLOPnlTournaments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlTournaments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLOPnlTournaments.Location = new System.Drawing.Point(3, 3);
            this.tblLOPnlTournaments.Margin = new System.Windows.Forms.Padding(10);
            this.tblLOPnlTournaments.Name = "tblLOPnlTournaments";
            this.tblLOPnlTournaments.Padding = new System.Windows.Forms.Padding(16);
            this.tblLOPnlTournaments.RowCount = 2;
            this.tblLOPnlTournaments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tblLOPnlTournaments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlTournaments.Size = new System.Drawing.Size(1852, 918);
            this.tblLOPnlTournaments.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1892, 997);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStripMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMainForm;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MainForm";
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageAddMap.ResumeLayout(false);
            this.tabPageAddCountry.ResumeLayout(false);
            this.tabPageAddTeam.ResumeLayout(false);
            this.tabPageAddPlayer.ResumeLayout(false);
            this.tabPageEditTournaments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageReportMatch;
        private System.Windows.Forms.TabPage tabPageAddMap;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlMaps;
        private System.Windows.Forms.TabPage tabPageAddCountry;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlCountries;
        private System.Windows.Forms.TabPage tabPageAddTeam;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlTeams;
        private System.Windows.Forms.TabPage tabPageAddPlayer;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayers;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapStatsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageEditTournaments;
        private System.Windows.Forms.TableLayoutPanel tblLOPnlTournaments;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlayerStats;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}