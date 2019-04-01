using System.Linq;
using CustomExtensionMethods;
using BrightIdeasSoftware;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Drawing;
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

        private ObjectListView CreateMapStatsListView()
        {
            var mapStatsLV = new ObjectListView()
            {
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", MapStatsDisplay.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(12, 16, 12, 12),
                MultiSelect = false,
                RowHeight = 42,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(999, 700),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            const int WINRATIOS_WIDTH = 170;
            const int GAMES_WIDTH = 55;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmImage = new OLVColumn() { Width = 54, Text = "Map" };
            var olvClmName = new OLVColumn() { Width = 125, Text = "Name" };
            var olvClmSpots = new OLVColumn() { Width = 55, Text = "Spots" };
            var olvClmTvZ = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "TvZ" };
            var olvClmZvP = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "ZvP" };
            var olvClmPvT = new OLVColumn() { Width = WINRATIOS_WIDTH, Text = "PvT" };
            var olvClmTotGames = new OLVColumn() { Width = 75, Text = "Total games" };
            var olvClmTvTGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "TvT" };
            var olvClmZvZGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "ZvZ" };
            var olvClmPvPGames = new OLVColumn() { Width = GAMES_WIDTH, Text = "PvP" };

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

                EloImage flag;

                if (this.eloDataBase().TryGetImage(map.ImageID, out flag)) { return new Image[] { flag.Image.ResizeSameAspectRatio(IMAGE_SIZE_MAX) }; }
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

                return map.Stats.TotalGames().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            };

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

    }
}
