using BrightIdeasSoftware;
using CustomControls;
using CustomControls.Utilities;
using CustomControls.Styles;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using EloSystemExtensions;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MapProfile : Form
    {
        private ResourceCacheSystem<Race, Image> raceImageCache;
        private GameFilter<Matchup> matchupFilter;
        private List<IGameFilter> gameFilters = new List<IGameFilter>();
        private ObjectListView gameListView;
        private PageSelecterLinker selectLinker;

        private MapProfile(Map map)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.lbName.Text = map.Name;

            this.lbSpawnPositions.Text = map.MapType.ToString().Substring(1).Replace("_", " ");
            this.lbSize.Text = String.Format("{0} x {1}", map.Size.Width, map.Size.Height);
            this.lbTileset.Text = map.Tileset != null ? map.Tileset.Name : "";
            this.lbDescription.Text = String.Join("\n\n", map.GetDescriptions());

            EloImage mapImg;

            if (GlobalState.DataBase.TryGetImage(map.ImageID, out mapImg)) { this.picBxMapImage.Image = mapImg.Image; }

            ObjectListView winRatioStatsLV = MapProfile.CreateWinRatioListView(map);
            winRatioStatsLV.Dock = DockStyle.Left;
            this.tblLOPnlMapStats.Controls.Add(winRatioStatsLV, 0, 2);
            this.tblLOPnlMapStats.SetColumnSpan(winRatioStatsLV, 2);

            ObjectListView mirrorMatchupStatsLV = MapProfile.CreateMirrorMatchupListView(map);
            this.tblLOPnlMapStats.Controls.Add(mirrorMatchupStatsLV, 2, 2);

            this.lbTotalGames.Text = map.Stats.TotalGames() > 0 ? map.Stats.TotalGames().ToString(Styles.NUMBER_FORMAT) : "0";

            this.matchupFilter = EloGUIControlsStaticMembers.CreateMatchupDrivenGameFilter();
            this.matchupFilter.FilterChanged += this.MatchupFilter_FilterChanged;
            this.tblLOPnlGames.Controls.Add(this.matchupFilter, 0, 1);
            this.gameFilters.Add(this.matchupFilter);

            this.gameListView = this.CreateGameListView();
            this.tblLOPnlGames.Controls.Add(this.gameListView, 0, 3);
            this.tblLOPnlGames.SetColumnSpan(this.gameListView, 2);
            this.selectLinker = new PageSelecterLinker(this.gameListView) { ItemsPerPage = (int)Settings.Default.MatchesPerPage };
            this.selectLinker.ItemGetter = () =>
            {
                return GlobalState.DataBase.GetAllGames().Where(game => game.Map != null && game.Map.Equals(map) && this.gameFilters.All(f => f.FilterGame(game))).OrderNewestFirst();
            };
            this.tblLOPnlGames.Controls.Add(this.selectLinker.Selecter, 1, 2);
            Styles.PageSelecterStyles.SetSpaceStyle(this.selectLinker.Selecter);


        }

        private void MatchupFilter_FilterChanged(object sender, EventArgs e)
        {
            this.SetButtonApplyFilterEnabledStatus();
        }

        private void SetButtonApplyFilterEnabledStatus()
        {
            this.btnApplyFilter.Enabled = this.gameFilters.Any(filter => filter.HasChangesNotApplied());
        }

        public static void ShowProfile(Map map, Form anchorForm = null)
        {
            var mapProfileForm = new MapProfile(map);

            if (anchorForm != null) { FormStyles.ShowFullFormRelativeToAnchor(mapProfileForm, anchorForm); }

            mapProfileForm.ShowDialog();

            mapProfileForm.Dispose();
        }

        private static ObjectListView CreateMirrorMatchupListView(Map map)
        {
            const int ROW_HEIGHT = 22;

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
                Size = new Size(275, 120),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmMatchup = new OLVColumn() { Width = 90, Text = "Matchup" };
            var olvClmGamesInMatchup = new OLVColumn() { Width = 90, Text = "Games" };
            var olvClmCoinTossFactor = new OLVColumn() { Width = 90, Text = "Coin Toss Factor" };

            performanceLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmMatchup, olvClmGamesInMatchup, olvClmCoinTossFactor });

            performanceLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmMatchup, olvClmGamesInMatchup, olvClmCoinTossFactor });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmGamesInMatchup, olvClmCoinTossFactor })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            const int MATCHUP_IMAGE_HEIGHT_MAX = ROW_HEIGHT - 2;

            olvClmMatchup.AspectGetter = obj =>
            {
                MirrorMatchup mirrorMatch = (MirrorMatchup)obj;

                return new Image[] { RaceIconProvider.GetMatchupImage(mirrorMatch.ToMatchupType()).ResizeSARWithinBounds(olvClmMatchup.Width, MATCHUP_IMAGE_HEIGHT_MAX) };
            };

            var imgMatchupRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4), CellPadding = new Rectangle(6, 1, 4, 1) };
            olvClmMatchup.Renderer = imgMatchupRenderer;

            olvClmGamesInMatchup.AspectGetter = obj =>
            {
                MirrorMatchup mirrorMatch = (MirrorMatchup)obj;

                RaceMatchupResults data;

                if (map.Stats.TryGetMatchup(mirrorMatch.ToMatchupType(), out data)) { return data.TotalGames.ToString(Styles.NUMBER_FORMAT); }
                else { return string.Empty; }
            };

            const string DATA_NOT_AVAILABLE_TEXT = "n/a";

            olvClmCoinTossFactor.AspectGetter = obj =>
            {
                MirrorMatchup mirrorMatch = (MirrorMatchup)obj;

                double coinTossFactor;

                if (GlobalState.MirrorMatchupEvaluation.TryGetCointossFactor(map, mirrorMatch, out coinTossFactor)) { return (coinTossFactor * 100).Round(2).ToString(); }
                else { return DATA_NOT_AVAILABLE_TEXT; }

            };

            performanceLV.SetObjects(Enum.GetValues(typeof(MirrorMatchup)).Cast<MirrorMatchup>());

            return performanceLV;
        }

        private static ObjectListView CreateWinRatioListView(Map map)
        {
            const int ROW_HEIGHT = 22;

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
                Size = new Size(440, 140),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmMatchup = new OLVColumn() { Width = 90, Text = "Matchup" };
            var olvClmGamesInMatchup = new OLVColumn() { Width = 90, Text = "Games" };
            var olvClmWinRatioCorrected = new OLVColumn() { Width = 100, Text = "Win Ratio Corrected", ToolTipText = "Win ratio corrected for players' rating difference" };
            var olvClmWinRatioStandard = new OLVColumn() { Width = 155, Text = "Win Ratio Standard" };

            performanceLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmMatchup, olvClmGamesInMatchup, olvClmWinRatioCorrected, olvClmWinRatioStandard });

            performanceLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmMatchup, olvClmGamesInMatchup, olvClmWinRatioCorrected, olvClmWinRatioStandard });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmGamesInMatchup, olvClmWinRatioCorrected, olvClmWinRatioStandard })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            const int MATCHUP_IMAGE_HEIGHT_MAX = ROW_HEIGHT - 2;

            olvClmMatchup.AspectGetter = obj =>
            {
                Matchup matchupType = (Matchup)obj;

                return new Image[] { RaceIconProvider.GetMatchupImage(matchupType).ResizeSARWithinBounds(olvClmMatchup.Width, MATCHUP_IMAGE_HEIGHT_MAX) };
            };

            var imgMatchupRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4), CellPadding = new Rectangle(6, 1, 4, 1) };
            olvClmMatchup.Renderer = imgMatchupRenderer;

            olvClmGamesInMatchup.AspectGetter = obj =>
            {
                Matchup matchupType = (Matchup)obj;

                RaceMatchupResults data;

                if (map.Stats.TryGetMatchup(matchupType, out data)) { return data.TotalGames.ToString(Styles.NUMBER_FORMAT); }
                else { return string.Empty; }
            };

            const string DATA_NOT_AVAILABLE_TEXT = "-";

            olvClmWinRatioCorrected.AspectGetter = obj =>
            {
                Matchup matchupType = (Matchup)obj;

                RaceMatchupResults data;

                if (map.Stats.TryGetMatchup(matchupType, out data))
                {
                    if (matchupType.Race1() == data.Race1)
                    {
                        return data.TotalGames > 0 ? String.Format("{0}% - {1}%", (data.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)
                            , (data.WinRatioRace2CorrectedForExpectedWR() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)) : DATA_NOT_AVAILABLE_TEXT;
                    }
                    else
                    {
                        return data.TotalGames > 0 ? String.Format("{0}% - {1}%", (data.WinRatioRace2CorrectedForExpectedWR() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)
                            , (data.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)) : DATA_NOT_AVAILABLE_TEXT;
                    }

                }
                else { return string.Empty; }

            };

            olvClmWinRatioStandard.AspectGetter = obj =>
            {
                Matchup matchupType = (Matchup)obj;

                RaceMatchupResults data;

                if (map.Stats.TryGetMatchup(matchupType, out data))
                {
                    if (matchupType.Race1() == data.Race1)
                    {
                        return data.TotalGames > 0 ? String.Format("{0}% - {1}%  |  {2} - {3}", (data.WinRatioRace1() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)
                            , (data.WinRatioRace2() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT), data.Race1Wins.ToString(Styles.NUMBER_FORMAT)
                            , data.Race2Wins.ToString(Styles.NUMBER_FORMAT)) : DATA_NOT_AVAILABLE_TEXT;
                    }
                    else
                    {
                        return data.TotalGames > 0 ? String.Format("{0}% - {1}%  |  {2} - {3}", (data.WinRatioRace2() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT)
                            , (data.WinRatioRace1() * 100).RoundToInt().ToString(Styles.NUMBER_FORMAT), data.Race2Wins.ToString(Styles.NUMBER_FORMAT)
                            , data.Race1Wins.ToString(Styles.NUMBER_FORMAT)) : DATA_NOT_AVAILABLE_TEXT;
                    }
                }
                else { return string.Empty; }

            };

            performanceLV.SetObjects(new Matchup[] { Matchup.TvZ, Matchup.ZvP, Matchup.PvT, Matchup.RvZ, Matchup.RvT, Matchup.RvP });

            return performanceLV;
        }

        private static void GamesLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);

                EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem);
            }
        }

        private ObjectListView CreateGameListView()
        {
            const int ROW_HEIGHT = 20;

            var gamesLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = ROW_HEIGHT,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(905, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            Styles.ObjectListViewStyles.SetHotItemStyle(gamesLV);
            Styles.ObjectListViewStyles.DeselectItemsOnMousUp(gamesLV);

            const int RACE_COLUMN_WIDTH = 50;
            const int RATING_CHANGE_COLUMN_WIDTH = 55;
            const int NAME_COLUMN_WIDTH = 100;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 75, Text = "Date" };
            var olvClmPlayer1Race = new OLVColumn() { Width = RACE_COLUMN_WIDTH, Text = "Race", ToolTipText = "Player 1's race" };
            var olvClmPlayer1Name = new OLVColumn() { Width = NAME_COLUMN_WIDTH, Text = "Name", ToolTipText = "Player 1's name" };
            var olvClmPlayer1RatingChange = new OLVColumn() { Width = RATING_CHANGE_COLUMN_WIDTH, Text = "Rating", ToolTipText = "Change in player 1's rating score" };
            var olvClmResult = new OLVColumn() { Width = 40, Text = "Result" };
            var olvClmPlayer2RatingChange = new OLVColumn() { Width = RATING_CHANGE_COLUMN_WIDTH, Text = "Rating", ToolTipText = "Change in player 2's rating score" };
            var olvClmPlayer2Name = new OLVColumn() { Width = NAME_COLUMN_WIDTH, Text = "Name", ToolTipText = "Player 2s name" };
            var olvClmPlayer2Race = new OLVColumn() { Width = RACE_COLUMN_WIDTH, Text = "Race", ToolTipText = "Player 2's race" };
            var olvClmTournament = new OLVColumn() { Width = 115, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 130, Text = "Season" };

            gamesLV.FormatCell += MapProfile.GamesLV_FormatCell;
            gamesLV.MouseClick += MapProfile.GamesLV_MouseClick;


            gamesLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmPlayer1Race, olvClmPlayer1Name, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange
                , olvClmPlayer2Name,olvClmPlayer2Race ,olvClmTournament, olvClmSeason });

            gamesLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmPlayer1Race, olvClmPlayer1Name, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange, olvClmPlayer2Name
                , olvClmPlayer2Race, olvClmTournament, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmPlayer1Race, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange, olvClmPlayer2Race })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmDate.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Match.DateTime.ToShortDateString(); }
                else { return string.Empty; }
            };

            const int RACE_IMAGE_HEIGHT_MAX = ROW_HEIGHT - 2;
            const int RACE_IMAGE_WIDTH_MAX = RACE_COLUMN_WIDTH;

            this.raceImageCache = new ResourceCacheSystem<Race, Image>() { ResourceGetter = (r) => RaceIconProvider.GetRaceBitmap(r).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX) };

            olvClmPlayer1Race.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return new Image[] { this.raceImageCache.GetResource(game.Player1Race) }; }
                else { return null; }
            };

            olvClmPlayer1Name.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Player1.Name; }
                else { return "N/A"; }
            };

            olvClmPlayer1RatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return Styles.StringStyles.ConvertRatingChangeString((game.RatingChange * (game.Player1.Equals(game.Winner) ? 1 : -1)).ToString()); }
                else { return ""; }
            };

            olvClmResult.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return String.Format("{0}", game.Winner.Equals(game.Player1) ? "1 - 0" : "0 - 1"); }
                else { return ""; }
            };

            var raceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 1, 6, 6) };
            olvClmPlayer1Race.Renderer = raceRenderer;
            olvClmPlayer2Race.Renderer = raceRenderer;


            olvClmPlayer2RatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return Styles.StringStyles.ConvertRatingChangeString((game.RatingChange * (game.Player2.Equals(game.Winner) ? 1 : -1)).ToString()); }
                else { return ""; }
            };

            olvClmPlayer2Name.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Player2.Name; }
                else { return "N/A"; }
            };

            olvClmPlayer2Race.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return new Image[] { this.raceImageCache.GetResource(game.Player2Race) }; }
                else { return null; }
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

        private static void GamesLV_MouseClick(object sender, MouseEventArgs e)
        {
            var olv = sender as ObjectListView;

            if (olv == null || olv.SelectedItem == null) { return; }

            var selectedGame = olv.SelectedItem.RowObject as Game;

            if (selectedGame != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: PlayerProfile.ShowProfile(selectedGame.Player1, olv.FindForm()); break;
                    case MouseButtons.Right: PlayerProfile.ShowProfile(selectedGame.Player2, olv.FindForm()); break;
                    case MouseButtons.Middle: PlayerProfile.ShowProfile(selectedGame.Player1, selectedGame.Player2, olv.FindForm()); break;
                    case MouseButtons.None:
                    case MouseButtons.XButton1:
                    case MouseButtons.XButton2: break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MouseButtons).Name, e.Button.ToString()));
                }
            }

            olv.SelectedItems.Clear();
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            this.gameFilters.ForEach(filter => filter.ApplyChanges());

            this.selectLinker.UpdateListItems();

            this.SetButtonApplyFilterEnabledStatus();
        }

        private void MapProfile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void MapProfile_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            GlobalState.OpenHelp();
        }
    }

}
