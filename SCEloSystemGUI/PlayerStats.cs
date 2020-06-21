using EloSystemExtensions;
using System.Collections.Generic;
using BrightIdeasSoftware;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public enum PlayerDevScope
    {
        Month_1, Months_3, Months_4, Months_6
    }

    internal partial class PlayerStats : Form
    {
        private const float TEXT_SIZE = 11.5F;
        private const int OLV_PLAYERSTATS_ROW_HEIGHT = 26;

        private ActivityFilter activityFilter;
        private List<IPlayerFilter> filters = new List<IPlayerFilter>();
        private ContentFilter<Country> countryFilter;
        private ContentFilter<Team> teamFilter;
        private ObjectListView olvPlayerStats;
        private ObjectListView olvPlayerSearch;
        private PlayerSearch searcher;
        private Dictionary<Team, Bitmap> teamBanners = new Dictionary<Team, Bitmap>();
        private Dictionary<string, Bitmap> raceIcons = new Dictionary<string, Bitmap>();
        private Dictionary<Country, Bitmap> flags = new Dictionary<Country, Bitmap>();
        private Dictionary<SCPlayer, string> raceUsageIdentifierStrings = new Dictionary<SCPlayer, string>();
        private IEnumerable<Tuple<SCPlayer, int>> playersFilteredAndRanked
        {
            get
            {
                return GlobalState.DataBase.GetPlayers().Where(player => this.filters.All(filter => filter.PlayerFilter(player))).OrderByDescending(player => player.RatingTotal()).ThenByDescending(p =>
                    GlobalState.DataBase.GamesByPlayer(p).OrderNewestFirst().First().Match.DateTime.Date).ThenByDescending(player => player.Stats.GamesTotal()).ThenByDescending(player =>
                        player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1));
            }
        }

        internal PlayerStats()
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.olvPlayerStats = this.CreatePlayerStatsListView();
            this.olvPlayerStats.SelectionChanged += this.OlvPlayerStats_SelectionChanged;

            this.tblLoPnlPlayerStats.Controls.Add(this.olvPlayerStats, 0, 2);

            this.countryFilter = new ContentFilter<Country>() { Header = "Country filter", ColumnHeader = "Country", Margin = new Padding(6) };
            this.countryFilter.ContentGetter = PlayerStats.GetCountry;
            this.countryFilter.SetItems(GlobalState.DataBase.GetCountries().OrderBy(country => country.Name));
            this.countryFilter.FilterChanged += this.OnFilterChanged;
            this.filters.Add(this.countryFilter);
            this.tblLoPnlFilters.Controls.Add(this.countryFilter, 0, 0);

            this.teamFilter = new ContentFilter<Team>() { Header = "Team filter", ColumnHeader = "Team", ImageDimensionMax = 21, Margin = new Padding(6) };
            this.teamFilter.ContentGetter = PlayerStats.GetTeam;
            this.teamFilter.SetItems(GlobalState.DataBase.GetTeams().OrderBy(team => team.Name));
            this.teamFilter.FilterChanged += this.OnFilterChanged;
            this.filters.Add(this.teamFilter);
            this.tblLoPnlFilters.Controls.Add(this.teamFilter, 1, 0);

            this.activityFilter = new ActivityFilter() { Margin = new Padding(6) };
            this.activityFilter.FilterChanged += this.OnFilterChanged;
            this.filters.Add(this.activityFilter);
            this.tblLoPnlFilters.Controls.Add(this.activityFilter, 2, 0);

            this.olvPlayerSearch = EloGUIControlsStaticMembers.CreatePlayerSearchListView();
            this.olvPlayerSearch.SelectionChanged += this.OlvPlayerSearch_SelectionChanged;
            this.searcher = new PlayerSearch(this.olvPlayerSearch);
            this.searcher.PlayerSearchInitiated += this.OnPlayerSearch;
            this.tabPagePlayerSearch.Controls.Add(this.searcher);

            this.SetPlayerList();

            this.SetbtnApllyEnabledStatus();

            GlobalState.DataBase.CountryPoolChanged += this.OnCountryPoolChanged;
            GlobalState.DataBase.PlayerPoolChanged += this.OnPlayerPoolChanged;
            GlobalState.DataBase.MatchPoolChanged += this.OnMatchPoolChanged;

            this.tabCtrlCustomizations.Visible = false;
            this.tblLoPnlPlayerStats.RowStyles[PlayerStats.FILTERROW_INDEX].Height = 0;

        }

        private void OlvPlayerSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (this.olvPlayerSearch.SelectedItem == null) { return; }

            var tuple = this.olvPlayerSearch.SelectedItem.RowObject as Tuple<SCPlayer, int>;

            if (tuple != null && this.olvPlayerStats.Scrollable)
            {
                const int ROW_HEIGHT_CORRECTED = PlayerStats.OLV_PLAYERSTATS_ROW_HEIGHT + 1;
                this.olvPlayerStats.LowLevelScroll(0, (tuple.Item2 - 1) * ROW_HEIGHT_CORRECTED - this.olvPlayerStats.LowLevelScrollPosition.Y * ROW_HEIGHT_CORRECTED);
            }
        }

        private void OlvPlayerStats_SelectionChanged(object sender, EventArgs e)
        {
            if (this.olvPlayerStats.SelectedItem == null) { return; }

            var tuple = this.olvPlayerStats.SelectedItem.RowObject as Tuple<SCPlayer, int>;

            if (tuple != null && tuple.Item1 != null)
            {
                PlayerProfile.ShowProfile(tuple.Item1 as SCPlayer, this);

                this.olvPlayerStats.SelectedItems.Clear();
            }

        }

        private void OnPlayerSearch(object sender, PlayerSearchEventArgs e)
        {
            Cursor previousCursor = Cursor.Current;

            Cursor.Current = Cursors.WaitCursor;

            this.olvPlayerSearch.SetObjects(this.playersFilteredAndRanked.Where(tuple => GlobalState.DataBase.PlayerLookup(e.SearchString).Contains(tuple.Item1)));

            Cursor.Current = previousCursor;
        }

        private static Country GetCountry(SCPlayer player)
        {
            return player.Country;
        }

        private static Team GetTeam(SCPlayer player)
        {
            return player.Team;
        }

        private void OnCountryPoolChanged(object sender, EventArgs e)
        {
            this.countryFilter.SetItems(GlobalState.DataBase.GetCountries().OrderBy(country => country.Name));
        }

        private void OnPlayerPoolChanged(object sender, EventArgs e)
        {
            this.SetPlayerList();
        }

        private void OnMatchPoolChanged(object sender, EventArgs e)
        {
            this.SetPlayerList();
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = this.filters.Any(filter => filter.HasChangesNotApplied());
        }

        private ObjectListView CreatePlayerStatsListView()
        {
            var playerStatsLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = true,
                HasCollapsibleGroups = false,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                UseHotItem = true,
                Margin = new Padding(4),
                MultiSelect = false,
                RowHeight = PlayerStats.OLV_PLAYERSTATS_ROW_HEIGHT,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(850, 900),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            playerStatsLV.FormatCell += PlayerStats.PlayerStats_FormatCell;

            Styles.ObjectListViewStyles.SetHotItemStyle(playerStatsLV);
            Styles.ObjectListViewStyles.AvoidFocus(playerStatsLV);

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmRank = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmName = new OLVColumn() { Width = 130, Text = "Name" };
            var olvClmCountry = new OLVColumn() { Width = 65, Text = "Country" };
            var olvClmTeam = new OLVColumn() { Width = 60, Text = "Team" };
            var olvClmMainRace = new OLVColumn() { Width = 60, Text = "Race" };
            var olvClmRankMain = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmRatingMain = new OLVColumn() { Width = 70, Text = "Rating - overall" };
            var olvClmRatingDevelopment = new OLVColumn() { Width = 70, Text = "Development" };
            var olvClmRatingVsZerg = new OLVColumn() { Width = 70, Text = "Vs Zerg" };
            var olvClmRatingVsTerran = new OLVColumn() { Width = 70, Text = "Vs Terran" };
            var olvClmRatingVsProtoss = new OLVColumn() { Width = 70, Text = "Vs Protoss" };
            var olvClmRatingVsRandom = new OLVColumn() { Width = 70, Text = "Vs Random" };

            playerStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmTeam,olvClmMainRace, olvClmRankMain, olvClmRatingMain, olvClmRatingDevelopment
                , olvClmRatingVsZerg, olvClmRatingVsTerran, olvClmRatingVsProtoss, olvClmRatingVsRandom });

            playerStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmTeam, olvClmMainRace, olvClmRankMain, olvClmRatingMain, olvClmRatingDevelopment, olvClmRatingVsZerg, olvClmRatingVsTerran, olvClmRatingVsProtoss, olvClmRatingVsRandom });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmCountry, olvClmTeam, olvClmMainRace, olvClmRankMain, olvClmRatingMain, olvClmRatingDevelopment, olvClmRatingVsZerg, olvClmRatingVsTerran, olvClmRatingVsProtoss, olvClmRatingVsRandom })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmRank.AspectGetter = obj =>
            {
                var rank = (obj as Tuple<SCPlayer, int>).Item2;

                return String.Format("{0}.", rank.ToString(Styles.NUMBER_FORMAT));
            };

            olvClmName.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.Name;
            };

            const int STANDARD_IMAGE_SIZE_MAX = 28;

            olvClmCountry.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage eloFlag;
                Bitmap flag;

                if (player.Country != null && this.flags.TryGetValue(player.Country, out flag)) { return new Image[] { flag }; }
                else if (player.Country != null && GlobalState.DataBase.TryGetImage(player.Country.ImageID, out eloFlag))
                {
                    this.flags.Add(player.Country, eloFlag.Image.ResizeSameAspectRatio(STANDARD_IMAGE_SIZE_MAX));

                    return new Image[] { this.flags[player.Country] };
                }
                else if (player.Country != null) { return player.Country.Name; }
                else { return null; }

            };

            const int TEAM_LOGO_SIZE_MAX = 23;

            olvClmTeam.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage eloTeamBanner;
                Bitmap teamBanner;

                if (player.Team != null && this.teamBanners.TryGetValue(player.Team, out teamBanner)) { return new Image[] { teamBanner }; }
                else if (player.Team != null && GlobalState.DataBase.TryGetImage(player.Team.ImageID, out eloTeamBanner))
                {
                    this.teamBanners.Add(player.Team, eloTeamBanner.Image.ResizeSameAspectRatio(TEAM_LOGO_SIZE_MAX));

                    return new Image[] { this.teamBanners[player.Team] };
                }
                else if (player.Team != null) { return player.Team.Name; }
                else { return null; }

            };

            var imgRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4) };
            olvClmCountry.Renderer = imgRenderer;
            olvClmTeam.Renderer = imgRenderer;

            const int RACE_IMAGE_HEIGHT_MAX = 26;
            const int RACE_IMAGE_WIDTH_MAX = 60;

            olvClmMainRace.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                Bitmap raceIcon;
                string raceUsageIdentifierString;

                if (player.Stats.GamesTotal() > 0)
                {
                    if (!this.raceUsageIdentifierStrings.TryGetValue(player, out raceUsageIdentifierString))
                    {
                        raceUsageIdentifierString = PlayerStats.MainRaceUsageToStringIdentifier(player);

                        this.raceUsageIdentifierStrings.Add(player, raceUsageIdentifierString);
                    }

                    if (!this.raceIcons.TryGetValue(raceUsageIdentifierString, out raceIcon))
                    {
                        raceIcon = RaceIconProvider.GetRaceUsageIcon(player).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX);

                        this.raceIcons.Add(raceUsageIdentifierString, raceIcon);
                    }

                    return new Image[] { this.raceIcons[this.raceUsageIdentifierStrings[player]] };
                }
                else { return null; }

            };

            var mainRaceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 2, 6, 6) };
            olvClmMainRace.Renderer = mainRaceRenderer;

            const int RANK_BOUNDS_HEIGHT = 1;
            const int RANK_BOUNDS_Y = 1;

            var mainRankRenderer = new ImageRenderer() { Bounds = new Rectangle(6, RANK_BOUNDS_Y, 6, RANK_BOUNDS_HEIGHT) };
            olvClmRankMain.Renderer = mainRankRenderer;

            olvClmRankMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return new Image[] { GlobalState.RankSystem.GetRankImageMain(player, PlayerStats.OLV_PLAYERSTATS_ROW_HEIGHT - (RANK_BOUNDS_Y + RANK_BOUNDS_HEIGHT), true) };
            };

            olvClmRatingMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingTotal().ToString(Styles.NUMBER_FORMAT);
            };

            olvClmRatingDevelopment.AspectGetter = obj =>
              {
                  var player = (obj as Tuple<SCPlayer, int>).Item1;

                  switch (Settings.Default.PlayerDevelopmentScope)
                  {
                      case PlayerDevScope.Month_1:
                          return Styles.StringStyles.ConvertRatingChangeString((player.RatingTotal()
                              - GlobalState.DataBase.PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-1)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_3:
                          return Styles.StringStyles.ConvertRatingChangeString((player.RatingTotal()
                              - GlobalState.DataBase.PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-3)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_4:
                          return Styles.StringStyles.ConvertRatingChangeString((player.RatingTotal()
                              - GlobalState.DataBase.PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-4)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_6:
                          return Styles.StringStyles.ConvertRatingChangeString((player.RatingTotal()
                              - GlobalState.DataBase.PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-6)).RatingTotal()).ToString());
                      default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerDevScope).Name, Settings.Default.PlayerDevelopmentScope));
                  }
              };

            olvClmRatingVsZerg.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Zerg.ToString(Styles.NUMBER_FORMAT);
            };

            olvClmRatingVsTerran.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Terran.ToString(Styles.NUMBER_FORMAT);
            };

            olvClmRatingVsProtoss.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Protoss.ToString(Styles.NUMBER_FORMAT);
            };

            olvClmRatingVsRandom.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Random.ToString(Styles.NUMBER_FORMAT);
            };

            return playerStatsLV;
        }

        private static string MainRaceUsageToStringIdentifier(SCPlayer player)
        {
            return player.Stats.GamesTotal() > 0 ?
                String.Join("", player.RaceUsageFrequency().Where(kvp => kvp.Value > 0).OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key.ToString().Substring(0, 1)))
                : "";
        }

        private static void PlayerStats_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 7) { e.SubItem.Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); }

            if (e.ColumnIndex == 8)
            {
                e.SubItem.Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem);
            }
        }

        internal void SetPlayerList()
        {
            this.olvPlayerStats.SetObjects(this.playersFilteredAndRanked);
        }

        private void SetbtnApllyEnabledStatus()
        {
            this.btnApply.Enabled = this.filters.Any(filter => filter.HasChangesNotApplied());
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.filters.ForEach(filter => filter.ApplyChanges());

            this.SetPlayerList();

            this.SetbtnApllyEnabledStatus();

            if (this.olvPlayerSearch.Items.Count > 0) { this.searcher.UpdateSearch(); }

        }

        private void btnToggleFilterVisibility_Click(object sender, EventArgs e)
        {
            this.ToggleFilterVisibility();
        }

        private void ToggleFilterVisibility()
        {
            if (!this.tabCtrlCustomizations.Visible)
            {
                this.tabCtrlCustomizations.Visible = true;

                TimedChangeHandler.HandleChanges(this.ShowFilters, this.StopFilterShowProcces);

                this.btnToggleCustomizationVisibility.Text = "Hide filters";
            }
            else
            {
                TimedChangeHandler.HandleChanges(this.HideCustomizations, this.StopFilterHideProcces);

                this.btnToggleCustomizationVisibility.Text = "Show filters";
            }
        }

        private void PlayerStats_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.tabCtrlCustomizations.Visible && this.StopFilterShowProcces()) { this.ToggleFilterVisibility(); }
                else if (!this.tabCtrlCustomizations.Visible) { this.Close(); }

            }
        }

        private void PlayerStats_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (KeyValuePair<Team, Bitmap> kvPair in this.teamBanners) { kvPair.Value.Dispose(); }

            foreach (KeyValuePair<string, Bitmap> kvPair in this.raceIcons) { kvPair.Value.Dispose(); }

            foreach (KeyValuePair<Country, Bitmap> kvPair in this.flags) { kvPair.Value.Dispose(); }

            this.teamBanners.Clear();

            this.raceIcons.Clear();

            this.flags.Clear();
        }
    }
}
