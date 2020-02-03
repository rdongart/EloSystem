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
    public partial class PlayerSelector : Form
    {
        public static SCPlayer SelectedPlayer { get; private set; }

        private ObjectListView playerListView;
        private PlayerSearch playerSearcher;
        private ResourceGetter eloDataBase;
        private PlayerSelectorFilter filter;

        private PlayerSelector(ResourceGetter eloDataBase, RankHandler rankHandler)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.eloDataBase = eloDataBase;

            this.playerListView = EloGUIControlsStaticMembers.CreatePlayerSearchListView(eloDataBase, rankHandler);
            this.playerListView.SelectionChanged += this.OlvPlayerListView_SelectionChanged;
            this.playerSearcher = new PlayerSearch(this.playerListView);
            this.playerSearcher.PlayerSearchInitiated += this.OnPlayerSearch;
            this.tblLOPnlPlayerSelector.Controls.Add(this.playerSearcher, 0, 0);
            this.tblLOPnlPlayerSelector.SetColumnSpan(this.playerSearcher, 2);
        }

        [STAThread]
        internal static DialogResult Show(ResourceGetter eloDataBase, RankHandler rankHandler, PlayerSelectorFilter filter = null, string header = "")
        {
            var selector = new PlayerSelector(eloDataBase, rankHandler) { Text = header };
            selector.filter = filter;

            return selector.ShowDialog();
        }

        private void OlvPlayerListView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.playerListView.SelectedItem == null) { PlayerSelector.SelectedPlayer = null; }
            else
            {
                var tuple = this.playerListView.SelectedItem.RowObject as Tuple<SCPlayer, int>;

                if (tuple != null && this.playerListView.Scrollable)
                {
                    int rowHeightCorrected = this.playerListView.RowHeight + 1;
                    this.playerListView.LowLevelScroll(0, (tuple.Item2 - 1) * rowHeightCorrected - this.playerListView.LowLevelScrollPosition.Y * rowHeightCorrected);

                    PlayerSelector.SelectedPlayer = tuple.Item1;
                }
                else { PlayerSelector.SelectedPlayer = null; }

            }

            this.btnOK.Enabled = PlayerSelector.SelectedPlayer != null;
        }

        private void OnPlayerSearch(object sender, PlayerSearchEventArgs e)
        {
            if (this.filter != null)
            {
                this.playerListView.SetObjects(this.eloDataBase().GetPlayers().Where(player => this.filter(player)).OrderByDescending(player => player.RatingTotal()).ThenByDescending(player =>
                      player.Stats.GamesTotal()).ThenByDescending(player => player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1)).Where(tuple =>
                           this.eloDataBase().PlayerLookup(e.SearchString).Contains(tuple.Item1)).ToArray());
            }
            else
            {
                this.playerListView.SetObjects(this.eloDataBase().GetPlayers().OrderByDescending(player => player.RatingTotal()).ThenByDescending(player =>
                    player.Stats.GamesTotal()).ThenByDescending(player => player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1)).Where(tuple =>
                         this.eloDataBase().PlayerLookup(e.SearchString).Contains(tuple.Item1)).ToArray());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PlayerSelector.SelectedPlayer = null;

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
