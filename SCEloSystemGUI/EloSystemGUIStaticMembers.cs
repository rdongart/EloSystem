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
                Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(6),
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

            matchLV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            matchLV.Size = new Size(784, 850);
            matchLV.HasCollapsibleGroups = false;
            matchLV.ShowGroups = false;
            matchLV.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            matchLV.RowHeight = 22;
            matchLV.UseCellFormatEvents = true;
            matchLV.FormatCell += EloSystemGUIStaticMembers.MatchLV_FormatCell;
            matchLV.Scrollable = true;
            matchLV.Margin = new Padding(3);
            matchLV.AlternateRowBackColor = Color.FromArgb(217, 217, 217);
            matchLV.UseAlternatingBackColors = true; matchLV.MultiSelect = false;

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
                if (cellValue < 0) { subItem.ForeColor = Color.Red; }
                else if (cellValue > 0) { subItem.ForeColor = Color.ForestGreen; }
                else { subItem.ForeColor = SystemColors.ControlText; }
            }
        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5) { EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem); }
        }

    }
}
