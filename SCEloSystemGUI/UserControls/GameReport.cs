
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

        public GameReport()
        {
            InitializeComponent();

            GameReport.PopulateCombobBoxRace(this.cmbBxPlayer1Race);
            this.cmbBxPlayer1Race.SelectedIndexChanged += this.CmbBxPlayerRace_SelectedIndexChanged;

            GameReport.PopulateCombobBoxRace(this.cmbBxPlayer2Race);
            this.cmbBxPlayer2Race.SelectedIndexChanged += this.CmbBxPlayerRace_SelectedIndexChanged;

            this.rdBtnPl1Win.CheckedChanged += this.RdBtnPlWin_CheckedChanged;
            this.rdBtnPl2Win.CheckedChanged += this.RdBtnPlWin_CheckedChanged;
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
            IList<Map> mapList = maps is IList<Map> ? (IList<Map>)maps : maps.ToArray();

            var selectedItem = cmBx.SelectedItem != null ? (cmBx.SelectedItem as Tuple<string, Map>).Item2 : null;

            cmBx.DisplayMember = "Item1";
            cmBx.ValueMember = "Item2";

            cmBx.Items.Clear();
            cmBx.Items.AddRange(mapList.Select(map => Tuple.Create<string, Map>(map.Name, map)).ToArray());
        }

        private bool HasSelectedRaces()
        {
            return this.cmbBxPlayer1Race.SelectedIndex > -1 && this.cmbBxPlayer2Race.SelectedIndex > -1;
        }

        private void CmbBxPlayerRace_SelectedIndexChanged(object sender, EventArgs e)
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

        private void UpdateControlValues()
        {
            if (this.cmbBxPlayer1Race.SelectedIndex >= 0)
            {
                var selItem = this.cmbBxPlayer1Race.SelectedItem as Tuple<string, Race>;

                if (selItem != null) { this.lbPl2RatingVsRace.Text = this.Player2.RatingsVs.GetValueFor(selItem.Item2).ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT); }
            }

            if (this.cmbBxPlayer2Race.SelectedIndex >= 0)
            {
                var selItem = this.cmbBxPlayer2Race.SelectedItem as Tuple<string, Race>;

                if (selItem != null) { this.lbPl1RatingVsRace.Text = this.Player1.RatingsVs.GetValueFor(selItem.Item2).ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT); }
            }


        }

        private void OnMapPoolUpdate(object sender, EventArgs e)
        {
            var senderElo = sender as EloData;

            if (senderElo == null) { return; }

            GameReport.PopulateComboboxWithMaps(this.cmbBxMap, senderElo.GetMaps().OrderBy(map => map.Name));
        }

        public GameReportStatus GetGameReportStatus(out GameEntry gameReport)
        {
            gameReport = null;

            if (this.WinnerPlayer == null || !this.HasSelectedRaces()) { return GameReportStatus.Failure; }

            Func<Map> GetMapOrDefault = () =>
            {
                var mapTpl = this.cmbBxMap.SelectedItem as Tuple<string, Map>;

                return (mapTpl != null) ? mapTpl.Item2 : null;

            };

            gameReport = new GameEntry(this.WinnerSlot, (this.cmbBxPlayer1Race.SelectedItem as Tuple<string, Race>).Item2, (this.cmbBxPlayer2Race.SelectedItem as Tuple<string, Race>).Item2, GetMapOrDefault());


            if (this.cmbBxMap.SelectedIndex < 0 || this.cmbBxMap.SelectedItem == null) { return GameReportStatus.MapIsMissing; }
            else { return GameReportStatus.Ready; }
        }
    }
}
