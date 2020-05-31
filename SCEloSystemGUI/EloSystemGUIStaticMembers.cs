using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystemExtensions;
using System.Linq;
using System.Collections.Generic;
using BrightIdeasSoftware;
using CustomControls;
using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    internal enum PerformanceTypes { Overall = 0, vs_Zerg, vs_Terran, vs_Protoss, vs_Random }

    internal static class EloSystemGUIStaticMembers
    {
        internal const string NUMBER_FORMAT = "#,#";

        internal const int GAMESPLAYED_DEFAULT_THRESHOLD = 8;
        internal const int GAMESPLAYED_DEFAULT_RACE_THRESHOLD = 3;
        internal const int RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD = 2;
        internal const int RECENTACTIVITY_GAMESPLAYED_DEFAULT_RACE_THRESHOLD = 1;
        internal const int RECENTACTIVITY_MONTHS_DEFAULT = 12;

        internal static Color OlvRowBackColor = Color.FromArgb(175, 175, 235);
        internal static Color OlvRowAlternativeBackColor = Color.FromArgb(210, 210, 210);
        private static Color ColorSchemeFrontColor1 = Color.FromArgb(115, 115, 170);
        private static Color ColorSchemeFrontColor2 = Color.FromArgb(225, 170, 0);
        private static Color ColorSchemeBackColor1 = Color.FromArgb(235, 250, 245);
        private static Color ColorSchemeBackColor2 = Color.FromArgb(149, 179, 215);
        internal static Color DrawColor = Color.FromArgb(0, 0, 224);
        internal static Color WinColor = Color.ForestGreen;
        internal static Color LoseColor = Color.FromArgb(217, 0, 0);

        internal static DialogResult GetEloSystemName(ref string fileName)
        {
            while (true)
            {
                if (Interaction.InputBox("New Elo System", "Name your Elo System", ref fileName) == DialogResult.OK)
                {
                    if (fileName == string.Empty) { MessageBox.Show("Failed to create new Elo System because name can not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else if (EloSystemGUIStaticMembers.IsInvalidFilename(fileName))
                    {
                        MessageBox.Show("Failed to create new Elo System name contained some invalid characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else { return DialogResult.OK; }

                }
                else { return DialogResult.Cancel; }

            }
        }

        internal static ObjectListView CreateMatchListView()
        {
            var matchLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Font = new Font("Calibri", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = false,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = 22,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(784, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 90, Text = "Date" };
            var olvClmPlayer1 = new OLVColumn() { Width = 130, Text = "Player 1" };
            var olvClmRatingChangePlayer1 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmResult = new OLVColumn() { Width = 70, Text = "Result" };
            var olvClmRatingChangePlayer2 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmPlayer2 = new OLVColumn() { Width = 130, Text = "Player 2" };
            var olvClmTournament = new OLVColumn() { Width = 130, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 110, Text = "Season" };

            matchLV.FormatCell += EloSystemGUIStaticMembers.MatchLV_FormatCell;

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2 })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmPlayer1.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player1.Name; }
                else { return ""; }
            };

            olvClmDate.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.DateValue.ToShortDateString(); }
                else { return string.Empty; }
            };

            olvClmRatingChangePlayer1.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return Styles.StringStyles.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player1)); }
                else { return ""; }
            };

            olvClmResult.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return String.Format("{0} - {1}", match.WinsBy(PlayerSlotType.Player1), match.WinsBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmRatingChangePlayer2.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return Styles.StringStyles.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmPlayer2.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player2.Name; }
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

        private static bool IsInvalidFilename(string fileName)
        {
            Regex checkForInvalidChararacters = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");

            return checkForInvalidChararacters.IsMatch(fileName);
        }

        internal static void FormatRatingChangeOLVCell(OLVListSubItem subItem)
        {
            int cellValue;

            if (int.TryParse(subItem.Text, out cellValue))
            {
                if (cellValue < 0) { subItem.ForeColor = EloSystemGUIStaticMembers.LoseColor; }
                else if (cellValue > 0) { subItem.ForeColor = EloSystemGUIStaticMembers.WinColor; }
                else if (cellValue == 0) { subItem.ForeColor = EloSystemGUIStaticMembers.DrawColor; }
                else { subItem.ForeColor = SystemColors.ControlText; }
            }
        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5) { EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem); }
        }

        internal static RowBorderDecoration OlvListViewRowBorderDecoration()
        {
            return new RowBorderDecoration()
            {
                BorderPen = new Pen(EloSystemGUIStaticMembers.ColorSchemeFrontColor2, 2F),
                BoundsPadding = new Size(1, 1),
                CornerRounding = 5F,
                FillBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0))
            };
        }

        internal static ObjectListView CreatePlayerPerformanceListView(SCPlayer player)
        {
            const int ROW_HEIGHT = 20;
            const string INFORMATION_NA = "-";

            List<PlayerStatsCloneDev> ratingDevelopment = GlobalState.DataBase.PlayerStatsDevelopment(player).OrderBy(item => item.Date.Date).ToList();

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
                Size = new Size(540, 130),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmPerformanceType = new OLVColumn() { Width = 80, Text = "" };
            var olvClmOwnRace = new OLVColumn() { HeaderTextAlign = HorizontalAlignment.Center, TextAlign = HorizontalAlignment.Center, Width = 80, Text = "Own Race" };
            var olvClmRank = new OLVColumn() { Width = 30, Text = "Rank" };
            var olvClmRatingCurrent = new OLVColumn() { Width = 60, Text = "Current rating" };
            var olvClmRatingPeak = new OLVColumn() { Width = 110, Text = "Peak rating" };
            var olvClmWinPercentage = new OLVColumn() { Width = 55, Text = "Win %" };
            var olvClmWinFrequency = new OLVColumn() { Width = 120, Text = "Win frequency" };

            performanceLV.FormatCell += EloSystemGUIStaticMembers.PlayerPerformanceLV_FormatCell;

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

                if (player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Overall: return player.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Zerg: return player.RatingVs.Zerg.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Terran: return player.RatingVs.Terran.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Protoss: return player.RatingVs.Protoss.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        case PerformanceTypes.vs_Random: return player.RatingVs.Random.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                    }
                }
                else { return INFORMATION_NA; }
            };

            const int RACE_IMAGE_HEIGHT_MAX = ROW_HEIGHT;
            const double RACE_USAGE_PROPORTION_THRESHOLD = 0.07;

            olvClmOwnRace.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                Func<Race, Image[]> GetRaceIcon = rc =>
                {
                    Dictionary<Race, int> raceUsageFrequencies = player.RaceUsageFrequency(rc).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    double summedFrequencies = raceUsageFrequencies.Sum(kvp => kvp.Value);

                    if (summedFrequencies > 0)
                    {
                        return new Image[] { RaceIconProvider.GetMultipleRacesUsageIcon(raceUsageFrequencies.Where(kvp => kvp.Value / summedFrequencies
                                    >= RACE_USAGE_PROPORTION_THRESHOLD).OrderByDescending(kvp=>kvp.Value).Select(kvp => kvp.Key)).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                    }
                    else { return null; }
                };

                if (player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Overall: return new Image[] { RaceIconProvider.GetRaceBitmap(player.GetPrimaryRace()).ResizeSARWithinBounds(olvClmOwnRace.Width, RACE_IMAGE_HEIGHT_MAX) };
                        case PerformanceTypes.vs_Zerg: return GetRaceIcon(Race.Zerg);
                        case PerformanceTypes.vs_Terran: return GetRaceIcon(Race.Terran);
                        case PerformanceTypes.vs_Protoss: return GetRaceIcon(Race.Protoss);
                        case PerformanceTypes.vs_Random: return GetRaceIcon(Race.Random);
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
                    case PerformanceTypes.Overall: return new Image[] { GlobalState.RankSystem.GetRankImageMain(player, imageHeight, true) };
                    case PerformanceTypes.vs_Zerg: return new Image[] { GlobalState.RankSystem.GetRankImageVsRace(player, imageHeight, true, Race.Zerg) };
                    case PerformanceTypes.vs_Terran: return new Image[] { GlobalState.RankSystem.GetRankImageVsRace(player, imageHeight, true, Race.Terran) };
                    case PerformanceTypes.vs_Protoss: return new Image[] { GlobalState.RankSystem.GetRankImageVsRace(player, imageHeight, true, Race.Protoss) };
                    case PerformanceTypes.vs_Random: return null;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PerformanceTypes).Name, pType.ToString()));
                }
            };


            olvClmRatingPeak.AspectGetter = obj =>
            {
                PerformanceTypes pType = (PerformanceTypes)obj;

                PlayerStatsCloneDev max;

                if (ratingDevelopment.Any())
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Overall:
                            max = ratingDevelopment.GetMaxRangeBy(item => item.RatingTotal()).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Zerg:
                            max = ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Zerg).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Zerg.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Terran:
                            max = ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Terran).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Terran.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Protoss:
                            max = ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Protoss).Last();

                            return String.Format("{0}   {1}. {2}", max.RatingVs.Protoss.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT), max.Date.ToString("y").Substring(0, 3)
                                , max.Date.ToString("y").Substring(max.Date.ToString("y").Length - 4, 4));

                        case PerformanceTypes.vs_Random:
                            max = ratingDevelopment.GetMaxRangeBy(item => item.RatingVs.Random).Last();

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

                if (player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Overall: return String.Format("{0}%", (100 * player.Stats.WinRatioTotal()).RoundToInt());
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

                if (player.Stats.GamesTotal() > 0)
                {
                    switch (pType)
                    {
                        case PerformanceTypes.Overall: return String.Format("{0}/{1}", player.Stats.WinsTotal(), player.Stats.GamesTotal());
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

            performanceLV.SetObjects(Enum.GetValues(typeof(PerformanceTypes)));

            return performanceLV;
        }

        private static void PlayerPerformanceLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex >= 2) { e.SubItem.Font = new Font(e.SubItem.Font.FontFamily, e.SubItem.Font.Size, FontStyle.Bold, e.SubItem.Font.Unit, e.SubItem.Font.GdiCharSet); }
        }


    }
}

