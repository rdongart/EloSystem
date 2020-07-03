using EloSystemExtensions;
using CustomExtensionMethods;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
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

        internal static Color RaceImageBackgroundColor { get { return Color.FromArgb(115, 115, 115); } }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPlayer">The player that the item players should show head-to-head data against.</param>
        /// <returns></returns>
        internal static ObjectListView CreateHeadToHeadSearchListView(SCPlayer targetPlayer)
        {
            var h2hLstV = EloGUIControlsStaticMembers.CreatePlayerSearchListView();

            var olvClmH2HGames = new OLVColumn() { Width = 60, Text = "Head-to-Head games", ToolTipText = "Head-to-Head games" };

            // the old rank column will be removed
            var newColumnOrder = h2hLstV.AllColumns.Take(1).Concat(new OLVColumn[] { olvClmH2HGames }).Concat(h2hLstV.AllColumns.Skip(2)).ToArray();

            h2hLstV.AllColumns.Clear();
            h2hLstV.Columns.Clear();
            h2hLstV.EmptyListMsg = String.Format("No players fit the search criteria and have played games vs {0}.", targetPlayer.Name);

            h2hLstV.AllColumns.AddRange(newColumnOrder);
            h2hLstV.Columns.AddRange(newColumnOrder);

            olvClmH2HGames.AspectGetter = obj =>
            {
                var opponent = (obj as Tuple<SCPlayer, int>).Item1;

                return GlobalState.DataBase.HeadToHeadGames(targetPlayer, opponent).Count();
            };

            olvClmH2HGames.AspectToStringConverter = obj => { return ((int)obj).ToString(Styles.NUMBER_FORMAT); };

            h2hLstV.Size = new Size(h2hLstV.Width + olvClmH2HGames.Width, h2hLstV.Height);

            h2hLstV.PrimarySortColumn = olvClmH2HGames;
            h2hLstV.PrimarySortOrder = SortOrder.Descending;
            h2hLstV.SecondarySortColumn = newColumnOrder[2];
            h2hLstV.SecondarySortOrder = SortOrder.Ascending;

            return h2hLstV;
        }

        internal static ObjectListView CreatePlayerSearchListView()
        {
            const int ROW_HEIGHT = 26;
            const float TEXT_SIZE = 11.5F;

            var playerStatsLV = new ObjectListView()
            {
                AllowColumnReorder = false,
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                EmptyListMsg = "No players fit the search criteria.",
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

            playerStatsLV.MouseMove += EloGUIControlsStaticMembers.ShowCurserHandOnMouseMove;

            playerStatsLV.HotItemStyle = new HotItemStyle();
            playerStatsLV.HotItemStyle.Decoration = EloSystemGUIStaticMembers.OlvListViewRowBorderDecoration();

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmRank = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmName = new OLVColumn() { Width = 140, Text = "Name" };
            var olvClmCountry = new OLVColumn() { Width = 65, Text = "Country" };
            var olvClmTeam = new OLVColumn() { Width = 60, Text = "Team" };
            var olvClmMainRace = new OLVColumn() { Width = 60, Text = "Race" };
            var olvClmRankMain = new OLVColumn() { Width = 50, Text = "Rank" };
            var olvClmRatingMain = new OLVColumn() { Width = 70, Text = "Rating - overall" };
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

                EloImage flag;

                if (player.Country != null && GlobalState.DataBase.TryGetImage(player.Country.ImageID, out flag)) { return new Image[] { flag.Image.ResizeSameAspectRatio(STANDARD_IMAGE_SIZE_MAX) }; }
                else if (player.Country != null) { return player.Country.Name; }
                else { return null; }

            };

            const int TEAM_LOGO_SIZE_MAX = 23;

            olvClmTeam.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                EloImage teamLogo;

                if (player.Team != null && GlobalState.DataBase.TryGetImage(player.Team.ImageID, out teamLogo)) { return new Image[] { teamLogo.Image.ResizeSameAspectRatio(TEAM_LOGO_SIZE_MAX) }; }
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

                return new Image[] { GlobalState.RankSystem.GetRankImageMain(player, ROW_HEIGHT - TOP_BOTTOM_MARGIN * 2, true) };
            };

            olvClmRatingMain.AspectGetter = obj =>
            {
                var player = (obj as Tuple<SCPlayer, int>).Item1;

                return player.RatingTotal().ToString(Styles.NUMBER_FORMAT);
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

        internal static void ShowCurserHandOnMouseMove(object sender, MouseEventArgs e)
        {
            var senderLstv = sender as ListView;

            if (senderLstv != null) { senderLstv.Cursor = Cursors.Hand; }
        }

        /// <summary>
        /// Adds a sequence of Tuple'string, Map' items to a ComboBox.
        /// </summary>
        /// <param name="cmBx"></param>
        /// <param name="maps"></param>
        /// <param name="matchCountPlayer">Enter a player to add a count of games played on the map to the map name.</param>
        /// <param name="matchCountOpponent">Enter a player to add a count of games played on the map to the map name.</param>
        internal static void PopulateComboboxWithMaps(ComboBox cmBx, IEnumerable<Map> maps, bool includeEmptyItem, SCPlayer matchCountPlayer = null, SCPlayer matchCountOpponent = null)
        {
            List<Map> mapList = maps.OrderBy(map => map.Name).ToList();

            var selectedItem = cmBx.SelectedItem != null ? (cmBx.SelectedItem as Tuple<string, Map>).Item2 : null;

            cmBx.DisplayMember = "Item1";
            cmBx.ValueMember = "Item2";

            bool includeMatchCount = matchCountPlayer != null || matchCountOpponent != null;

            Func<IEnumerable<Game>> GetGames = () =>
            {
                if (matchCountPlayer != null && matchCountOpponent != null) { return GlobalState.DataBase.HeadToHeadGames(matchCountPlayer, matchCountOpponent); }
                else if (matchCountPlayer != null) { return GlobalState.DataBase.GamesByPlayer(matchCountPlayer); }
                else if (matchCountOpponent != null) { return GlobalState.DataBase.GamesByPlayer(matchCountOpponent); }
                else { return new Game[] { }; }
            };

            Func<Map, string> GetGameCountString = mp =>
              {
                  int gameCount = GetGames().Where(game => game.Map == mp).Count();

                  return gameCount > 0 ? String.Format("    ({0})", gameCount.ToString("#,#")) : "";
              };

            cmBx.Items.Clear();

            if (includeEmptyItem) { cmBx.Items.Add(Tuple.Create<string, Map>("<any>", null)); }

            cmBx.Items.AddRange(mapList.Select(map => Tuple.Create<string, Map>(String.Format("{0}{1}", map.Name, includeMatchCount ? GetGameCountString(map) : ""), map)).ToArray());

            int mapItemNone = includeEmptyItem ? 1 : 0;

            if (selectedItem != null && mapList.Contains(selectedItem)) { cmBx.SelectedIndex = mapList.IndexOf(selectedItem) + mapItemNone; }
        }

        internal static Image BackGroundFrame(Size size, Color frameColor, int frameWidth)
        {
            var img = new Bitmap(size.Width, size.Height, PixelFormat.Format64bppPArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int frameHalfSize = (frameWidth / 2.0).RoundDown();

                using (var brush = new SolidBrush(frameColor)) { g.DrawRectangle(new Pen(brush, frameWidth), 0, 0, size.Width - frameHalfSize, size.Height - frameHalfSize); }
            }

            return img;
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

        internal static Image ImageGetterMethod(IHasImageID item)
        {
            EloImage eloImg;

            if (GlobalState.DataBase.TryGetImage(item.ImageID, out eloImg)) { return eloImg.Image; }
            else { return null; }
        }

        internal static GameFilter<Matchup> CreateMatchupDrivenGameFilter()
        {
            var gameFilter = new GameFilter<Matchup>();

            gameFilter.Dock = DockStyle.Fill;
            gameFilter.BorderStyle = BorderStyle.FixedSingle;
            gameFilter.Margin = new Padding(4);
            gameFilter.ColumnHeader = "Matchup";
            gameFilter.Header = "Matchup Filter:";
            gameFilter.ImageColumnnWidth = 100;

            gameFilter.ItemImageGetter = (m) => RaceIconProvider.GetMatchupImage(m);
            gameFilter.FilterAspectGetter = (g) => g.MatchType;
            gameFilter.SetItems(Enum.GetValues(typeof(Matchup)).Cast<Matchup>());

            return gameFilter;
        }

    }
}
