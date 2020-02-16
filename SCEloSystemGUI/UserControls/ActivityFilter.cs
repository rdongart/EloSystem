using CustomExtensionMethods;
using EloSystem;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class ActivityFilter : UserControl, IPlayerFilter
    {
        private bool recentActivityFilterIsDisabled;
        private int gamesPlayed;
        private int recentActivityGamesPlayed;
        private int recentActivityMonths;
        public event EventHandler FilterChanged = delegate { };

        public ActivityFilter()
        {
            InitializeComponent();

            this.gamesPlayed = EloSystemGUIStaticMembers.GAMESPLAYED_DEFAULT_THRESHOLD;
            this.recentActivityGamesPlayed = EloSystemGUIStaticMembers.RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD;
            this.recentActivityMonths = EloSystemGUIStaticMembers.RECENTACTIVITY_MONTHS_DEFAULT;
            this.recentActivityFilterIsDisabled = false;
            this.numUDGamesTotal.Value = this.gamesPlayed;
            this.numUDGamesRecent.Value = this.recentActivityGamesPlayed;
            this.numUDRecentMonths.Value = this.recentActivityMonths;
            this.chkBxDisableRecencyFilter.Checked = this.recentActivityFilterIsDisabled;
            this.chkBxDisableRecencyFilter.CheckedChanged += this.chkBxDisableRecencyFilter_ValueChanged;
        }

        public void ApplyChanges()
        {
            this.recentActivityFilterIsDisabled = this.chkBxDisableRecencyFilter.Checked;
            this.gamesPlayed = this.numUDGamesTotal.Value.RoundToInt();
            this.recentActivityGamesPlayed = this.numUDGamesRecent.Value.RoundToInt();
            this.recentActivityMonths = this.numUDRecentMonths.Value.RoundToInt();
        }

        public bool HasChangesNotApplied()
        {
            return this.chkBxDisableRecencyFilter.Checked != this.recentActivityFilterIsDisabled || this.gamesPlayed != this.numUDGamesTotal.Value
                || this.recentActivityGamesPlayed != this.numUDGamesRecent.Value || this.recentActivityMonths != this.numUDRecentMonths.Value;
        }

        public bool PlayerFilter(SCPlayer player)
        {
            return player.Stats.GamesTotal() >= this.gamesPlayed && (this.recentActivityFilterIsDisabled || GlobalState.DataBase.GetAllGames().Where(game => game.HasPlayer(player) && DateTime.Today.CompareTo(game.Match.DateTime.AddMonths(this.recentActivityMonths)) <= 0).Count() >= this.recentActivityGamesPlayed);
        }

        private void activityFilter_ValueChanged(object sender, EventArgs e)
        {
            this.FilterChanged.Invoke(this, e);
        }

        private void chkBxDisableRecencyFilter_ValueChanged(object sender, EventArgs e)
        {
            this.numUDGamesRecent.Enabled = !this.chkBxDisableRecencyFilter.Checked;
            this.numUDRecentMonths.Enabled = !this.chkBxDisableRecencyFilter.Checked;
        }
    }
}
