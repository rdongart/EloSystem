using CustomControls;
using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public class MatchContextSelector
    {
        public Season SelectedSeason
        {
            get
            {
                var selItem = this.SeasonSelector.SelectedItem as Tuple<string, Season>;

                return selItem != null ? selItem.Item2 : null;
            }
        }
        public Tournament SelectedTournament
        {
            get
            {
                return this.TournamentSelector.SelectedValue as Tournament;
            }
        }
        public ImprovedImageComboBox<Tournament> TournamentSelector { get; private set; }
        public ComboBox SeasonSelector { get; private set; }

        public MatchContextSelector(ImprovedImageComboBox<Tournament> tournamentSelector) : this(tournamentSelector, new ComboBox()) { }

        public MatchContextSelector(ComboBox seasonSelector) : this(new ImprovedImageComboBox<Tournament>(), seasonSelector) { }

        public MatchContextSelector(ImprovedImageComboBox<Tournament> tournamentSelector, ComboBox seasonSelector)
        {
            this.TournamentSelector = tournamentSelector;
            this.SeasonSelector = seasonSelector;

            this.SeasonSelector.Enabled = false;

            this.TournamentSelector.SelectedIndexChanged += this.TournamentSelector_SelectedIndexChanged;
        }

        private void TournamentSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateSeasonItems();

            this.SeasonSelector.Enabled = this.SelectedTournament != null;
        }

        public void UpdateSeasonItems()
        {
            if (this.SelectedTournament == null)
            {
                this.SeasonSelector.SelectedIndex = -1;

                this.SeasonSelector.Items.Clear();
            }
            else
            {
                List<Season> seasonList = this.SelectedTournament.GetSeasons().OrderBy(season => season.Name).ToList();

                var selectedItem = this.SeasonSelector.SelectedItem != null ? (this.SeasonSelector.SelectedItem as Tuple<string, Season>).Item2 : null;

                this.SeasonSelector.DisplayMember = "Item1";
                this.SeasonSelector.ValueMember = "Item2";

                this.SeasonSelector.Items.Clear();
                this.SeasonSelector.Items.AddRange(seasonList.Select(season => Tuple.Create<string, Season>(season.Name, season)).ToArray());

                if (selectedItem != null && seasonList.Contains(selectedItem)) { this.SeasonSelector.SelectedIndex = seasonList.IndexOf(selectedItem); }
                else { this.SeasonSelector.SelectedIndex = -1; }
            }
        }

        public void AddItems(IEnumerable<Tournament> items)
        {
            this.TournamentSelector.AddItems(items.ToArray(), true);
        }

        public void UpdateSeason()
        {
            this.UpdateSeasonItems();
        }

        public void TrySetSelections(Tournament tournament, Season season)
        {
            if (tournament != null && this.TournamentSelector.Items.Cast<Tuple<string, Tournament, Image>>().Any(item => item.Item2 == tournament))
            {
                this.TournamentSelector.TrySetSelectedIndex(tournament);

                if (season != null && this.SeasonSelector.Items.Cast<Tuple<string, Season>>().Any(item => item.Item2 == season))
                {
                    this.SeasonSelector.SelectedIndex = this.SeasonSelector.Items.IndexOf(this.SeasonSelector.Items.Cast<Tuple<string, Season>>().First(item => item.Item2 == season));
                }
                else { this.SeasonSelector.SelectedIndex = -1; }
            }
            else { this.TournamentSelector.SelectedIndex = 0; }
        }
    }
}
