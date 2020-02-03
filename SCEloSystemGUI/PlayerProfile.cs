using BrightIdeasSoftware;
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
        Matches, Games
    }

    public partial class PlayerProfile : Form
    {
        private const string WIN_TEXT = "WIN";
        private const string LOSS_TEXT = "LOSS";

        private enum PerformanceTypes { Main = 0, vs_Zerg, vs_Terran, vs_Protoss, vs_Random }

        private List<PlayerStatsCloneDev> ratingDevelopment;
        private ObjectListView gameResultsListView;
        private ObjectListView matchResultsListView;
        private RankHandler rankHandler;
        private ResourceGetter eloDataSource;
        private ResultsFilters resultsFilter;
        private SCPlayer player;

        internal PlayerProfile(SCPlayer player, ResourceGetter eloSystem, RankHandler rankHandler)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.player = player;
            this.eloDataSource = eloSystem;
            this.rankHandler = rankHandler;

            this.lbName.Text = player.Name;

            this.SetPlayerDetails();

            this.ratingDevelopment = this.eloDataSource().PlayerStatsDevelopment(player).OrderBy(item => item.Date.Date).ToList();

            ObjectListView performanceLstV = this.CreatePlayerPerformanceListView();
            performanceLstV.Margin = new Padding(3, 3, 3, 6);
            performanceLstV.SetObjects(Enum.GetValues(typeof(PerformanceTypes)));
            this.tblLOPnlPerformance.Controls.Add(performanceLstV, 0, 0);
            this.tblLOPnlPerformance.BackColor = Color.Transparent;

            this.cmbBxSetDevInterval.DisplayMember = "Item1";
            this.cmbBxSetDevInterval.ValueMember = "Item2";
            this.cmbBxSetDevInterval.Items.AddRange(Enum.GetValues(typeof(DevelopmentInterval)).Cast<DevelopmentInterval>().Select(interval => Tuple.Create<string, DevelopmentInterval>(interval.ToString().Replace("_", " "), interval)).ToArray());

            this.cmbBxSetDevInterval.SelectedIndex = this.cmbBxSetDevInterval.Items.Cast<Tuple<string, DevelopmentInterval>>().TakeWhile(item => item.Item2 != Settings.Default.RatingDevInterval).Count();

            switch (Settings.Default.PlayerResultDisplayTypes)
            {
                case ResultsDisplay.Matches: this.tabCtrlResults.SelectedTab = this.tabPageMatchResults; break;
                case ResultsDisplay.Games: this.tabCtrlResults.SelectedTab = this.tabPageSingleGames; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(ResultsDisplay).Name, Settings.Default.PlayerResultDisplayTypes));
            }

            this.resultsFilter = new ResultsFilters(this.eloDataSource, this.rankHandler, this.player);
            this.tblLOPnlResults.Controls.Add(this.resultsFilter, 0, 0);
            this.resultsFilter.ResultFilterChanged += this.OnHeadToHeadOpponentChanged;

            this.matchResultsListView = this.CreateMatchListView();
            this.tabPageMatchResults.Controls.Add(this.matchResultsListView);

            this.gameResultsListView = this.CreateGameListView();
            this.tabPageSingleGames.Controls.Add(this.gameResultsListView);

            this.SetResults();
        }

        private void OnHeadToHeadOpponentChanged(object sender, EventArgs e)
        {
            this.SetResults();
        }

        private void SetResults()
        {
            Func<Game, bool> OpponentFilter;
            Func<Game, bool> MapFilter;

            if (this.resultsFilter.OpponentPlayer != null) { OpponentFilter = game => { return game.HasPlayer(this.resultsFilter.OpponentPlayer); }; }
            else { OpponentFilter = game => { return true; }; }

            if (this.resultsFilter.SelectedMap != null) { MapFilter = game => { return game.Map == this.resultsFilter.SelectedMap; }; }
            else { MapFilter = game => { return true; }; }

            this.matchResultsListView.SetObjects(this.eloDataSource().GetAllGames().Where(game => game.HasPlayer(this.player) && OpponentFilter(game) && MapFilter(game)).OrderByDescending(game =>
                 game.Match.DateTime.Date).ThenByDescending(game => game.Match.DailyIndex).ToMatchEditorItems());

            this.gameResultsListView.SetObjects(this.eloDataSource().GetAllGames().Where(game => game.HasPlayer(this.player) && OpponentFilter(game) && MapFilter(game)).OrderByDescending(game =>
                game.Match.DateTime.Date).ThenByDescending(game => game.Match.DailyIndex));
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
            foreach (var title in ratingChart.Titles) { title.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); }
            ratingChart.AntiAliasing = AntiAliasingStyles.All;
            ratingChart.Dock = DockStyle.Fill;
            ratingChart.Location = new Point(0, 0);
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
            chartArea.Position.Auto = true;
            chartArea.Position.X = 2F;
            chartArea.Position.Y = 10F;
            chartArea.Position.Width = 98F;
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

            var totalRatingSerie = new Series("Main") { ChartType = SeriesChartType.Area, Color = Color.FromArgb(140, 0, 0, 0), MarkerSize = MARKER_SIZE, BorderWidth = MARKER_SIZE };
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

        private static void PlayerPerformanceLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex >= 2) { e.SubItem.Font = new Font(e.SubItem.Font.FontFamily, e.SubItem.Font.Size, FontStyle.Bold, e.SubItem.Font.Unit, e.SubItem.Font.GdiCharSet); }
        }

        private ObjectListView CreatePlayerPerformanceListView()
        {
            const int ROW_HEIGHT = 20;
            const string INFORMATION_NA = "-";

            var performanceLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.None,
                Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = false,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = ROW_HEIGHT,
                Scrollable = false,
                ShowGroups = false,
                Size = new Size(550, 130),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmPerformanceType = new OLVColumn() { Width = 85, Text = "" };
            var olvClmOwnRace = new OLVColumn() { HeaderTextAlign = HorizontalAlignment.Center, TextAlign = HorizontalAlignment.Center, Width = 80, Text = "Own Race" };
            var olvClmRank = new OLVColumn() { Width = 30, Text = "Rank" };
            var olvClmRatingCurrent = new OLVColumn() { Width = 60, Text = "Current rating" };
            var olvClmRatingPeak = new OLVColumn() { Width = 110, Text = "Peak rating" };
            var olvClmWinPercentage = new OLVColumn() { Width = 60, Text = "Win %" };
            var olvClmWinFrequency = new OLVColumn() { Width = 120, Text = "Win frequency" };

            performanceLV.FormatCell += PlayerProfile.PlayerPerformanceLV_FormatCell;

            performanceLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmPerformanceType, olvClmOwnRace, olvClmRank, olvClmRatingCurrent, olvClmRatingPeak, olvClmWinPercentage
                , olvClmWinFrequency, });

            performanceLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmPerformanceType, olvClmOwnRace, olvClmRank, olvClmRatingCurrent, olvClmRatingPeak, olvClmWinPercentage
                , olvClmWinFrequency, });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmRatingPeak, olvClmRank, olvClmRatingCurrent, olvClmWinPercentage, olvClmWinFrequency })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Right;
                clm.TextAlign = HorizontalAlignment.Right;
                clm.CellPadding = new Rectangle(0, 0, 6, 0);
            }

            olvClmPerformanceType.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                return pType.ToString().Replace("_", ". ");
            };

            olvClmRatingCurrent.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Main: return this.player.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Zerg: return this.player.RatingVs.Zerg.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Terran: return this.player.RatingVs.Terran.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Protoss: return this.player.RatingVs.Protoss.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Random: return this.player.RatingVs.Random.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return INFORMATION_NA; }
            };

            const int RACE_IMAGE_HEIGHT_MAX = ROW_HEIGHT;

            olvClmOwnRace.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Main: return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.GetPrimaryRace()).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        case PerformanceTypes.vs_Zerg:
                            return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.GetPrimaryRaceVs(Race.Zerg)).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        case PerformanceTypes.vs_Terran:
                            return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.GetPrimaryRaceVs(Race.Terran)).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        case PerformanceTypes.vs_Protoss:
                            return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.GetPrimaryRaceVs(Race.Protoss)).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        case PerformanceTypes.vs_Random:
                            return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.GetPrimaryRaceVs(Race.Random)).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return null; }

            };

            var imgRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4) };
            olvClmOwnRace.Renderer = imgRenderer;

            var rankImgRenderer = new ImageRenderer() { Bounds = new Rectangle(5, 1, 5, 1) };
            olvClmRank.Renderer = rankImgRenderer;

            int imageHeight = ROW_HEIGHT - rankImgRenderer.Bounds.Y - rankImgRenderer.Bounds.Height;

            olvClmRank.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                switch (pType)
                {
                    case PerformanceTypes.Main: return new Image[] { this.rankHandler.GetRankImageMain(this.player, imageHeight, true) };
                    case PerformanceTypes.vs_Zerg: return new Image[] { this.rankHandler.GetRankImageVsRace(this.player, imageHeight, true, Race.Zerg) };
                    case PerformanceTypes.vs_Terran: return new Image[] { this.rankHandler.GetRankImageVsRace(this.player, imageHeight, true, Race.Terran) };
                    case PerformanceTypes.vs_Protoss: return new Image[] { this.rankHandler.GetRankImageVsRace(this.player, imageHeight, true, Race.Protoss) };
                    case PerformanceTypes.vs_Random: return null;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                }
            };


            olvClmRatingPeak.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                PlayerStatsCloneDev max;

                if (this.ratingDevelopment.Any())
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Main:
                            max = this.ratingDevelopment.GetMaxRangeBy(item => item.RatingTotal()).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Zerg:
                            max = this.ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Zerg).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Zerg.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Terran:
                            max = this.ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Terran).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Terran.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Protoss:
                            max = this.ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Protoss).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Protoss.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Random:
                            max = this.ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Random).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Random.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return "-"; }
            };

            olvClmWinPercentage.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Main: return String.Format("{0}%", (100 * player.Stats.WinRatioTotal()).RoundToInt());
                        case PerformanceTypes.vs_Zerg: return player.Stats.GamesVs(Race.Zerg) > 0 ? String.Format("{0}%", (100 * player.Stats.WinRatioVs(Race.Zerg)).RoundToInt()) : INFORMATION_NA;
                        case PerformanceTypes.vs_Terran: return player.Stats.GamesVs(Race.Terran) > 0 ? String.Format("{0}%", (100 * player.Stats.WinRatioVs(Race.Terran)).RoundToInt()) : INFORMATION_NA;
                        case PerformanceTypes.vs_Protoss: return player.Stats.GamesVs(Race.Protoss) > 0 ? String.Format("{0}%", (100 * player.Stats.WinRatioVs(Race.Protoss)).RoundToInt()) : INFORMATION_NA;
                        case PerformanceTypes.vs_Random: return player.Stats.GamesVs(Race.Random) > 0 ? String.Format("{0}%", (100 * player.Stats.WinRatioVs(Race.Random)).RoundToInt()) : INFORMATION_NA;
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return INFORMATION_NA; }

            };

            olvClmWinFrequency.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Main: return String.Format("{0}/{1}", player.Stats.WinsTotal(), player.Stats.GamesTotal());
                        case PerformanceTypes.vs_Zerg: return player.Stats.GamesVs(Race.Zerg) > 0 ? String.Format("{0}/{1}", player.Stats.WinsVs(Race.Zerg), player.Stats.GamesVs(Race.Zerg)) : INFORMATION_NA;
                        case PerformanceTypes.vs_Terran:
                            return player.Stats.GamesVs(Race.Terran) > 0 ? String.Format("{0}/{1}", player.Stats.WinsVs(Race.Terran), player.Stats.GamesVs(Race.Terran)) : INFORMATION_NA;
                        case PerformanceTypes.vs_Protoss:
                            return player.Stats.GamesVs(Race.Protoss) > 0 ? String.Format("{0}/{1}", player.Stats.WinsVs(Race.Protoss), player.Stats.GamesVs(Race.Protoss)) : INFORMATION_NA;
                        case PerformanceTypes.vs_Random:
                            return player.Stats.GamesVs(Race.Random) > 0 ? String.Format("{0}/{1}", player.Stats.WinsVs(Race.Random), player.Stats.GamesVs(Race.Random)) : INFORMATION_NA;
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return INFORMATION_NA; }

            };

            return performanceLV;
        }

        private void PlayerProfile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) { this.Close(); }
        }

        private void SetPlayerDetails()
        {
            EloImage countryRes;

            this.tblLOPnlPlayerDetails.Controls.Remove(this.picBxCountry);
            this.tblLOPnlPlayerDetails.Controls.Remove(this.lbCountryName);

            if (this.player.Country != null)
            {
                if (this.eloDataSource().TryGetImage(this.player.Country.ImageID, out countryRes))
                {
                    this.picBxCountry.Image = countryRes.Image;

                    this.tblLOPnlPlayerDetails.Controls.Add(this.picBxCountry, 1, 0);

                    this.toolTipPlayerProfile.SetToolTip(this.picBxCountry, this.player.Country.Name);
                }
                else
                {
                    this.lbCountryName.Text = player.Country.Name;
                    this.tblLOPnlPlayerDetails.Controls.Add(this.lbCountryName, 1, 0);
                }
            }


            EloImage teamRes;

            this.tblLOPnlPlayerDetails.Controls.Remove(this.picBxTeam);
            this.tblLOPnlPlayerDetails.Controls.Remove(this.lbTeamName);

            if (this.player.Team != null)
            {
                if (this.eloDataSource().TryGetImage(this.player.Team.ImageID, out teamRes))
                {
                    this.picBxTeam.Image = teamRes.Image;

                    this.tblLOPnlPlayerDetails.Controls.Add(this.picBxTeam, 1, 1);

                    this.toolTipPlayerProfile.SetToolTip(this.picBxTeam, this.player.Team.Name);
                }
                else
                {
                    this.lbTeamName.Text = this.player.Team.Name;
                    this.tblLOPnlPlayerDetails.Controls.Add(this.lbTeamName, 1, 1);
                }
            }

            this.lbIRLName.Text = this.player.IRLName;

            foreach (string alias in this.player.GetAliases()) { this.lstViewAliases.Items.Add(alias); }

            DateTime birthDate;

            if (this.player.TryGetBirthDate(out birthDate)) { this.lbDateOfBirth.Text = birthDate.ToLongDateString(); }
            else { this.lbDateOfBirth.Text = ""; }


            EloImage playerRes;

            if (this.eloDataSource().TryGetImage(this.player.ImageID, out playerRes))
            {
                this.picBxPlayerPhoto.Image = playerRes.Image;

                this.toolTipPlayerProfile.SetToolTip(this.picBxPlayerPhoto, this.player.Name);
            }


            if (this.player.Stats.GamesTotal() > 0) { this.picBxRace.Image = RaceIconProvider.GetRaceUsageIcon(this.player); }


            this.picBxRank.Image = rankHandler.GetRankImageMain(this.player, this.picBxRank.Height, true);
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
                ShowGroups = false,
                Size = new Size(750, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
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

                if (match != null)
                {
                    return EloGUIControlsStaticMembers.ConvertRatingChangeString(match.RatingChangeBy(match.Player1.Equals(this.player) ? PlayerSlotType.Player1 : PlayerSlotType.Player2));
                }
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

        private ObjectListView CreateGameListView()
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
                ShowGroups = false,
                Size = new Size(905, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 85, Text = "Date" };
            var olvClmResultType = new OLVColumn() { Width = 60, Text = "Result" };
            var olvClmOwnRace = new OLVColumn() { Width = 50, Text = "Own Race" };
            var olvClmOpponentRace = new OLVColumn() { Width = 50, Text = "Opponent Race" };
            var olvClmOpponent = new OLVColumn() { Width = 135, Text = "Opponent" };
            var olvClmRatingChange = new OLVColumn() { Width = 65, Text = "Rating Change" };
            var olvClmMap = new OLVColumn() { Width = 170, Text = "Map" };
            var olvClmTournament = new OLVColumn() { Width = 140, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 130, Text = "Season" };

            matchLV.FormatCell += PlayerProfile.MatchLV_FormatCell;

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOwnRace, olvClmOpponentRace, olvClmOpponent, olvClmRatingChange, olvClmMap ,olvClmTournament
                , olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmResultType, olvClmOwnRace, olvClmOpponentRace, olvClmOpponent, olvClmRatingChange, olvClmMap, olvClmTournament
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

            olvClmOwnRace.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.Equals(game.Winner) ? game.WinnersRace : game.LosersRace).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX
                        , RACE_IMAGE_HEIGHT_MAX) };
                }
                else { return null; }
            };

            olvClmOpponentRace.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (this.player.Stats.GamesTotal() > 0)
                {
                    return new Image[] { RaceIconProvider.GetRaceBitmap(this.player.Equals(game.Winner) ? game.LosersRace: game.WinnersRace).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX
                        , RACE_IMAGE_HEIGHT_MAX) };
                }
                else { return null; }
            };

            var raceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 1, 6, 6) };
            olvClmOwnRace.Renderer = raceRenderer;
            olvClmOpponentRace.Renderer = raceRenderer;

            olvClmRatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return EloGUIControlsStaticMembers.ConvertRatingChangeString((game.RatingChange * (this.player.Equals(game.Winner) ? 1 : -1)).ToString()); }
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

            return matchLV;
        }

    }
}
