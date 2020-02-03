using System.Collections.Generic;
using CustomExtensionMethods.Drawing;
using BrightIdeasSoftware;
using CustomControls;
using EloSystem;
using EloSystem.ResourceManagement;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal static class EloGUIControlsStaticMembers
    {
        internal const string DEFAULT_TXTBX_TEXT = "Type the name here...";

        private static string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

        internal static ImprovedImageComboBox<T> CreateStandardImprovedImageComboBox<T>(ImageGetter<T> imageGetter) where T : class, IHasName
        {
            return new ImprovedImageComboBox<T>()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 160,
                Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(4, 2, 4, 2),
                ItemHeight = 20,
                Margin = new Padding(6, 3, 6, 3),
                SelectedItemFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(154, 26),
                ImageGetter = imageGetter,
                NameGetter = t => t.Name
            };
        }

        internal static ObjectListView CreatePlayerSearchListView(ResourceGetter eloDataBase, RankHandler rankHandler)
        {
            const int ROW_HEIGHT = 26;
            const float TEXT_SIZE = 11.5F;

            var playerStatsLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", TEXT_SIZE, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(4),
                Cursor = Cursors.Hand,
                MultiSelect = false,
                RowHeight = ROW_HEIGHT,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(825, 900),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
                UseHotItem = true,
                FullRowSelect = true
            };

            playerStatsLV.MouseMove += EloGUIControlsStaticMembers.PlayerStatsLV_MouseMove;

            playerStatsLV.HotItemStyle = new HotItemStyle();
            playerStatsLV.HotItemStyle.Decoration = EloSystemGUIStaticMembers.OlvListViewRowBorderDecoration();

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmRank = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmName = new OLVColumn() { Width = 140, Text = "Name" };
            var olvClmCountry = new OLVColumn() { Width = 65, Text = "Country" };
            var olvClmTeam = new OLVColumn() { Width = 60, Text = "Team" };
            var olvClmMainRace = new OLVColumn() { Width = 60, Text = "Race" };
            var olvClmRankMain = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmRatingMain = new OLVColumn() { Width = 70, Text = "Rating - main" };
            var olvClmAliases = new OLVColumn() { Width = 170, Text = "Aliases" };
            var olvClmIRLName = new OLVColumn() { Width = 155, Text = "IRL name" };

            playerStatsLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmTeam, olvClmMainRace, olvClmRankMain, olvClmRatingMain, olvClmAliases
                , olvClmIRLName });

            playerStatsLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmRank, olvClmName, olvClmCountry, olvClmTeam, olvClmMainRace, olvClmRankMain, olvClmRatingMain, olvClmAliases
                , olvClmIRLName });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmCountry, olvClmTeam, olvClmMainRace, olvClmRankMain, olvClmRatingMain })
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

            const int STANDARD_IMAGE_SIZE_MAX = 28;

            olvClmCountry.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage flag;

                if (player.Country != null && eloDataBase().TryGetImage(player.Country.ImageID, out flag)) { return new Image[] { flag.Image.ResizeSameAspectRatio(STANDARD_IMAGE_SIZE_MAX) }; }
                else if (player.Country != null) { return player.Country.Name; }
                else { return null; }

            };

            const int TEAM_LOGO_SIZE_MAX = 23;

            olvClmTeam.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage teamLogo;

                if (player.Team != null && eloDataBase().TryGetImage(player.Team.ImageID, out teamLogo)) { return new Image[] { teamLogo.Image.ResizeSameAspectRatio(TEAM_LOGO_SIZE_MAX) }; }
                else { return player.TeamName; }

            };

            var imgRenderer = new ImageRenderer() { Bounds = new Rectangle(4, 2, 4, 4) };
            olvClmCountry.Renderer = imgRenderer;
            olvClmTeam.Renderer = imgRenderer;

            const int RACE_IMAGE_HEIGHT_MAX = ROW_HEIGHT;
            const int RACE_IMAGE_WIDTH_MAX = 60;

            olvClmMainRace.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                Image raceImg = RaceIconProvider.GetRaceUsageIcon(player);

                if (player.Stats.GamesTotal() > 0) { return new Image[] { RaceIconProvider.GetRaceUsageIcon(player).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX) }; }
                else { return null; }

            };

            var mainRaceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 2, 6, 6) };
            olvClmMainRace.Renderer = mainRaceRenderer;


            const int TOP_BOTTOM_MARGIN = 1;
            var mainRankRenderer = new ImageRenderer() { Bounds = new Rectangle(6, TOP_BOTTOM_MARGIN, 6, TOP_BOTTOM_MARGIN) };
            olvClmRankMain.Renderer = mainRaceRenderer;

            olvClmRankMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return new Image[] { rankHandler.GetRankImageMain(player, ROW_HEIGHT - TOP_BOTTOM_MARGIN * 2, true) };
            };

            olvClmRatingMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingTotal().ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT);
            };

            olvClmAliases.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return string.Join(", ", player.GetAliases());
            };

            olvClmIRLName.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.IRLName;
            };

            return playerStatsLV;
        }

        internal static string ConvertRatingChangeString(string ratingChangeTxt)
        {
            int ratingChangeValue = 0;

            bool hasRatingValue = int.TryParse(ratingChangeTxt, out ratingChangeValue);

            return String.Format("{0}{1}", ratingChangeValue > 0 ? "+" : "", hasRatingValue ? ratingChangeValue.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT) : ratingChangeTxt);
        }

        internal static void PlayerStatsLV_MouseMove(object sender, MouseEventArgs e)
        {
            var senderLstv = sender as ListView;

            if (senderLstv != null) { senderLstv.Cursor = Cursors.Hand; }
        }

        internal static void PopulateComboboxWithMaps(ComboBox cmBx, IEnumerable<Map> maps)
        {
            List<Map> mapList = maps.OrderBy(map => map.Name).ToList();

            var selectedItem = cmBx.SelectedItem != null ? (cmBx.SelectedItem as Tuple<string, Map>).Item2 : null;

            cmBx.DisplayMember = "Item1";
            cmBx.ValueMember = "Item2";

            cmBx.Items.Clear();
            cmBx.Items.Add(Tuple.Create<string, Map>("<any>", null));
            cmBx.Items.AddRange(mapList.Select(map => Tuple.Create<string, Map>(map.Name, map)).ToArray());

            const int MAP_ITEM_NONE = 1;

            if (selectedItem != null && mapList.Contains(selectedItem)) { cmBx.SelectedIndex = mapList.IndexOf(selectedItem) + MAP_ITEM_NONE; }
        }

        [STAThread]
        internal static bool TryGetFilePathFromUser(out string filePath)
        {
            filePath = "";

            while (true)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select image";
                openFileDialog.InitialDirectory = EloGUIControlsStaticMembers.initialDirectory;
                openFileDialog.Filter = "Image files|*.gif;*.bmp;*.jpg;*.jpeg;*.png*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.Multiselect = false;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    EloGUIControlsStaticMembers.initialDirectory = Path.GetDirectoryName(filePath);

                    return true;
                }
                else { return false; }

            }
        }
    }
}
