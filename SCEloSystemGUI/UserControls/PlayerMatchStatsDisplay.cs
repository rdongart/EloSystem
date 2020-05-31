using CustomExtensionMethods.Drawing;
using CustomExtensionMethods;
using System.Drawing;
using BrightIdeasSoftware;
using EloSystem;
using EloSystem.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class PlayerMatchStatsDisplay : UserControl
    {
        private const string INFORMATION_NA = "-";

        private SCPlayer player;

        public PlayerMatchStatsDisplay()
        {
            InitializeComponent();

            this.ResetAllLabels();

            this.picBxMainRace.BackgroundImage = EloGUIControlsStaticMembers.BackGroundFrame(this.picBxMainRace.Size, Color.Black, (this.picBxMainRace.Size.Width * 0.12).RoundToInt());
        }

        internal void AddPlayerStats(SCPlayer player)
        {
            if (player == null)
            {
                this.ResetAllLabels();
                this.pnlPerformanceStats.Controls.Clear();
                this.picBxPlayer.Image = null;
                this.picBxRank.Image = null;
                this.picBxTeam.Image = null;
                this.picBxCountry.Image = null;
                this.picBxMainRace.Image = null;
                this.tblLoPnlPlayerAffiliations.Visible = false;
                return;
            }

            this.player = player;

            this.tblLoPnlPlayerAffiliations.Visible = true;

            this.lbName.Text = player.Name;

            EloImage countryRes;

            if (this.picBxCountry.Image != null) { this.picBxCountry.Image.Dispose(); }
            this.picBxCountry.Image = null;
            this.toolTipStatsDisplay.SetToolTip(this.picBxCountry, string.Empty);
            this.lbPlCountry.Text = string.Empty;

            this.tblLoPnlPlayerAffiliations.Controls.Remove(this.picBxCountry);
            this.tblLoPnlPlayerAffiliations.Controls.Remove(this.lbPlCountry);

            if (player.Country != null)
            {
                if (GlobalState.DataBase.TryGetImage(player.Country.ImageID, out countryRes))
                {
                    Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxCountry, countryRes.Image);

                    this.tblLoPnlPlayerAffiliations.Controls.Add(this.picBxCountry, 3, 0);

                    this.toolTipStatsDisplay.SetToolTip(this.picBxCountry, player.Country.Name);
                }
                else
                {
                    this.lbPlCountry.Text = player.Country.Name;
                    this.tblLoPnlPlayerAffiliations.Controls.Add(this.lbPlCountry, 3, 0);
                    this.lbPlCountry.Dock = DockStyle.Fill;
                }
            }

            EloImage teamRes;

            if (this.picBxTeam.Image != null) { this.picBxTeam.Image.Dispose(); }
            this.picBxTeam.Image = null;
            this.toolTipStatsDisplay.SetToolTip(this.picBxTeam, string.Empty);
            this.lbPlTeam.Text = string.Empty;

            this.tblLoPnlPlayerAffiliations.Controls.Remove(this.picBxTeam);
            this.tblLoPnlPlayerAffiliations.Controls.Remove(this.lbPlTeam);

            if (player.Team != null)
            {
                if (GlobalState.DataBase.TryGetImage(player.Team.ImageID, out teamRes))
                {
                    Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxTeam, teamRes.Image);

                    this.tblLoPnlPlayerAffiliations.Controls.Add(this.picBxTeam, 4, 0);

                    this.toolTipStatsDisplay.SetToolTip(this.picBxTeam, String.Format("Team: {0}", player.Team.Name));
                }
                else
                {
                    this.lbPlTeam.Text = player.Team.Name;
                    this.tblLoPnlPlayerAffiliations.Controls.Add(this.lbPlTeam, 4, 0);
                    this.lbPlTeam.Dock = DockStyle.Fill;
                }
            }

            EloImage playerRes;

            if (this.picBxPlayer.Image != null) { this.picBxPlayer.Image.Dispose(); }
            this.picBxPlayer.Image = null;
            this.toolTipStatsDisplay.SetToolTip(this.picBxPlayer, string.Empty);

            if (GlobalState.DataBase.TryGetImage(player.ImageID, out playerRes))
            {
                this.picBxPlayer.Image = playerRes.Image;

                this.toolTipStatsDisplay.SetToolTip(this.picBxPlayer, player.Name);
            }


            this.lbPlAliases.Text = String.Join(", ", player.GetAliases());

            this.pnlPerformanceStats.Controls.Clear();

            ObjectListView performanceData = EloSystemGUIStaticMembers.CreatePlayerPerformanceListView(player);
            performanceData.Dock = DockStyle.Fill;

            if (player.Stats.GamesTotal() > 0) { Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxMainRace, RaceIconProvider.GetRaceUsageIcon(player)); }
            else { Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxMainRace, null); }
            this.toolTipStatsDisplay.SetToolTip(this.picBxMainRace, "Main race");

            this.picBxRank.Image = GlobalState.RankSystem.GetRankImageMain(player, this.picBxRank.Height, true);
            this.toolTipStatsDisplay.SetToolTip(this.picBxRank, "Overall rank");

            this.pnlPerformanceStats.Controls.Add(performanceData);
        }

        private void ResetAllLabels()
        {
            foreach (Label lb in this.GetLabels())
            {
                lb.Text = "";

                lb.Image = null;
            }
        }

        private IEnumerable<Label> GetLabels()
        {
            foreach (Label lb in new Label[] { this.lbPlCountry, this.lbPlTeam, this.lbPlAliases }) { yield return lb; }
        }

        private void lbName_Click(object sender, EventArgs e)
        {
            PlayerProfile.ShowProfile(this.player, this.FindForm());
        }
    }
}
