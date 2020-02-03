using BrightIdeasSoftware;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MapStatsDisplay : Form
    {
        private const float TEXT_SIZE = 11.5F;

        private ObjectListView olvMapStats;
        private ResourceGetter eloDataBase;

        public MapStatsDisplay(ResourceGetter dataGetter)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.eloDataBase = dataGetter;

            this.olvMapStats = this.CreateMapStatsListView();

            this.Controls.Add(this.olvMapStats);

            this.olvMapStats.SetObjects(this.eloDataBase().GetMaps().OrderBy(m => m.Name).ToArray());
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

                    this.olvMapStats.PrimarySortColumn = this.olvMapStats.AllColumns[e.Column];
                }
                else if (e.Column == ZVP_COLUMN)
                {
                    this.olvMapStats.AllColumns[0].AspectGetter = obj =>
                    {
                        var map = obj as Map;

                        return (map.Stats.PvZ.WinRatioRace2CorrectedForExpectedWR() * 100).RoundToInt();
                    };

                    this.olvMapStats.PrimarySortColumn = this.olvMapStats.AllColumns[e.Column];
                }
                else if (e.Column == PVT_COLUMN)
                {
                    this.olvMapStats.AllColumns[0].AspectGetter = obj =>
                    {
                        var map = obj as Map;

                        return (map.Stats.PvT.WinRatioRace1CorrectedForExpectedWR() * 100).RoundToInt();

                    };

                    this.olvMapStats.PrimarySortColumn = this.olvMapStats.AllColumns[e.Column];
                }

                this.olvMapStats.Sorting = this.olvMapStats.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                this.olvMapStats.Sort(this.olvMapStats.AllColumns[0], this.olvMapStats.Sorting);
            }
        }

        private ObjectListView CreateMapStatsListView()
        {
            var mapStatsLV = new ObjectListView()
            {
                AllowColumnReorder = false,
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", MapStatsDisplay.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Clickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(12, 16, 12, 12),
                MultiSelect = false,
                RowHeight = 42,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(1029, 700),
                SortGroupItemsByPrimaryColumn = true,
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            mapStatsLV.ColumnClick += this.MapStatsLV_ColumnClick;

            const int WINRATIOS_WIDTH = 170;
            const int GAMES_WIDTH = 55;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null, Sortable = true };
            var olvClmImage = new OLVColumn() { Width = 54, Text = "Map", Sortable = false };
            var olvClmName = new OLVColumn() { Width = 155, Text = "Name", Sortable = true };
            var olvClmSpots = new OLVColumn() { Width = 55, Text = "Spots", Sortable = true };
            var olvClmTvZ = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "TvZ", Sortable = true };
            var olvClmZvP = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "ZvP", Sortable = true };
            var olvClmPvT = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "PvT", Sortable = true };
            var olvClmTotGames = new OLVColumn() { Width = 75, Text = "Total games", Sortable = true };
            var olvClmTvTGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "TvT", Sortable = false };
            var olvClmZvZGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "ZvZ", Sortable = false };
            var olvClmPvPGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "PvP", Sortable = false };

            mapStatsLV.PrimarySortColumn = olvClmName;

            mapStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty,olvClmImage,  olvClmName, olvClmSpots, olvClmTvZ, olvClmZvP, olvClmPvT, olvClmTotGames
                , olvClmTvTGames, olvClmZvZGames, olvClmPvPGames });

            mapStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmImage, olvClmName, olvClmSpots, olvClmTvZ, olvClmZvP, olvClmPvT, olvClmTotGames
                , olvClmTvTGames, olvClmZvZGames, olvClmPvPGames });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmImage, olvClmSpots })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            foreach (OLVColumn clm in new OLVColumn[] { olvClmTvZ, olvClmZvP, olvClmPvT })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
            }

            foreach (OLVColumn clm in new OLVColumn[] { olvClmTotGames, olvClmTvTGames, olvClmZvZGames, olvClmPvPGames })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Right;
            }

            const int IMAGE_SIZE_MAX = 42;

            olvClmImage.AspectGetter = obj =>
            {
                var map = obj as Map;

                EloImage mapImage;

                if (this.eloDataBase().TryGetImage(map.ImageID, out mapImage)) { return new Image[] { mapImage.Image.ResizeSameAspectRatio(IMAGE_SIZE_MAX) }; }
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

            olvClmTotGames.AspectToStringConverter = asp => { return ((int)asp).ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT); };

            olvClmTvTGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.TvT.TotalGames.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            };

            olvClmZvZGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.ZvZ.TotalGames.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            };

            olvClmPvPGames.AspectGetter = obj =>
            {
                var map = obj as Map;

                return map.Stats.PvP.TotalGames.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            };

            return mapStatsLV;
        }

        private void MapStatsDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
