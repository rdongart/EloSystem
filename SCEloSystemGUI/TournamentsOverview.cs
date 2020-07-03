using CustomControls.Styles;
using BrightIdeasSoftware;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class TournamentsOverview : Form
    {
        private const float TEXT_SIZE = 13F;

        private Dictionary<Tournament, Bitmap> logos = new Dictionary<Tournament, Bitmap>();
        private ObjectListView tournaments;

        private TournamentsOverview()
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.tournaments = this.CreateTournamentStatsListView();
            this.tournaments.EmptyListMsg = "No tournaments added yet.";

            this.tournaments.SetObjects(GlobalState.DataBase.GetTournaments());

            this.Controls.Add(this.tournaments);
        }

        public static void ShowOverview(Form anchorForm = null)
        {
            System.Windows.Forms.Cursor previousCursor = System.Windows.Forms.Cursor.Current;

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            var overviewDisplay = new TournamentsOverview();

            System.Windows.Forms.Cursor.Current = previousCursor;

            if (anchorForm != null) { FormStyles.ShowFullFormRelativeToAnchor(overviewDisplay, anchorForm); }

            overviewDisplay.ShowDialog();

            overviewDisplay.Dispose();
        }

        private ObjectListView CreateTournamentStatsListView()
        {
            var tournamentStatsLV = new ObjectListView()
            {
                AllowColumnReorder = false,
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", TournamentsOverview.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Clickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(12, 16, 12, 12),
                MultiSelect = false,
                RowHeight = 50,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(500, 1400),
                SortGroupItemsByPrimaryColumn = true,
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
                UseHotItem = true,
            };

            Styles.ObjectListViewStyles.SetHotItemStyle(tournamentStatsLV);
            
            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null, Sortable = true };
            var olvClmLogo = new OLVColumn() { Width = 110, Text = "Logo", Sortable = false, TextAlign = HorizontalAlignment.Center };
            var olvClmName = new OLVColumn() { Width = 360, Text = "Name", Sortable = true };
            var olvClmGames = new OLVColumn() { Width = 120, Text = "Games", Sortable = true };

            tournamentStatsLV.PrimarySortColumn = olvClmName;
            tournamentStatsLV.SelectionChanged += this.TournamentStatsLV_SelectionChanged;

            tournamentStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmLogo, olvClmName, olvClmGames });

            tournamentStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmLogo, olvClmName, olvClmGames });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmGames })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            const int IMAGE_SIZE_MAX = 48;


            olvClmLogo.AspectGetter = obj =>
            {
                var tournament = obj as Tournament;

                Bitmap tournamentLogo;
                EloImage eloTournamentLogo;

                if (this.logos.TryGetValue(tournament, out tournamentLogo)) { return new Image[] { tournamentLogo }; }
                else if (GlobalState.DataBase.TryGetImage(tournament.ImageID, out eloTournamentLogo))
                {
                    this.logos.Add(tournament, eloTournamentLogo.Image.ResizeSameAspectRatio(IMAGE_SIZE_MAX));

                    return new Image[] { this.logos[tournament] };
                }
                else { return null; }

            };

            var mapImageRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 3, 4, 4) };
            olvClmLogo.Renderer = mapImageRenderer;

            olvClmName.AspectGetter = obj =>
            {
                var tournament = obj as Tournament;

                return tournament.NameLong;
            };

            olvClmGames.AspectGetter = obj =>
            {
                var tournament = obj as Tournament;

                return tournament.GetGames().Count();
            };

            olvClmGames.AspectToStringConverter = obj =>
            {
                var aspect = (int)obj;

                return aspect.ToString(Styles.NUMBER_FORMAT);

            };

            return tournamentStatsLV;
        }

        private void TournamentStatsLV_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.tournaments.SelectedItem == null) { return; }

            var selectedTournament = this.tournaments.SelectedItem.RowObject as Tournament;

            if (selectedTournament != null) { TournamentProfile.ShowProfile(selectedTournament, this); }

            this.tournaments.SelectedItems.Clear();
        }

        private void TournamentsOverview_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (KeyValuePair<Tournament, Bitmap> kvPair in this.logos.ToList()) { kvPair.Value.Dispose(); }

            this.logos.Clear();
        }

        private void TournamentsOverview_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
