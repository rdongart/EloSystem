using CustomExtensionMethods;
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

        internal ResourceGetter EloDataSource { set; private get; }

        public PlayerMatchStatsDisplay()
        {
            InitializeComponent();

            this.ResetAllLabels();
        }

        internal void AddPlayerStats(SCPlayer player)
        {
            if (player == null)
            {
                this.ResetAllLabels();
                return;
            }

            EloImage countryRes;

            if (player.Country != null)
            {
                if (this.EloDataSource().TryGetImage(player.Country.ImageID, out countryRes))
                {
                    if (this.lbPlCountry.Image != null) { this.lbPlCountry.Image.Dispose(); }

                    this.lbPlCountry.Image = countryRes.Image;
                }

                this.lbPlCountry.Text = player.Country.Name;
            }

            EloImage teamRes;

            if (player.Team != null)
            {
                if (this.EloDataSource().TryGetImage(player.Team.ImageID, out teamRes))
                {
                    if (this.lbPlTeam.Image != null) { this.lbPlTeam.Image.Dispose(); }

                    this.lbPlTeam.Image = teamRes.Image;
                }
                
                this.lbPlTeam.Text = player.Team.Name;
            }

            this.lbPlAliases.Text = String.Join(", ", player.GetAliases());

            this.lbPlRaceMain.Text = player.Stats.GamesTotal() > 0 ? player.GetPrimaryRace().ToString() : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlRaceVsProtoss.Text = player.Stats.GamesVs(Race.Protoss) > 0 ? player.GetPrimaryRaceVs(Race.Protoss).ToString() : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlRaceVsRandom.Text = player.Stats.GamesVs(Race.Random) > 0 ? player.GetPrimaryRaceVs(Race.Random).ToString() : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlRaceVsTerran.Text = player.Stats.GamesVs(Race.Terran) > 0 ? player.GetPrimaryRaceVs(Race.Terran).ToString() : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlRaceVsZerg.Text = player.Stats.GamesVs(Race.Zerg) > 0 ? player.GetPrimaryRaceVs(Race.Zerg).ToString() : PlayerMatchStatsDisplay.INFORMATION_NA;

            this.lbPlRatingMain.Text = player.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            this.lbPlRatingVsProtoss.Text = player.RatingsVs.Protoss.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            this.lbPlRatingVsRandom.Text = player.RatingsVs.Random.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            this.lbPlRatingVsTerran.Text = player.RatingsVs.Terran.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            this.lbPlRatingVsZerg.Text = player.RatingsVs.Zerg.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);

            this.lbPlWRMain.Text = player.Stats.GamesTotal() > 0 ?
                String.Format("{0}% ({1}/{2}", (100 * player.Stats.WinRatioTotal()).RoundToInt(), player.Stats.WinsTotal(), player.Stats.GamesTotal()) : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlWRVsProtoss.Text = player.Stats.GamesVs(Race.Protoss) > 0 ? String.Format("{0}% ({1}/{2}", (100 * player.Stats.WinRatioVs(Race.Protoss)).RoundToInt(), player.Stats.WinsVs(Race.Protoss)
                , player.Stats.GamesVs(Race.Protoss)) : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlWRVsRandom.Text = player.Stats.GamesVs(Race.Random) > 0 ? String.Format("{0}% ({1}/{2}", (100 * player.Stats.WinRatioVs(Race.Random)).RoundToInt(), player.Stats.WinsVs(Race.Random)
                , player.Stats.GamesVs(Race.Random)) : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlWRVsTerran.Text = player.Stats.GamesVs(Race.Terran) > 0 ? String.Format("{0}% ({1}/{2}", (100 * player.Stats.WinRatioVs(Race.Terran)).RoundToInt(), player.Stats.WinsVs(Race.Terran)
                , player.Stats.GamesVs(Race.Terran)) : PlayerMatchStatsDisplay.INFORMATION_NA;
            this.lbPlWRVsZerg.Text = player.Stats.GamesVs(Race.Zerg) > 0 ? String.Format("{0}% ({1}/{2}", (100 * player.Stats.WinRatioVs(Race.Zerg)).RoundToInt(), player.Stats.WinsVs(Race.Zerg)
                , player.Stats.GamesVs(Race.Zerg)) : PlayerMatchStatsDisplay.INFORMATION_NA;
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
            foreach (Label lb in new Label[] { this.lbPlCountry, this.lbPlTeam, this.lbPlAliases, this.lbPlRaceMain, this.lbPlRaceVsProtoss, this.lbPlRaceVsRandom, this.lbPlRaceVsTerran
                , this.lbPlRaceVsZerg, this.lbPlRatingMain, this.lbPlRatingVsProtoss, this.lbPlRatingVsRandom, this.lbPlRatingVsTerran, this.lbPlRatingVsZerg, this.lbPlWRMain, this.lbPlWRVsProtoss
                , this.lbPlWRVsRandom, this.lbPlWRVsTerran, this.lbPlWRVsZerg }) { yield return lb; }
        }
    }
}
