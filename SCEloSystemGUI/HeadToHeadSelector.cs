using BrightIdeasSoftware;
using EloSystem;
using EloSystemExtensions;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class HeadToHeadSelector : Form
    {
        public static SCPlayer SelectedPlayer { get; private set; }

        private ObjectListView playerListView;
        private PlayerSearch playerSearcher;
        private PlayerSelectorFilter filter;

        private HeadToHeadSelector(SCPlayer targetPlayer)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.playerListView = EloGUIControlsStaticMembers.CreateHeadToHeadSearchListView(targetPlayer);
            this.playerListView.SelectionChanged += this.OlvPlayerListView_SelectionChanged;
            this.playerSearcher = new PlayerSearch(this.playerListView);
            this.playerSearcher.PlayerSearchInitiated += this.OnPlayerSearch;
            this.tblLOPnlPlayerSelector.Controls.Add(this.playerSearcher, 0, 0);
            this.tblLOPnlPlayerSelector.SetColumnSpan(this.playerSearcher, 2);
        }

        [STAThread]
        internal static DialogResult Show(SCPlayer targetPlayer, PlayerSelectorFilter filter = null, string header = "")
        {
            var selector = new HeadToHeadSelector(targetPlayer) { Text = header };
            selector.filter = filter;

            return selector.ShowDialog();
        }

        private void OlvPlayerListView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.playerListView.SelectedItem == null) { HeadToHeadSelector.SelectedPlayer = null; }
            else
            {
                var tuple = this.playerListView.SelectedItem.RowObject as Tuple<SCPlayer, int>;

                if (tuple != null && this.playerListView.Scrollable)
                {
                    int rowHeightCorrected = this.playerListView.RowHeight + 1;
                    this.playerListView.LowLevelScroll(0, (tuple.Item2 - 1) * rowHeightCorrected - this.playerListView.LowLevelScrollPosition.Y * rowHeightCorrected);

                    HeadToHeadSelector.SelectedPlayer = tuple.Item1;
                }
                else { HeadToHeadSelector.SelectedPlayer = null; }

            }

            this.btnOK.Enabled = HeadToHeadSelector.SelectedPlayer != null;
        }

        private void OnPlayerSearch(object sender, PlayerSearchEventArgs e)
        {
            Cursor previousCursor = Cursor.Current;

            Cursor.Current = Cursors.WaitCursor;

            if (this.filter != null)
            {
                this.playerListView.SetObjects(GlobalState.DataBase.GetPlayers().Where(player => this.filter(player)).OrderByDescending(player => player.RatingTotal()).ThenByDescending(player =>
                      player.Stats.GamesTotal()).ThenByDescending(player => player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1)).Where(tuple =>
                           GlobalState.DataBase.PlayerLookup(e.SearchString).Contains(tuple.Item1)).ToArray());
            }
            else
            {
                this.playerListView.SetObjects(GlobalState.DataBase.GetPlayers().OrderByDescending(player => player.RatingTotal()).ThenByDescending(player =>
                    player.Stats.GamesTotal()).ThenByDescending(player => player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1)).Where(tuple =>
                         GlobalState.DataBase.PlayerLookup(e.SearchString).Contains(tuple.Item1)).ToArray());
            }

            Cursor.Current = previousCursor;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HeadToHeadSelector.SelectedPlayer = null;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Close();
        }
    }
}
