using CustomExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using CustomControls;
using EloSystem;
using EloSystem.ResourceManagement;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public enum GameReportStatus
    {
        Ready, MapIsMissing, Failure
    }

    public partial class GameReport : UserControl
    {
        internal int RaceIndexPlayer1
        {
            get
            {
                return this.cmbBxPlayer1Race.SelectedIndex;
            }
        }
        internal int RaceIndexPlayer2
        {
            get
            {
                return this.cmbBxPlayer2Race.SelectedIndex;
            }
        }
        internal Player1EWRGetter Pl1EWRGetter { get; set; }
        internal RatingChangeGetter RatingCalculator { get; private set; }
        internal ResourceGetter EloDataSource
        {
            private get
            {
                return this.eloDataSource;
            }
            set
            {
                if (this.eloDataSource != null) { this.eloDataSource().MapPoolChanged -= this.OnMapPoolUpdate; }

                this.eloDataSource = value;

                this.eloDataSource().MapPoolChanged += this.OnMapPoolUpdate;

                GameReport.PopulateComboboxWithMaps(this.cmbBxMap, value().GetMaps().OrderBy(map => map.Name));
            }

        }
        internal SCPlayer Player1
        {
            get
            {
                return this.player1;
            }
            set
            {
                this.player1 = value;

                this.UpdateControlValues();
                this.SetTextOnWinnerRadioButtons();
            }
        }
        internal SCPlayer Player2
        {
            get
            {
                return this.player2;
            }
            set
            {
                this.player2 = value;

                this.UpdateControlValues();
                this.SetTextOnWinnerRadioButtons();
            }
        }
        internal SCPlayer WinnerPlayer
        {
            get
            {
                if (this.rdBtnPl1Win.Checked) { return this.Player1; }
                else if (this.rdBtnPl2Win.Checked) { return this.Player2; }
                else { return null; }
            }
        }
        internal string Title
        {
            get
            {
                return this.lbGameHeader.Text;
            }
            set
            {
                this.lbGameHeader.Text = value;
            }
        }
        private Race racePlayer1
        {
            get
            {
                return (this.cmbBxPlayer1Race.SelectedItem as Tuple<string, Race>).Item2;
            }
        }
        private Race racePlayer2
        {
            get
            {
                return (this.cmbBxPlayer2Race.SelectedItem as Tuple<string, Race>).Item2;
            }
        }
        private ResourceGetter eloDataSource;
        private SCPlayer player1;
        private SCPlayer player2;
        public PlayerSlotType WinnerSlot
        {
            get
            {
                return this.WinnerPlayer == this.Player1 ? PlayerSlotType.Player1 : PlayerSlotType.Player2;
            }
        }
        public event EventHandler<EventArgs> RemoveButtonClicked = delegate { };
        /// <summary>
        /// This event fires when player race selection or game winner selection changes.
        /// </summary>
        public event EventHandler<EventArgs> GameDataReported = delegate { };
        public event EventHandler<RaceSelectionEventArgs> RaceSelectionChanged = delegate { };

        public GameReport(Player1EWRGetter ewrGetter, RatingChangeGetter ratingCalculator)
        {
            InitializeComponent();

            this.Pl1EWRGetter = ewrGetter;
            this.RatingCalculator = ratingCalculator;

            GameReport.PopulateCombobBoxRace(this.cmbBxPlayer1Race);
            this.cmbBxPlayer1Race.SelectedIndexChanged += this.CmbBxPlayer1Race_SelectedIndexChanged;

            GameReport.PopulateCombobBoxRace(this.cmbBxPlayer2Race);
            this.cmbBxPlayer2Race.SelectedIndexChanged += this.CmbBxPlayer2Race_SelectedIndexChanged;

            this.rdBtnPl1Win.CheckedChanged += this.RdBtnPlWin_CheckedChanged;
            this.rdBtnPl2Win.CheckedChanged += this.RdBtnPlWin_CheckedChanged;
        }

        public GameReport(GameEntryEditorItem editorItem, SCPlayer player1, SCPlayer player2, ResourceGetter resourceGetter, Player1EWRGetter ewrGetter, RatingChangeGetter ratingCalculator)
            : this(ewrGetter, ratingCalculator)
        {
            this.EloDataSource = resourceGetter;

            this.cmbBxPlayer1Race.SelectedIndex = (int)editorItem.Player1Race;
            this.cmbBxPlayer2Race.SelectedIndex = (int)editorItem.Player2Race;

            if (editorItem.Map != null && this.cmbBxMap.Items.Cast<Tuple<string, Map>>().Any(item => item.Item2 == editorItem.Map))
            {
                this.cmbBxMap.SelectedIndex = this.cmbBxMap.Items.Cast<Tuple<string, Map>>().IndexOf(this.cmbBxMap.Items.Cast<Tuple<string, Map>>().First(item => item.Item2 == editorItem.Map));
            }
            else { this.cmbBxMap.SelectedIndex = -1; }

            if (editorItem.Player1IsSetToWinner) { this.rdBtnPl1Win.Checked = true; }
            else if (editorItem.Player2IsSetToWinner) { this.rdBtnPl2Win.Checked = true; }

            this.player1 = player1;
            this.player2 = player2;

            this.UpdateControlValues();
            this.SetTextOnWinnerRadioButtons();
        }

        private void RdBtnPlWin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdBtnPl1Win.Checked)
            {
                this.lbPl1Win.Visible = true;
                this.lbPl2Win.Visible = false;
            }
            else if (this.rdBtnPl2Win.Checked)
            {
                this.lbPl1Win.Visible = false;
                this.lbPl2Win.Visible = true;
            }

            this.UpdateControlValues();

            this.GameDataReported.Invoke(this, new EventArgs());
        }

        private static void PopulateCombobBoxRace(ComboBox cmBx)
        {
            cmBx.DisplayMember = "Item1";
            cmBx.ValueMember = "Item2";

            cmBx.Items.Clear();
            cmBx.Items.AddRange(Enum.GetValues(typeof(Race)).Cast<Race>().Select(enm => Tuple.Create<string, Race>(enm.ToString().ToUpper(), enm)).ToArray());
        }

        private static void PopulateComboboxWithMaps(ComboBox cmBx, IEnumerable<Map> maps)
        {
            List<Map> mapList = maps.OrderBy(map => map.Name).ToList();

            var selectedItem = cmBx.SelectedItem != null ? (cmBx.SelectedItem as Tuple<string, Map>).Item2 : null;

            cmBx.DisplayMember = "Item1";
            cmBx.ValueMember = "Item2";

            cmBx.Items.Clear();
            cmBx.Items.AddRange(mapList.Select(map => Tuple.Create<string, Map>(map.Name, map)).ToArray());

            if (selectedItem != null && mapList.Contains(selectedItem)) { cmBx.SelectedIndex = mapList.IndexOf(selectedItem); }
        }

        private bool HasSelectedRaces()
        {
            return this.cmbBxPlayer1Race.SelectedIndex > -1 && this.cmbBxPlayer2Race.SelectedIndex > -1;
        }

        private void CmbBxPlayer1Race_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RaceSelectionChanged.Invoke(this, new RaceSelectionEventArgs((Race)this.cmbBxPlayer1Race.SelectedIndex, PlayerSlotType.Player1));

            this.RaceSelectionChangedUpdate();
        }

        private void CmbBxPlayer2Race_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RaceSelectionChanged.Invoke(this, new RaceSelectionEventArgs((Race)this.cmbBxPlayer2Race.SelectedIndex, PlayerSlotType.Player2));

            this.RaceSelectionChangedUpdate();
        }

        private void RaceSelectionChangedUpdate()
        {
            this.UpdateControlValues();

            this.GameDataReported.Invoke(this, new EventArgs());
        }

        private void SetTextOnWinnerRadioButtons()
        {
            if (this.Player1 != null) { this.rdBtnPl1Win.Text = this.Player1.Name; }

            if (this.Player2 != null) { this.rdBtnPl2Win.Text = this.Player2.Name; }
        }

        private void btnRemoveGame_Click(object sender, EventArgs e)
        {
            this.RemoveButtonClicked.Invoke(sender, e);
        }

        internal void UpdateControlValues()
        {
            if (this.HasSelectedRaces())
            {
                if (this.cmbBxMap.SelectedIndex > -1)
                {
                    Tuple<string, Map> selectedMapItem = this.cmbBxMap.SelectedItem as Tuple<string, Map>;

                    RaceMatchupResults stats;

                    if (selectedMapItem != null && selectedMapItem.Item2.Stats.TryGetMatchup(this.racePlayer1, this.racePlayer2, out stats))
                    {
                        string statsTextA, statsTextB;

                        if (this.racePlayer1 == this.racePlayer2)
                        {
                            statsTextA = String.Format("{0}\ngames", stats.TotalGames);
                            statsTextB = statsTextA;
                        }
                        else
                        {
                            statsTextA = String.Format("{0}\n{1}/{2}", stats.TotalGames > 0 ? (100 * stats.WinRatioRace1CorrectedForExpectedWR()).RoundToInt().ToString() + "%" : "", stats.Race1Wins
                                , stats.TotalGames);
                            statsTextB = String.Format("{0}\n{1}/{2}", stats.TotalGames > 0 ? (100 * stats.WinRatioRace2CorrectedForExpectedWR()).RoundToInt().ToString() + "%" : "", stats.Race2Wins
                                , stats.TotalGames);
                        }

                        if (this.racePlayer1 == stats.Race1)
                        {
                            this.lbMapWRPlayer1Race.Text = statsTextA;

                            this.lbMapWRPlayer2Race.Text = statsTextB;
                        }
                        else
                        {
                            this.lbMapWRPlayer1Race.Text = statsTextB;

                            this.lbMapWRPlayer2Race.Text = statsTextA;
                        }

                    }
                }

                if (this.Player1 != null && this.Player2 != null)
                {
                    // set the expected win ratio
                    double player1EWR = this.Pl1EWRGetter(this.racePlayer1, this.racePlayer2);

                    this.lbEWRPlayer1.Text = String.Format("{0}%", (100 * player1EWR).RoundToInt());
                    this.lbEWRPlayer2.Text = String.Format("{0}%", (100 * (1 - player1EWR)).RoundToInt());

                    if (this.WinnerPlayer != null)
                    {
                        // set the rating values
                        int ratingChange = this.RatingCalculator(this.WinnerPlayer == this.Player1 ? PlayerSlotType.Player1 : PlayerSlotType.Player2, this.racePlayer1, this.racePlayer2);

                        int player1RatingPoints = this.WinnerPlayer == this.Player1 ? ratingChange : ratingChange * -1;

                        this.lbPl1RatingVsRace.Text = player1RatingPoints == 0 ? "0" : player1RatingPoints.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);

                        if (player1RatingPoints < 0) { this.lbPl1RatingVsRace.ForeColor = Color.Red; }
                        else if (player1RatingPoints > 0) { this.lbPl1RatingVsRace.ForeColor = Color.ForestGreen; }
                        else { this.lbPl1RatingVsRace.ForeColor = Color.Black; }


                        int player2RatingPoints = this.WinnerPlayer == this.Player2 ? ratingChange : ratingChange * -1;

                        this.lbPl2RatingVsRace.Text = player2RatingPoints == 0 ? "0" : player2RatingPoints.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);

                        if (player2RatingPoints < 0) { this.lbPl2RatingVsRace.ForeColor = Color.Red; }
                        else if (player2RatingPoints > 0) { this.lbPl2RatingVsRace.ForeColor = Color.ForestGreen; }
                        else { this.lbPl2RatingVsRace.ForeColor = Color.Black; }
                    }
                }
            }
        }

        private void OnMapPoolUpdate(object sender, EventArgs e)
        {
            var senderElo = sender as EloData;

            if (senderElo == null) { return; }

            GameReport.PopulateComboboxWithMaps(this.cmbBxMap, senderElo.GetMaps().OrderBy(map => map.Name));
        }

        public bool RaceIsSelectedFor(PlayerSlotType slot)
        {
            switch (slot)
            {
                case PlayerSlotType.Player1: return this.cmbBxPlayer1Race.SelectedIndex > -1;
                case PlayerSlotType.Player2: return this.cmbBxPlayer2Race.SelectedIndex > -1;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, slot.ToString()));
            }
        }

        public GameReportStatus GetGameReportStatus(out GameEntry gameReport)
        {
            gameReport = null;

            if (this.WinnerPlayer == null || !this.HasSelectedRaces()) { return GameReportStatus.Failure; }

            gameReport = new GameEntry(this.WinnerSlot, (this.cmbBxPlayer1Race.SelectedItem as Tuple<string, Race>).Item2, (this.cmbBxPlayer2Race.SelectedItem as Tuple<string, Race>).Item2
                , this.GetMapOrDefault());


            if (this.cmbBxMap.SelectedIndex < 0 || this.cmbBxMap.SelectedItem == null) { return GameReportStatus.MapIsMissing; }
            else { return GameReportStatus.Ready; }
        }

        public Map GetMapOrDefault()
        {
            var mapTpl = this.cmbBxMap.SelectedItem as Tuple<string, Map>;

            return (mapTpl != null) ? mapTpl.Item2 : null;
        }

        public void SetRaceFor(PlayerSlotType playerSlot, Race race)
        {
            switch (playerSlot)
            {
                case PlayerSlotType.Player1: this.cmbBxPlayer1Race.SelectedIndex = (int)race; break;
                case PlayerSlotType.Player2: this.cmbBxPlayer2Race.SelectedIndex = (int)race; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, playerSlot.ToString()));
            }
        }

        private void cmbBxMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateControlValues();

            this.GameDataReported.Invoke(this, new EventArgs());
        }
    }
}
