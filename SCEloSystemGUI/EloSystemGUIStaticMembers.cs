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
                AlternateRowBackColor = Color.FromArgb(217, 217, 217),
                Font = new Font("Calibri", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
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

                if (match != null) { return EloGUIControlsStaticMembers.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player1)); }
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

                if (match != null) { return EloGUIControlsStaticMembers.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player2)); }
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

    }
}

