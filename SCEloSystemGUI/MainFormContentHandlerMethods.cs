using BrightIdeasSoftware;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm
    {
        private static ObjectListView CreateMatchListView()
        {
            var matchLV = new ObjectListView();

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmPlayer1 = new OLVColumn() { Width = 130, Text = "Player 1" };
            var olvClmRatingChangePlayer1 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmResult = new OLVColumn() { Width = 70, Text = "Result" };
            var olvClmRatingChangePlayer2 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmPlayer2 = new OLVColumn() { Width = 130, Text = "Player 2" };
            var olvClmTournament = new OLVColumn() { Width = 130, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 110, Text = "Season" };

            matchLV.Size = new Size(694, 700);
            matchLV.HasCollapsibleGroups = false;
            matchLV.ShowGroups = false;
            matchLV.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            matchLV.RowHeight = 22;
            matchLV.UseCellFormatEvents = true;
            matchLV.FormatCell += MatchLV_FormatCell;
            matchLV.Scrollable = true;

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2 })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            matchLV.AlternateRowBackColor = Color.FromArgb(217, 217, 217);
            matchLV.UseAlternatingBackColors = true;

            matchLV.FullRowSelect = false;

            olvClmPlayer1.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null) { return match.Key.Player1.Name; }
                else { return ""; }
            };

            olvClmRatingChangePlayer1.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null)
                {
                    int ratingChange = match.Key.RatingChangeBy(PlayerSlotType.Player1);

                    return String.Format("{0}{1}", ratingChange > 0 ? "+" : "", ratingChange.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT));
                }
                else { return ""; }
            };

            olvClmResult.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null) { return String.Format("{0} - {1}", match.Key.WinsBy(PlayerSlotType.Player1), match.Key.WinsBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmRatingChangePlayer2.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null)
                {
                    int ratingChange = match.Key.RatingChangeBy(PlayerSlotType.Player2);

                    return String.Format("{0}{1}", ratingChange > 0 ? "+" : "", ratingChange.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT));
                }
                else { return ""; }
            };

            olvClmPlayer2.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null) { return match.Key.Player2.Name; }
                else { return ""; }
            };

            olvClmTournament.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null && match.First().Tournament != null) { return match.First().Tournament.Name; }
                else { return ""; }
            };

            olvClmSeason.AspectGetter = obj =>
            {
                var match = obj as IGrouping<Match, Game>;

                if (match != null && match.First().Season != null) { return match.First().Season.Name; }
                else { return ""; }
            };

            return matchLV;
        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 4)
            {
                int cellValue;

                if (int.TryParse(e.SubItem.Text, out cellValue))
                {
                    if (cellValue < 0) { e.SubItem.ForeColor = Color.Red; }
                    else if (cellValue > 0) { e.SubItem.ForeColor = Color.ForestGreen; }
                    else
                    {
                        e.SubItem.ForeColor = SystemColors.ControlText;
                    }
                }
            }
        }

        private static void DisplayContentEditFailureMessage()
        {
            MessageBox.Show(String.Format("A failure occurred while trying to add edit content."));
        }

        private static bool ShouldContentBeReplaced(string name, string type)
        {
            DialogResult dlgResult = MessageBox.Show(String.Format("A {0} named {1} already exists. Are you sure you would like to overwrite that {0}?", type, name), "Name is already in use", MessageBoxButtons.OKCancel);

            switch (dlgResult)
            {
                case DialogResult.OK:
                case DialogResult.Yes: return true;
                case DialogResult.None:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.No: return false;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", dlgResult.ToString(), typeof(ContentTypes).Name));
            }

        }

        private static void DisplayContentEditSuccesMessage()
        {
            MessageBox.Show(String.Format("Content was successfully edited."));
        }

        private void OnEditTournament(object sender, EventArgs e)
        {
            var editorSender = sender as DblNameContentEditor<Tournament>;

            if (editorSender != null) { this.EditContent<Tournament>(editorSender); }
        }

        private void OnEditTeam(object sender, EventArgs e)
        {
            var editorSender = sender as DblNameContentEditor<Team>;

            if (editorSender != null) { this.EditContent<Team>(editorSender); }
        }

        private void EditContent<T>(DblNameContentEditor<T> editor) where T : EloSystemContent, IHasDblName
        {
            editor.SelectedItem.Name = editor.NameShort;
            editor.SelectedItem.NameLong = editor.NameLong;

            if (editor.NewImage != null) { this.eloSystem.EidtImage(editor.SelectedItem, editor.NewImage); }
            else if (editor.RemoveCurrentImage) { this.eloSystem.EidtImage(editor.SelectedItem, null); }
        }

        private void AddContent(object sender, ContentAddingEventArgs e)
        {
            IContentAdder adder = e.ContentAdder;

            if (adder == null) { return; }

            EloSystemContent currentContent;

            switch (adder.ContentType)
            {
                case ContentTypes.Country: currentContent = this.eloSystem.GetCountry(adder.ContentName); break;
                case ContentTypes.Map: currentContent = this.eloSystem.GetMap(adder.ContentName); break;
                case ContentTypes.Player: currentContent = this.eloSystem.GetPlayer(adder.ContentName); break;
                case ContentTypes.Team: currentContent = this.eloSystem.GetTeam(adder.ContentName); break;
                case ContentTypes.Tournament: currentContent = this.eloSystem.GetTournament(adder.ContentName); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            if (currentContent != null && MainForm.ShouldContentBeReplaced(adder.ContentName, adder.ContentType.ToString()) == false) { return; }
            else if (currentContent != null)
            {
                switch (adder.ContentType)
                {
                    case ContentTypes.Country: this.eloSystem.RemoveCountry(currentContent as Country); break;
                    case ContentTypes.Map: this.eloSystem.RemoveMap(currentContent as Map); break;
                    case ContentTypes.Player: this.eloSystem.RemovePlayer(currentContent as SCPlayer); break;
                    case ContentTypes.Team: this.eloSystem.RemoveTeam(currentContent as Team); break;
                    case ContentTypes.Tournament: this.eloSystem.RemoveTournament(currentContent as Tournament); break;
                    default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
                }
            }

            switch (adder.ContentType)
            {
                case ContentTypes.Country: this.eloSystem.AddCountry(adder.ContentName, adder.NewImage); break;
                case ContentTypes.Map:
                    var mapAdder = e.ContentAdder as MapAdder;

                    this.eloSystem.AddMap(mapAdder.ContentName, mapAdder.MapType, mapAdder.MapSize, mapAdder.NewImage);

                    break;
                case ContentTypes.Player:
                    var playerAdder = e.ContentAdder as PlayerAdder;

                    this.eloSystem.AddPlayer(playerAdder.ContentName, playerAdder.GetAliases(), playerAdder.IRLName, playerAdder.StartRating, playerAdder.SelectedTeam, playerAdder.SelectedCountry
                        , playerAdder.NewImage, playerAdder.BirthDateWasSet ? playerAdder.BirthDate : new DateTime());

                    break;
                case ContentTypes.Team:
                    var teamAdder = e.ContentAdder as DblNameContentAdder;

                    this.eloSystem.AddTeam(teamAdder.NameShort, teamAdder.NameLong, teamAdder.NewImage); break;
                case ContentTypes.Tournament:
                    var tournamentAdder = e.ContentAdder as DblNameContentAdder;

                    this.eloSystem.AddTournament(tournamentAdder.NameShort, tournamentAdder.NameLong, adder.NewImage); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            MessageBox.Show(string.Format("Your {0} was added succesfully.", adder.ContentType.ToString().ToLower()));
        }

        private void AddSeason(object sender, EventArgs e)
        {
            var adderSender = sender as SeasonAdder;

            if (seasonAdder == null || adderSender.SelectedTournament == null) { return; }

            this.eloSystem.AddSeason(adderSender.ContentName, adderSender.SelectedTournament);
        }

        private void AddTilSet(object sender, EventArgs e)
        {
            var hasNameSender = sender as HasNameContentAdder<Tileset>;

            if (hasNameSender == null)
            {
                MainForm.DisplayContentEditFailureMessage();
                return;
            }

            Tileset currentContent = this.eloSystem.GetTileSet(hasNameSender.Name);

            if (currentContent != null) { MessageBox.Show(String.Format("A {0} named {1} already exists.", currentContent.GetType().Name, currentContent.Name)); }
            else
            {
                this.eloSystem.AddTileSet(hasNameSender.ContentName);
                MainForm.DisplayContentEditSuccesMessage();
            }
        }

        private void TeamAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddTeamsToImgCmbBox();
        }

        private void CountryAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddCountriesToImgCmbBox();
        }

        private void TileSetAdder_OnAddButtonClick(object sender, EventArgs e)
        {
            this.AddTileSetsToCmbBox();
        }

        private void PlayerAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddPlayersToImgCmbBox();
        }

        private void TournamentAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddTournamentsToImgCmbBox();
        }

        private void TournamentEdited_OnEditedButtonClick(object sender, EventArgs e)
        {
            this.AddTournamentsToImgCmbBox();
        }

        private void TeamEdited_OnEditedButtonClick(object sender, EventArgs e)
        {
            this.AddTeamsToImgCmbBox();
        }

        private void AddTileSetsToCmbBox()
        {
            var selectedItem = this.mapAdder.cmbBxTileset.SelectedItem != null ? (this.mapAdder.cmbBxTileset.SelectedItem as Tuple<string, Tileset>).Item2 : null;

            List<Tileset> tileSetList = this.eloSystem.GetTileSets().OrderBy(tileSet => tileSet.Name).ToList();

            this.mapAdder.cmbBxTileset.DisplayMember = "Item1";
            this.mapAdder.cmbBxTileset.ValueMember = "Item2";

            this.mapAdder.cmbBxTileset.Items.Clear();
            this.mapAdder.cmbBxTileset.Items.AddRange(tileSetList.Select(tileSet => Tuple.Create<string, Tileset>(tileSet.Name, tileSet)).ToArray());

            if (selectedItem != null && tileSetList.Contains(selectedItem)) { this.mapAdder.cmbBxTileset.SelectedIndex = tileSetList.IndexOf(selectedItem); }
        }

        private void AddCountriesToImgCmbBox()
        {
            var currentSelection = this.playerAdder.ImgCmbBxCountries.SelectedValue as Country;

            Func<Country, Image> GetImage = c =>
            {
                EloImage eloImg;

                if (this.eloSystem.TryGetImage(c.ImageID, out eloImg)) { return eloImg.Image; }
                else { return null; }

            };

            this.playerAdder.ImgCmbBxCountries.DisplayMember = "Item1";
            this.playerAdder.ImgCmbBxCountries.ValueMember = "Item2";
            this.playerAdder.ImgCmbBxCountries.ImageMember = "Item3";

            var items = (new Tuple<string, Country, Image>[] { Tuple.Create<string, Country, Image>("none", null, null) }).Concat(this.eloSystem.GetCountries().OrderBy(country => country.Name).Select(country =>
                Tuple.Create<string, Country, Image>(country.Name, country, GetImage(country)))).ToList();

            this.playerAdder.ImgCmbBxCountries.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection))
            {
                this.playerAdder.ImgCmbBxCountries.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection));
            }
        }

        private void AddPlayersToImgCmbBox()
        {
            EloGUIControlsStaticMembers.AddPlayersToImgCmbBox(this.matchReport.ImgCmbBxPlayer1, this.eloSystem);
            EloGUIControlsStaticMembers.AddPlayersToImgCmbBox(this.matchReport.ImgCmbBxPlayer2, this.eloSystem);
        }

        private void AddTeamsToImgCmbBox()
        {
            this.teamEditor.AddContentItems(this.eloSystem.GetTeams(), this.ImageGetterMethod);

            var currentSelection = this.playerAdder.ImgCmbBxTeams.SelectedValue as Team;

            Func<Team, Image> GetImage = c =>
            {
                EloImage eloImg;

                if (this.eloSystem.TryGetImage(c.ImageID, out eloImg)) { return eloImg.Image; }
                else { return null; }

            };

            this.playerAdder.ImgCmbBxTeams.DisplayMember = "Item1";
            this.playerAdder.ImgCmbBxTeams.ValueMember = "Item2";
            this.playerAdder.ImgCmbBxTeams.ImageMember = "Item3";

            var items = (new Tuple<string, Team, Image>[] { Tuple.Create<string, Team, Image>("none", null, null) }).Concat(this.eloSystem.GetTeams().OrderBy(team => team.Name).Select(team =>
                Tuple.Create<string, Team, Image>(String.Format("{0}{1}", team.Name, team.NameLong != string.Empty ? " (" + team.NameLong + ")" : string.Empty), team, GetImage(team)))).ToList();

            this.playerAdder.ImgCmbBxTeams.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection))
            {
                this.playerAdder.ImgCmbBxTeams.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection));
            }
        }

        private void AddTournamentsToImgCmbBox()
        {
            this.seasonAdder.AddTournamentItems(this.eloSystem.GetTournaments(), this.ImageGetterMethod);

            this.tournamentEditor.AddContentItems(this.eloSystem.GetTournaments(), this.ImageGetterMethod);
        }

        private void AddRecentMatches()
        {
            this.oLstVRecentMatches.SetObjects(this.eloSystem.GetAllGames().OrderByDescending(game=>game.Match.Date).GroupBy(game => game.Match).Take(50));
        }

        private void OnMatchReported(object sender, EventArgs e)
        {
            this.AddRecentMatches();
        }


    }

}