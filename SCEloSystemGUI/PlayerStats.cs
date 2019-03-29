using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using SCEloSystemGUI.Properties;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.UserControls;
using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomControls;

namespace SCEloSystemGUI
{
    public enum PlayerDevScope
    {
        Month_1, Months_3, Months_4, Months_6
    }

    public partial class PlayerStats : Form
    {
        private const float TEXT_SIZE = 11.5F;

        private ObjectListView olvPlayerStats;
        private ResourceGetter eloDataBase;

        public PlayerStats(ResourceGetter databaseGetter)
        {
            InitializeComponent();

            this.eloDataBase = databaseGetter;

            this.olvPlayerStats = this.CreatePlayerStatsListView();

            this.tblLoPnlPlayerStats.Controls.Add(this.olvPlayerStats, 0, 1);

            this.olvPlayerStats.AddObjects(this.eloDataBase().GetPlayers().OrderByDescending(player => player.RatingTotal()).ThenByDescending(player => player.Stats.GamesTotal()).ThenByDescending(player => player.Stats.WinRatioTotal()).Select((player, rank) => new Tuple<SCPlayer, int>(player, rank + 1)).ToArray());
        }

        private ObjectListView CreatePlayerStatsListView()
        {
            var playerStatsLV = new ObjectListView()
            {
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(4),
                MultiSelect = false,
                RowHeight = 26,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(725, 900),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            playerStatsLV.FormatCell += PlayerStats.PlayerStats_FormatCell;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmRank = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmName = new OLVColumn() { Width = 130, Text = "Name" };
            var olvClmCountry = new OLVColumn() { Width = 60, Text = "Country" };
            var olvClmMainRace = new OLVColumn() { Width = 50, Text = "Race" };
            var olvClmRatingMain = new OLVColumn() { Width = 70, Text = "Rating - main" };
            var olvClmRatingDevelopment = new OLVColumn() { Width = 70, Text = "Development" };
            var olvClmRatingVsZerg = new OLVColumn() { Width = 70, Text = "Vs Zerg" };
            var olvClmRatingVsTerran = new OLVColumn() { Width = 70, Text = "Vs Terran" };
            var olvClmRatingVsProtoss = new OLVColumn() { Width = 70, Text = "Vs Protoss" };
            var olvClmRatingVsRandom = new OLVColumn() { Width = 70, Text = "Vs Random" };

            playerStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmMainRace, olvClmRatingMain, olvClmRatingDevelopment, olvClmRatingVsZerg
                , olvClmRatingVsTerran, olvClmRatingVsProtoss, olvClmRatingVsRandom });

            playerStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmMainRace, olvClmRatingMain, olvClmRatingDevelopment, olvClmRatingVsZerg
                , olvClmRatingVsTerran, olvClmRatingVsProtoss, olvClmRatingVsRandom });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmCountry, olvClmMainRace, olvClmRatingMain, olvClmRatingDevelopment, olvClmRatingVsZerg, olvClmRatingVsTerran, olvClmRatingVsProtoss
                , olvClmRatingVsRandom })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmRank.AspectGetter = obj =>
            {
                var rank = (obj as Tuple<SCPlayer, int>).Item2;

                return String.Format("{0}.", rank.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT));
            };

            olvClmName.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.Name;
            };

            const int IMAGE_SIZE_MAX = 28;

            olvClmCountry.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage flag;

                if (player.Country != null && this.eloDataBase().TryGetImage(player.Country.ImageID, out flag)) { return new Image[] { flag.Image.ResizeSameAspectRatio(IMAGE_SIZE_MAX) }; }
                else { return null; }

            };

            var flagRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4) };
            olvClmCountry.Renderer = flagRenderer;

            olvClmMainRace.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                if (player.Stats.GamesTotal() > 0)
                {
                    Race primaryRace = player.GetPrimaryRace();

                    switch (primaryRace)
                    {
                        case Race.Zerg: return new Image[] { Resources.Zicon.ResizeSameAspectRatio(IMAGE_SIZE_MAX) };
                        case Race.Terran: return new Image[] { Resources.Ticon.ResizeSameAspectRatio(IMAGE_SIZE_MAX) };
                        case Race.Protoss: return new Image[] { Resources.Picon.ResizeSameAspectRatio(IMAGE_SIZE_MAX) };
                        case Race.Random: return new Image[] { Resources.Ricon.ResizeSameAspectRatio(IMAGE_SIZE_MAX) };
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, primaryRace.ToString()));
                    }
                }
                else { return null; }

            };

            var mainRaceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 2, 6, 6) };
            olvClmMainRace.Renderer = mainRaceRenderer;

            olvClmRatingMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingTotal().ToString("#,#");
            };

            olvClmRatingDevelopment.AspectGetter = obj =>
              {
                  var player = (obj as Tuple<SCPlayer, int>).Item1;

                  switch (Settings.Default.PlayerDevelopmentScope)
                  {
                      case PlayerDevScope.Month_1:
                          return EloGUIControlsStaticMembers.ConvertRatingChangeString((player.RatingTotal()
                              - this.eloDataBase().PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-1)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_3:
                          return EloGUIControlsStaticMembers.ConvertRatingChangeString((player.RatingTotal()
                              - this.eloDataBase().PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-3)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_4:
                          return EloGUIControlsStaticMembers.ConvertRatingChangeString((player.RatingTotal()
                              - this.eloDataBase().PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-4)).RatingTotal()).ToString());
                      case PlayerDevScope.Months_6:
                          return EloGUIControlsStaticMembers.ConvertRatingChangeString((player.RatingTotal()
                              - this.eloDataBase().PlayerStatsAtPointInTime(player, DateTime.Today.AddMonths(-6)).RatingTotal()).ToString());
                      default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerDevScope).Name, Settings.Default.PlayerDevelopmentScope));
                  }
              };

            olvClmRatingVsZerg.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Zerg.ToString("#,#");
            };

            olvClmRatingVsTerran.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Terran.ToString("#,#");
            };

            olvClmRatingVsProtoss.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Protoss.ToString("#,#");
            };

            olvClmRatingVsRandom.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingVs.Random.ToString("#,#");
            };

            return playerStatsLV;
        }

        private static void PlayerStats_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 5) { e.SubItem.Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); }

            if (e.ColumnIndex == 6)
            {
                e.SubItem.Font = new Font("Calibri", PlayerStats.TEXT_SIZE, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem);
            }
        }
    }
}
