using CustomControls.Styles;
using System.Collections.Generic;
using BrightIdeasSoftware;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using EloSystemExtensions;
using SCEloSystemGUI.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SCEloSystemGUI.UserControls;

namespace SCEloSystemGUI
{
    public partial class MapStatsDisplay : Form
    {
        private const float TEXT_SIZE = 11.5F;

        private ObjectListView olvMapStats;
        private Dictionary<Map, Bitmap> imagesByMaps = new Dictionary<Map, Bitmap>();

        private MapStatsDisplay()
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.olvMapStats = this.CreateMapStatsListView();
            this.olvMapStats.SelectionChanged += this.OlvMapStats_SelectionChanged;

            this.Controls.Add(this.olvMapStats);

            this.olvMapStats.SetObjects(GlobalState.DataBase.GetMaps().OrderBy(m => m.Name).ToArray());

        }

        private void OlvMapStats_SelectionChanged(object sender, EventArgs e)
        {
            if (this.olvMapStats.SelectedItem == null) { return; }

            var selectedMap = this.olvMapStats.SelectedItem.RowObject as Map;

            if (selectedMap != null)
            {
                MapProfile.ShowProfile(selectedMap, this);

                this.olvMapStats.SelectedItems.Clear();
            }
        }

        public static void ShowMapList(Form anchorForm = null)
        {
            var mapListForm = new MapStatsDisplay();

            if (anchorForm != null) { FormStyles.ShowFullFormRelativeToAnchor(mapListForm, anchorForm); }

            mapListForm.ShowDialog();

            foreach (KeyValuePair<Map, Bitmap> kvPair in mapListForm.imagesByMaps.ToList()) { kvPair.Value.Dispose(); }

            mapListForm.Dispose();
        }

        private void MapStatsLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            const int NAME_COLUMN = 2;
            const int SPOTS_COLUMN = 3;
            const int TVZ_COLUMN = 4;
            const int ZVP_COLUMN = 5;
            const int PVT_COLUMN = 6;
            const int TOT_COLUMN = 7;

            this.olvMapStats.SecondarySortColumn = this.olvMapStats.PrimarySortColumn;
            this.olvMapStats.SortGroupItemsByPrimaryColumn = true;

            if (e.Column == NAME_COLUMN || e.Column == SPOTS_COLUMN || e.Column == TOT_COLUMN)
            {
                this.olvMapStats.PrimarySortColumn = this.olvMapStats.AllColumns[e.Column];
                this.olvMapStats.Sort();
            }
            else
            {
                if (e.Column == TVZ_COLUMN)
                {
                    this.olvMapStats.AllColumns[0].AspectGetter = obj =>
                    {
                        var map = obj as Map;

                        return (map.Stats.ZvT.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt();
                    };

                }
                else if (e.Column == ZVP_COLUMN)
                {
                    this.olvMapStats.AllColumns[0].AspectGetter = obj =>
                    {
                        var map = obj as Map;

                        return (map.Stats.PvZ.WinRatioRace2CorrectedForExpectedWR() * 100).RoundToInt();
                    };

                }
                else if (e.Column == PVT_COLUMN)
                {
                    this.olvMapStats.AllColumns[0].AspectGetter = obj =>
                    {
                        var map = obj as Map;

                        return (map.Stats.PvT.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt();

                    };
                }

                this.olvMapStats.PrimarySortColumn = this.olvMapStats.AllColumns[e.Column];

                this.olvMapStats.Sorting = this.olvMapStats.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                this.olvMapStats.Sort(this.olvMapStats.AllColumns[0], this.olvMapStats.Sorting);
            }
        }

        private ObjectListView CreateMapStatsListView()
        {
            var mapStatsLV = new ObjectListView()
            {
                AllowColumnReorder = false,
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", MapStatsDisplay.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Clickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(12, 16, 12, 12),
                MultiSelect = false,
                PrimarySortOrder = SortOrder.Ascending,
                RowHeight = 42,
                Scrollable = true,
                ShowFilterMenuOnRightClick = false,
                ShowGroups = false,                
                Size = new Size(1200, 700),
                SortGroupItemsByPrimaryColumn = true,
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
                UseHotItem = true,
            };

            mapStatsLV.ColumnClick += this.MapStatsLV_ColumnClick;

            Styles.ObjectListViewStyles.SetHotItemStyle(mapStatsLV);
            Styles.ObjectListViewStyles.AvoidFocus(mapStatsLV);

            const int WINRATIOS_WIDTH = 170;
            const int GAMES_WIDTH = 55;
            const int COINTOSS_FACTOR_WIDTH = 55;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null, Sortable = true, IsVisible = false };
            var olvClmImage = new OLVColumn() { Width = 54, Text = "Map", Sortable = false };
            var olvClmName = new OLVColumn() { Width = 155, Text = "Name", Sortable = true };
            var olvClmSpots = new OLVColumn() { Width = 55, Text = "Spots", Sortable = true };
            var olvClmTvZ = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "TvZ", Sortable = true };
            var olvClmZvP = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "ZvP", Sortable = true };
            var olvClmPvT = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "PvT", Sortable = true };
            var olvClmTotGames = new OLVColumn() { Width = 75, Text = "Total games", Sortable = true };
            var olvClmZvZGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "ZvZ", Sortable = false };
            var olvClmTvTGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "TvT", Sortable = false };
            var olvClmPvPGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "PvP", Sortable = false };
            var olvClmZvZCointossFactor = new OLVColumn() { Width = COINTOSS_FACTOR_WIDTH, Text = "ZvZ CTF", Sortable = true, ToolTipText = "ZvZ Cointoss Factor" };
            var olvClmTvTCointossFactor = new OLVColumn() { Width = COINTOSS_FACTOR_WIDTH, Text = "TvT CTF", Sortable = true, ToolTipText = "TvT Cointoss Factor" };
            var olvClmPvPCointossFactor = new OLVColumn() { Width = COINTOSS_FACTOR_WIDTH, Text = "PvP CTF", Sortable = true, ToolTipText = "PvP Cointoss Factor" };

            mapStatsLV.PrimarySortColumn = olvClmName;

            mapStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmImage, olvClmName, olvClmSpots, olvClmTvZ, olvClmZvP, olvClmPvT, olvClmTotGames, olvClmZvZGames, olvClmTvTGames, olvClmPvPGames
                , olvClmZvZCointossFactor, olvClmTvTCointossFactor, olvClmPvPCointossFactor });

            mapStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmImage, olvClmName, olvClmSpots, olvClmTvZ, olvClmZvP, olvClmPvT, olvClmTotGames, olvClmZvZGames, olvClmTvTGames, olvClmPvPGames
                , olvClmZvZCointossFactor, olvClmTvTCointossFactor, olvClmPvPCointossFactor });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmImage, olvClmSpots })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            foreach (OLVColumn clm in new OLVColumn[] { olvClmTvZ, olvClmZvP, olvClmPvT }) { clm.HeaderTextAlign = HorizontalAlignment.Center; }

            foreach (OLVColumn clm in new OLVColumn[] { olvClmTotGames, olvClmTvTGames, olvClmZvZGames, olvClmPvPGames, olvClmZvZCointossFactor, olvClmTvTCointossFactor, olvClmPvPCointossFactor })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            const int IMAGE_SIZE_MAX = 42;

            olvClmImage.AspectGetter = obj =>
            {
                var map = obj as Map;

                Bitmap mapImage;
                EloImage eloMapImage;

                if (this.imagesByMaps.TryGetValue(map, out mapImage)) { return new Image[] { mapImage }; }
                else if (GlobalState.DataBase.TryGetImage(map.ImageID, out eloMapImage))
                {
                    this.imagesByMaps.Add(map, eloMapImage.Image.ResizeSameAspectRatio(IMAGE_SIZE_MAX));

                    return new Image[] { this.imagesByMaps[map] };
                }
                else { return null; }

            };

            var mapImageRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 3, 4, 4) };
            olvClmImage.Renderer = mapImageRenderer;

            olvClmName.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Name;
            };

            olvClmSpots.AspectGetter = obj =>
            {
                var map = obj as Map;

                return ((int)map.MapType).ToString();
            };

            olvClmTvZ.AspectGetter = obj =>
            {
                var map = obj as Map;

                if (map.Stats.ZvT.TotalGames > 0)
                {
                    return String.Format("{0}% | {1} - {2}", (map.Stats.ZvT.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt(), map.Stats.ZvT.Race1Wins, map.Stats.ZvT.Race2Wins);
                }
                else { return "-"; }

            };

            olvClmZvP.AspectGetter = obj =>
            {
                var map = obj as Map;

                if (map.Stats.PvZ.TotalGames > 0)
                {
                    return String.Format("{0}% | {1} - {2}", (map.Stats.PvZ.WinRatioRace2CorrectedForExpectedWR() * 100).RoundToInt(), map.Stats.PvZ.Race2Wins, map.Stats.PvZ.Race1Wins);
                }
                else { return "-"; }

            };

            olvClmPvT.AspectGetter = obj =>
            {
                var map = obj as Map;

                if (map.Stats.PvT.TotalGames > 0)
                {
                    return String.Format("{0}% | {1} - {2}", (map.Stats.PvT.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt(), map.Stats.PvT.Race1Wins, map.Stats.PvT.Race2Wins);
                }
                else { return "-"; }
            };


            olvClmTotGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.TotalGames();
            };

            olvClmTotGames.AspectToStringConverter = asp => { return ((int)asp).ToString(Styles.NUMBER_FORMAT); };

            olvClmTvTGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.TvT.TotalGames.ToString(Styles.NUMBER_FORMAT);
            };

            olvClmZvZGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.ZvZ.TotalGames.ToString(Styles.NUMBER_FORMAT);
            };

            olvClmPvPGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.PvP.TotalGames.ToString(Styles.NUMBER_FORMAT);
            };

            const int COINTOSS_FACTOR_DECIMALS = 2;

            olvClmZvZCointossFactor.AspectGetter = obj =>
            {
                var map = obj as Map;

                double coinTossFactor = 0;

                return GlobalState.MirrorMatchupEvaluation.TryGetCointossFactor(map, MirrorMatchup.ZvZ, out coinTossFactor) ? coinTossFactor.Round(COINTOSS_FACTOR_DECIMALS).ToString() : "";

            };

            olvClmTvTCointossFactor.AspectGetter = obj =>
            {
                var map = obj as Map;

                double coinTossFactor = 0;

                return GlobalState.MirrorMatchupEvaluation.TryGetCointossFactor(map, MirrorMatchup.TvT, out coinTossFactor) ? coinTossFactor.Round(COINTOSS_FACTOR_DECIMALS).ToString() : "";

            };

            olvClmPvPCointossFactor.AspectGetter = obj =>
            {
                var map = obj as Map;

                double coinTossFactor = 0;

                return GlobalState.MirrorMatchupEvaluation.TryGetCointossFactor(map, MirrorMatchup.PvP, out coinTossFactor) ? coinTossFactor.Round(COINTOSS_FACTOR_DECIMALS).ToString() : "";

            };

            return mapStatsLV;
        }

        private void MapStatsDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
