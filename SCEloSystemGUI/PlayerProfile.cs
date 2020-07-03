using BrightIdeasSoftware;
using CustomControls;
using CustomControls.Utilities;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCEloSystemGUI
{
    public enum DevelopmentInterval
    {
        All_Time, _3_Years, _12_Months, _6_Months, _3_Months
    }

    public enum ResultsDisplay
    {
        Matches, Games, Maps
    }

    public partial class PlayerProfile : Form
    {
        private const string WIN_TEXT = "WIN";
        private const string LOSS_TEXT = "LOSS";

        private ResourceCacheSystem<Race, Image> raceImageCache;
        private List<PlayerStatsCloneDev> ratingDevelopment;
        private ObjectListView gameResultsListView;
        private ObjectListView matchResultsListView;
        private ObjectListView mapStatsListView;
        private PageSelecterLinker matchLVSelecterLink;
        private PageSelecterLinker gamesLVSelecterLink;
        private ResultsFilters resultsFilter;
        private SCPlayer player;

        private PlayerProfile(SCPlayer player)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.player = player;

            this.lbName.Text = player.Name;

            this.SetPlayerDetails();

            this.ratingDevelopment = GlobalState.DataBase.PlayerStatsDevelopment(player).OrderBy(item => item.Date.Date).ToList();

            ObjectListView performanceLstV = EloSystemGUIStaticMembers.CreatePlayerPerformanceListView(this.player);
            performanceLstV.Margin = new Padding(3, 3, 3, 6);
            this.tblLOPnlPerformance.Controls.Add(performanceLstV, 0, 0);
            this.tblLOPnlPerformance.BackColor = Color.Transparent;

            this.cmbBxSetDevInterval.DisplayMember = "Item1";
            this.cmbBxSetDevInterval.ValueMember = "Item2";
            this.cmbBxSetDevInterval.Items.AddRange(Enum.GetValues(typeof(DevelopmentInterval)).Cast<DevelopmentInterval>().Select(interval =>
                Tuple.Create<string, DevelopmentInterval>(interval.ToString().Replace("_", " "), interval)).ToArray());

            this.cmbBxSetDevInterval.SelectedIndex = this.cmbBxSetDevInterval.Items.Cast<Tuple<string, DevelopmentInterval>>().TakeWhile(item => item.Item2 != Settings.Default.RatingDevInterval).Count();

            switch (Settings.Default.PlayerResultDisplayTypes)
            {
                case ResultsDisplay.Matches: this.tabCtrlResults.SelectedTab = this.tabPageMatchResults; break;
                case ResultsDisplay.Games: this.tabCtrlResults.SelectedTab = this.tabPageSingleGames; break;
                case ResultsDisplay.Maps: this.tabCtrlResults.SelectedTab = this.tabPageMaps; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(ResultsDisplay).Name, Settings.Default.PlayerResultDisplayTypes));
            }

            this.resultsFilter = new ResultsFilters(this.player);
            this.tblLOPnlResults.Controls.Add(this.resultsFilter, 0, 0);
            this.resultsFilter.ResultFilterChanged += this.OnResultsFilterChanged;

            this.matchResultsListView = this.CreateMatchListView();
            this.matchLVSelecterLink = new PageSelecterLinker(this.matchResultsListView) { ItemsPerPage = (int)Settings.Default.MatchesPerPage };
            this.matchLVSelecterLink.ItemGetter = () => this.resultsFilter.MatchFilterFromGames(GlobalState.DataBase.GetAllGames()).OrderNewestFirst();
            this.tblLOPnlMatches.Controls.Add(this.matchResultsListView, 0, 1);
            Styles.PageSelecterStyles.SetSpaceStyle(this.matchLVSelecterLink.Selecter);
            this.tblLOPnlMatches.Controls.Add(this.matchLVSelecterLink.Selecter, 0, 0);

            this.gameResultsListView = this.CreateGameListView();
            this.gamesLVSelecterLink = new PageSelecterLinker(this.gameResultsListView) { ItemsPerPage = (int)Settings.Default.MatchesPerPage };
            this.gamesLVSelecterLink.ItemGetter = () => this.resultsFilter.GamesFilter(GlobalState.DataBase.GetAllGames()).Where(game => this.OpponentRaceFilter(game)).OrderNewestFirst();
            this.tblLOPnlGames.Controls.Add(this.gameResultsListView, 0, 1);
            this.tblLOPnlGames.SetColumnSpan(this.gameResultsListView, 2);
            this.tblLOPnlGames.Controls.Add(this.gamesLVSelecterLink.Selecter, 1, 0);
            Styles.PageSelecterStyles.SetSpaceStyle(this.gamesLVSelecterLink.Selecter);

            this.mapStatsListView = this.CreateMapStatsListView();
            this.tabPageMaps.Controls.Add(this.mapStatsListView);

            this.SetResults();
        }

        public static void ShowProfile(SCPlayer player, Form anchorForm = null)
        {
            PlayerProfile.ShowProfile(player, null, anchorForm);
        }

        public static void ShowProfile(SCPlayer player, SCPlayer headToHeadOpponent, Form anchorform = null)
        {
            PlayerProfile.ShowProfile(player, headToHeadOpponent, null, anchorform);
        }

        public static void ShowProfile(SCPlayer player, SCPlayer headToHeadOpponent, Map headToHeadMap, Form anchorform = null)
        {
            System.Windows.Forms.Cursor previousCursor = System.Windows.Forms.Cursor.Current;

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            var profileForm = new PlayerProfile(player);

            if (headToHeadOpponent != null && !player.Equals(headToHeadOpponent)) { profileForm.resultsFilter.SetHeadToHeadOpponent(headToHeadOpponent); }

            if (headToHeadMap != null) { profileForm.resultsFilter.SetMapFilter(headToHeadMap); }

            System.Windows.Forms.Cursor.Current = previousCursor;

            if (anchorform != null) { CustomControls.Styles.FormStyles.ShowFullFormRelativeToAnchor(profileForm, anchorform); }

            profileForm.ShowDialog();

            profileForm.Dispose();
        }

        private void OnResultsFilterChanged(object sender, EventArgs e)
        {
            this.SetResults();
        }

        private void SetResults()
        {
            this.matchLVSelecterLink.UpdateListItems();

            this.gamesLVSelecterLink.UpdateListItems();

            this.mapStatsListView.SetObjects(GlobalState.DataBase.GetMaps().OrderBy(map => map.Name).Select(map => Tuple.Create<Map, WinRateCounter>(map, GlobalState.DataBase.MapStatsForPlayer(this.player
                , map, this.resultsFilter.OpponentPlayer, this.resultsFilter.SelectedTournament))));

        }

        private void OnOpponentRaceChanged(object sender, EventArgs e)
        {
            this.gamesLVSelecterLink.UpdateListItems();
        }

        private bool OpponentRaceFilter(Game game)
        {
            if (this.rdBtnAny.Checked) { return true; }
            else if (this.rdBtnZerg.Checked) { return (game.Player1.Equals(this.player) ? game.Player2Race : game.Player1Race) == Race.Zerg; }
            else if (this.rdBtnTerran.Checked) { return (game.Player1.Equals(this.player) ? game.Player2Race : game.Player1Race) == Race.Terran; }
            else if (this.rdBtnProtoss.Checked) { return (game.Player1.Equals(this.player) ? game.Player2Race : game.Player1Race) == Race.Protoss; }
            else if (this.rdBtnRandom.Checked) { return (game.Player1.Equals(this.player) ? game.Player2Race : game.Player1Race) == Race.Random; }
            else { return false; }
        }

        private static double AxisXDynamicDateTimeInterval(DateTime oldestDate, DateTime newestDate)
        {
            TimeSpan intervalTime = newestDate.Subtract(oldestDate);

            const int DAYS_3YEARS = 3 * 365;
            const int DAYS_2YEARS = 2 * 365;
            const int DAYS_1YEAR = 365;
            const int DAYS_6MONTHS = 365 / 2;

            const double INTERVAL_WHOLEYEARS = 1;
            const double INTERVAL_6MONTHS = 6 / 12.0;
            const double INTERVAL_QUANTILES = 4 / 12.0;
            const double INTERVAL_THIRDS = 3 / 12.0;
            const double INTERVAL_MONTHS = 1 / 12.0;

            if (intervalTime.TotalDays >= DAYS_3YEARS) { return INTERVAL_WHOLEYEARS; }
            else if (intervalTime.TotalDays >= DAYS_2YEARS) { return INTERVAL_6MONTHS; }
            else if (intervalTime.TotalDays >= DAYS_1YEAR) { return INTERVAL_QUANTILES; }
            else if (intervalTime.TotalDays >= DAYS_6MONTHS) { return INTERVAL_THIRDS; }
            else { return INTERVAL_MONTHS; }
        }

        private static Chart GetRatingDevChart(IEnumerable<PlayerStatsCloneDev> data)
        {
            var ratingChart = new Chart();

            ratingChart.Titles.Add("Rating development");
            foreach (var title in ratingChart.Titles) { title.Font = new Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); }
            ratingChart.AntiAliasing = AntiAliasingStyles.All;
            ratingChart.Dock = DockStyle.Fill;
            ratingChart.BackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor;
            ratingChart.Margin = new Padding(3, 6, 3, 3);

            IEnumerable<int> ratingData = data.SelectMany(item => new int[4] { item.RatingTotal(), item.RatingVs.Protoss, item.RatingVs.Terran, item.RatingVs.Zerg });

            const double BUFFER = 0.1;
            int ratingMin = ratingData.Min();
            int ratingMax = ratingData.Max();

            int yAxisRange = ((ratingMax - ratingMin) / (1 - BUFFER * 2)).RoundToInt();

            const double ROUND_VALUE = 100;
            const int INTERVAL_MIN = 100;
            const double INTERVAL_GOAL = 4;

            var chartArea = new ChartArea();
            chartArea.AxisY.Minimum = Math.Max(0, ((ratingMin - yAxisRange * BUFFER) / ROUND_VALUE).RoundDown() * ROUND_VALUE);
            chartArea.AxisY.Maximum = ((ratingMax + yAxisRange * BUFFER) / ROUND_VALUE).RoundUp() * ROUND_VALUE;
            chartArea.AxisY.Interval = Math.Max(INTERVAL_MIN, ((yAxisRange / INTERVAL_GOAL) / ROUND_VALUE).RoundToInt() * ROUND_VALUE);
            chartArea.AxisX.Interval = PlayerProfile.AxisXDynamicDateTimeInterval(data.GetMinBy(item => item.Date).Date, data.GetMaxBy(item => item.Date).Date);
            chartArea.Position.Auto = false;
            chartArea.Position.X = 0F;
            chartArea.Position.Y = 9F;
            chartArea.Position.Width = 99F;
            chartArea.Position.Height = 82F;
            chartArea.AxisX.IsMarginVisible = true;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor;


            chartArea.AxisX.CustomLabels.Clear();

            chartArea.AxisX.LabelStyle.Format = "d";
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Years;
            chartArea.AxisX.IntervalOffsetType = DateTimeIntervalType.Years;
            chartArea.AxisX.IsLabelAutoFit = true;

            ratingChart.ChartAreas.Add(chartArea);

            var legend = new Legend();
            legend.BackColor = Color.Transparent;
            legend.ForeColor = Color.Black;
            legend.BorderDashStyle = ChartDashStyle.Solid;
            legend.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            legend.Name = "legend";
            legend.Position.X = 0F;
            legend.Position.Y = chartArea.Position.Height + 10F;
            legend.Position.Width = 100F;
            legend.Position.Height = 10F;
            legend.IsTextAutoFit = true;


            ratingChart.Legends.Add(legend);

            const int MARKER_SIZE = 2;

            var totalRatingSerie = new Series("Overall") { ChartType = SeriesChartType.Area, Color = Color.FromArgb(140, 0, 0, 0), MarkerSize = MARKER_SIZE, BorderWidth = MARKER_SIZE };
            var vsZergRatingSerie = new Series("vs Zerg") { ChartType = SeriesChartType.Line, Color = Color.FromArgb(235, 0, 0), MarkerSize = MARKER_SIZE, BorderWidth = MARKER_SIZE };
            var vsTerranRatingSerie = new Series("vs Terran") { ChartType = SeriesChartType.Line, Color = Color.FromArgb(0, 0, 235), MarkerSize = MARKER_SIZE, BorderWidth = MARKER_SIZE };
            var vsProtossRatingSerie = new Series("vs Protoss") { ChartType = SeriesChartType.Line, Color = Color.FromArgb(0, 185, 0), MarkerSize = MARKER_SIZE, BorderWidth = MARKER_SIZE };

            IEnumerator<PlayerStatsCloneDev> eData = data.OrderBy(item => item.Date.Date).GetEnumerator();

            while (eData.MoveNext())
            {
                PlayerStatsCloneDev currentItem = eData.Current;

                totalRatingSerie.Points.AddXY(currentItem.Date, currentItem.RatingTotal());
                vsZergRatingSerie.Points.AddXY(currentItem.Date, currentItem.RatingVs.Zerg);
                vsTerranRatingSerie.Points.AddXY(currentItem.Date, currentItem.RatingVs.Terran);
                vsProtossRatingSerie.Points.AddXY(currentItem.Date, currentItem.RatingVs.Protoss);
            }

            ratingChart.Series.Add(totalRatingSerie);
            ratingChart.Series.Add(vsProtossRatingSerie);
            ratingChart.Series.Add(vsTerranRatingSerie);
            ratingChart.Series.Add(vsZergRatingSerie);

            return ratingChart;
        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 6)
            {
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);

                if (e.ColumnIndex == 2)
                {
                    if (e.CellValue.ToString() == PlayerProfile.WIN_TEXT) { e.SubItem.ForeColor = EloSystemGUIStaticMembers.WinColor; }
                    else if (e.CellValue.ToString() == PlayerProfile.LOSS_TEXT) { e.SubItem.ForeColor = EloSystemGUIStaticMembers.LoseColor; }
                }
                else if (e.ColumnIndex == 6) { EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem); }
            }
        }

        private void SetPlayerDetails()
        {
            this.lbPlayerInfoName.Text = this.player.Name;

            EloImage countryRes;

            this.tblLOPnlPlayerDetails.Controls.Remove(this.picBxCountry);
            this.tblLOPnlPlayerDetails.Controls.Remove(this.lbCountryName);

            if (this.player.Country != null)
            {
                if (GlobalState.DataBase.TryGetImage(this.player.Country.ImageID, out countryRes))
                {
                    Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxCountry, countryRes.Image);

                    this.tblLOPnlPlayerDetails.Controls.Add(this.picBxCountry, 3, 0);

                    this.toolTipPlayerProfile.SetToolTip(this.picBxCountry, this.player.Country.Name);
                }
                else
                {
                    this.lbCountryName.Text = player.Country.Name;
                    this.tblLOPnlPlayerDetails.Controls.Add(this.lbCountryName, 3, 0);
                    this.lbCountryName.Dock = DockStyle.Fill;
                }
            }


            EloImage teamRes;

            this.tblLOPnlPlayerDetails.Controls.Remove(this.picBxTeam);
            this.tblLOPnlPlayerDetails.Controls.Remove(this.lbTeamName);

            if (this.player.Team != null)
            {
                if (GlobalState.DataBase.TryGetImage(this.player.Team.ImageID, out teamRes))
                {
                    Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxTeam, teamRes.Image);

                    this.tblLOPnlPlayerDetails.Controls.Add(this.picBxTeam, 4, 0);

                    this.toolTipPlayerProfile.SetToolTip(this.picBxTeam, String.Format("Team: {0}", this.player.Team.Name));
                }
                else
                {
                    this.lbTeamName.Text = this.player.Team.Name;
                    this.tblLOPnlPlayerDetails.Controls.Add(this.lbTeamName, 4, 0);
                    this.lbTeamName.Dock = DockStyle.Fill;
                }
            }

            this.lbIRLName.Text = this.player.IRLName;

            foreach (string alias in this.player.GetAliases()) { this.lstViewAliases.Items.Add(alias); }

            DateTime birthDate;

            if (this.player.TryGetBirthDate(out birthDate)) { this.lbDateOfBirth.Text = birthDate.ToLongDateString(); }
            else { this.lbDateOfBirth.Text = ""; }


            EloImage playerRes;

            if (GlobalState.DataBase.TryGetImage(this.player.ImageID, out playerRes))
            {
                this.picBxPlayerPhoto.Image = playerRes.Image;

                this.toolTipPlayerProfile.SetToolTip(this.picBxPlayerPhoto, this.player.Name);
            }


            if (this.player.Stats.GamesTotal() > 0) { Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxRace, RaceIconProvider.GetRaceUsageIcon(this.player)); }
            else { Styles.PictureBoxStyles.SetPictureBoxStyleAndImage(this.picBxRace, null); }

            this.picBxRank.Image = GlobalState.RankSystem.GetRankImageMain(this.player, this.picBxRank.Height, true);
        }

        private void cmbBxSetDevInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbBxSetDevInterval.SelectedIndex < 0) { return; }

            Settings.Default.RatingDevInterval = (this.cmbBxSetDevInterval.SelectedItem as Tuple<string, DevelopmentInterval>).Item2;

            this.SetChart();
        }

        private void SetChart()
        {
            this.pnlRatingDev.Controls.Clear();

            IEnumerable<PlayerStatsCloneDev> intervalData = this.ratingDevelopment.FilterByInterval(Settings.Default.RatingDevInterval);

            this.cmbBxSetDevInterval.Visible = true;

            if (!this.ratingDevelopment.Any()) { this.cmbBxSetDevInterval.Visible = false; }
            else if (!intervalData.Any()) { this.pnlRatingDev.Controls.Add(PlayerProfile.GetRatingDevChart(new PlayerStatsCloneDev[] { this.ratingDevelopment.OrderBy(item => item.Date).Last() })); }
            else { this.pnlRatingDev.Controls.Add(PlayerProfile.GetRatingDevChart(intervalData)); }
        }

        private void tabCtrlResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabCtrlResults.SelectedTab == this.tabPageMatchResults) { Settings.Default.PlayerResultDisplayTypes = ResultsDisplay.Matches; }
            else if (this.tabCtrlResults.SelectedTab == this.tabPageSingleGames) { Settings.Default.PlayerResultDisplayTypes = ResultsDisplay.Games; }
            else if (this.tabCtrlResults.SelectedTab == this.tabPageMaps) { Settings.Default.PlayerResultDisplayTypes = ResultsDisplay.Maps; }
        }

        private void PlayerMapStatsLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            const int ALTERNATIV_SORTCOLUMN_INDEX = 0;
            const int NAME_COLUMNINDEX = 1;
            const int OVERALL_COLUMNINDEX = 2;
            const int VS_Z_COLUMNINDEX = 3;
            const int VS_T_COLUMNINDEX = 4;
            const int VS_P_COLUMNINDEX = 5;
            const int VS_R_COLUMNINDEX = 6;

            this.mapStatsListView.SecondarySortColumn = this.mapStatsListView.PrimarySortColumn;
            this.mapStatsListView.SortGroupItemsByPrimaryColumn = true;

            if (e.Column == NAME_COLUMNINDEX)
            {
                this.mapStatsListView.PrimarySortColumn = this.mapStatsListView.AllColumns[e.Column];
                this.mapStatsListView.Sort();
            }
            else
            {
                if (e.Column == OVERALL_COLUMNINDEX)
                {
                    this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX].AspectGetter = obj =>
                    {
                        var matchData = obj as Tuple<Map, WinRateCounter>;

                        return (matchData.Item2.WinRatioTotal() * 100).RoundToInt();
                    };


                }
                else if (e.Column == VS_Z_COLUMNINDEX)
                {
                    this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX].AspectGetter = obj =>
                    {
                        var matchData = obj as Tuple<Map, WinRateCounter>;

                        return (matchData.Item2.WinRatioVs(Race.Zerg) * 100).RoundToInt();
                    };
                }
                else if (e.Column == VS_T_COLUMNINDEX)
                {
                    this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX].AspectGetter = obj =>
                    {
                        var matchData = obj as Tuple<Map, WinRateCounter>;

                        return (matchData.Item2.WinRatioVs(Race.Terran) * 100).RoundToInt();
                    };
                }
                else if (e.Column == VS_P_COLUMNINDEX)
                {
                    this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX].AspectGetter = obj =>
                    {
                        var matchData = obj as Tuple<Map, WinRateCounter>;

                        return (matchData.Item2.WinRatioVs(Race.Protoss) * 100).RoundToInt();

                    };
                }
                else if (e.Column == VS_R_COLUMNINDEX)
                {
                    this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX].AspectGetter = obj =>
                    {
                        var matchData = obj as Tuple<Map, WinRateCounter>;

                        return (matchData.Item2.WinRatioVs(Race.Random) * 100).RoundToInt();

                    };
                }

                this.mapStatsListView.PrimarySortColumn = this.mapStatsListView.AllColumns[e.Column];

                this.mapStatsListView.Sorting = this.mapStatsListView.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                this.mapStatsListView.Sort(this.mapStatsListView.AllColumns[ALTERNATIV_SORTCOLUMN_INDEX], this.mapStatsListView.Sorting);
            }
        }

        private ObjectListView CreateMapStatsListView()
        {
            var mapLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Clickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = 20,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(645, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            mapLV.ColumnClick += this.PlayerMapStatsLV_ColumnClick;

            const int STATS_WIDTH_DEFAULT = 120;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmMapName = new OLVColumn() { Width = 150, Text = "Map" };
            var olvClmWinRateVsAll = new OLVColumn() { Width = STATS_WIDTH_DEFAULT + 35, Text = "Overall" };
            var olvClmWinRateVsZerg = new OLVColumn() { Width = STATS_WIDTH_DEFAULT, Text = "vs Zerg" };
            var olvClmWinRateVsTerran = new OLVColumn() { Width = STATS_WIDTH_DEFAULT, Text = "vs Terran" };
            var olvClmWinRateVsProtoss = new OLVColumn() { Width = STATS_WIDTH_DEFAULT, Text = "vs Protoss" };
            var olvClmWinRateVsRandom = new OLVColumn() { Width = STATS_WIDTH_DEFAULT, Text = "vs Random" };

            mapLV.PrimarySortColumn = olvClmMapName;

            mapLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmMapName, olvClmWinRateVsAll, olvClmWinRateVsZerg, olvClmWinRateVsTerran, olvClmWinRateVsProtoss, olvClmWinRateVsRandom });

            mapLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmMapName, olvClmWinRateVsAll, olvClmWinRateVsZerg, olvClmWinRateVsTerran, olvClmWinRateVsProtoss, olvClmWinRateVsRandom });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmWinRateVsAll, olvClmWinRateVsZerg, olvClmWinRateVsTerran, olvClmWinRateVsProtoss, olvClmWinRateVsRandom })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            olvClmMapName.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return matchData.Item1.Name; }
                else { return string.Empty; }
            };

            Func<double, int, int, string> MapStatsText = (winRatio, wins, totalGames) => { return totalGames > 0 ? String.Format("{0}%  -  {1}/{2}", (winRatio * 100).RoundToInt(), wins, totalGames) : ""; };

            olvClmWinRateVsAll.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return MapStatsText(matchData.Item2.WinRatioTotal(), matchData.Item2.WinsTotal(), matchData.Item2.GamesTotal()); }
                else { return ""; }
            };

            Func<Race, WinRateCounter, string> VsRacePerformanceText = (race, winRates) => { return MapStatsText(winRates.WinRatioVs(race), winRates.WinsVs(race), winRates.GamesVs(race)); };

            olvClmWinRateVsZerg.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return VsRacePerformanceText(Race.Zerg, matchData.Item2); }
                else { return ""; }
            };

            olvClmWinRateVsTerran.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return VsRacePerformanceText(Race.Terran, matchData.Item2); }
                else { return ""; }
            };

            olvClmWinRateVsProtoss.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return VsRacePerformanceText(Race.Protoss, matchData.Item2); }
                else { return ""; }
            };

            olvClmWinRateVsRandom.AspectGetter = obj =>
            {
                var matchData = obj as Tuple<Map, WinRateCounter>;

                if (matchData != null) { return VsRacePerformanceText(Race.Random, matchData.Item2); }
                else { return ""; }
            };

            return mapLV;
        }

        private ObjectListView CreateMatchListView()
        {
            var matchLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = 20,
                Scrollable = true,
                ShowFilterMenuOnRightClick = false,
                ShowGroups = false,
                Size = new Size(750, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            matchLV.MouseClick += this.MatchLV_MouseClick;
            Styles.ObjectListViewStyles.SetHotItemStyle(matchLV);
            Styles.ObjectListViewStyles.DeselectItemsOnMousUp(matchLV);

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null, IsVisible = false };
            var olvClmDate = new OLVColumn() { Width = 85, Text = "Date" };
            var olvClmResultType = new OLVColumn() { Width = 60, Text = "Result" };
            var olvClmOpponent = new OLVColumn() { Width = 140, Text = "Opponent" };
            var olvClmWins = new OLVColumn() { Width = 50, Text = "Wins" };
            var olvClmLosses = new OLVColumn() { Width = 50, Text = "Losses" };
            var olvClmRatingChange = new OLVColumn() { Width = 75, Text = "Rating Change" };
            var olvClmTournament = new OLVColumn() { Width = 140, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 130, Text = "Season" };

            matchLV.FormatCell += PlayerProfile.MatchLV_FormatCell;

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOpponent, olvClmWins, olvClmLosses, olvClmRatingChange, olvClmTournament, olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOpponent, olvClmWins, olvClmLosses, olvClmRatingChange, olvClmTournament, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmResultType, olvClmWins, olvClmLosses, olvClmRatingChange })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmResultType.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return this.player.Equals(match.SourceMatch.Winner) ? PlayerProfile.WIN_TEXT : PlayerProfile.LOSS_TEXT; }
                else { return ""; }
            };

            olvClmDate.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.SourceMatch.DateTime.ToShortDateString(); }
                else { return string.Empty; }
            };

            olvClmOpponent.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player1.Equals(this.player) ? match.Player2.Name : match.Player1.Name; }
                else { return ""; }
            };

            olvClmWins.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player1.Equals(this.player) ? match.WinsBy(PlayerSlotType.Player1) : match.WinsBy(PlayerSlotType.Player2); }
                else { return ""; }
            };

            olvClmLosses.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player1.Equals(this.player) ? match.WinsBy(PlayerSlotType.Player2) : match.WinsBy(PlayerSlotType.Player1); }
                else { return ""; }
            };

            olvClmRatingChange.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return Styles.StringStyles.ConvertRatingChangeString(match.RatingChangeBy(match.Player1.Equals(this.player) ? PlayerSlotType.Player1 : PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmTournament.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null && match.Tournament != null) { return match.Tournament.Name; }
                else { return ""; }
            };

            olvClmSeason.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null && match.Season != null) { return match.Season.Name; }
                else { return ""; }
            };

            return matchLV;
        }

        private void MatchLV_MouseClick(object sender, MouseEventArgs e)
        {
            var olv = sender as ObjectListView;

            if (olv == null || olv.SelectedItem == null) { return; }

            var selectedMatch = olv.SelectedItem.RowObject as MatchEditorItem;

            if (selectedMatch != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: this.resultsFilter.SetHeadToHeadOpponent(selectedMatch.Player1.Equals(this.player) ? selectedMatch.Player2 : selectedMatch.Player1); break;
                    case MouseButtons.Right:

                        if (selectedMatch.Tournament != null) { this.resultsFilter.SetTournamentFilter(selectedMatch.Tournament); }

                        break;
                    case MouseButtons.Middle:
                    case MouseButtons.None:
                    case MouseButtons.XButton1:
                    case MouseButtons.XButton2: break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MouseButtons).Name, e.Button.ToString()));
                }
            }

            olv.SelectedItems.Clear();
        }

        private ObjectListView CreateGameListView()
        {
            var gamesLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = 20,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(905, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            gamesLV.MouseClick += this.GamesLV_MouseClick;
            Styles.ObjectListViewStyles.SetHotItemStyle(gamesLV);
            Styles.ObjectListViewStyles.DeselectItemsOnMousUp(gamesLV);

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 85, Text = "Date" };
            var olvClmResultType = new OLVColumn() { Width = 60, Text = "Result" };
            var olvClmOwnRace = new OLVColumn() { Width = 50, Text = "Own Race" };
            var olvClmOpponentRace = new OLVColumn() { Width = 50, Text = "Opponent Race" };
            var olvClmOpponent = new OLVColumn() { Width = 135, Text = "Opponent" };
            var olvClmRatingChange = new OLVColumn() { Width = 65, Text = "Rating Change" };
            var olvClmMap = new OLVColumn() { Width = 170, Text = "Map" };
            var olvClmTournament = new OLVColumn() { Width = 130, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 130, Text = "Season" };

            gamesLV.FormatCell += PlayerProfile.MatchLV_FormatCell;

            gamesLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOwnRace, olvClmOpponentRace, olvClmOpponent, olvClmRatingChange, olvClmMap ,olvClmTournament
                , olvClmSeason });

            gamesLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOwnRace, olvClmOpponentRace, olvClmOpponent, olvClmRatingChange, olvClmMap, olvClmTournament
                , olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmResultType, olvClmOwnRace, olvClmOpponentRace, olvClmRatingChange })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmResultType.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return this.player.Equals(game.Winner) ? PlayerProfile.WIN_TEXT : PlayerProfile.LOSS_TEXT; }
                else { return ""; }
            };

            olvClmDate.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Match.DateTime.ToShortDateString(); }
                else { return string.Empty; }
            };

            olvClmOpponent.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Player1.Equals(this.player) ? game.Player2.Name : game.Player1.Name; }
                else { return ""; }
            };

            const int RACE_IMAGE_HEIGHT_MAX = 18;
            const int RACE_IMAGE_WIDTH_MAX = 50;

            this.raceImageCache = new ResourceCacheSystem<Race, Image>() { ResourceGetter = (r) => RaceIconProvider.GetRaceBitmap(r).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX) };

            olvClmOwnRace.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (this.player.Stats.GamesTotal() > 0) { return new Image[] { this.raceImageCache.GetResource(this.player.Equals(game.Winner) ? game.WinnersRace : game.LosersRace) }; }
                else { return null; }
            };

            olvClmOpponentRace.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (this.player.Stats.GamesTotal() > 0) { return new Image[] { this.raceImageCache.GetResource(this.player.Equals(game.Winner) ? game.LosersRace : game.WinnersRace) }; }
                else { return null; }
            };

            var raceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 1, 6, 6) };
            olvClmOwnRace.Renderer = raceRenderer;
            olvClmOpponentRace.Renderer = raceRenderer;

            olvClmRatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return Styles.StringStyles.ConvertRatingChangeString((game.RatingChange * (this.player.Equals(game.Winner) ? 1 : -1)).ToString()); }
                else { return ""; }
            };

            olvClmMap.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null && game.Map != null) { return game.Map.Name; }
                else { return "N/A"; }
            };

            olvClmTournament.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null && game.Tournament != null) { return game.Tournament.Name; }
                else { return ""; }
            };

            olvClmSeason.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null && game.Season != null) { return game.Season.Name; }
                else { return ""; }
            };

            return gamesLV;
        }

        private void GamesLV_MouseClick(object sender, MouseEventArgs e)
        {
            var olv = sender as ObjectListView;

            if (olv == null || olv.SelectedItem == null) { return; }

            var selectedGame = olv.SelectedItem.RowObject as Game;

            if (selectedGame != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: this.resultsFilter.SetHeadToHeadOpponent(selectedGame.Player1.Equals(this.player) ? selectedGame.Player2 : selectedGame.Player1); break;
                    case MouseButtons.Right:

                        if (selectedGame.Map != null) { this.resultsFilter.SetMapFilter(selectedGame.Map); }

                        break;
                    case MouseButtons.Middle:
                    case MouseButtons.None:
                    case MouseButtons.XButton1:
                    case MouseButtons.XButton2: break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MouseButtons).Name, e.Button.ToString()));
                }
            }

            olv.SelectedItems.Clear();
        }

        private void PlayerProfile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

    }
}
